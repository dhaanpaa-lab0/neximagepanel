using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NEXImageControlPanel.SystemServices;

namespace NEXImageControlPanel.PageViewModels
{

    public partial class SystemInfoPageViewModel : ObservableObject
    {

        
        public string ComputerName => Environment.MachineName;
        public string UserName => Environment.UserName;

      
        public SystemInfoPageViewModel()
        {
            
        }

    }
}
