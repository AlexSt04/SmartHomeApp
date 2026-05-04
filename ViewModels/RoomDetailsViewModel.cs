using SmartHouseApp.Models;
using SmartHouseApp.Utils;
using SmartHouseApp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace SmartHouseApp.ViewModels
{
    public class RoomDetailsViewModel : ViewModelBase
    {
        private readonly Room _room;
        private ObservableCollection<DeviceViewModel> _devices;

        public string Name => _room.Name;
        
        public ObservableCollection<DeviceViewModel> Devices
        {
            get => _devices;
            set => SetProperty(ref _devices, value);
        }

        public ICommand AddDeviceCommand { get; }
        public ICommand DeleteDeviceCommand { get; }
        public ICommand BackCommand { get; }

        public RoomDetailsViewModel(Room room)
        {
            _room = room;
            LoadDevices();

            AddDeviceCommand = new RelayCommand<string>(AddDevice);
            DeleteDeviceCommand = new RelayCommand<DeviceViewModel>(DeleteDevice);
            BackCommand = new RelayCommand(() => NavigationService.Instance.NavigateTo(new Views.Pages.DashboardView(), "Dashboard"));
        }

        private void LoadDevices()
        {
            Devices = new ObservableCollection<DeviceViewModel>(
                _room.Devices.Select(d => new DeviceViewModel(d))
            );
        }

        private void AddDevice(string type)
        {
            Device newDevice = type switch
            {
                "Light" => new Light(_room.Name),
                "Thermostat" => new Thermostat(_room.Name),
                "DoorLock" => new DoorLock(_room.Name),
                _ => null
            };

            if (newDevice != null)
            {
                _room.AddDevice(newDevice);
                LoadDevices();
            }
        }

        private void DeleteDevice(DeviceViewModel deviceVm)
        {
            var device = _room.Devices.FirstOrDefault(d => d.Name == deviceVm.Name);
            if (device != null)
            {
                _room.Devices.Remove(device);
                LoadDevices();
            }
        }
    }
}
