using SmartHouseApp.Models;

namespace SmartHouseApp.Factories
{
     public class BasicSmartHomeFactory : ISmartHomeFactory
     {
          public Device CreateLight(string room)
          {
               return new Light(room);
          }

          public Device CreateThermostat(string room)
          {
               return new Thermostat(room);
          }

          public Device CreateDoorLock(string room)
          {
               return new DoorLock(room);
          }
     }
}