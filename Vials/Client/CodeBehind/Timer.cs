using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.CodeBehind
{
    public class Timer : VialComponentBase
    {
        private System.Timers.Timer _timer;
        private DateTime Start = default;
        
        public void StartTimer()
        {
            Start = DateTime.Now;
            _timer = new System.Timers.Timer(100);
            _timer.Elapsed += TimerElapsed;
            _timer.Enabled = true;
        }

        private void TimerElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("tick");
            StateHasChanged();
        }

        protected string GetTimeString()
        {
            var elapsed = DateTime.Now - Start;

            return $"{elapsed.Hours.ToString("00")}:{elapsed.Minutes.ToString("00")}:{elapsed.Seconds.ToString("00")}";
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                StartTimer();
            }
            
            base.OnAfterRender(firstRender);
        }
    }
}
