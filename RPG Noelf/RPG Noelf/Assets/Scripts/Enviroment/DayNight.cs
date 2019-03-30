using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RPG_Noelf.Assets.Scripts.Enviroment
{
    class DayNight
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private int days;
        private int hours, minutes;

        public int DayLenght { get; } = 20;

        public DayNight()
        {
            ResetTime();
            DispatcherSetup();
        }

        public DayNight(int DayLenght)
        {
            this.DayLenght = DayLenght;
            ResetTime();
            DispatcherSetup();
        }

        private void ResetTime()
        {
            days = 1;
            hours = 10;
            minutes = 0;
        }

        private void DispatcherSetup()
        {
            dispatcherTimer.Tick += Timer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 60 / ((86400 * DayLenght) / 1440));
            dispatcherTimer.Start();
        }

        private void Timer(object sender, object e)
        {
            TickATime();
        }

        private void TickATime()
        {
            if(minutes < 60)
            {
                minutes++;
            } else
            {
                hours++;
                minutes = 0;
                if(hours > 24)
                {
                    hours = 0;
                    days++;
                }
            }
        }

    }
}
