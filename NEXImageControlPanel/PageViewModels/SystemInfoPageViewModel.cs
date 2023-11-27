using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NEXImageControlPanel.PageViewModels
{
    public class SystemInfoPageViewModel : ObservableObject
    {
        public string ComputerName => Environment.MachineName;

    }
}
