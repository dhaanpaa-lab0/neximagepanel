using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NexImagePanel.Models
{
    public partial class ImageScript : ObservableObject
    {
        [ObservableProperty]
        public string? scriptDescription;

        [ObservableProperty]
        public string? scriptName;

        [RelayCommand]
        private void ExecuteScript()
        {
            MessageBox.Show($"Script: {ScriptName}");
        }
    }
}
