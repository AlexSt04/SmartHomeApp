using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Services;
using SmartHouseApp.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
     public class RoomsViewModel : ViewModelBase
     {
          private ObservableCollection<RoomViewModel> _rooms = new();
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
               var room = new Room(newRoomName);
               SmartHomeManager.Instance.AddRoom(room);

               // ✅ Salvează în DB
               ServiceLocator.Database.UpsertRoom(room);
               ServiceLocator.Logger.LogRoomAction(newRoomName, "Room added");

               LoadRooms();
          }

          private void DeleteRoom(RoomViewModel roomVm)
          {
               var result = MessageBox.Show(
                   $"Are you sure you want to delete '{roomVm.Name}'?",
                   "Confirm Delete",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Warning
               );

               if (result == MessageBoxResult.Yes)
               {
                    var room = SmartHomeManager.Instance.Rooms.FirstOrDefault(r => r.Name == roomVm.Name);
                    if (room != null)
                    {
                         SmartHomeManager.Instance.Rooms.Remove(room);

                         // ✅ Șterge din DB
                         ServiceLocator.Database.DeleteRoom(room.Name);
                         ServiceLocator.Logger.LogRoomAction(room.Name, "Room deleted");

                         LoadRooms();
                    }
               }
          }

          private void EditRoom(RoomViewModel roomVm)
          {
               var room = SmartHomeManager.Instance.Rooms.FirstOrDefault(r => r.Name == roomVm.Name);
               if (room != null)
               {
                    string oldName = room.Name;
                    room.Name += " (Edited)";

                    // ✅ Actualizează în DB
                    ServiceLocator.Database.DeleteRoom(oldName);
                    ServiceLocator.Database.UpsertRoom(room);
                    ServiceLocator.Logger.LogRoomAction(room.Name, $"Room renamed from '{oldName}'");

                    LoadRooms();
               }
          }
     }
}