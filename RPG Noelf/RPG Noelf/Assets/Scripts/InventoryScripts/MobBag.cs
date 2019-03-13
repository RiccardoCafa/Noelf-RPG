using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class MobBag : Bag
    {
       
        public List<MobSlot> itens;

        public override bool AddGold(int coins)
        {
            throw new NotImplementedException();
        }
        public override bool AddToBag(uint itemID, uint amount)
        {
            throw new NotImplementedException();
        }
        public override SlotInventory DropFromBag(uint itemID, uint amount)
        {
            SlotInventory saquinho;
            foreach(MobSlot slots in itens)
            {
                if(slots.itemID == itemID && slots.itemAmount >= amount && slots.Dropou() == true)
                {
                    if(slots.itemAmount - amount == 0)
                    {

                    }
                }
            }
            

        }
    }
}
