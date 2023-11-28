using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEXImageControlPanel.Config;
using NEXImageControlPanel.Interfaces;
using NEXImageControlPanel.Models;
using NEXImageControlPanel.SystemServices;
using NEXImageControlPanel.ViewModels;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using NEXImageControlPanel.Managers;
using NEXImageControlPanel.PageViewModels;

namespace NEXImageControlPanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
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
            
            // Infrastructure
            services.AddSingleton<IConfiguration>(builder.Build());
            services.AddSingleton<ICoreServices, CoreServices>();
            services.AddSingleton<IScriptRunnerServices, ScriptRunnerServices>();
            services.AddSingleton<IAppPageManager, AppPageManager>();
            services.AddSingleton<IDesktopServices, DesktopServices>();
            services.AddSingleton<PanelConfig>();

            // View Models
            services.AddTransient<MainWindowViewModel>();
            
            // Page View Models
            services.AddTransient<SystemInfoPageViewModel>();
            services.AddTransient<DesktopBackgroundPageViewModel>();
            services.AddTransient<ImageBackupPageViewModel>();
            services.AddTransient<ImageScriptsPageViewModel>();

            return services.BuildServiceProvider();
        }
    }

}
