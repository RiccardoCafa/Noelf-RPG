using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    public class Slot
    {
        public uint ItemID { get; set; }
        public uint ItemAmount { get; set; }

        public Slot(uint ItemID, uint ItemAmount)
        {
            if(!Encyclopedia.encyclopedia.ContainsKey(ItemID))
            {
                this.ItemAmount = 1;
                this.ItemID = 1;
                return;
            }
            if (ItemAmount == 0) ItemAmount = 1;
            if(Encyclopedia.encyclopedia[ItemID] is Weapon || Encyclopedia.encyclopedia[ItemID] is Armor)
            {
                ItemAmount = 1;
            }
            this.ItemAmount = ItemAmount;
            this.ItemID = ItemID;
        }

    }
}
