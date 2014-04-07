using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CustomTaskbar
{
    public partial class Form2 : Form
    {
        public Size IconSize = new Size(25, 25);
        List<Window> processlist = new List<Window>();
        List<Window> windowslist = new List<Window>();

        private IntPtr h;
        public int getrunprocesses
        {
            get
            {
                int i = 0;
                foreach (Process p in Process.GetProcesses())
                    if (p.MainWindowTitle != "" && p.ProcessName != "explorer" && p.Handle != h)
                        i++;

                return i;
            }
        }
        public int getopenedwindows
        {
            get
            {
                int i = 0;
                foreach (Process p in Process.GetProcesses())
                    if (!String.IsNullOrEmpty(p.MainWindowTitle))
                        i++;
                return i;
            }
        }

        public Form2()
        {
            InitializeComponent();
            this.h = this.Handle;
            this.listView1.LargeImageList = this.imageList1;
            this.listView2.LargeImageList = this.imageList2;
            RefreshList();
            new Thread(new ThreadStart(CheckWindows))
            {
                Priority = ThreadPriority.Normal
            }.Start();
            new Thread(new ThreadStart(CheckOpenedWindows))
            {
                Priority = ThreadPriority.Normal
            }.Start();
        }
        private void CheckOpenedWindows()
        {
            while (true)
            {
                if (processlist.Count != getopenedwindows)
                {
                    MessageBox.Show("true");
                    RefreshWindowsList();
                }
                Thread.Sleep(50);

            }
        }
        private void CheckWindows()
        {
            while (true)
            {
                if (processlist.Count != getrunprocesses)
                {
                    MessageBox.Show("true");
                    RefreshList();
                }
                Thread.Sleep(50);

            }
        }
        private void RefreshList()
        {
            processlist.Clear();
            foreach (Process p in Process.GetProcesses())
                if (p.MainWindowTitle != "" && p.ProcessName != "explorer" && p.Handle != h)
                    this.processlist.Add(new Window()
                    {
                        Handle = p.MainWindowHandle,
                        WindowName = p.ProcessName,
                        IsMinimized = true,
                        Icon = GetIconFromProcess(p)
                    });
            ReDraw();
        }
        private void RefreshWindowsList()
        {
            windowslist.Clear();
            foreach (Process p in Process.GetProcesses())
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                    this.windowslist.Add(new Window()
                    {
                        Handle = p.MainWindowHandle,
                        WindowName = p.ProcessName,
                        IsMinimized = true,
                        Icon = GetIconFromProcess(p)
                    });
            ReDrawWindows();
        }
        private void ReDrawWindows()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Mydeleg(delegate()
                {
                    this.listView2.Items.Clear();
                    int i = 0;
                    foreach (Window w in processlist)
                    {
                        imageList2.Images.Add(w.Handle.ToString(), w.Icon);
                        this.listView2.Items.Add(w.Handle.ToString(), w.WindowName, w.Handle.ToString());
                        i++;
                    }
                }));
            }
        }
        private delegate void Mydeleg();
        private void ReDraw()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Mydeleg(delegate()
                {
                    this.listView1.Items.Clear();
                    int i = 0;
                    foreach (Window w in processlist)
                    {
                        imageList1.Images.Add(w.Handle.ToString(), w.Icon);
                        this.listView1.Items.Add(w.Handle.ToString(), w.WindowName, w.Handle.ToString());
                        i++;
                    }
                }));
            }
           

        }
        private Bitmap GetIconFromProcess(Process p)
        {
            try
            {
                return new Bitmap(Icon.ExtractAssociatedIcon(p.MainModule.FileName).ToBitmap(), IconSize);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("loading icon is not possible");
                Bitmap b = new Bitmap(this.IconSize.Width, this.IconSize.Height);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.Red);
                return b;
            }
        }


    }

}
