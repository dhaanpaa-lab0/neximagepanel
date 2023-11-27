using System.Windows;
using System.Windows.Controls;
using NEXImageControlPanel.PageViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace NEXImageControlPanel.Pages
{
    /// <summary>
    /// Interaction logic for SystemInfoPage.xaml
    /// </summary>
    public partial class SystemInfoPage : Page
    {
        public SystemInfoPage()
        {
            var app = (App)Application.Current;
            this.DataContext = app.Services.GetService<SystemInfoPageViewModel>();
            InitializeComponent();
        }
    }
}
