using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;

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
        public ICommand DeleteRoomCommand { get; }
        public ICommand EditRoomCommand { get; }

        public RoomsViewModel()
        {
            AddRoomCommand = new RelayCommand(AddRoom);
            DeleteRoomCommand = new RelayCommand<RoomViewModel>(DeleteRoom);
            EditRoomCommand = new RelayCommand<RoomViewModel>(EditRoom);
            LoadRooms();
        }

        public void LoadRooms()
        {
            Rooms = new ObservableCollection<RoomViewModel>(
                SmartHomeManager.Instance.Rooms.Select(r => new RoomViewModel(r))
            );
        }

        private void AddRoom()
        {
            string newRoomName = $"Room {SmartHomeManager.Instance.Rooms.Count + 1}";
            SmartHomeManager.Instance.AddRoom(new Room(newRoomName));
            LoadRooms();
        }

        private void DeleteRoom(RoomViewModel roomVm)
        {
            var result = MessageBox.Show($"Are you sure you want to delete {roomVm.Name}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                var room = SmartHomeManager.Instance.Rooms.FirstOrDefault(r => r.Name == roomVm.Name);
                if (room != null)
                {
                    SmartHomeManager.Instance.Rooms.Remove(room);
                    LoadRooms();
                }
            }
        }

        private void EditRoom(RoomViewModel roomVm)
        {
            // Simple edit for demo: append " (Edited)"
            var room = SmartHomeManager.Instance.Rooms.FirstOrDefault(r => r.Name == roomVm.Name);
            if (room != null)
            {
                room.Name += " (Edited)";
                LoadRooms();
            }
        }
    }
}
