using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NEXImageControlPanel.Config;
using NEXImageControlPanel.Interfaces;
using NEXImageControlPanel.Models;

namespace NEXImageControlPanel.PageViewModels
{
    public partial class ImageScriptsPageViewModel : ObservableObject
    {
        
        private readonly PanelConfig _config;
        private readonly IScriptRunnerServices _runner;
        public ImageScriptsPageViewModel(PanelConfig config, IScriptRunnerServices runner)
        {
            _config = config;
            _runner = runner;
            Scripts = _config.Scripts;
        }

        [ObservableProperty]
        private List<ScriptModel> _scripts;

        [RelayCommand]
        private void ExecuteScript(ScriptModel script)
        {
            _runner.ExecuteScript(script.ScriptName, script.Type);
            
        }
    }
}
