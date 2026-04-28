namespace SmartHouseApp.Models
{
     public class Light : Device
     {
          public bool IsOn { get; private set; }

          public Light(string room) : base("Light", room) { }

          public override void TurnOn()
          {
               IsOn = true;
          }

          public override void TurnOff()
          {
               IsOn = false;
          }
     }
}