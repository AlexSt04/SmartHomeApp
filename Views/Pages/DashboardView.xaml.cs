using SmartHouseApp.ViewModels;
using System.Windows.Controls;

namespace SmartHouseApp.Views.Pages
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }
    }
}
