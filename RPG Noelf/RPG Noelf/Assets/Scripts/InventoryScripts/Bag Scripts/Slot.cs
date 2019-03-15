using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    class Slot
    {
        public uint ItemID { get; set; }
        public uint ItemAmount { get; set; }

        public Slot(uint ItemID, uint ItemAmount)
        {
            this.ItemAmount = ItemAmount;
            this.ItemID = ItemID;
        }

    }
}
