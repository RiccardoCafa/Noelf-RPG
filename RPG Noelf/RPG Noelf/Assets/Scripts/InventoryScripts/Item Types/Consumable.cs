using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{

    class Consumable : Item
    {
        public float Bonus { get; set; }

        public Consumable(string name) :
                            base(name)
        { 
            IsStackable = true;

        }


        public double returnBonus()//retorna o valor do bonus com base na quantidade
        {
            //TODO bonus consumable
            return 0;
        }

        public bool canUse()//retorna se pode usar ou não
        {
            //TODO can use consumable
            return true;
        }

        public void afterUsed(int nTimes)//decrementa quando usado um numero n de vezes
        {
            //TODO after used consumable
        }


    }

    


}
    