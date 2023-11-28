namespace NEXImageControlPanel.Interfaces;

public interface ICoreServices
{
    public string GetPowerShellPath();
    public string GetPowerShell7Path();
    public string GetCommandPromptPath();
    public void OpenFileInNotepad(string filePath);
    public void OpenFolderInExplorer(string folderPath);
}
