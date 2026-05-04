namespace SmartHouseApp.Models
{
    public class Light : Device
    {
        public bool IsOn { get; private set; }
        
        public Light(string room) : base("Light", room) { }
        
        public override bool IsActive => IsOn;
        public override string GetStatus() => IsOn ? "ON" : "OFF";

        public override void TurnOn()
        {
            IsOn = true;
            OnStatusChanged();
        }

        public override void TurnOff()
        {
            IsOn = false;
            OnStatusChanged();
        }

        public override Device Clone()
        {
            return new Light(this.Room) { IsOn = this.IsOn };
        }
    }
}