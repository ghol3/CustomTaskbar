using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace CustomTaskbar
{
    class WindowManager
    {

        public Size IconSize { get; set; }
        public List<Window> Windows { get; set; }

        [DefaultValue(true)]
        public bool CheckProcessesThread { get; set; }

        public event ProcessEventHandler CheckingProcesses;
        public delegate void ProcessEventHandler(object sender, ProcessEventValues args);

        private int RunningWindows
        {
            get
            {
                int number = 0;
                foreach (Process p in Process.GetProcesses())
                    if (p.MainWindowTitle != "" && p.ProcessName != "explorer")
                        number++;
                return number;
            }
        }

        public WindowManager()
        {
            this.Windows = new List<Window>();
            this.IconSize = new Size(25, 25);
            new Thread(new ThreadStart(this.CheckProcesses))
            {
                Priority = ThreadPriority.Normal
            }.Start();

            RefreshProcessList();
            InvokeCheckingProcessesEvent(new ProcessEventValues()
            {
                WindowsValues = Windows
            });
        }
        private void RefreshProcessList()
        {
            this.Windows.Clear();
            foreach (Process p in Process.GetProcesses())
                if (p.MainWindowTitle != "" && p.ProcessName != "explorer")
                    this.Windows.Add(new Window()
                    {
                        Handle = p.MainWindowHandle,
                        WindowName = p.ProcessName,
                        IsMinimized = true,
                        Icon = GetIconFromProcess(p),
                        windowState = p.StartInfo.WindowStyle
                    });
        }
        private void CheckProcesses()
        {
            while (true)
            {
                if (Windows.Count != RunningWindows)
                {
                    RefreshProcessList();
                    InvokeCheckingProcessesEvent(new ProcessEventValues()
                        {
                            WindowsValues = Windows
                        });
                }
                Thread.Sleep(50);
            }
        }

        private void InvokeCheckingProcessesEvent(ProcessEventValues eventValues)
        {
            ProcessEventHandler processEvent = this.CheckingProcesses;
            if (processEvent == null)
                return;
            processEvent((object)this, eventValues);
        }

        private Bitmap GetIconFromProcess(Process p)
        {
            try { return new Bitmap(Icon.ExtractAssociatedIcon(p.MainModule.FileName).ToBitmap(), IconSize); }
            catch
            {
                Bitmap b = new Bitmap(this.IconSize.Width, this.IconSize.Height);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.Blue);
                return b;
            }
        }
    }
    public struct ProcessEventValues
    {
        public List<Window> WindowsValues { get; set; }
    }
    
    public struct Window
    {
        public IntPtr Handle { get; set; }
        public string WindowName { get; set; }
        public bool IsMinimized { get; set; }
        public Bitmap Icon { get; set; }
        public ProcessWindowStyle windowState { get; set; }
    }
}
