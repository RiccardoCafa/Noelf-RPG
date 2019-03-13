using RPG_Noelf.Assets.Scripts.Crafting_Scripts;
using RPG_Noelf.Assets.Scripts.InventoryScripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    abstract class Bag
    {
       
        public int freeSlots { get; set; } 
        public int Gold { get; set; } 
       
 
        public abstract bool AddToBag(uint itemID, uint amount);
        public abstract SlotInventory DropFromBag(uint itemID, uint amount);//remove o item da bag

        public abstract bool AddGold(int coins); //adicionar gold na mochila
        
        public uint AmountInBag(uint itemID) //retorna a quantidade de um item especifico na mochila
        {
           

            foreach(SlotInventory sl in slots)
            {
                if (sl.itemID == itemID ) return sl.itemAmount;
            }

            return 0;
        }

        public bool CanAddMore()
        {
            if (this.freeSlots > 0) return true;
            else return false;
        }

    }
}
