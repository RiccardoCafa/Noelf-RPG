using System;
using System.Collections.Generic;
using Windows.UI.Core;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    public class Trigger
    {
        private List<Solid> characterToTrigger = new List<Solid>();
        public Solid TriggeringCharacter;
        public Solid Me;
        public bool exist = true;
        public double DistanceOffSet = 150;

        public Trigger(Solid T)
        {
            Me = T;
        }

        public void AddTrigger(Solid ToBeTriggered)
        {
            characterToTrigger.Add(ToBeTriggered);
        }

        public bool Triggering()
        {
            if (characterToTrigger.Count == 0) return false;
            double Nearest = -1;
            double Valor;
            foreach (Solid c in characterToTrigger)
            {
                if (c == null) return false;
                Valor = c.GetDistance(Me.Xi + Me.Width/2, Me.Yi + Me.Height/2);
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
