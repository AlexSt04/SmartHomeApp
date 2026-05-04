using System;
using System.Windows;
using System.Windows.Controls;

namespace SmartHouseApp.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        private ContentControl _mainContent;
        private TextBlock _pageTitle;

        private NavigationService() { }

        public void Initialize(ContentControl mainContent, TextBlock pageTitle)
        {
            _mainContent = mainContent;
            _pageTitle = pageTitle;
        }

        public void NavigateTo(UserControl page, string title)
        {
            if (_mainContent == null) return;
            _mainContent.Content = page;
            if (_pageTitle != null) _pageTitle.Text = title;
        }
    }
}
