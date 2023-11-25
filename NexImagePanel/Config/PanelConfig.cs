using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexImagePanel.Models;

namespace NexImagePanel.Config
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
