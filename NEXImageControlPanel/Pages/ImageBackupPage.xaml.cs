using Microsoft.Extensions.DependencyInjection;
using NEXImageControlPanel.PageViewModels;
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

namespace NEXImageControlPanel.Pages
{
    /// <summary>
    /// Interaction logic for ImageBackupPage.xaml
    /// </summary>
    public partial class ImageBackupPage : Page
    {
        public ImageBackupPage()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            this.DataContext = app.Services.GetService<ImageBackupPageViewModel>();
        }
    }
}
