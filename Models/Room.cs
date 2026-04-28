using System.Collections.Generic;

namespace SmartHouseApp.Models
{
     public class Room
     {
          public string Name { get; set; }
          public List<Device> Devices { get; set; }

          public Room(string name)
          {
               Name = name;
               Devices = new List<Device>();
          }

          public void AddDevice(Device device)
          {
               Devices.Add(device);
          }

          public void TurnAllOn()
          {
               foreach (var device in Devices)
               {
                    device.TurnOn();
               }
          }

          public void TurnAllOff()
          {
               foreach (var device in Devices)
               {
                    device.TurnOff();
               }
          }
     }
}