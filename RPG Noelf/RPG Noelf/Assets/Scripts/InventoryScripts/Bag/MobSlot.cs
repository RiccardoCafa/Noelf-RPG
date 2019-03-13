using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class MobSlot: Bag
    {
        public double chanceDrop { get; set; }
        public Random dr0p = new Random();

        
        public MobSlot(uint IDkey, uint amount, double chanceDrop)
        {
            this.chanceDrop = chanceDrop;
        }

        public bool Dropou()
        {
           if(dr0p.NextDouble() <= chanceDrop)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool AddToBag(uint itemID, uint amount)
        {
            throw new NotImplementedException();
        }

        public override PlayerBag DropFromBag(uint itemID, uint amount)
        {
            throw new NotImplementedException();
        }

        public override bool AddGold(int coins)
        {
            throw new NotImplementedException();
        }
    }
}
