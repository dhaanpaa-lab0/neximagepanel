using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexImagePanel
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}
