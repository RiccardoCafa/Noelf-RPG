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
        List<SkillGenerics> skilltime = new List<SkillGenerics>();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Time(SkillGenerics skill)
        {
            skilltime.Add(skill);
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
            foreach (SkillGenerics habilite in skilltime)
            {
                double cool = habilite.cooldown;
                if (cool == 0)
                {
                    habilite.cooldown = cool;
                    habilite.Useabilite = true;
                    skilltime.Remove(habilite);
                }
                else
                {
                    cool--;
                }
                if (habilite.tipobuff == SkillTypeBuff.normal)
                {
                    //n faz nada
                }
                else
                {
                    double efect = habilite.Timer;
                    if(efect == 0)
                    {
                        habilite.Usetroca = true;
                        habilite.Timer = efect;
                    }
                    else
                    {
                        efect--;
                    }
                }
            }
        }

    }
}
