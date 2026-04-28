using SmartHouseApp.Models;
using System.Collections.Generic;

namespace SmartHouseApp.Patterns.Flyweight
{
     public class DeviceFlyweightFactory
     {
          private static Dictionary<string, Device> _cache = new();

          public static Device GetLight(string room)
          {
               string key = "Light";

               if (!_cache.ContainsKey(key))
                    _cache[key] = new Light(room);

               return _cache[key].Clone();
          }

          public static Device GetThermostat(string room)
          {
               string key = "Thermostat";

               if (!_cache.ContainsKey(key))
                    _cache[key] = new Thermostat(room);

               return _cache[key].Clone();
          }
     }
}