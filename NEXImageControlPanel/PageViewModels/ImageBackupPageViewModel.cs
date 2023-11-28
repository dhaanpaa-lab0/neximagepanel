using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NEXImageControlPanel.Config;
using NEXImageControlPanel.Models;
using System.ComponentModel;
using System.Windows;
using NEXImageControlPanel.Interfaces;

namespace NEXImageControlPanel.PageViewModels
{
    public partial class ImageBackupPageViewModel : ObservableObject
    {
        private readonly IScriptRunnerServices _runner;
        [ObservableProperty] private PanelConfig _config;
        [ObservableProperty] private string _internalBackupPath;
        [ObservableProperty] private string _externalBackupPath;
        [ObservableProperty] private List<ImageScript> _imageScripts;
        [RelayCommand]
        private void SelectExternalBackupPath()
        {
            var s = new OpenFolderDialog
            {
                InitialDirectory = "C:",
                Title = "Select External Backup Folder",

            };

            s.FolderOk += SelectExternalBackupPath_FolderOkClick;
            s.ShowDialog();
        }

        private void SelectExternalBackupPath_FolderOkClick(object? sender, CancelEventArgs args)
        {
            if (sender == null) return;
            var hmc = (OpenFolderDialog)sender;
            ExternalBackupPath = hmc.FolderName;
        }

        [RelayCommand]
        public void ExecuteImageScript(ImageScript script)
        {
            if (script is { ScriptName: not null })
            {
                _runner.ExecuteScript(script.ScriptName, ScriptType.Batch);
            }
        }

        public ImageBackupPageViewModel(PanelConfig config, IScriptRunnerServices runner)
        {
            _runner = runner;
            InternalBackupPath = config.InternalBackupPartitionPath;
            ExternalBackupPath = "";
            ImageScripts = config.ImageScripts;
            Config = config;
        }
    }
}
