using System.Collections.Generic;
using SmartHouseApp.Models;

namespace SmartHouseApp.Managers
{
     public class SmartHomeManager
     {
          private static SmartHomeManager _instance;

          public static SmartHomeManager Instance
          {
               get
               {
                    if (_instance == null)
                         _instance = new SmartHomeManager();

                    return _instance;
               }
          }

          public List<Room> Rooms { get; set; }

          private SmartHomeManager()
          {
               Rooms = new List<Room>();
          }

          public void AddRoom(Room room)
          {
               Rooms.Add(room);
          }
     }
}