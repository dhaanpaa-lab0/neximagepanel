using System.Diagnostics;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NEXImageControlPanel.Config;
using NEXImageControlPanel.Interfaces;
using NEXImageControlPanel.Models;
using MessageBox = System.Windows.MessageBox;

namespace NEXImageControlPanel.ViewModels;


public partial class RunScriptsWindowViewModel(PanelConfig config, ICoreServices core, IScriptRunnerServices runner) : ObservableObject
{
    [ObservableProperty]
    private ScriptModel? _selectedScript;

    public List<ScriptModel> Scripts => config.Scripts;
    



    [RelayCommand]
    public void RunScript()
    {
        if (SelectedScript == null) return;
        var absoluteScriptPath = runner.GetAbsoluteScriptPath(SelectedScript.ScriptName);
        if (!Path.Exists(absoluteScriptPath))
        {
            MessageBox.Show($"Script Not Found: {absoluteScriptPath}");
            return;
        }
        var psi = SelectedScript.Type switch
        {
            ScriptType.PowerShell7 => runner.CreatePowerShell7Process(SelectedScript.ScriptName),
            ScriptType.PowerShell => runner.CreatePowerShellProcess(SelectedScript.ScriptName),
            ScriptType.Batch => runner.CreateBatchProcess(SelectedScript.ScriptName),
            ScriptType.Executable => runner.CreateExecutableProcess(SelectedScript.ScriptName),
            _ => throw new ArgumentOutOfRangeException()
        };
        

        var p = Process.Start(psi);
        p?.WaitForExit();

    }
}