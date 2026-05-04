using SmartHouseApp.ViewModels;
using System.Windows.Controls;

namespace SmartHouseApp.Views.Pages
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }
    }
}
