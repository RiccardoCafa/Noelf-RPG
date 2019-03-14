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
        public const uint mobCarry = 10;
        


        public override void AddGold(int coins)
        {
            //throw new NotImplementedException();
        }

        public override bool AddToBag(uint itemID, uint amount)
        {
            Slot addedSlot = new Slot(itemID, amount);
            if(addedSlot != null)
            {
                if(Slots.Count() < mobCarry && Encyclopedia.SearchStackID(itemID) == false && Slots.Contains(addedSlot) == false)
                {
                    Slots.Add(addedSlot);
                    FreeSlots--;
                    return true;
                }else if (Slots.Count()<mobCarry && Encyclopedia.SearchStackID(itemID) == true && Slots.Contains(addedSlot) == true) 
                {
                    


                }






            }
           
                    
        }

        public MobBag DropItens(uint idkey, uint amount, int GoldDrop)
        {
            MobBag drop;
            MobBag genericDrop = new MobBag();
            genericDrop.AddGold(GoldDrop);
            
            foreach(MobSlot sl in Slots)
            {
                if(sl.ItemID == idkey)
                {
                    drop = new MobBag();
                    drop.AddGold(GoldDrop);
                    drop.AddToBag(idkey, amount);
                    return drop;

                }
                else
                {
                    return genericDrop;
                }
            }

            return genericDrop;
        }


        


        
    }
}
