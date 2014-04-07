using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CustomTaskbar
{
    static class Winapi
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref WorkAreaRectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam,
                ref WorkAreaRectangle pvParam, uint fWinIni);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
                int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        public static void TaskBarVisible(bool visible)
        {
            IntPtr taskBarHandle = FindWindow("Shell_TrayWnd", null);
            IntPtr startWnd = FindWindowEx(taskBarHandle, IntPtr.Zero, "Button", "Start");     // Find start button 
            if (startWnd == IntPtr.Zero)
                startWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, "Start");     // If we dont find start button we try alternative way (because on my pc dont work firs way)
            ShowWindow(taskBarHandle, visible ? 1 : 0);
            ShowWindow(startWnd, visible ? 1 : 0);
        }

        public static void StartMenuVisible(bool visible)
        {
            IntPtr startmenu = FindWindow("DV2ControlHost", null);
            ShowWindow(startmenu, visible ? 1 : 0);
        }
        public static IntPtr GetStartMenuHandle()
        {
            return FindWindow("DV2ControlHost", null);
        }
        public static void WindowVisible(IntPtr handle, bool visible)
        {
            ShowWindow(handle, visible ? 1 : 0);
        }
        public static void HideWindow(IntPtr handle)
        {
            ShowWindow(handle, 0);
        }
    }
    public enum WorkArea : int
    {
        Set = 0x002F,
        Get = 0x0030
    }
    public struct WorkAreaRectangle
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    public enum WindowShowStyle : int
    {
        Hide = 0,
        ShowNormal = 1,
        ShowMinimized = 2,
        ShowMaximized = 3,
        Maximize = 3,
        ShowNormalNoActivate = 4,
        Show = 5,
        Minimize = 6,
        ShowMinNoActivate = 7,
        ShowNoActivate = 8,
        Restore = 9,
        ShowDefault = 10,
        ForceMinimized = 11
    }
}
