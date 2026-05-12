using System.Windows;
using SmartHouseApp.Managers;
using SmartHouseApp.Builders;
using SmartHouseApp.Services;
using SmartHouseApp.Views.Pages;

namespace SmartHouseApp
{
     public partial class MainWindow : Window
     {
          public MainWindow()
          {
               InitializeComponent();

               NavigationService.Instance.Initialize(MainContent, PageTitle);

               // ✅ Încearcă să încarce datele din LiteDB
               LoadFromDatabase();

               NavigationService.Instance.NavigateTo(new DashboardView(), "Dashboard");
          }

          private void LoadFromDatabase()
          {
               var savedRooms = ServiceLocator.Database.LoadRooms();

               if (savedRooms.Count > 0)
               {
                    // Date existente în DB — le adaugă în Manager
                    foreach (var room in savedRooms)
                    {
                         try { SmartHomeManager.Instance.AddRoom(room); }
                         catch { /* Room duplicat, ignoră */ }
                    }
                    ServiceLocator.Logger.LogInfo($"Loaded {savedRooms.Count} rooms from database.");
               }
               else
               {
                    // Prima rulare — creăm date demo și le salvăm în DB
                    var builder = new RoomBuilder();
                    SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Living Room").AddLight().AddThermostat().Build());
                    SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Kitchen").AddLight().Build());
                    SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Entrance").AddDoorLock().Build());

                    ServiceLocator.Database.SaveAllRooms(SmartHomeManager.Instance.Rooms);
                    ServiceLocator.Logger.LogInfo("First run: created demo rooms and saved to database.");
               }
          }

          private void Dashboard_Click(object sender, RoutedEventArgs e)
              => NavigationService.Instance.NavigateTo(new DashboardView(), "Dashboard");

          private void Rooms_Click(object sender, RoutedEventArgs e)
              => NavigationService.Instance.NavigateTo(new RoomsView(), "Rooms");

          private void Devices_Click(object sender, RoutedEventArgs e)
              => NavigationService.Instance.NavigateTo(new DevicesView(), "Devices");

          private void Logs_Click(object sender, RoutedEventArgs e)
              => NavigationService.Instance.NavigateTo(new LogsView(), "System Logs"); // ✅ interfață reală

          private void Settings_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
              => NavigationService.Instance.NavigateTo(new SettingsView(), "Settings");

          // ✅ Scenes
          private void Scenes_Click(object sender, RoutedEventArgs e)
              => NavigationService.Instance.NavigateTo(new ScenesView(), "Scenes & Automation");

          

     }
}