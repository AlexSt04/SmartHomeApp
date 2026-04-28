namespace SmartHouseApp.Models
{
     public class Thermostat : Device
     {
          public int Temperature { get; set; }

          public Thermostat(string room) : base("Thermostat", room)
          {
               Temperature = 22;
          }

          public override void TurnOn() { }
          public override void TurnOff() { }
     }
}