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

        public override void AddGold(int coins)
        {
            //throw new NotImplementedException();
        }
        public override bool AddToBag(uint itemID, uint amount)
        {
            //throw new NotImplementedException();
            return false;
        }

        
    }
}
