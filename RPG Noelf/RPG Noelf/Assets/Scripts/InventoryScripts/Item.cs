using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{

    public enum Category//definição de raridade do item
    {
        Normal, Uncommon, Epic, Legendary
    }

  
    class Item
    {
        public string description { get; set; }
        public double dropRating { get; set; }
        public int GoldValue { get; set; } // valor em ouro do item
        public string Name { get; set; } // nome do item;
        public bool IsStackable;//se é possivel acumular ou não
        public Category ItemCategory { get; set; }//raridade do item
        public uint ItemID { get; set; }// ID de indentificação dos itens
        public string PathImage { get; set; }

        public Item(string name)
        {
            this.Name = name;
        }

        public string GetTypeString()
        {
            switch(ItemCategory)
            {
                case Category.Normal:
                    return "Normal";
                case Category.Legendary:
                    return "Legendary";
                case Category.Uncommon:
                    return "Uncommon";
                case Category.Epic:
                    return "Epic";
            }
            return "";
        }
        
    }
}
