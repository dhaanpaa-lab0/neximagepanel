using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NEXImageControlPanel.Config;
using NEXImageControlPanel.Interfaces;
using NEXImageControlPanel.Models;
using NEXImageControlPanel.Pages;
using NEXImageControlPanel.SystemServices;

namespace NEXImageControlPanel.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _externalBackupPath;

        [ObservableProperty] private List<MainWindowPage> _windowPages;
        private readonly PanelConfig _config;
        private readonly ICoreServices _core;

        [ObservableProperty]
        private Page currentWindowPage;

        /// <inheritdoc/>
        public MainWindowViewModel(PanelConfig config, ICoreServices core, IAppPageManager pageManager)
        {
            _config = config;
            _core = core;
            _windowPages = pageManager.GetAppPages();
            CurrentWindowPage = new HomePage();
        }

        public string InternalBackupPath => _config.InternalBackupPartitionPath;


        public string ComputerName => Environment.MachineName;

        

        //<Button Margin = "6" Grid.Column="0" Grid.Row="2">Set Black Background</Button>
        [RelayCommand]
        public void SetBlackDesktop() => DesktopServices.SetSolidColorWallpaper(Color.Black);

        //<Button Margin = "6" Grid.Column="0" Grid.Row="2">Set Gray Background</Button>
        [RelayCommand]
        public void SetGrayDesktop() => DesktopServices.SetSolidColorWallpaper(Color.DarkGray);
        //<Button Margin = "6" Grid.Column="0" Grid.Row="2">Set DarkBlue Background</Button>
        [RelayCommand]
        public void SetDarkBlueDesktop() => DesktopServices.SetSolidColorWallpaper(Color.DarkSlateBlue);

        //<Button Margin = "6" Grid.Column="0" Grid.Row="2">Set LightBlue Background</Button>
        [RelayCommand]
        public void SetLightBlueDesktop() => DesktopServices.SetSolidColorWallpaper(Color.LightBlue);

        //<Button Margin = "6" Grid.Column="0" Grid.Row="2">Set Black Background</Button>
        [RelayCommand]
        public void SetDarkCyanDesktop() => DesktopServices.SetSolidColorWallpaper(Color.DarkCyan);

        //[RelayCommand]
        //public void ShowScriptsWindow() => new RunScriptsWindow().Show();

        [RelayCommand]
        public void EditPanelConfig() =>
            _core.OpenFileInNotepad(Path.Combine(Environment.CurrentDirectory, "appSettings.json"));

        [RelayCommand]
        public void OpenPanelScriptsFolder() =>
            _core.OpenFolderInExplorer(Path.Combine(Environment.CurrentDirectory, "Scripts"));

        [RelayCommand]
        public void ShowPage(string parameter)
        {
            var p = WindowPages.FirstOrDefault(x => x.PageName == parameter);

            if (p != null)
            {
                CurrentWindowPage = p.WindowPage.Invoke();
            }
        }

        //[RelayCommand]
        //public void OpenImageBackupWindow() =>
        //    new ImageBackupWindow().Show();

        [RelayCommand]
        public void Backup()
        {
            if (!Directory.Exists(this.ExternalBackupPath)) return;
            var imageNew = $"{Environment.MachineName}.CurrentImage.TBI";
            var sourceImageFileName = Path.Combine(_config.InternalBackupPartitionPath, _config.ImageFileName);
            if (!File.Exists(sourceImageFileName)) return;
            var destImageFileName = Path.Combine(ExternalBackupPath, imageNew);
            File.Copy(sourceImageFileName, destImageFileName);
            MessageBox.Show($"Image has been copied to: {destImageFileName}");
        }
    }
}
