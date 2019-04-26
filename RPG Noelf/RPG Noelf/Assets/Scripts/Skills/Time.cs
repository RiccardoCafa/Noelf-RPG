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
        private double RealTime;
        public Time(SkillGenerics skill)
        {
            skill.CountTime = RealTime + skill.cooldown;
            skill.CountBuffTime = RealTime + skill.Timer;
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
                if (habilite.CountTime == RealTime)
                {
                    habilite.CountTime = 0;
                    habilite.Useabilite = true;
                    skilltime.Remove(habilite);
                }
                else
                {
                    RealTime++;
                }
                if (habilite.tipobuff == SkillTypeBuff.normal)
                {
                    //n faz nada
                }
                else
                {
                    if(habilite.CountBuffTime == RealTime)
                    {
                        habilite.Usetroca = true;
                        habilite.CountBuffTime = 0;
                    }
                    else
                    {
                        RealTime++;
                    }
                    RealTime++;
                }
            }
        }

    }
}
