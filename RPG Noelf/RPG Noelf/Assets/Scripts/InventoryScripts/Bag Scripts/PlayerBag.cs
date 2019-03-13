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

        public PlayerBag()
        {
            Slots = new List<Slot>();
        }

        public override void AddGold(int coins)
        {
            if(Gold <= 9999999)
            {
                Gold += coins;
            }
        }

        public override bool AddToBag(uint itemID, uint amount)
        {
            Slot playerSlot = Slots.Find(x => x.ItemID == itemID && x.ItemAmount < MaxStack);
            if (Slots.Count < MaxSlots)
            {
                if(playerSlot != null)
                {
                    if(playerSlot.ItemAmount + amount > MaxStack)
                    {
                        uint offset = playerSlot.ItemAmount + amount - MaxStack;
                        playerSlot.ItemAmount += offset;
                        Slot slot = new Slot(itemID, amount - offset);
                        if(Slots.Count < MaxSlots)
                        {
                            Slots.Add(slot);
                            return true;
                        } else
                        {
                            Drop(); // Dropa o restante que não cabe mais na mochila
                            return false;
                        }
                    } else
                    {
                        playerSlot.ItemAmount += amount;
                        return true;
                    }
                } else
                {
                    Slots.Add(new Slot(itemID, amount));
                    return true;
                }
            }
            else return false;
        }

        public Slot GetSlot(int index)
        {
            if (index < Slots.Count)
            {
                return (Slot)Slots[index];
            }
            return null;
        }

        public Slot GetSlot(uint itemID)
        {
            return (Slot)Slots.Find(x => x.ItemID == itemID);
        }

        public uint GetSlotItemID(int index)
        {
            if(index < Slots.Count)
            {
                return Slots[index].ItemID;
            }
            return 0;
        }

        public void DropFromBag(int index)
        {
            if(index < Slots.Count)
            {
                Slots.RemoveAt(index);
            }
        }

        public void Drop()
        {

        }
    }
}
