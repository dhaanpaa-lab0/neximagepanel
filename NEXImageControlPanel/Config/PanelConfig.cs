using System.IO;
using Microsoft.Extensions.Configuration;
using NEXImageControlPanel.Models;

namespace NEXImageControlPanel.Config
{
    public class PanelConfig(IConfiguration configuration)
    {
        public string ImageFileName => configuration["imageFileName"] ?? "";
        public string InternalBackupPartitionPath => configuration["internalBackupPartitionPath"] ?? "D:\\";
        public string ScriptsFolder => Path.Combine(Environment.CurrentDirectory, "Scripts");

        public List<ScriptModel> Scripts
        {
            get
            {
                var scriptsConfigSection = configuration.GetSection("scripts");

                return scriptsConfigSection.Exists()
                    ? scriptsConfigSection.Get<List<ScriptModel>>() ?? new List<ScriptModel>()
                    : new();
            }
        }

        public List<ImageScript> ImageScripts
        {
            get
            {
                var scriptsConfigSection = configuration.GetSection("imageScripts");

                return scriptsConfigSection.Exists()
                    ? scriptsConfigSection.Get<List<ImageScript>>() ?? new List<ImageScript>()
                    : new();
            }
        }
    }

}
