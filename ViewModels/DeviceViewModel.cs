using SmartHouseApp.Models;
using SmartHouseApp.Utils;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
    public class DeviceViewModel : ViewModelBase
    {
        private readonly Device _device;
        private string _status;
        
        public string Name => _device.Name;
        public string Room => _device.Room;
        
        public string Icon => _device switch
        {
            Light _ => "💡",
            Thermostat _ => "🌡️",
            DoorLock _ => "🔐",
            _ => "📱"
        };

        public string StatusColor => _device.IsActive ? "#10B981" : "#EF4444";
        public string ActionButtonText => _device is DoorLock ? (_device.IsActive ? "Unlock" : "Lock") : "Toggle";

        public string Status
        {
            get => _status;
            set 
            {
                if (SetProperty(ref _status, value))
                {
                    OnPropertyChanged(nameof(StatusColor));
                }
            }
        }

        
        public ICommand ToggleCommand { get; }
        
        public DeviceViewModel(Device device)
        {
            _device = device;
            _status = device.GetStatus();
            
            ToggleCommand = new RelayCommand(Toggle);
            
            // Subscribe to device status changes
            _device.StatusChanged += (s, e) =>
            {
                Status = _device.GetStatus();
            };
        }
        
        private void Toggle()
        {
            if (_device.IsActive)
                _device.TurnOff();
            else
                _device.TurnOn();
        }
    }
}
