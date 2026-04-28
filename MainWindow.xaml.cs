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
               var facade = new SmartHomeFacade();

               facade.AddRoom("Kitchen");

               facade.AddDeviceToRoom("Kitchen", new Light("Kitchen"));
               facade.AddDeviceToRoom("Kitchen", new Thermostat("Kitchen"));

               // Adapter
               var sensor = new TemperatureSensorAdapter("Kitchen");
               facade.AddDeviceToRoom("Kitchen", sensor);

               facade.TurnAllOn("Kitchen");

               MessageBox.Show("Devices: " + facade.GetDeviceCount("Kitchen"));
          }


     }
}