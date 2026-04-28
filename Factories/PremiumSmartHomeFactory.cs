using SmartHouseApp.Models;

namespace SmartHouseApp.Factories
{
     public class PremiumSmartHomeFactory : ISmartHomeFactory
     {
          public Device CreateLight(string room)
          {
               var light = new Light(room);
               light.TurnOn(); // vine preset
               return light;
          }

          public Device CreateThermostat(string room)
          {
               var thermostat = new Thermostat(room);
               thermostat.Temperature = 24;
               return thermostat;
          }

          public Device CreateDoorLock(string room)
          {
               return new DoorLock(room);
          }
     }
}