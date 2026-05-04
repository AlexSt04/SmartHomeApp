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
        
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
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
