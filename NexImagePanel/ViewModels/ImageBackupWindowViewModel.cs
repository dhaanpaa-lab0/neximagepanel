using CommunityToolkit.Mvvm.ComponentModel;

namespace NexImagePanel.ViewModels;

public partial class ImageBackupWindowViewModel : ObservableObject
{
    [ObservableProperty] private string? internalBackupPath;
    [ObservableProperty] private string? externalBackupPath;

}