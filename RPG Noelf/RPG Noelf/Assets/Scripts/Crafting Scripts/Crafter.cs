using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{
    class Crafter
    {

        //metodo para checar se é possivel craftar o item
       public bool CanCraft(Bag bag, Recipe recipe)
       {
            Slot[] Recipeitens = recipe.itens.ToArray();
            Slot[] bagItens = bag.Slots.ToArray();

            int count = bagItens.Length;
            
            for(int i = 0; i < count; i++)
            {
                if(bagItens[i].Equals(Recipeitens) != true)
                {
                    return false;
                }
            }
            return true;
        }
        
        //metodo que compara dois itens
        public bool CompareItens(Slot item1, Slot item2)
        {
            if(item1.ItemID == item2.ItemID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //metodo que tira os itens da mochila caso seja possivel craftar
        public bool RemoveNescessaryItens(Recipe item, Bag materials)
        {
            if (CanCraft(materials, item) == true)
            {
                foreach (Slot slots in materials.Slots)
                {
                    if (item.itens.Contains(slots) == true)
                    {
                        materials.Slots.Remove(slots);
                    }
                }
                return true;
            }
            else
            {
                return false;

            }
        }

        //método para criar itens
        public uint CreateNewItem(Recipe item, Bag materials)
        {
          if(RemoveNescessaryItens(item,materials) == true)
            {
                return item.generatedID;
            }
            else
            {
                return 0;
            }          
        }

        //metodo para destruir itens
        public List<Slot> DestroyItem(uint key, Recipe keyRecipe)
        {
            List<Slot> leftover;

            if(keyRecipe.itens != null)
            {
                if(keyRecipe.generatedID == key)
                {
                    leftover = keyRecipe.itens;
                    key = 0;
                    return leftover;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return leftover =  new List<Slot>();
            }


        }

    }
}
