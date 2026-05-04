using SmartHouseApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartHouseApp.ViewModels
{
    public class RoomViewModel : ViewModelBase
    {
        private readonly Room _room;
        private ObservableCollection<DeviceViewModel> _devices;
        
        public string Name => _room.Name;
        
        public ObservableCollection<DeviceViewModel> Devices
        {
            get => _devices;
            set => SetProperty(ref _devices, value);
        }
        
        public int DeviceCount => _room.Devices.Count;
        
        public RoomViewModel(Room room)
        {
            _room = room;
            _devices = new ObservableCollection<DeviceViewModel>(
                _room.Devices.Select(d => new DeviceViewModel(d))
            );
        }
    }
}
