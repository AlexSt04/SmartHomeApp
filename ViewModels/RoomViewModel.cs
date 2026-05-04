using SmartHouseApp.Models;
using SmartHouseApp.Utils;
using SmartHouseApp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
    public class RoomViewModel : ViewModelBase
    {
        public Room Room { get; }
        private ObservableCollection<DeviceViewModel> _devices;
        
        public string Name
        {
            get => Room.Name;
            set
            {
                Room.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        
        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        private string _tempName;
        public string TempName
        {
            get => _tempName;
            set => SetProperty(ref _tempName, value);
        }
        
        public ObservableCollection<DeviceViewModel> Devices
        {
            get => _devices;
            set => SetProperty(ref _devices, value);
        }
        
        public int DeviceCount => Room.Devices.Count;
        
        public ICommand ViewDetailsCommand { get; }
        public ICommand StartEditCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand CancelEditCommand { get; }

        public RoomViewModel(Room room)
        {
            Room = room;
            _devices = new ObservableCollection<DeviceViewModel>(
                Room.Devices.Select(d => new DeviceViewModel(d))
            );

            ViewDetailsCommand = new RelayCommand(() => 
                NavigationService.Instance.NavigateTo(new Views.Pages.RoomDetailsView(Room), Room.Name));

            StartEditCommand = new RelayCommand(() => 
            {
                TempName = Name;
                IsEditing = true;
            });

            SaveEditCommand = new RelayCommand(() => 
            {
                Name = TempName;
                IsEditing = false;
            });

            CancelEditCommand = new RelayCommand(() => 
            {
                IsEditing = false;
            });
        }
    }
}
