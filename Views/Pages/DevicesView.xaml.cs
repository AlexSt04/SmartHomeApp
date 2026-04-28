using SmartHouseApp.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartHouseApp.Views.Pages
{
     /// <summary>
     /// Interaction logic for DevicesView.xaml
     /// </summary>
     public partial class DevicesView : UserControl
     {
          public DevicesView()
          {
               InitializeComponent();
               LoadDevices();
          }

          private void LoadDevices()
          {
               DeviceList.Items.Clear();

               foreach (var room in SmartHomeManager.Instance.Rooms)
               {
                    foreach (var device in room.Devices)
                    {
                         DeviceList.Items.Add($"{device.Name} - {(device.IsOn ? "ON" : "OFF")}");
                    }
               }
          }

          private void Toggle_Click(object sender, RoutedEventArgs e)
          {
               foreach (var room in SmartHomeManager.Instance.Rooms)
               {
                    foreach (var device in room.Devices)
                    {
                         if (device.IsOn) device.TurnOff();
                         else device.TurnOn();
                    }
               }

               LoadDevices();
          }
     }
}
