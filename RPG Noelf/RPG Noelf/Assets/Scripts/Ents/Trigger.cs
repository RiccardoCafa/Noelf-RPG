using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    public class Trigger
    {
        private List<Character> characterToTrigger = new List<Character>();
        public Character TriggeringCharacter;
        public Character Me;
        public bool exist = true;
        public double DistanceOffSet = 150;

        public Trigger(Character T)
        {
            Me = T;
            //new Task(() => TriggerUpdate());
        }

        public void AddTrigger(Character ToBeTriggered)
        {
            characterToTrigger.Add(ToBeTriggered);
        }

        public async void TriggerUpdate()
        {
            while(exist)
            {
                if (characterToTrigger.Count == 0) continue;
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (DispatchedHandler)(() =>
                {
                    double Nearest = -1;
                    double Valor;
                    foreach(Character c in characterToTrigger)
                    {
                        Valor = Character.GetDistance(c.xCharacVal, c.yCharacVal, Me.xCharacVal, Me.yCharacVal);
                        if (Valor <= DistanceOffSet)
                        {
                            if(Nearest == -1)
                            {
                                Nearest = Valor;
                                TriggeringCharacter = c;
                            } else
                            {
                                if(Nearest >= Valor)
                                {
                                    Nearest = Valor;
                                    TriggeringCharacter = c;
                                }
                            }
                        }
                    }
                    if (Nearest == -1) TriggeringCharacter = null;
                }));
            }
        }

        public bool Triggering()
        {
            if (characterToTrigger.Count == 0) return false;
            double Nearest = -1;
            double Valor;
            foreach (Character c in characterToTrigger)
            {
                Valor = Character.GetDistance(c.xCharacVal, c.yCharacVal, Me.xCharacVal, Me.yCharacVal);
                if (Valor <= DistanceOffSet)
                {
                    if (Nearest == -1)
                    {
                        Nearest = Valor;
                        TriggeringCharacter = c;
                    }
                    else
                    {
                        if (Nearest >= Valor)
                        {
                            Nearest = Valor;
                            TriggeringCharacter = c;
                        }
                    }
                }
            }
            if (Nearest == -1) return false;
            else return true;
        }

    }
}
