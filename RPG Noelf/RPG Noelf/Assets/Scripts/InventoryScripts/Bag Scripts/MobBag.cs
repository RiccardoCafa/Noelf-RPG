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

        public override bool AddToBag(uint itemID, uint amount)
        {
            Slot adic = new Slot(itemID, amount);
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
