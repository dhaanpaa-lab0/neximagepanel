using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace NEXImageControlPanel.SystemServices;

public class DesktopServices
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDCHANGE = 0x02;

    public static void SetSolidColorWallpaper(Color color)
    {
        // Create a bitmap with the desired color
        Bitmap bmp = new Bitmap(1, 1);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.Clear(color);
        }

        // Save the bitmap to a temporary file
        string tempPath = Path.Combine(Path.GetTempPath(), $"{Environment.UserName}_solidcolor.bmp");
        bmp.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

        // Set the temporary file as wallpaper
        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
    }

}