using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexImagePanel.Models
{
    public class ScriptModel
    {
        public string ScriptName { get; set; } = "";

        public string Description { get; set; } = "";
        public ScriptType Type { get; set; } = ScriptType.PowerShell7;
    }
}
