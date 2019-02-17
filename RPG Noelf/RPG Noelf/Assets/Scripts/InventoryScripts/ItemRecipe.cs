using RPG_Noelf.Assets.Images.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class ItemRecipe
    {
        public Item material1 { get; set; }
        public Item material2 { get; set; }
        public int nMateriasl1 { get; }
        public int nMateriasl2 { get; }
        //public Item material3 { get; set; }
        public static String description;
        public ItemRecipe(int it1, int it2)
        {
            material1 = new Item();
            material2 = new Item();
            nMateriasl1 = it1;
            nMateriasl2 = it2;
        }


        public bool followTheRecipe(Item item1, Item item2)
        {
            if (item1.itemID != material1.itemID && item2.itemID != material2.itemID)
            {//se o item 1 e o item 2 forem diferentes da receita
                return false;
            }
            else if (item1.itemID != material2.itemID && item2.itemID != material1.itemID)
            {//se o item 1 e 2 são diferentes, porem trocados
                return false;
            }
            else if (item1.itemID == material1.itemID && item2.itemID != material2.itemID)
            {//se o item 1 for igual mas o item 2 não
                return false;
            }
            else if (item1.itemID == material2.itemID && item2.itemID != material1.itemID)
            {//se o item 1 for igual e o item 2 não, so que inverso
                return false;
            }
            else if (item1.itemID != material1.itemID && item2.itemID == material2.itemID)
            {// se o item 1 for diferente e o 2 igual
                return false;
            }
            else if (item1.itemID != material2.itemID && item2.itemID == material1.itemID)
            {// se o item 1 for diferente e o 2 igual, so que inverso
                return false;
            }
            else if (item1.itemID == material1.itemID && item2.itemID == material2.itemID)
            {// se ambos forem iguais ao requerido
                if (item1.amount == nMateriasl1 && item2.amount == nMateriasl2)
                {// se a quantidade é a nescessaria, caso item1 = material1 e item2 = material2
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (item1.amount == nMateriasl2 && item2.amount == nMateriasl1)
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
}
