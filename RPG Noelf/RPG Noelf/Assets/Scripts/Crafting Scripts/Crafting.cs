using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System.Linq;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{

    public class Crafting
    {
        Bag bag;

        public Crafting()
        {
            bag = GameManager.player._Inventory;
        }

        private bool IsPossibleToCraft(uint item)
        {
            Recipe generic = CraftingEncyclopedia.CraftItems[item];
            var material = (from Slot s in bag.Slots
                            join m in generic.ListaMaterial on s.ItemID equals m.ItemID
                            where s.ItemAmount >= m.ItemAmount
                            select s).ToList<Slot>();
            return material.Count > 0;

        }       
        private void RemoveMaterials(uint id)
        {
            if (IsPossibleToCraft(id))
            {
                Recipe generic = CraftingEncyclopedia.CraftItems[id];
                foreach (Slot s in generic.ListaMaterial)
                {
                    Slot ps = bag.GetSlot(s.ItemID);
                    if (ps == null) continue;
                    if (ps.ItemID == s.ItemID && ps.ItemAmount >= s.ItemAmount)
                    {
                        bool a = bag.RemoveFromBag(s.ItemID, s.ItemAmount);
                    }
                }
            }
        }
        public bool CraftItem(uint itemID)
        {
            Slot NewItem;
            if (IsPossibleToCraft(itemID))
            {
                NewItem = new Slot(itemID, 1);
                RemoveMaterials(itemID);
                bag.AddToBag(NewItem);
                return true;
            }
            return false;
        }
    }
}
