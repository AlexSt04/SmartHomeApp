using SmartHouseApp.Models;
using SmartHouseApp.Services;
using SmartHouseApp.Utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartHouseApp.ViewModels
{
     // ─────────────────────────────────────────────────────────────────────
     //  One action row in the "Create Scene" form
     // ─────────────────────────────────────────────────────────────────────
     public class SceneActionRowViewModel : ViewModelBase
     {
          public List<string> DeviceTypes { get; } = new()
            { "All", "Light", "Thermostat", "DoorLock", "SmartTV", "AirConditioner" };

          public List<string> ActionTypes { get; } = new()
            { "Turn On", "Turn Off", "Set Temperature", "Lock", "Unlock" };

          private string _deviceType = "All";
          public string DeviceType
          {
               get => _deviceType;
               set { SetProperty(ref _deviceType, value); OnPropertyChanged(nameof(ShowTemp)); }
          }

          private string _actionType = "Turn On";
          public string ActionType
          {
               get => _actionType;
               set { SetProperty(ref _actionType, value); OnPropertyChanged(nameof(ShowTemp)); }
          }

          private int _temperature = 22;
          public int Temperature
          {
               get => _temperature;
               set => SetProperty(ref _temperature, value);
          }

          public bool ShowTemp => ActionType == "Set Temperature";

          public ICommand RemoveCommand { get; }

          public SceneActionRowViewModel(Action<SceneActionRowViewModel> onRemove)
          {
               RemoveCommand = new RelayCommand(() => onRemove(this));
          }

          public SceneAction ToModel() => new()
          {
               DeviceType = DeviceType,
               ActionType = ActionType switch
               {
                    "Turn On" => SceneActionType.TurnOn,
                    "Turn Off" => SceneActionType.TurnOff,
                    "Set Temperature" => SceneActionType.SetTemperature,
                    "Lock" => SceneActionType.Lock,
                    "Unlock" => SceneActionType.Unlock,
                    _ => SceneActionType.TurnOn
               },
               TemperatureValue = Temperature
          };
     }

     // ─────────────────────────────────────────────────────────────────────
     //  One scene card
     // ─────────────────────────────────────────────────────────────────────
     public class SceneCardViewModel : ViewModelBase
     {
          public Scene Scene { get; }
          public string Name => Scene.Name;
          public string Icon => Scene.Icon;
          public string Description => Scene.Description;
          public bool IsPreset => Scene.IsPreset;
          public int ActionCount => Scene.Actions.Count;

          // Brush calculat direct — nu e nevoie de converter în XAML
          public Brush AccentBrush
          {
               get
               {
                    try
                    {
                         var c = (Color)ColorConverter.ConvertFromString(Scene.AccentColor);
                         return new SolidColorBrush(c);
                    }
                    catch { return new SolidColorBrush(Colors.Green); }
               }
          }

          // Ce device-uri sunt afectate (taguri)
          public string DeviceTags => string.Join("   ", Scene.Actions
              .Select(a => a.DeviceType == "All" ? "All Devices" : a.DeviceType)
              .Distinct()
              .Select(t => $"• {t}"));

          private bool _isActivating;
          public bool IsActivating
          {
               get => _isActivating;
               set => SetProperty(ref _isActivating, value);
          }

          public ICommand ActivateCommand { get; }
          public ICommand DeleteCommand { get; }

          public SceneCardViewModel(Scene scene,
                                    Action<SceneCardViewModel> onActivate,
                                    Action<SceneCardViewModel>? onDelete = null)
          {
               Scene = scene;
               ActivateCommand = new RelayCommand(() => onActivate(this));
               DeleteCommand = new RelayCommand(() => onDelete?.Invoke(this));
          }
     }

     // ─────────────────────────────────────────────────────────────────────
     //  Main ScenesViewModel
     // ─────────────────────────────────────────────────────────────────────
     public class ScenesViewModel : ViewModelBase
     {
          private readonly SceneService _sceneService;
          private readonly SceneRepository _repo;
          private System.Timers.Timer? _statusTimer;

          public ObservableCollection<SceneCardViewModel> PresetCards { get; } = new();
          public ObservableCollection<SceneCardViewModel> CustomCards { get; } = new();

          // ── Status banner ─────────────────────────────────────────────────
          private string _statusMessage = "";
          public string StatusMessage
          {
               get => _statusMessage;
               set => SetProperty(ref _statusMessage, value);
          }

          private bool _showStatus;
          public bool ShowStatus
          {
               get => _showStatus;
               set => SetProperty(ref _showStatus, value);
          }

          // ── Create panel ──────────────────────────────────────────────────
          private bool _showCreate;
          public bool ShowCreate
          {
               get => _showCreate;
               set => SetProperty(ref _showCreate, value);
          }

          private string _newName = "";
          public string NewName
          {
               get => _newName;
               set => SetProperty(ref _newName, value);
          }

          private string _newIcon = "⚡";
          public string NewIcon
          {
               get => _newIcon;
               set => SetProperty(ref _newIcon, value);
          }

          public List<string> IconOptions { get; } = new()
            { "⚡", "🏠", "💡", "🌿", "🎵", "🍳", "🖥️", "🔒", "🌙", "☀️", "❄️", "🔥" };

          public ObservableCollection<SceneActionRowViewModel> NewActions { get; } = new();

          public bool HasCustomScenes => CustomCards.Count > 0;

          public ICommand ToggleCreateCommand { get; }
          public ICommand AddActionRowCommand { get; }
          public ICommand SaveSceneCommand { get; }

          // ─────────────────────────────────────────────────────────────────
          public ScenesViewModel()
          {
               _sceneService = new SceneService(ServiceLocator.Logger);
               _repo = new SceneRepository();

               ToggleCreateCommand = new RelayCommand(() => ShowCreate = !ShowCreate);
               AddActionRowCommand = new RelayCommand(AddActionRow);
               SaveSceneCommand = new RelayCommand(SaveScene);

               LoadPresets();
               LoadCustom();
               AddActionRow(); // un rând gol implicit în formular
          }

          // ── Load ──────────────────────────────────────────────────────────
          private void LoadPresets()
          {
               PresetCards.Clear();
               foreach (var scene in Models.PresetScenes.All())
                    PresetCards.Add(new SceneCardViewModel(scene, Activate));
          }

          private void LoadCustom()
          {
               CustomCards.Clear();
               foreach (var scene in _repo.LoadCustomScenes())
                    CustomCards.Add(new SceneCardViewModel(scene, Activate, DeleteCustom));
               OnPropertyChanged(nameof(HasCustomScenes));
          }

          // ── Activate ──────────────────────────────────────────────────────
          private void Activate(SceneCardViewModel card)
          {
               card.IsActivating = true;
               var result = _sceneService.Execute(card.Scene);
               card.IsActivating = false;

               string msg = result.AffectedCount > 0
                   ? $"✅  \"{card.Name}\" activată — {result.AffectedCount} device(s) actualizate"
                   : $"⚠️  \"{card.Name}\" activată, dar niciun device compatibil găsit în casă";

               ShowBanner(msg);
          }

          private void ShowBanner(string msg)
          {
               _statusTimer?.Stop();
               StatusMessage = msg;
               ShowStatus = true;
               _statusTimer = new System.Timers.Timer(4500) { AutoReset = false };
               _statusTimer.Elapsed += (_, _) =>
                   Application.Current?.Dispatcher.Invoke(() => ShowStatus = false);
               _statusTimer.Start();
          }

          // ── Create ────────────────────────────────────────────────────────
          private void AddActionRow() =>
              NewActions.Add(new SceneActionRowViewModel(row => NewActions.Remove(row)));

          private void SaveScene()
          {
               if (string.IsNullOrWhiteSpace(NewName))
               {
                    MessageBox.Show("Introdu un nume pentru scenă.", "Validare",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
               }
               if (NewActions.Count == 0)
               {
                    MessageBox.Show("Adaugă cel puțin o acțiune.", "Validare",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
               }

               var scene = new Scene
               {
                    Name = NewName.Trim(),
                    Icon = NewIcon,
                    Description = $"{NewActions.Count} acțiune(i) configurate",
                    AccentColor = "#10B981",
                    IsPreset = false,
                    Actions = NewActions.Select(a => a.ToModel()).ToList()
               };

               _repo.SaveScene(scene);
               ServiceLocator.Logger.LogInfo($"Custom scene '{scene.Name}' saved ({scene.Actions.Count} actions).");

               NewName = "";
               NewIcon = "⚡";
               NewActions.Clear();
               AddActionRow();
               ShowCreate = false;
               LoadCustom();
               ShowBanner($"✅  Scena \"{scene.Name}\" salvată cu succes!");
          }

          private void DeleteCustom(SceneCardViewModel card)
          {
               var r = MessageBox.Show($"Ștergi scena \"{card.Name}\"?",
                   "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Warning);
               if (r == MessageBoxResult.Yes)
               {
                    _repo.DeleteScene(card.Scene.Id);
                    ServiceLocator.Logger.LogInfo($"Custom scene '{card.Name}' deleted.");
                    LoadCustom();
               }
          }
     }
}