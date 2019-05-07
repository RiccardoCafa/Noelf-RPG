using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{
    class CraftingEncyclopedia
    {
        public static Dictionary<uint, Recipe> CraftItems;

        public static void LoadCraftings()
        {
            Recipe IronIngot = new Recipe("Iron Ingot");
            IronIngot.Add(new Slot(1, 5));
            CraftItems.Add(42, IronIngot);
            



        }

        public static bool HaveRecipe(uint item)
        {
            if (CraftItems.ContainsKey(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
