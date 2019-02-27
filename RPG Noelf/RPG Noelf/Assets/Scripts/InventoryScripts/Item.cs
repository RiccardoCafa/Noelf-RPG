using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Images.Inventory_Scripts
{

        public enum Category//definição de raridade do item
        {
            Normal,Magical,Epic, Legendary
        }

  
      class Item//item não precisa ser abstrato
    {
        
        public int goldValue { get; set; } // valor em ouro do item
        public int amount { get; set; } // quantidade de um mesmo item
        public string name { get; set; } // nome do item;
        public bool isStackable;//se é possivel acumular ou não
        public Category itemCategory { get; set; }//raridade do item
        public string itemID { get; set; }// ID de indentificação dos itens
        
    }


}
