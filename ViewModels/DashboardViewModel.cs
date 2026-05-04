using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartHouseApp.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public int RoomCount => SmartHomeManager.Instance.Rooms.Count;
        public int DeviceCount => SmartHomeManager.Instance.Rooms.Sum(r => r.Devices.Count);
        public int ActiveLightsCount => SmartHomeManager.Instance.Rooms
            .SelectMany(r => r.Devices)
            .OfType<Light>()
            .Count(l => l.IsOn);

        public ObservableCollection<RoomViewModel> Rooms { get; }
        public ObservableCollection<string> RecentActivity { get; }

        public DashboardViewModel()
        {
            Rooms = new ObservableCollection<RoomViewModel>(
                SmartHomeManager.Instance.Rooms.Select(r => new RoomViewModel(r))
            );

            RecentActivity = new ObservableCollection<string>
            {
                "• Light (Kitchen) turned ON",
                "• Thermostat (Living) set to 22°C",
                "• DoorLock (Entrance) locked"
            };
        }
    }
}
