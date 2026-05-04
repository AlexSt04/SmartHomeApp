using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
    public class RoomsViewModel : ViewModelBase
    {
        private ObservableCollection<RoomViewModel> _rooms;
        public ObservableCollection<RoomViewModel> Rooms
        {
            get => _rooms;
            set => SetProperty(ref _rooms, value);
        }

        public ICommand AddRoomCommand { get; }

        public RoomsViewModel()
        {
            AddRoomCommand = new RelayCommand(AddRoom);
            LoadRooms();
        }

        private void LoadRooms()
        {
            Rooms = new ObservableCollection<RoomViewModel>(
                SmartHomeManager.Instance.Rooms.Select(r => new RoomViewModel(r))
            );
        }

        private void AddRoom()
        {
            // Simple implementation for demo
            string newRoomName = $"Room {SmartHomeManager.Instance.Rooms.Count + 1}";
            SmartHomeManager.Instance.AddRoom(new Room(newRoomName));
            LoadRooms();
        }
    }
}
