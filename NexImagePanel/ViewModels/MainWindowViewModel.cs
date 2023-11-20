using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using NexImagePanel.Config;
using NexImagePanel.Interfaces;
using NexImagePanel.SystemServices;

namespace NexImagePanel.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        private PanelConfig _config;
        private ICoreServices _core;
        [ObservableProperty]
        private string? _externalBackupPath;

        public string InternalBackupPath => _config.InternalBackupPartitionPath;

        public MainWindowViewModel(PanelConfig config, ICoreServices core)
        {
            _config = config;
            _core = core;
        }


        public string ComputerName => Environment.MachineName;

        [RelayCommand]
        private void SelectBackupDestination()
        {
            using var dialog = new FolderBrowserDialog();
            // Set initial folder (optional)
            dialog.SelectedPath = @"C:\";

            // Show the dialog and capture the result
            var result = dialog.ShowDialog();

            // Check if the user selected a folder and act accordingly
            if (result != DialogResult.OK) return;
            var selectedFolder = dialog.SelectedPath;
            // Do something with the selected folder path
            ExternalBackupPath = selectedFolder;

        }

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

        [RelayCommand]
        public void ShowScriptsWindow() => new RunScriptsWindow().Show();

        [RelayCommand]
        public void EditPanelConfig() =>
            _core.OpenFileInNotepad(Path.Combine(Environment.CurrentDirectory, "appSettings.json"));

        [RelayCommand]
        public void OpenPanelScriptsFolder() =>
            _core.OpenFolderInExplorer(Path.Combine(Environment.CurrentDirectory, "Scripts"));


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
