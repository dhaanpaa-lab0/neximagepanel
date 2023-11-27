using CommunityToolkit.Mvvm.ComponentModel;

namespace NEXImageControlPanel.Models
{
    public partial class ImageScript : ObservableObject
    {
        [ObservableProperty]
        public string? scriptDescription;

        [ObservableProperty]
        public string? scriptName;


    }
}
