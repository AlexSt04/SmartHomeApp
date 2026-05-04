using SmartHouseApp.Models;
using System.Collections.Generic;

namespace SmartHouseApp.Patterns.Flyweight
{
    public class DeviceFlyweightFactory
    {
        private static readonly Dictionary<string, Device> _cache = new();
        
        public static Device GetLight(string room)
        {
            string key = $"Light_{room}";
            
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = new Light(room);
            }
            
            return _cache[key].Clone();
        }
        
        public static Device GetThermostat(string room)
        {
            string key = $"Thermostat_{room}";
            
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = new Thermostat(room);
            }
            
            return _cache[key].Clone();
        }
        
        public static Device GetDoorLock(string room)
        {
            string key = $"DoorLock_{room}";
            
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = new DoorLock(room);
            }
            
            return _cache[key].Clone();
        }
        
        public static int GetCacheSize() => _cache.Count;
        public static void ClearCache() => _cache.Clear();
    }
}