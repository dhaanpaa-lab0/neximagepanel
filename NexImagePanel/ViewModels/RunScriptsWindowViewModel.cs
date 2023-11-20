using System.Diagnostics;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NexImagePanel.Config;
using NexImagePanel.Interfaces;
using NexImagePanel.Models;
using MessageBox = System.Windows.MessageBox;

namespace NexImagePanel.ViewModels;


public partial class RunScriptsWindowViewModel(PanelConfig config, ICoreServices core) : ObservableObject
{
    [ObservableProperty]
    private ScriptModel? _selectedScript;

    public List<ScriptModel> Scripts => config.Scripts;



    public ProcessStartInfo CreatePowerShell7Process(string script) =>
        new()
        {
            FileName = core.GetPowerShell7Path(),
            Arguments = GetAbsoluteScriptPath(script),
            UseShellExecute = true,
            CreateNoWindow = false
        };

    private string GetAbsoluteScriptPath(string script) => 
        Path.Combine(config.ScriptsFolder, script);

    public ProcessStartInfo CreatePowerShellProcess(string script) =>
        new()
        {
            FileName = core.GetPowerShellPath(),
            Arguments = GetAbsoluteScriptPath(script),
            UseShellExecute = true,
            CreateNoWindow = false
        };

    public ProcessStartInfo CreateBatchProcess(string script) => new()
    {
        FileName = core.GetCommandPromptPath(),
        ArgumentList = { "/c", GetAbsoluteScriptPath(script) },
        UseShellExecute = true,
        CreateNoWindow = false
    };

    public ProcessStartInfo CreateExecutableProcess(string script) => new()
    {
        FileName = GetAbsoluteScriptPath(script),
        UseShellExecute = true, 
        CreateNoWindow = false
    };

    [RelayCommand]
    public void RunScript()
    {
        if (SelectedScript == null) return;
        var absoluteScriptPath = GetAbsoluteScriptPath(SelectedScript.ScriptName);
        if (!Path.Exists(absoluteScriptPath))
        {
            MessageBox.Show($"Script Not Found: {absoluteScriptPath}");
            return;
        }
        var psi = SelectedScript.Type switch
        {
            ScriptType.PowerShell7 => CreatePowerShell7Process(SelectedScript.ScriptName),
            ScriptType.PowerShell => CreatePowerShellProcess(SelectedScript.ScriptName),
            ScriptType.Batch => CreateBatchProcess(SelectedScript.ScriptName),
            ScriptType.Executable => CreateExecutableProcess(SelectedScript.ScriptName),
            _ => throw new ArgumentOutOfRangeException()
        };
        

        var p = Process.Start(psi);
        p?.WaitForExit();

    }
}