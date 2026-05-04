using System;
using System.Collections.Generic;
using System.Linq;
using SmartHouseApp.Models;
using SmartHouseApp.Observers;

namespace SmartHouseApp.Managers
{
    public sealed class SmartHomeManager : ISubject
    {
        private static readonly Lazy<SmartHomeManager> _instance = 
            new(() => new SmartHomeManager());
        
        public static SmartHomeManager Instance => _instance.Value;
        
        private readonly List<IObserver> _observers = new();
        public List<Room> Rooms { get; set; } = new();
        
        private SmartHomeManager() { }
        
        // Observer Pattern Implementation
        public void Subscribe(IObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }
        
        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
        
        public void Notify(ObserverNotification notification)
        {
            foreach (var observer in _observers.ToList())
            {
                observer.Update(notification);
            }
        }
        
        // Methods that trigger notifications
        public void AddRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));
            
            if (Rooms.Any(r => r.Name == room.Name))
                throw new InvalidOperationException($"Room '{room.Name}' already exists");
            
            Rooms.Add(room);
            
            // 🔔 NOTIFY OBSERVERS
            Notify(new ObserverNotification
            {
                EventType = "RoomAdded",
                Source = this,
                Timestamp = DateTime.Now,
                Data = new() { { "RoomName", room.Name }, { "TotalRooms", Rooms.Count } }
            });
        }
    }
}