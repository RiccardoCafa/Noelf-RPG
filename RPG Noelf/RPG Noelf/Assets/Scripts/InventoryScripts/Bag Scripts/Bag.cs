using RPG_Noelf.Assets.Scripts.Crafting_Scripts;
using RPG_Noelf.Assets.Scripts.InventoryScripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    abstract class Bag
    {
        public List<Slot> Slots;

        public int FreeSlots { get; set; }
        public int MaxSlots { get; set; }
        public int Gold { get; set; }

        public const uint MaxStack = 9999;

        public abstract bool AddToBag(uint itemID, uint amount);
        public abstract void AddGold(int coins); //adicionar gold na mochila

        public bool CanAddMore()
        {
            if (FreeSlots > 0) return true;
            else return false;
        }

        public uint AmountInBag(uint itemID)
        {
            uint total = 0;
            var itensEncontrados = from item in Slots
                                   where item.ItemID == itemID
                                   select item;
            foreach (MobSlot ps in itensEncontrados)
            {
                total += ps.ItemAmount;
            }
            return total;
        }

    }
}
