using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace NexImagePanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : System.Windows.Application


    {
        public IConfiguration Configuration { get; private set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create a ConfigurationBuilder
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path where the configuration file is located
                .AddJsonFile("appsettings.json"); // Add the JSON configuration file

            // Build the IConfiguration instance
            Configuration = builder.Build();
        }
    }

}
