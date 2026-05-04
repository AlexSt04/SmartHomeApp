namespace SmartHouseApp.Models
{
    public class DoorLock : Device
    {
        public bool IsLocked { get; private set; }
        
        public DoorLock(string room) : base("DoorLock", room) { }
        
        public override bool IsActive => IsLocked;
        public override string GetStatus() => IsLocked ? "LOCKED" : "UNLOCKED";

        public override void TurnOn()
        {
            IsLocked = true;
            OnStatusChanged();
        }

        public override void TurnOff()
        {
            IsLocked = false;
            OnStatusChanged();
        }

        public override Device Clone()
        {
            return new DoorLock(this.Room) { IsLocked = this.IsLocked };
        }
    }
}