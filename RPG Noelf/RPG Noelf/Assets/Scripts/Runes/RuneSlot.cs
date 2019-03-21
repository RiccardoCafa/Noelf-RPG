using RPG_Noelf.Assets.Scripts.Runes;
using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class RuneSlot
    {
        public bool ableToThis { get; set; }
        public bool active { get; set; }
        public Rune equipedRune { get; set; }

        public RuneSlot(bool able)
        {
            ableToThis = able;
            active = false;
        }


        //setar a runa a partir de uma runa nova
        public void SetRune(Rune rune)
        {
           if(ableToThis == true)
            {
                try
                {
                    equipedRune = rune;
                    active = true;
                }
                catch (NullReferenceException e)
                {
                    active = false;
                    throw e;
                }

            }
                      
        }

        //verificar se a runa está ativa no slot
        public bool CheckRuneWorking()
        {
            if(active == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
