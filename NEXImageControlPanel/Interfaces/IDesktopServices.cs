using System.Drawing;

namespace NEXImageControlPanel.Interfaces;

public interface IDesktopServices
{
    void SetSolidColorWallpaper(Color color, string message1 = "", string message2 = "");
}