using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class SlotInventory
    {
        public const uint maxStack = 99;
        public uint itemID { get; set; }
        public uint itemAmount { get; set; }

        public SlotInventory(uint id, byte amount)
        {
            itemID = id;
            itemAmount = amount;
        }

        // incrementar o monte e retornar o que sobrou
       public uint increaseAmount(uint ex)
        {
            if((itemAmount+ex) >= maxStack)
            {
                return itemAmount+ ex - maxStack;
            }
            else
            {
                itemAmount = itemAmount + ex;
                return 0;
            }
        }

        //retorna a quantidade final apos decrescer
        public uint decreaseAmount(uint amt)
        {
            if((itemAmount - amt)<= 0)
            {
                itemAmount = 0;
                itemID = 0;
                return 0;
            }
            else
            {
                itemAmount = itemAmount - amt;
                return 0;
            }
        }

        public bool stackableID(uint id)
        {


        }

    }
}
