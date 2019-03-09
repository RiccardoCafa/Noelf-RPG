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
        public static  Dictionary<string,Item> store;

        public static void InsertForSearch(Bag playerBag)
        {
            foreach(Item i in playerBag.slots)
            {
                store.Add(i.itemID, i);
            }
        }

        public static Item searchForItem(string key)
        {
            Item item;
            item = store[key];
            return item;
        }


    }
}
