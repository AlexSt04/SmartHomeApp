namespace SmartHouseApp.Models
{
     public class TemperatureSensorAdapter : Device
     {
          private ExternalTemperatureSensor _sensor;

          public TemperatureSensorAdapter(string room)
              : base("ExternalSensor", room)
          {
               _sensor = new ExternalTemperatureSensor();
          }

          public double ReadTemperature()
          {
               return _sensor.GetTemperature();
          }

          public override void TurnOn() { }
          public override void TurnOff() { }

          public override Device Clone()
          {
               return new TemperatureSensorAdapter(this.Room);
          }
     }
}