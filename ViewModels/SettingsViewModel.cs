using SmartHouseApp.Services;
using SmartHouseApp.Utils;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
     public class SettingsViewModel : ViewModelBase
     {
          private string _systemName = "My Smart Home";
          public string SystemName
          {
               get => _systemName;
               set => SetProperty(ref _systemName, value);
          }

          private bool _notificationsEnabled = true;
          public bool NotificationsEnabled
          {
               get => _notificationsEnabled;
               set => SetProperty(ref _notificationsEnabled, value);
          }

          // ✅ Informații baza de date
          public string DatabasePath => ServiceLocator.Database.GetDatabasePath();

          public string DatabaseSize
          {
               get
               {
                    try
                    {
                         var info = new FileInfo(DatabasePath);
                         if (info.Exists)
                              return $"{info.Length / 1024.0:F1} KB";
                         return "Not created yet";
                    }
                    catch { return "Unknown"; }
               }
          }

          public string LogsPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

          public string LogsSize
          {
               get
               {
                    try
                    {
                         var dir = new DirectoryInfo(LogsPath);
                         if (!dir.Exists) return "0 KB";
                         long size = dir.GetFiles("*.log").Sum(f => f.Length);
                         return $"{size / 1024.0:F1} KB";
                    }
                    catch { return "Unknown"; }
               }
          }

          public ICommand SaveCommand { get; }
          public ICommand ClearLogsCommand { get; }
          public ICommand ResetDatabaseCommand { get; }

          public SettingsViewModel()
          {
               SaveCommand = new RelayCommand(() =>
               {
                    ServiceLocator.Logger.LogInfo($"Settings saved. House name: {SystemName}");
                    MessageBox.Show("Settings saved successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
               });

               ClearLogsCommand = new RelayCommand(() =>
               {
                    var res = MessageBox.Show("Are you sure you want to delete all log files?",
                        "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                         try
                         {
                              foreach (var f in Directory.GetFiles(LogsPath, "*.log"))
                                   File.Delete(f);
                              OnPropertyChanged(nameof(LogsSize));
                              MessageBox.Show("Logs cleared.", "Done");
                         }
                         catch (Exception ex)
                         {
                              MessageBox.Show($"Error: {ex.Message}", "Error");
                         }
                    }
               });

               ResetDatabaseCommand = new RelayCommand(() =>
               {
                    var res = MessageBox.Show(
                        "⚠️ This will DELETE all rooms and devices!\n\nAre you absolutely sure?",
                        "Reset Database",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning
                    );

                    if (res == MessageBoxResult.Yes)
                    {
                         ServiceLocator.Database.ClearAll();
                         Managers.SmartHomeManager.Instance.Rooms.Clear();
                         OnPropertyChanged(nameof(DatabaseSize));
                         ServiceLocator.Logger.LogWarning("Database reset by user.");
                         MessageBox.Show("Database has been reset. Restart the application.", "Done");
                    }
               });
          }
     }
}