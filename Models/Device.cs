namespace SmartHouseApp.Models
{
     public abstract class Device
     {
          public string Name { get; set; }
          public string Room { get; set; }

          protected Device(string name, string room)
          {
               Name = name;
               Room = room;
          }

          public abstract void TurnOn();
          public abstract void TurnOff();
     }
}