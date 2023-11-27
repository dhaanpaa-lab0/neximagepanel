namespace NEXImageControlPanel.Models
{
    public class ScriptModel
    {
        public string ScriptName { get; set; } = "";

        public string Description { get; set; } = "";
        public ScriptType Type { get; set; } = ScriptType.PowerShell7;
    }
}
