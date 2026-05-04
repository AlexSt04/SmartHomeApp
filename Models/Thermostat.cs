namespace SmartHouseApp.Models
{
    public class Thermostat : Device
    {
        public int Temperature { get; set; }
        
        public Thermostat(string room) : base("Thermostat", room)
        {
            Temperature = 22;
        }

        public override bool IsActive => Temperature > 20;  // Example logic
        public override string GetStatus() => $"{Temperature}°C";

        public override void TurnOn() 
        {
            // Implementation for turning on thermostat if needed
            OnStatusChanged();
        }

        public override void TurnOff() 
        {
            // Implementation for turning off thermostat if needed
            OnStatusChanged();
        }

        public override Device Clone()
        {
            return new Thermostat(this.Room)
            {
                Temperature = this.Temperature
            };
        }
    }
}