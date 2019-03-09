using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    public enum Category
    {
        Normal,Magical,Epic, Legendary
    }

  
    class Item
    {
        
        public int goldValue { get; set; } // valor em ouro do item
        public int amount { get; set; } // quantidade de um mesmo item
        public string name { get; set; } // nome do item;
        public bool isStackable;//se é possivel acumular ou não
        public Category itemCategory { get; set; }
        public string itemID { get; set; }// ID de indentificação dos itens
        public string pathImage { get; set; }

        public Item(int goldValue, int amount, string name, bool isStackable, Category categoria, string itemID, string pathImage)
        {
            this.goldValue = goldValue;
            this.amount = amount;
            this.name = name;
            this.isStackable = isStackable;
            itemCategory = categoria;
            this.itemID = itemID;
            this.pathImage = pathImage;
        }
        
    }
}
