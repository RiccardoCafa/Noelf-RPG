using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class PlayerBag : Bag
    {
        public override bool AddGold(int coins)
        {
            return false;
            //throw new NotImplementedException();
        }

        public override bool AddToBag(uint itemID, uint amount)
        {
            return false;
            //throw new NotImplementedException();
        }

        public override PlayerBag DropFromBag(uint itemID, uint amount)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
