using SmartHouseApp.Models;

namespace SmartHouseApp.Factories
{
     public interface ISmartHomeFactory
     {
          Device CreateLight(string room);
          Device CreateThermostat(string room);
          Device CreateDoorLock(string room);
     }
}