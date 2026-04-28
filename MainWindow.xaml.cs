using SmartHouseApp.Builders;
using SmartHouseApp.Factories;
using SmartHouseApp.Managers;
using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Patterns.Behavioral.ChainOfResponsibility;
using SmartHouseApp.Patterns.Behavioral.State;
using SmartHouseApp.Patterns.Behavioral.TemplateMethod;
using SmartHouseApp.Patterns.Behavioral.Visitor;
using SmartHouseApp.Patterns.Decorator;
using SmartHouseApp.Patterns.Flyweight;
using SmartHouseApp.Patterns.Proxy;
using System.Windows;

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
               // Chain
               var power = new PowerHandler();
               var security = new SecurityHandler();
               power.SetNext(security);
               power.Handle("SECURITY");

               // State
               var context = new DeviceContext();
               context.State = new OffState();
               context.Request();
               context.Request();

               // Template
               var light = new LightTemplate();
               light.Execute();

               // Visitor
               var device = new LightDevice();
               device.Accept(new StatusVisitor());

               MessageBox.Show("Lab 7 OK");
          }


     }
}