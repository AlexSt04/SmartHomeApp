using System.Windows;
using SmartHouseApp.Models;
using SmartHouseApp.Managers;
using SmartHouseApp.Factories;
using SmartHouseApp.Builders;
using SmartHouseApp.Managers;
using SmartHouseApp.Patterns.Decorator;
using SmartHouseApp.Patterns.Proxy;
using SmartHouseApp.Patterns.Flyweight;

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

               var light = DeviceFlyweightFactory.GetLight("Kitchen");

               var proxy = new DeviceProxy(light, true);

               var decorated = new NotificationDecorator(proxy);

               var room = new Room("Kitchen");
               room.AddDevice(decorated);

               manager.AddRoom(room);

               decorated.TurnOn();

               MessageBox.Show("Lab 5 OK");
          }


     }
}