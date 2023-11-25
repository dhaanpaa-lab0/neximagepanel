using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NexImagePanel.Config;
using NexImagePanel.Models;

namespace NexImagePanel.ViewModels;

public partial class ImageBackupWindowViewModel : ObservableObject
{
    private PanelConfig _panelConfig;
    [ObservableProperty] private string? _internalBackupPath;
    [ObservableProperty] private string? _externalBackupPath;
    [ObservableProperty] private List<ImageScript> _imageScripts;

    
    public ImageBackupWindowViewModel(PanelConfig panelConfig)
    {
        _panelConfig = panelConfig;
        ImageScripts = panelConfig.ImageScripts;
        InternalBackupPath = panelConfig.InternalBackupPartitionPath;
    }

}