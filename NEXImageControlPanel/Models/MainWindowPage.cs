using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NEXImageControlPanel.Models
{
    public class MainWindowPage
    {
        public string Title { get; set; } = "";
        public Expression<Func<Page>> WindowPage { get; set; }
        public string PageName { get; set; } = "";
    }
}
