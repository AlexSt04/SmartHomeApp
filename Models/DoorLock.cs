namespace SmartHouseApp.Models
{
     public class DoorLock : Device
     {
          public bool IsLocked { get; private set; }

          public DoorLock(string room) : base("DoorLock", room) { }

          public override void TurnOn()
          {
               IsLocked = true;
          }

          public override void TurnOff()
          {
               IsLocked = false;
          }
     }
}