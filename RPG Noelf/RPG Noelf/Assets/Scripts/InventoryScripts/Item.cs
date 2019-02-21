using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Images.Inventory_Scripts
{
  
     abstract class Item
    {
        public int goldValue { get; set; } // valor em ouro do item
        public int amount { get; set; } // quantidade de um mesmo item
        public string name { get; set; } // nome do item;
        public bool isStackable;//se é possivel acumular ou não
        public int itemID// ID de indentificação dos itens
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
            }

        }
            
            
        
    }


}
