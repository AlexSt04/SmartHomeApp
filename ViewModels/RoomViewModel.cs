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
        
        public string Name => Room.Name;
        
        public ObservableCollection<DeviceViewModel> Devices
        {
            get => _devices;
            set => SetProperty(ref _devices, value);
        }
        
        public int DeviceCount => Room.Devices.Count;
        
        public ICommand ViewDetailsCommand { get; }

        public RoomViewModel(Room room)
        {
            Room = room;
            _devices = new ObservableCollection<DeviceViewModel>(
                Room.Devices.Select(d => new DeviceViewModel(d))
            );

            ViewDetailsCommand = new RelayCommand(() => 
                NavigationService.Instance.NavigateTo(new Views.Pages.RoomDetailsView(Room), Room.Name));
        }
    }
}
