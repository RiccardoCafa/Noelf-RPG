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

        public Consumable(int goldValue, int amount, string name, bool isStackable, Category categoria, int itemID, string pathImage) :
                            base(goldValue, amount, name, isStackable, categoria, itemID, pathImage)
        {
            itemType = "Consumable";
            isStackable = true;
        }


        public float returnBonus()//retorna o valor do bonus com base na quantidade
        {
            if(amount == 0)
            {
                return 0;
            }
            else
            {
                return giveBonus*amount;
            }

        }

        public bool canUse()//retorna se pode usar ou não
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
       
        public void afterUsed(int nTimes)//decrementa quando usado um numero n de vezes
        {
            if (nTimes > amount)
            {
                nTimes = amount;
                amount = amount - amount;
            }
            else
            {
               amount =  amount - nTimes;
               
            }
        }



    }
}
