using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;

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

            return true;



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
