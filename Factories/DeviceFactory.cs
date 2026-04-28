using SmartHouseApp.Models;

namespace SmartHouseApp.Factories
{
     public class DeviceFactory : IDeviceFactory
     {
          public Device CreateDevice(string type, string room)
          {
               return type switch
               {
                    "Light" => new Light(room),
                    "Thermostat" => new Thermostat(room),
                    "DoorLock" => new DoorLock(room),
                    _ => throw new ArgumentException("Unknown device type")
               };
          }
     }
}