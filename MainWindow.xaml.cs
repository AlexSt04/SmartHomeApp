using System.Windows;
using SmartHouseApp.Models;
using SmartHouseApp.Managers;
using SmartHouseApp.Factories;
using SmartHouseApp.Builders;
using SmartHouseApp.Managers;

namespace SmartHouseApp
{
     public partial class MainWindow : Window
     {
          public MainWindow()
          {
               InitializeComponent();
          }

          private void Test_Click(object sender, RoutedEventArgs e)
          {
               var manager = SmartHomeManager.Instance;

               // Builder creeaza camera
               var builder = new RoomBuilder();

               var room = builder
                   .CreateRoom("Bedroom")
                   .AddLight()
                   .AddThermostat()
                   .AddDoorLock()
                   .Build();

               manager.AddRoom(room);

               // Prototype (clone)
               var clonedDevice = room.Devices[0].Clone();

               MessageBox.Show("Room devices: " + room.Devices.Count +
                               "\nCloned: " + clonedDevice.Name);
          }


     }
}