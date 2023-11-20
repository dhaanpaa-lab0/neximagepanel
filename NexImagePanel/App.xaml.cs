using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using NexImagePanel.Config;
using NexImagePanel.Interfaces;
using NexImagePanel.SystemServices;
using NexImagePanel.ViewModels;

namespace NexImagePanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : System.Windows.Application


    {
        


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Services = ConfigureServices();
        }

        public IServiceProvider Services { get; private set; }


        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path where the configuration file is located
                .AddJsonFile("appsettings.json"); // Add the JSON configuration file
            services.AddSingleton<IConfiguration>(builder.Build());
            services.AddSingleton<PanelConfig>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<RunScriptsWindowViewModel>();
            services.AddSingleton<ICoreServices, CoreServices>();
            return services.BuildServiceProvider();
        }
    }

}
