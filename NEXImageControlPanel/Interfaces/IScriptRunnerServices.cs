using System.Diagnostics;
using NEXImageControlPanel.Models;

namespace NEXImageControlPanel.Interfaces;

public interface IScriptRunnerServices
{
    public ProcessStartInfo CreatePowerShell7Process(string script);
    public string GetAbsoluteScriptPath(string script);
    public ProcessStartInfo CreatePowerShellProcess(string script);
    public ProcessStartInfo CreateBatchProcess(string script);

    public ProcessStartInfo CreateExecutableProcess(string script);
    public string GetAbsoluteScriptPath(string script, ScriptType scriptType);
    public void ExecuteScript(string script, ScriptType scriptType);
}