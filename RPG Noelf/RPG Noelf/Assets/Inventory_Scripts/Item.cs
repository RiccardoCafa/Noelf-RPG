using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Inventory_Scripts
{
   abstract class Item
    {
        public int goldValue { get; set; } // valor em ouro do item
        public int amount { get; set; } // quantidade de um mesmo item
        public string name { get; set; } // nome do item;
        public int itemID { get; set; } // id do item para indentificação
        // slots para runas
        public bool isStackable { get; }
    }
}
