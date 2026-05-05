using LiteDB;

namespace SmartHouseApp.Services
{
     /// <summary>
     /// DTO pentru persistarea unui device în LiteDB.
     /// Stochează tipul ca string pentru a putea reconstitui obiectul corect.
     /// </summary>
     public class DeviceRecord
     {
          public string Type { get; set; } = "";       // "Light", "Thermostat", "DoorLock", "SmartTV", "AirConditioner"
          public string Name { get; set; } = "";
          public string Room { get; set; } = "";
          public bool IsActive { get; set; }
          public int Temperature { get; set; } = 22;   // pentru Thermostat / AirConditioner
          public int Channel { get; set; } = 1;        // pentru SmartTV
          public int Volume { get; set; } = 20;        // pentru SmartTV
     }

     /// <summary>
     /// DTO pentru persistarea unui Room în LiteDB.
     /// </summary>
     public class RoomRecord
     {
          [BsonId]
          public string Name { get; set; } = "";
          public List<DeviceRecord> Devices { get; set; } = new();
     }
}