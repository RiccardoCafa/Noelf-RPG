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
        public Dictionary<uint, Recipe> crafting { get; set; }

        public static void LoadCrafting()
        {
            Recipe rec1 = new Recipe("Blueprint: Iron Sword", 2)
            {
                generatedID = 13,
                

            };





        }

        public static bool CanCraft()



    }
}
