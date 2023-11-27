using System.Diagnostics;
using NexImagePanel.Models;

namespace NexImagePanel.Interfaces;

public interface IScriptRunnerServices
{
    public ProcessStartInfo CreatePowerShell7Process(string script);
    public string GetAbsoluteScriptPath(string script);
    public ProcessStartInfo CreatePowerShellProcess(string script);
    public ProcessStartInfo CreateBatchProcess(string script);

    public ProcessStartInfo CreateExecutableProcess(string script);
    public string GetAbsoluteScriptPath(string script, ScriptType scriptType);

}