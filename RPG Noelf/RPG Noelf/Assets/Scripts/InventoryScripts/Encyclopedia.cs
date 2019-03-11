using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    static class Encyclopedia
    {
        public static Dictionary<int, Item> encyclopedia;

        public static bool UpdateEncyclopedia(Bag bag)
        {
            
            

        }

        public static Item searchFor(int key)
        {
            Item generic;
            generic = encyclopedia[key];
            return generic;

        }

        
    }
}
