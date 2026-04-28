using SmartHouseApp.Models;

namespace SmartHouseApp.Managers
{
     public class SmartHomeFacade
     {
          private SmartHomeManager manager = SmartHomeManager.Instance;

          public void AddRoom(string name)
          {
               manager.AddRoom(new Room(name));
          }

          public void AddDeviceToRoom(string roomName, Device device)
          {
               var room = manager.Rooms.Find(r => r.Name == roomName);
               room?.AddDevice(device);
          }

          public SmartHomeManager GetManager() => manager;
     }
}