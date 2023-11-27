using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NexImagePanel.ViewModels;

namespace NexImagePanel.Models
{
    public partial class ImageScript : ObservableObject
    {
        [ObservableProperty]
        public string? scriptDescription;

        [ObservableProperty]
        public string? scriptName;


    }
}
