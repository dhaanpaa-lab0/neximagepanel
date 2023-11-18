using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using static System.Windows.Forms.DialogResult;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Path = System.IO.Path;


namespace NexImagePanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfiguration _configuration;

        public MainWindow()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            this._configuration = app.Configuration;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtComputerName.Text = Environment.MachineName;
        }

        private string UsbBackupDrive { get; set; } = "";

        private string ImageFileName => _configuration["imageFileName"] ?? "";

        private void BtnSetUsbDrive_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            // Set initial folder (optional)
            dialog.SelectedPath = @"C:\";

            // Show the dialog and capture the result
            var result = dialog.ShowDialog();

            // Check if the user selected a folder and act accordingly
            if (result != OK) return;
            var selectedFolder = dialog.SelectedPath;
            // Do something with the selected folder path
            TxtUsbDrive.Text = selectedFolder;
            this.UsbBackupDrive = selectedFolder;
        }

        private void BtnCopyToBackupFolder_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(this.UsbBackupDrive)) return;
            var imageNew = $"{Environment.MachineName}.CurrentImage.TBI";
            var sourceImageFileName = Path.Combine("D:",ImageFileName);
            if (!File.Exists(sourceImageFileName)) return;
            var destImageFileName = Path.Combine(UsbBackupDrive, imageNew);
            File.Copy(sourceImageFileName,destImageFileName);
            MessageBox.Show($"Image has been copied to: {destImageFileName}");
        }
    }
}