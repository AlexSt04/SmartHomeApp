using System;

namespace SmartHouseApp.Models
{
    public class Thermostat : Device
    {
        public bool IsRunning { get; private set; }
        public int Temperature { get; set; } = 22;

        public Thermostat(string room) : base("Thermostat", room)
        {
        }

        public override void TurnOn()
        {
            IsRunning = true;
            OnStatusChanged();
        }

        public override void TurnOff()
        {
            IsRunning = false;
            OnStatusChanged();
        }

        public override string GetStatus()
        {
            return IsRunning ? $"HEATING ({Temperature}°C)" : $"{Temperature}°C (Standby)";
        }

        public override bool IsActive => IsRunning;

        public override Device Clone() => new Thermostat(Room) 
        { 
            Name = this.Name, 
            IsRunning = this.IsRunning, 
            Temperature = this.Temperature 
        };
    }
}