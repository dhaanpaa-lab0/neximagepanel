using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NEXImageControlPanel.Interfaces;
using NEXImageControlPanel.SystemServices;

namespace NEXImageControlPanel.PageViewModels;

public partial class DesktopBackgroundPageViewModel : ObservableObject
{
    private readonly IDesktopServices desktopServices;

    public DesktopBackgroundPageViewModel(IDesktopServices desktopServices)
    {
        this.desktopServices = desktopServices;
        Message1 = "";
        Message2 = "";
    }

    [ObservableProperty] public string message1;
    [ObservableProperty] public string message2;

    [RelayCommand]
    public void SetDesktopColor(Color color)
    {
        desktopServices.SetSolidColorWallpaper(color, Message1, Message2);
    }
}