using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class Time
    {
        //fazer isso dps
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private double tempo,tempot;
        public Time(int tempot)
        {
            this.tempot = tempot;
            DispatcherSetup();
        }
        private void DispatcherSetup()
        {
            dispatcherTimer.Tick += Timer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void Timer(object sender, object e)
        {
            tempo++;
            if(tempo == tempot)
            {
                dispatcherTimer.Stop();
            }
        }

    }
}
