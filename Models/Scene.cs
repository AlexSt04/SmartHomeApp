namespace SmartHouseApp.Models
{
     public enum SceneActionType
     {
          TurnOn,
          TurnOff,
          SetTemperature,
          Lock,
          Unlock
     }

     /// <summary>
     /// O acțiune dintr-o scenă: ce tip de device, în ce cameră, ce operație.
     /// </summary>
     public class SceneAction
     {
          /// <summary>"All" | "Light" | "Thermostat" | "DoorLock" | "SmartTV" | "AirConditioner"</summary>
          public string DeviceType { get; set; } = "All";

          /// <summary>Filtru cameră. "" = toate camerele.</summary>
          public string RoomFilter { get; set; } = "";

          public SceneActionType ActionType { get; set; } = SceneActionType.TurnOn;

          /// <summary>Valoare temperatură (doar când ActionType = SetTemperature).</summary>
          public int TemperatureValue { get; set; } = 22;
     }

     /// <summary>
     /// O scenă = lista de acțiuni executate simultan pe device-uri.
     /// </summary>
     public class Scene
     {
          public string Id { get; set; } = Guid.NewGuid().ToString();
          public string Name { get; set; } = "";
          public string Icon { get; set; } = "🏠";
          public string Description { get; set; } = "";
          public string AccentColor { get; set; } = "#10B981";
          public bool IsPreset { get; set; } = false;
          public List<SceneAction> Actions { get; set; } = new();
     }
}