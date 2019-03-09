using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class Consumable:Item 
    {
        public float giveBonus { get; set; }

        public Consumable(int goldValue, int amount, string name, bool isStackable, Category categoria, string itemID, string pathImage) :
                            base(goldValue, amount, name, isStackable, categoria, itemID, pathImage)
        {

            isStackable = true;
        }


        public float returnBonus()
        {
            if(amount == 0)
            {
                return 0;
            }
            else
            {
                return giveBonus;
            }

        }

        public bool canUse()
        {
            if(amount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
       
        public void afterUsed(int nTimes)
        {
            if (nTimes > amount)
            {
                amount = 0;
            }
            else
            {
               amount =  amount - nTimes;
               
            }
        }



    }
}
