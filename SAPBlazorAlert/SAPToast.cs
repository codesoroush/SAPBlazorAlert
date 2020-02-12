using Microsoft.AspNetCore.Components;
using SAPBlazorAlert.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAPBlazorAlert
{
    public class SAPToast
    {


        private System.Timers.Timer timer1;

        internal SAPToastAreaRazor SAPToastArea { get; set; } = null;
        private double RemainSec { get; set; }
        public Guid ToastId => Guid.NewGuid();
        internal string CloseAnimate { get; set; }
        internal string ShowAnimate { get; set; }
        internal string RemainTimePercent { get; set; }
        /// <summary>
        /// by default progressbar show is false,
        /// this progress bar is at bottom of toast like a border
        /// this feature use your server resources 
        /// </summary>
        [Parameter]
        public bool ShowProgressbar { get; set; } = false;

        /// <summary>
        /// text to show in toast
        /// </summary>
        public string Title { get; set; }
        public SAPAlertType AlertType { get; set; } = SAPAlertType.None;
        /// <summary>
        /// default time is 5 sec , min value must be 1 sec
        /// </summary>
        public TimeSpan AutoCloseDelay { get; set; } = TimeSpan.FromSeconds(5);
        /// <summary>
        /// this image shown when alertType set to custom
        /// </summary>
        [Parameter]
        public string CustomToastImage { get; set; } = "https://github.com/codesoroush/SAPBlazorAlert/blob/master/SAPBlazorAlert/blazorlogo.png?raw=true"; internal List<SAPToast> Toasts { get; set; } = new List<SAPToast>();

  

        internal void StartCountDown()
        {
            timer1 = new System.Timers.Timer();
            timer1.Elapsed += Timer1_Elapsed;
            timer1.Interval = 50; // 0.1 second
            timer1.Start();
            RemainSec = Convert.ToInt32(AutoCloseDelay.TotalSeconds);
            RemainTimePercent = "100%";

        }

        internal void StopTimer()
        {
            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();

            }

        }

        internal void PauseTimer()
        {
            if (timer1 != null)
            {
                timer1.Stop();

            }

        }

        internal void ResumeTimer()
        {
            if (timer1 != null)
            {
                timer1.Start();

            }

        }
        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RemainSec -= 0.05;
            if (RemainSec < 0.05)
            {
                if (timer1 != null)
                {
                    timer1.Stop();
                    timer1.Dispose();
                    SAPToastArea.DeleteToast(this);

                }


            }

            RemainTimePercent = (RemainSec/AutoCloseDelay.TotalSeconds * 100).ToString("N0") + "%";
            if (SAPToastArea != null)
            {
                SAPToastArea.CallStateHasChanged();
            }
        }
    }
}
