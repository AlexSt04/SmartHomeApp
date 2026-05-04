using SmartHouseApp.ViewModels;
using System.Windows.Controls;

namespace SmartHouseApp.Views.Pages
{
    public partial class DevicesView : UserControl
    {
        public DevicesView()
        {
            InitializeComponent();
            DataContext = new DevicesViewModel();
        }
    }
}
