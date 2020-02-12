using Microsoft.AspNetCore.Components;
using SAPBlazorAnimate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SAPBlazorAlert.Model
{
    public class SAPToastAreaRazor : ComponentBase
    {
        CancellationTokenSource cts;
        /// <summary>
        /// default animation : AnimateBook.Specials.JackInTheBox
        /// for change animation use this class : @AnimateBook
        /// </summary>
        [Parameter]
        public string AnimationType { get; set; } = AnimateBook.Specials.JackInTheBox;
        [Parameter]
        public SAPToastAreatPosition ToastAreatPosition { get; set; } = SAPToastAreatPosition.Right;
        /// <summary>
        /// tip : row direction doesnt work with center toast area position
        /// </summary>
        [Parameter]
        public SAPToastDirection SAPToastDirection { get; set; } = SAPToastDirection.Column;
        internal string AreaDirection => SAPToastDirection == SAPToastDirection.Column ? "sap-toast-column" : "sap-toast-row";
        internal string AreaPosition => ToastAreatPosition == SAPToastAreatPosition.Center ? "sap-toast-area-center" : ToastAreatPosition == SAPToastAreatPosition.Left ? "sap-toast-area-left" : "sap-toast-area-right";
        internal List<SAPToast> Toasts { get; set; } = new List<SAPToast>();

        public void CreateToast(SAPToast toast)
        {
            toast.ShowAnimate = AnimationType;
            toast.SAPToastArea = this;
            Toasts.Add(toast);
            if (toast.ShowProgressbar)
            {
                toast.StartCountDown();

            }
            else
            {
                AutoCloseHandler(toast);
            }
            
            StateHasChanged();
        }

        private async void AutoCloseHandler(SAPToast toast)
        {
            cts = new CancellationTokenSource();

            await Task.Run(() => AutoCloseTask(cts.Token, toast));
        }
        internal void DeleteToast(SAPToast toast)
        {
            toast.CloseAnimate = AnimateBook.ZoomExits.ZoomOut;
            InvokeAsync(StateHasChanged);
            Task.Delay(300);

            if (toast.ShowProgressbar)
            {
                toast.StopTimer();
            }
            toast.ShowAnimate = "";
  
            Toasts.Remove(toast);
            if (cts != null)
            {
                cts.Cancel();
            }
            InvokeAsync(StateHasChanged);
            
        }

        internal async void CallStateHasChanged()
        {
            await InvokeAsync(StateHasChanged);

        }

        internal void ToastCloseController(SAPToast toast,bool isPaused)
        {
            if (isPaused)
            {
                toast.PauseTimer();

            }
            else
            {
                toast.ResumeTimer();

            }
        }
        private async void AutoCloseTask(CancellationToken token, SAPToast toast)
        {


            try
            {


                await Task.Delay(500);
                toast.ShowAnimate = "";
                await InvokeAsync(StateHasChanged);
                await Task.Delay(toast.AutoCloseDelay - TimeSpan.FromSeconds(0.5));
                toast.CloseAnimate = AnimateBook.ZoomExits.ZoomOut;
                await InvokeAsync(StateHasChanged);
                await Task.Delay(300);

                if (!token.IsCancellationRequested)
                {
                    Toasts.Remove(toast);
                    await InvokeAsync(StateHasChanged);
                }

            }
            catch (Exception e)
            {

            }


        }
    }
}
