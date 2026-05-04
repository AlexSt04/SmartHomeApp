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
            SmartTV _ => "📺",
            AirConditioner _ => "❄️",
            _ => "📱"
        };

        public string StatusColor => _device.IsActive ? "#10B981" : "#EF4444";
        public string ActionButtonText => _device is DoorLock ? (_device.IsActive ? "Unlock" : "Lock") : (_device.IsActive ? "Turn Off" : "Turn On");

        public string Status
        {
            get => _status;
            set 
            {
                if (SetProperty(ref _status, value))
                {
                    OnPropertyChanged(nameof(StatusColor));
                    OnPropertyChanged(nameof(ActionButtonText));
                }
            }
        }

        public bool IsTemperatureControlVisible => _device is Thermostat || _device is AirConditioner;

        public int Temperature
        {
            get
            {
                if (_device is Thermostat t) return t.Temperature;
                if (_device is AirConditioner ac) return ac.Temperature;
                return 0;
            }
            set
            {
                if (_device is Thermostat t) t.Temperature = value;
                if (_device is AirConditioner ac) ac.Temperature = value;
                OnPropertyChanged(nameof(Temperature));
                Status = _device.GetStatus();
            }
        }
        
        public ICommand ToggleCommand { get; }
        
        public DeviceViewModel(Device device)
        {
            _device = device;
            _status = _device.GetStatus();
            
            ToggleCommand = new RelayCommand(() => 
            {
                if (_device.IsActive) _device.TurnOff();
                else _device.TurnOn();
                Status = _device.GetStatus();
            });
        }
    }
}
