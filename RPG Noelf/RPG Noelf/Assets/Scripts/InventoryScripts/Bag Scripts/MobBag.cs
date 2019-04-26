using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    class MobBag : Bag
    {
        public const uint mobCarry = 10;
        


        public override void AddGold(int coins)
        {
            //throw new NotImplementedException();
        }

        public override bool AddToBag(Slot slot)
        {
            Slot adic = new Slot(slot.ItemID, slot.ItemAmount);
            if(adic!= null && Slots.Count() < mobCarry)
            {
                Slots.Add(adic);
                return true;
            }
            else
            {
                return true;
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
                    drop.AddToBag(new Slot(idkey, amount));
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
