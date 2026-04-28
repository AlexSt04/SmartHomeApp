using System.Collections.Generic;
using SmartHouseApp.Models;

namespace SmartHouseApp.Managers
{
     public class SmartHomeManager
     {
          public List<Room> Rooms { get; set; } = new();

          public void AddRoom(Room room)
          {
               Rooms.Add(room);
          }
     }
}