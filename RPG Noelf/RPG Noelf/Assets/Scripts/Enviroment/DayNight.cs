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
            MainPage.instance.dayText.Text = "Day " + days + " - " + hours + ":" + minutes;
            dispatcherTimer.Tick += Timer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1/*86400 / (60 * DayLenght)*/);
            dispatcherTimer.Start();
        }

        private void Timer(object sender, object e)
        {
            string h, m;
            TickATime();
            if (hours < 10)
            {
                h = "0" + hours;
            }
            else h = hours.ToString();
            if (minutes < 10)
            {
                m = "0" + minutes;
            }
            else m = minutes.ToString();
            MainPage.instance.dayText.Text = "Day " + days + " - " + h + ":" + m;
        }

        private void TickATime()
        {
            if(minutes + 1 < 60)
            {
                minutes++;
            } else
            {
                hours++;
                minutes = 0;
                if(hours >= 24)
                {
                    hours = 0;
                    days++;
                }
            }
        }

    }
}
