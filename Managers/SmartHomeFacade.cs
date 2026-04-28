using SmartHouseApp.Models;
using System.Linq;

namespace SmartHouseApp.Managers
{
     public class SmartHomeFacade
     {
          private SmartHomeManager _manager;

          public SmartHomeFacade()
          {
               _manager = SmartHomeManager.Instance;
          }

          public void AddRoom(string roomName)
          {
               var room = new Room(roomName);
               _manager.AddRoom(room);
          }

          public void AddDeviceToRoom(string roomName, Device device)
          {
               var room = _manager.Rooms.FirstOrDefault(r => r.Name == roomName);
               room?.AddDevice(device);
          }

          public void TurnAllOn(string roomName)
          {
               var room = _manager.Rooms.FirstOrDefault(r => r.Name == roomName);
               room?.TurnAllOn();
          }

          public int GetDeviceCount(string roomName)
          {
               var room = _manager.Rooms.FirstOrDefault(r => r.Name == roomName);
               return room?.Devices.Count ?? 0;
          }
     }
}