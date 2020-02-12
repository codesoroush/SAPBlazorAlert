using Microsoft.AspNetCore.Components;
using SAPBlazorAnimate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SAPBlazorAlert.Model
{
    public class SAPAlertBoxRazor:ComponentBase
    {
        CancellationTokenSource cts;
        /// <summary>
        /// shoundt set in outer of component :)
        /// </summary>
        internal bool Show { get; set; } = false;
        /// <summary>
        /// what alert type you want , just choose and see
        /// </summary>
        [Parameter]
        public SAPAlertType SAPAlertType { get; set; } = SAPAlertType.None;
        [Parameter]
        public string Title { get; set; }
        /// <summary>
        /// if you want see this image on top of alert , you must set SAPAlertType to Custom
        /// </summary>
        [Parameter]
        public string CustomAlertTypeImage { get; set; } = "https://github.com/codesoroush/SAPBlazorAlert/blob/master/SAPBlazorAlert/blazorlogo.png?raw=true";
        /// <summary>
        /// Set a time to close it
        /// </summary>
        [Parameter]
        public TimeSpan? AutoCloseDelay { get; set; } = null;

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        /// <summary>
        /// trigger alert to appear
        /// </summary>
        public void ShowAlert()
        {
            Show = true;
            AutoCloseHandler();

        }

        /// <summary>
        /// IMPORTANT : This feature prevent user access to close alert
        /// </summary>
        [Parameter]
        public bool PreventUserToCloseAlert { get; set; } = false;
        /// <summary>
        /// change alert box width with this prop
        /// values are : small, medium and large
        /// </summary>
        [Parameter]
        public SAPAlertBoxSize AlertBoxSize { get; set; } = SAPAlertBoxSize.Small;

        /// <summary>
        /// should't use outer of component
        /// </summary>
        internal void InterruptCloseTask()
        {
            if (cts != null)
            {
                cts.Cancel();

            }
        }
        /// <summary>
        /// delete alert box
        /// </summary>
        public void HideAlert()
        {

           
                InterruptCloseTask();
                Show = false;
            
           

        }



        internal void CloseAlert()
        {

            if (!PreventUserToCloseAlert)
            {
                InterruptCloseTask();
                Show = false;
            }

        



        }

        /// <summary>
        /// define a text that appear in bottom of alert
        /// </summary>
        [Parameter]
        public string FooterText { get; set; }
        /// <summary>
        /// <br>default animation is Zoom In , if you want set another animation just use AnimationBook class.</br>  
        /// <br>ex:@AnimateBook.ZoomEntrances.ZoomIn</br>  
        /// </summary>
        [Parameter]
        public string AnimationType { get; set; } = AnimateBook.ZoomEntrances.ZoomIn;
        async void AutoCloseHandler()
        {
            cts = new CancellationTokenSource();
            await Task.Run(() => AutoCloseTask(cts.Token));

        }


        private async void AutoCloseTask(CancellationToken token)
        {
            if (AutoCloseDelay.HasValue)
            {

                await Task.Delay(AutoCloseDelay.Value);

                try
                {
                    if (!token.IsCancellationRequested)
                    {
                        Show = false;
                        await InvokeAsync(StateHasChanged);
                    }

                    else
                    {
                        Console.WriteLine("SAPBlazorAlert : AutoCloseCanceled");

                    }

                }
                catch (Exception e)
                {

                    Console.WriteLine("SAPBlazorAlert : " + e.Message);
                }

            }
        }

     
    }
}
