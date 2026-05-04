using SmartHouseApp.Models;
using System;

namespace SmartHouseApp.Models
{
    public class SmartTV : Device
    {
        public bool IsOn { get; private set; }
        public int CurrentChannel { get; private set; } = 1;
        public int Volume { get; private set; } = 20;

        public SmartTV(string room) : base("Smart TV", room) { }

        public override void TurnOn() { IsOn = true; OnStatusChanged(); }
        public override void TurnOff() { IsOn = false; OnStatusChanged(); }
        public override string GetStatus() => IsOn ? $"ON (CH: {CurrentChannel}, Vol: {Volume})" : "OFF";
        public override bool IsActive => IsOn;

        public void ChangeChannel(int channel) { CurrentChannel = channel; OnStatusChanged(); }

        public override Device Clone() => new SmartTV(Room) 
        { 
            Name = this.Name, 
            IsOn = this.IsOn, 
            CurrentChannel = this.CurrentChannel, 
            Volume = this.Volume 
        };
    }

    public class AirConditioner : Device
    {
        public bool IsActive_AC { get; private set; }
        public int Temperature { get; set; } = 22;

        public AirConditioner(string room) : base("Air Conditioner", room) { }

        public override void TurnOn() { IsActive_AC = true; OnStatusChanged(); }
        public override void TurnOff() { IsActive_AC = false; OnStatusChanged(); }
        public override string GetStatus() => IsActive_AC ? $"ON ({Temperature}°C)" : "OFF";
        public override bool IsActive => IsActive_AC;

        public override Device Clone() => new AirConditioner(Room) 
        { 
            Name = this.Name, 
            IsActive_AC = this.IsActive_AC, 
            Temperature = this.Temperature 
        };
    }
}
