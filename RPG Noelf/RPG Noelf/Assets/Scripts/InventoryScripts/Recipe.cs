
using RPG_Noelf.Assets.Images.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class Recipe
    {
        public Item craftedItem { get; }
        public Dictionary<string,Item> materials;

        public Recipe(Item craft)
        {
            craftedItem = craft;

        }

    }

    
}


