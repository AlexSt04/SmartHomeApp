using SmartHouseApp.Utils;
using SmartHouseApp.Services;
using System.Windows;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _systemName = "My Smart Home";
        public string SystemName
        {
            get => _systemName;
            set => SetProperty(ref _systemName, value);
        }

        private bool _notificationsEnabled = true;
        public bool NotificationsEnabled
        {
            get => _notificationsEnabled;
            set => SetProperty(ref _notificationsEnabled, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand ClearLogsCommand { get; }

        public SettingsViewModel()
        {
            SaveCommand = new RelayCommand(() => MessageBox.Show("Settings saved successfully!", "Success"));
            ClearLogsCommand = new RelayCommand(() => MessageBox.Show("Activity logs have been cleared.", "Logs"));
        }
    }
}
