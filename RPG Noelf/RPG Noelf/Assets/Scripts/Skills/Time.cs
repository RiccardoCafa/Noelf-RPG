using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class Time//Classe que faz o CD e o tempo de duracao dos buffs
    {
        List<SkillGenerics> skilltime = new List<SkillGenerics>();//lista de skills que foram usadas
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private double RealTime = 0;
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

            foreach (SkillGenerics habilite in skilltime)//para verificar se as skills ja acabaram seus tempos de CD
            {
                if (habilite.CountTime >= RealTime)
                {
                    habilite.CountTime = 0;
                    habilite.Active = true;
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
                    if (habilite.CountBuffTime >= RealTime)
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
