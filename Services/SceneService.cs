using SmartHouseApp.Managers;
using SmartHouseApp.Models;

namespace SmartHouseApp.Services
{
     public class SceneResult
     {
          public string SceneName { get; set; } = "";
          public int AffectedCount { get; set; }
          public List<string> Details { get; set; } = new();
     }

     /// <summary>
     /// Execută o scenă: iterează toate device-urile din SmartHomeManager
     /// și aplică acțiunile care se potrivesc cu filtrele scenei.
     /// </summary>
     public class SceneService
     {
          private readonly ILoggerService _logger;

          public SceneService(ILoggerService logger)
          {
               _logger = logger;
          }

          public SceneResult Execute(Scene scene)
          {
               int affected = 0;
               var details = new List<string>();

               foreach (var room in SmartHomeManager.Instance.Rooms)
               {
                    foreach (var device in room.Devices)
                    {
                         foreach (var action in scene.Actions)
                         {
                              if (!string.IsNullOrEmpty(action.RoomFilter) &&
                                  action.RoomFilter != room.Name)
                                   continue;

                              if (!DeviceMatches(device, action.DeviceType))
                                   continue;

                              ApplyAction(device, action);
                              affected++;
                              details.Add($"{device.Name} ({room.Name}) → {DescribeAction(action)}");
                         }
                    }
               }

               _logger.LogInfo($"Scene '{scene.Name}' activated — {affected} device(s) affected.");
               return new SceneResult
               {
                    SceneName = scene.Name,
                    AffectedCount = affected,
                    Details = details
               };
          }

          private static bool DeviceMatches(Device device, string filter) => filter switch
          {
               "All" => true,
               "Light" => device is Light,
               "Thermostat" => device is Thermostat,
               "DoorLock" => device is DoorLock,
               "SmartTV" => device is SmartTV,
               "AirConditioner" => device is AirConditioner,
               _ => false
          };

          private static void ApplyAction(Device device, SceneAction action)
          {
               switch (action.ActionType)
               {
                    case SceneActionType.TurnOn:
                         device.TurnOn();
                         break;
                    case SceneActionType.TurnOff:
                         device.TurnOff();
                         break;
                    case SceneActionType.SetTemperature:
                         if (device is Thermostat t) { t.Temperature = action.TemperatureValue; t.TurnOn(); }
                         if (device is AirConditioner ac) { ac.Temperature = action.TemperatureValue; ac.TurnOn(); }
                         break;
                    case SceneActionType.Lock:
                         if (device is DoorLock dl) dl.TurnOn();
                         break;
                    case SceneActionType.Unlock:
                         if (device is DoorLock du) du.TurnOff();
                         break;
               }
          }

          private static string DescribeAction(SceneAction action) => action.ActionType switch
          {
               SceneActionType.TurnOn => "ON",
               SceneActionType.TurnOff => "OFF",
               SceneActionType.SetTemperature => $"temp → {action.TemperatureValue}°C",
               SceneActionType.Lock => "LOCKED",
               SceneActionType.Unlock => "UNLOCKED",
               _ => "applied"
          };
     }
}