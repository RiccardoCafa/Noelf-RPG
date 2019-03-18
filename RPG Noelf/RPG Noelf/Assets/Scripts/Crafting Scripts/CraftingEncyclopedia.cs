using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{
    class CraftingEncyclopedia
    {
        public static Dictionary<uint, Recipe> crafting { get; set; }//o ID do crafting é o id do item criado

        public static void LoadCrafting()
        {
            crafting = new Dictionary<uint, Recipe>();

            Recipe rec1 = new Recipe("Blueprint: Iron Sword")
            {
                generatedID = 13,              
            };
            rec1.itens.Add(new Slot(42, 2));//duas barras de ferro
            rec1.itens.Add(new Slot(3, 2));//um galho de madeira
            crafting.Add(rec1.generatedID, rec1);//receita adicionada no crafting

            Recipe rec2 = new Recipe("Recipe: Iron Ingot")
            {
                generatedID = 42
            };
            rec2.itens.Add(new Slot(1, 5));//5 pepitas de ferro
            crafting.Add(rec2.generatedID,rec2);//adicionada no crafting


        }


        
     
                    


    }
}
