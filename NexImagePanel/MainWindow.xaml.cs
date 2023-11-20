using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using NexImagePanel.ViewModels;
using Application = System.Windows.Application;


namespace NexImagePanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            this.DataContext = app.Services.GetService<MainWindowViewModel>();
        }

        private void Exit_ImagePanel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}