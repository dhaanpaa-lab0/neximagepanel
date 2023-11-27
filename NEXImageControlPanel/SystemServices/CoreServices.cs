using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using NEXImageControlPanel.Interfaces;

namespace NEXImageControlPanel.SystemServices;

public class CoreServices : ICoreServices
{
    public string GetPowerShellPath()
    {
        const string fallbackPath = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";
        const string powerShellKey = @"SOFTWARE\Microsoft\PowerShell\1\ShellIds\Microsoft.PowerShell";
        const string pathValueName = "Path";

        using var rk = Registry.LocalMachine.OpenSubKey(powerShellKey);
        var value = rk?.GetValue(pathValueName);

        return value != null ? value.ToString() ?? fallbackPath :
            fallbackPath;
    }

    public string GetPowerShell7Path()
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "where.exe",
                Arguments = "pwsh.exe",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();

        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return !string.IsNullOrEmpty(output) ?
            output.Split('\n')[0].Trim() :
            string.Empty;
    }

    public string GetCommandPromptPath()
    {
        var systemRoot = Environment.GetFolderPath(Environment.SpecialFolder.System);
        var cmdPath = Path.Combine(systemRoot, "cmd.exe");
        return cmdPath;
    }

    public void OpenFileInNotepad(string filePath)
    {
        Process.Start("notepad.exe", filePath);
    }

    public void OpenFolderInExplorer(string folderPath)
    {
        Process.Start("explorer.exe", folderPath);

    }
}