using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{

    class Crafting
    {
        public bool IsPossibleToCraft(uint item, Bag playerBag)
        {
            int count = 0;
            if(CraftingEncyclopedia.HaveRecipe(item) == true)
            {
                Recipe generic = CraftingEncyclopedia.CraftItems[item];
                foreach(Slot s in generic.ListaMaterial)
                {
                    if (playerBag.Slots.Contains(s))
                    {
                        count = count + 1;
                    }
                }
                if(count == generic.NumMaterials)
                {
                    return true;
                }
                return false;
            }
            return false;
        }       

        public void RemoveMaterials(uint id, Bag bag)
        {
            if (IsPossibleToCraft(id, bag))
            {
                Recipe generic = CraftingEncyclopedia.CraftItems[id];
                foreach (Slot s in generic.ListaMaterial)
                {
                    if (bag.Slots.Contains(s))
                    {
                        bag.Slots.Remove(s);
                    }
                }
            }
        }
        
        public Slot CraftItem(uint itemID, Bag bag)
        {
            Slot NewItem;
            if (IsPossibleToCraft(itemID, bag))
            {
                NewItem = new Slot(itemID, 1);
                RemoveMaterials(itemID, bag);
                return NewItem;
            }
            Slot carvao = new Slot(2, 1);
            return carvao;
           
        }
        







        
    }
}
