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
        

        public Slot SmithingCraft(Bag PlayerBag, uint desiredID)
        {
            Recipe itemRecipe;
            Slot NewItem;
            Item generic; 
            if(CraftingEncyclopedia.CheckIDRecipe(desiredID) == true)
            {
                itemRecipe = CraftingEncyclopedia.PushRecipe(desiredID);
                generic = Encyclopedia.SearchFor(desiredID);
                if(generic is Weapon|| generic is Armor)
                {
                    if(CraftingEncyclopedia.CheckMaterialsFromRecipe(desiredID,PlayerBag) == true)
                    {
                        NewItem = new Slot(desiredID, 1);
                        return NewItem;
                    }


                }
              return null;
            }
            return null;
        }

        public Slot AlchemyCraft(Bag PlayerBag, uint desiredID)
        {

            Recipe itemRecipe;
            Slot NewItem;
            Item generic;
            if (CraftingEncyclopedia.CheckIDRecipe(desiredID) == true)
            {
                itemRecipe = CraftingEncyclopedia.PushRecipe(desiredID);
                generic = Encyclopedia.SearchFor(desiredID);
                if (generic is Consumable)
                {
                    if (CraftingEncyclopedia.CheckMaterialsFromRecipe(desiredID, PlayerBag) == true)
                    {
                        NewItem = new Slot(desiredID, 1);
                        return NewItem;
                    }


                }
                return null;
            }
            return null;

        }

        public List<Slot> DestroyItem(uint ItemId)
        {
          List<Slot> Material;
          Recipe generic = CraftingEncyclopedia.PushRecipe(ItemId);
            if(generic == null)
            {
                Slot a = new Slot(ItemId, 1);
                Material = new List<Slot>();
                Material.Add(a);
                return Material;
            }
            else
            {
                Material = generic.Materials;
                return Material;
            }
        }



        
    }
}
