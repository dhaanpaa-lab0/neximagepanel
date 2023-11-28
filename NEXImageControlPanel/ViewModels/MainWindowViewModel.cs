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
        private Page _currentWindowPage;

        /// <inheritdoc/>
        public MainWindowViewModel(PanelConfig config, ICoreServices core, IAppPageManager pageManager)
        {
            _config = config;
            _core = core;
            var mainWindowPages = pageManager.GetAppPages().Select(x => new MainWindowPage()
            {
                Order = x.PageName == nameof(HomePage) ? -1 : 0,
                PageName = x.PageName,
                Title = x.Title,
                WindowPage = x.WindowPage,

            }).OrderBy(x => x.Order).ToList();
            
            WindowPages = mainWindowPages;
            CurrentWindowPage = new HomePage();
        }

       

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
                CurrentWindowPage = p.WindowPage.Compile().Invoke();
            }
        }

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
