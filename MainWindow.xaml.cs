using System.Windows;
using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Builders;
using SmartHouseApp.Patterns.Flyweight;
using SmartHouseApp.Patterns.Decorator;
using SmartHouseApp.Patterns.Proxy;
using SmartHouseApp.Views.Pages;

namespace SmartHouseApp
{
     public partial class MainWindow : Window
     {
          private readonly SmartHomeFacade facade;

          public MainWindow()
          {
               InitializeComponent();

               facade = new SmartHomeFacade();

               // Default page
               MainContent.Content = new DashboardView();
          }

          // =========================
          // NAVIGATION
          // =========================

          private void Dashboard_Click(object sender, RoutedEventArgs e)
          {
               MainContent.Content = new DashboardView();
          }

          private void Rooms_Click(object sender, RoutedEventArgs e)
          {
               MainContent.Content = new RoomsView();
          }

          private void Devices_Click(object sender, RoutedEventArgs e)
          {
               MainContent.Content = new DevicesView();
          }

          private void Logs_Click(object sender, RoutedEventArgs e)
          {
               MessageBox.Show("Logs system will be implemented in Lab 6 (Observer + Command).");
          }

          // =========================
          // DEMO ACTIONS (PATTERNS)
          // =========================

          private void TestPatterns_Click(object sender, RoutedEventArgs e)
          {
               // =========================
               // LAB 3 - BUILDER
               // =========================
               var builder = new RoomBuilder();

               var room = builder
                   .CreateRoom("Living Room")
                   .AddLight()
                   .AddThermostat()
                   .AddDoorLock()
                   .Build();

               SmartHomeManager.Instance.AddRoom(room);

               // =========================
               // LAB 5 - FLYWEIGHT
               // =========================
               var light = DeviceFlyweightFactory.GetLight("Living Room");

               // =========================
               // PROXY (SECURITY)
               // =========================
               var securedDevice = new DeviceProxy(light, true);

               // =========================
               // DECORATOR (FEATURE EXTENSION)
               // =========================
               var smartDevice = new NotificationDecorator(securedDevice);

               room.AddDevice(smartDevice);

               // =========================
               // EXECUTION DEMO
               // =========================
               smartDevice.TurnOn();

               MessageBox.Show(
                   $"Room created with {room.Devices.Count} devices",
                   "Smart House Demo"
               );
          }
     }
}