using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Utils;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
    public class DevicesViewModel : ViewModelBase
    {
        private ObservableCollection<DeviceViewModel> _allDevices;
        public ObservableCollection<DeviceViewModel> AllDevices
        {
            get => _allDevices;
            set => SetProperty(ref _allDevices, value);
        }

        public List<string> DeviceTypes { get; } = new List<string> { "Light", "Thermostat", "Door Lock", "Smart TV", "Air Conditioner" };
        public ObservableCollection<string> Rooms { get; }

        private string _selectedRoom;
        public string SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value);
        }

        private string _selectedType = "Light";
        public string SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }

        private string _newName = "Global Device";
        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }
        
        public ICommand RefreshCommand { get; }
        public ICommand ToggleAllCommand { get; }
        public ICommand AddDeviceCommand { get; }
        
        public DevicesViewModel()
        {
            RefreshCommand = new RelayCommand(LoadDevices);
            ToggleAllCommand = new RelayCommand(ToggleAll);
            AddDeviceCommand = new RelayCommand(AddGlobalDevice);

            Rooms = new ObservableCollection<string>(SmartHomeManager.Instance.Rooms.Select(r => r.Name));
            if (Rooms.Count > 0) SelectedRoom = Rooms[0];
            
            LoadDevices();
        }
        
        private void LoadDevices()
        {
            var devices = new ObservableCollection<DeviceViewModel>();
            foreach (var room in SmartHomeManager.Instance.Rooms)
            {
                foreach (var device in room.Devices)
                {
                    devices.Add(new DeviceViewModel(device));
                }
            }
            AllDevices = devices;
        }

        private void AddGlobalDevice()
        {
            var room = SmartHomeManager.Instance.Rooms.FirstOrDefault(r => r.Name == SelectedRoom);
            if (room == null) return;

            Device newDevice = SelectedType switch
            {
                "Light" => new Light(room.Name) { Name = NewName },
                "Thermostat" => new Thermostat(room.Name) { Name = NewName },
                "Door Lock" => new DoorLock(room.Name) { Name = NewName },
                "Smart TV" => new SmartTV(room.Name) { Name = NewName },
                "Air Conditioner" => new AirConditioner(room.Name) { Name = NewName },
                _ => null
            };

            if (newDevice != null)
            {
                room.AddDevice(newDevice);
                LoadDevices();
            }
        }
        
        private void ToggleAll()
        {
            if (AllDevices == null) return;
            foreach (var device in AllDevices)
            {
                device.ToggleCommand.Execute(null);
            }
        }
    }
}
