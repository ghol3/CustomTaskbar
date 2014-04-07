using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CustomTaskbar
{
    static class Desktop
    {
        public static Size Size { get { return SystemInformation.VirtualScreen.Size; } }

        private static WorkAreaRectangle DefaultWindowsWorkingArea;

        public static void NewWorkingArea()
        {
            DefaultWindowsWorkingArea.left = SystemInformation.WorkingArea.Left;
            DefaultWindowsWorkingArea.top = SystemInformation.WorkingArea.Top;
            DefaultWindowsWorkingArea.right = SystemInformation.WorkingArea.Right;
            DefaultWindowsWorkingArea.bottom = SystemInformation.WorkingArea.Bottom;

            WorkAreaRectangle dRec;
            dRec.left = SystemInformation.VirtualScreen.Left;
            dRec.top = SystemInformation.VirtualScreen.Top; 
            dRec.right = SystemInformation.VirtualScreen.Right;
            dRec.bottom = SystemInformation.VirtualScreen.Bottom - 25;// We reserve the 25 pixels on bottom for our taskbar
            Winapi.SystemParametersInfo((int)WorkArea.Set, 0, ref dRec, 0);
        }

        public static void OldWorkingArea()
        {
            Winapi.SystemParametersInfo((int)WorkArea.Set, 0, ref DefaultWindowsWorkingArea, 0);
        }
    }
}
