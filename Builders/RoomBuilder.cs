using SmartHouseApp.Models;

namespace SmartHouseApp.Builders
{
     public class RoomBuilder
     {
          private Room _room;

          public RoomBuilder CreateRoom(string name)
          {
               _room = new Room(name);
               return this;
          }

          public RoomBuilder AddLight()
          {
               _room.AddDevice(new Light(_room.Name));
               return this;
          }

          public RoomBuilder AddThermostat()
          {
               _room.AddDevice(new Thermostat(_room.Name));
               return this;
          }

          public RoomBuilder AddDoorLock()
          {
               _room.AddDevice(new DoorLock(_room.Name));
               return this;
          }

          public Room Build()
          {
               return _room;
          }
     }
}