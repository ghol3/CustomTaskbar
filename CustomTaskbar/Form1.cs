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

namespace CustomTaskbar
{
    public partial class Form1 : Form
    {

        private StartButton startButton;
        private IntPtr startMenuHandle;
        private bool ThisVisible;
        private WindowManager manager;
        private Calendar calendar;
        private ToolTip tooltip;
        private int position = 0;

        private Label TimeLabel;
        private Panel TimePanel;

        private Panel ProcessPanel;

        public Form1()
        {
            InitializeComponent();
            this.tooltip = new ToolTip();
             

            this.ProcessPanel = new Panel();
            this.ProcessPanel.Size = new Size(405, 25); // 15 apps in taskbar
            this.ProcessPanel.Location = new Point(65, 0);
            this.ProcessPanel.BackColor = Color.Red;
            this.Controls.Add(this.ProcessPanel);

            this.ShowInTaskbar = false;
            this.manager = new WindowManager();
            this.manager.IconSize = new Size(25, 25);
            this.manager.CheckingProcesses += new WindowManager.ProcessEventHandler(RefreshProcesses);



            Winapi.SetWindowPos(this.Handle, IntPtr.Zero, 0, Desktop.Size.Height - 25, Desktop.Size.Width, 25, 0x0040);
            this.BackColor = Color.FromArgb(166, 221, 0);
            this.KeyDown += new KeyEventHandler(key_down);
            this.startButton = new StartButton();
            this.startMenuHandle = Winapi.GetStartMenuHandle();
            this.ThisVisible = true;
            
            AddTime();
            //HideWindowsButton();
            this.calendar = new Calendar();
            calendar.ChangeTime += new Calendar.CustomEventHandler(Change_Time);
            
        }
        private void Change_Time(object sender, CustomEventArgs e)
        {

            this.Invoke((MethodInvoker)delegate
            {
                this.TimeLabel.Text = e.Time;
            });
            
        }
        private void RefreshProcesses(object sender, ProcessEventValues e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.ProcessPanel.Controls.Clear();
                foreach (Window w in e.WindowsValues)
                {
                    Panel a = new Panel();
                    tooltip.SetToolTip(a, w.WindowName);
                    a.Name = w.WindowName;
                    a.Tag = w;
                    a.Size = manager.IconSize;
                    a.Location = new Point(position, 0);
                    a.BackgroundImage = w.Icon;
                    a.Click += new EventHandler(SetWindowTopMost);
                    this.position += 27;
                    this.ProcessPanel.Controls.Add(a);
                }
                this.position = 0;
            });
        }
        private void SetWindowTopMost(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            Window w = (Window)p.Tag;
            MessageBox.Show(w.windowState.ToString());
            if (w.windowState == ProcessWindowStyle.Minimized || w.windowState == ProcessWindowStyle.Hidden)
            {
                Winapi.ShowWindow(w.Handle, 1);
                Winapi.SetForegroundWindow(w.Handle);
                w.windowState = ProcessWindowStyle.Normal;
            }
            else if (w.windowState == ProcessWindowStyle.Normal || w.windowState == ProcessWindowStyle.Maximized)
            {
                Winapi.ShowWindow(w.Handle, 6);
                w.windowState = ProcessWindowStyle.Minimized;
            }
            
        }

        private void key_down(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                
                this.Close();
                
            }
            else if (e.KeyCode == Keys.H && e.Shift)
            {
                Winapi.WindowVisible(this.Handle, !ThisVisible);
                Winapi.TaskBarVisible(ThisVisible);
            }
            
        }

        private void StartButtonPanel_MouseMove(object sender, MouseEventArgs e)
        {
            this.StartButtonPanel.BackgroundImage = startButton.onMove;
        }

        private void StartButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            this.StartButtonPanel.BackgroundImage = startButton.Normal;
        }

        private void StartButtonPanel_Click(object sender, EventArgs e)
        {
            //Winapi.StartMenuVisible(true);
            Winapi.WindowVisible(startMenuHandle, true);
            WorkAreaRectangle w = new WorkAreaRectangle();
            Winapi.GetWindowRect(startMenuHandle, ref w);
            Size size = new Size(w.right - w.left,
                                 w.bottom - w.top);
            Winapi.SetWindowPos(startMenuHandle, IntPtr.Zero, 0, size.Height - 300, size.Width, size.Height, 0x0040);
        }
        
        private void AddTime()
        {
            this.TimePanel = new Panel();
            this.TimePanel.Name = "TimePanel";
            this.TimePanel.BackColor = Color.Red;
            this.TimePanel.Size = new Size(50, 25);
            //p.Location = new Point(Desktop.Size.Width - p.Width, 0);
            this.TimePanel.Dock = DockStyle.Right;
            this.Controls.Add(this.TimePanel);

            this.TimeLabel = new Label();
            this.TimeLabel.Text = Time.Hour_Minut;
            this.TimeLabel.Location = new Point(5, 3);
            this.TimePanel.Controls.Add(TimeLabel);
            
   
            //new Thread(new ThreadStart(this.DrawTime))
            //{
          //      Priority = ThreadPriority.Lowest
          //  }.Start();
        }
        private void HideWindowsButton()
        {
            Panel p = new Panel();
            p.Name = "HidePanel";
            p.BackColor = Color.Black;
            p.Dock = DockStyle.Right;
            p.Size = new Size(5, 25);
            p.MouseMove += new MouseEventHandler(Hide_Windows_Button_Move);
            this.Controls.Add(p);
            
        }
        private void Hide_Windows_Button_Move(object sender, MouseEventArgs e)
        {/*
            Panel p = (Panel)sender;
            foreach(Window w in manager.Windows)
            {
                Winapi.HideWindow(w.Handle);
            }*/
        }
        private delegate void TimeDelegate();

    }
    
}
