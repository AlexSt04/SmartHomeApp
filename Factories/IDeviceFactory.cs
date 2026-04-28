using SmartHouseApp.Models;

namespace SmartHouseApp.Factories
{
     public interface IDeviceFactory
     {
          Device CreateDevice(string type, string room);
     }
}