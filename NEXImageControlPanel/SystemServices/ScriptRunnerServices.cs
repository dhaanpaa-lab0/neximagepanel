﻿using System.Diagnostics;
using System.IO;
using System.Text;
using NEXImageControlPanel.Config;
using NEXImageControlPanel.Interfaces;
using NEXImageControlPanel.Models;

namespace NEXImageControlPanel.SystemServices;

public class ScriptRunnerServices(ICoreServices core, PanelConfig config) : IScriptRunnerServices
{
    public ProcessStartInfo CreatePowerShell7Process(string script) =>
        new()
        {
            FileName = core.GetPowerShell7Path(),
            Arguments = GetAbsoluteScriptPath(script),
            UseShellExecute = true,
            CreateNoWindow = false
        };
    private static string AddExtension(string filePath, string extension)
    {
        // Check if the file already has the desired extension
        if (Path.GetExtension(filePath).Equals(extension, StringComparison.OrdinalIgnoreCase))
        {
            return filePath;
        }

        // Add the extension
        return filePath + extension;
    }
    public string GetAbsoluteScriptPath(string script)
    {
        var scriptFileName = string.IsNullOrEmpty(Path.GetExtension(script)) 
            ? AddExtension(script, ".ps1") 
            : script;
        return Path.Combine(config.ScriptsFolder, scriptFileName);
    }

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

    private static string GetScriptExtension(ScriptType scriptType) =>
        scriptType switch
        {
            ScriptType.PowerShell7 => ".ps1",
            ScriptType.PowerShell => ".ps1",
            ScriptType.Batch => ".cmd",
            ScriptType.Executable => ".exe",
            _ => throw new ArgumentOutOfRangeException(nameof(scriptType), scriptType, null)
        };

    public string GetAbsoluteScriptPath(string script, ScriptType scriptType)
    {
        return GetAbsoluteScriptPath(AddExtension(script, GetScriptExtension(scriptType)));
    }

    public void ExecuteScript(string script, ScriptType scriptType)
    {
        var absoluteScriptPath = GetAbsoluteScriptPath(script, scriptType);

        var psi = scriptType switch
        {
            ScriptType.PowerShell7 => CreatePowerShell7Process(absoluteScriptPath),
            ScriptType.PowerShell => CreatePowerShellProcess(absoluteScriptPath),
            ScriptType.Batch => CreateBatchProcess(absoluteScriptPath),
            ScriptType.Executable => CreateExecutableProcess(absoluteScriptPath),
            _ => throw new ArgumentOutOfRangeException()
        };

        var p = Process.Start(psi);
        p?.WaitForExit();
    }
}