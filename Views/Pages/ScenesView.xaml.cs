using SmartHouseApp.ViewModels;
using System.Windows.Controls;

namespace SmartHouseApp.Views.Pages
{
     public partial class ScenesView : UserControl
     {
          public ScenesView()
          {
               InitializeComponent();
               DataContext = new ScenesViewModel();
          }
     }
}