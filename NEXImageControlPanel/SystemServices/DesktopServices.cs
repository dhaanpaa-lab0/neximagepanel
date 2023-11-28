using NEXImageControlPanel.Interfaces;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using FontFamily = System.Windows.Media.FontFamily;
using FontStyle = System.Drawing.FontStyle;

namespace NEXImageControlPanel.SystemServices;

public class DesktopServices : IDesktopServices
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDCHANGE = 0x02;

    private static (double screenHeight, double screenWidth) GetScreenWidth()
    {
        var height = SystemParameters.PrimaryScreenHeight;
        var width = SystemParameters.PrimaryScreenWidth;
        
        return (screenHeight: height, screenWidth: width);
    }

    public void SetSolidColorWallpaper(Color color, string message1 = "", string message2 = "")
    {
        var (screenHeight, screenWidth) = GetScreenWidth();
        // Create a bitmap with the desired color
        var bmp = new Bitmap(Convert.ToInt32(screenWidth), Convert.ToInt32(screenHeight));
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(color);
            if (!string.IsNullOrEmpty(message1))
            {
                var font = new Font(System.Drawing.FontFamily.GenericMonospace, 20, FontStyle.Bold);
                var m = g.MeasureString(message1, font);
                var screenLocationY = (0 + m.Height) + 40;
                var screenLocationX = (screenWidth - m.Width) - 40;
                g.DrawString(message1, font, new SolidBrush(Color.White), (float)screenLocationX,
                    screenLocationY);
            }

            if (!string.IsNullOrEmpty(message2))
            {
                var font = new Font(System.Drawing.FontFamily.GenericMonospace, 20, FontStyle.Bold);
                var m = g.MeasureString(message2, font);
                var screenLocationY = (0 + m.Height) + 80;
                var screenLocationX = (screenWidth - m.Width) - 40;
                g.DrawString(message2, font, new SolidBrush(Color.White), (float)screenLocationX,
                    screenLocationY);
            }
            //g.DrawString(message1, );
        }

        // Save the bitmap to a temporary file
        string tempPath = Path.Combine(Path.GetTempPath(), $"{Environment.UserName}_solidcolor.bmp");
        Debug.Print(tempPath);
        bmp.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

        // Set the temporary file as wallpaper
        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
    }



}