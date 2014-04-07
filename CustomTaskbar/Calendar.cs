using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace CustomTaskbar
{
    class Calendar
    {
        public event CustomEventHandler ChangeTime;
        public delegate void CustomEventHandler(object sender, CustomEventArgs args);

        [DefaultValue(true)]
        public bool checkTime { get; set; }
        private string timeNow;

        public Calendar()
        {
            this.checkTime = true;
            this.timeNow = Time.Hour_Minut;
            new Thread(new ThreadStart(CheckChangening))
            {
                Priority = ThreadPriority.Lowest
            }.Start();
            InvokeChangeTime(new CustomEventArgs() { Time = Time.Hour_Minut });
            timeNow = Time.Hour_Minut;
        }
        private void CheckChangening()
        {
            while (checkTime)
            {
                if (timeNow != Time.Hour_Minut)
                {
                    InvokeChangeTime(new CustomEventArgs() { Time = Time.Hour_Minut });
                    timeNow = Time.Hour_Minut;
                }
                Thread.Sleep(15000);
            }
        }
        private void InvokeChangeTime(CustomEventArgs e)
        {
            CustomEventHandler handler = ChangeTime;
            if (handler == null)
                return;
            handler((object)this, e);
        }

    }

    class CustomEventArgs : EventArgs
    {
        public string Time { get; set; }
    }
    struct Time
    {
        public static string Day { get { return DateTime.Now.Day.ToString(); } }
        public static string Hour { get { return DateTime.Now.Hour.ToString(); } }
        public static string Minute { get { return DateTime.Now.Minute.ToString(); } }
        public static string Seconds { get { return DateTime.Now.Second.ToString(); } }

        public static string Day_MonthName_Year { get { return DateTime.Now.ToLongDateString().ToString(); } }
        public static string Hour_Minut_Second { get { return DateTime.Now.ToLongTimeString().ToString(); } }

        public static string Hour_Minut { get { return DateTime.Now.ToShortTimeString(); } }
        public static string Day_Month_Year { get { return DateTime.Now.ToShortDateString(); } }
    }
}
