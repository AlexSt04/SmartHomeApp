using System.Windows;
using SmartHouseApp.Models;
using SmartHouseApp.Managers;
using SmartHouseApp.Factories;

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
               var manager = new SmartHomeManager();

               var room = new Room("Living Room");

               // alegem fabrica
               ISmartHomeFactory factory = new PremiumSmartHomeFactory();

               // cream device-uri fara new
               var light = factory.CreateLight("Living Room");
               var thermostat = factory.CreateThermostat("Living Room");

               room.AddDevice(light);
               room.AddDevice(thermostat);

               manager.AddRoom(room);

               MessageBox.Show("Devices: " + room.Devices.Count);
          }
     }
}