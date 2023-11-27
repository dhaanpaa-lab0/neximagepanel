﻿using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NexImagePanel.Config;
using NexImagePanel.Interfaces;
using NexImagePanel.Models;

namespace NexImagePanel.ViewModels;

public partial class ImageBackupWindowViewModel : ObservableObject
{
    private PanelConfig _panelConfig;
    private readonly IScriptRunnerServices _runner;
    [ObservableProperty] private string? _internalBackupPath;
    [ObservableProperty] private string? _externalBackupPath;
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
    public void DoScript(string parameter)
    {
        var script = this.ImageScripts.FirstOrDefault(s => s.ScriptName == parameter);
        if (script is { ScriptName: not null })
        {
            MessageBox.Show(_runner.GetAbsoluteScriptPath(script.ScriptName, ScriptType.Batch));
        }
    }

    public ImageBackupWindowViewModel(PanelConfig panelConfig, IScriptRunnerServices runner)
    {
        _panelConfig = panelConfig;
        _runner = runner;
        ImageScripts = panelConfig.ImageScripts;
        InternalBackupPath = panelConfig.InternalBackupPartitionPath;
    }

}