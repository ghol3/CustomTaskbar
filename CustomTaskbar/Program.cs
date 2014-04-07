using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomTaskbar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.Run(new Form2());
            // Make new Working Area
            Winapi.TaskBarVisible(false);
            Desktop.NewWorkingArea();
            //run my custom taskbar
            Application.Run(new Form1());

            // Restore Working Area Size
            Desktop.OldWorkingArea();
            Winapi.TaskBarVisible(true);
        }
    }
}
