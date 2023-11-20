using Microsoft.Extensions.DependencyInjection;
using NexImagePanel.ViewModels;
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
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace NexImagePanel
{
    /// <summary>
    /// Interaction logic for RunScriptsWindow.xaml
    /// </summary>
    public partial class RunScriptsWindow : Window
    {
        public RunScriptsWindow()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            this.DataContext = app.Services.GetService<RunScriptsWindowViewModel>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
