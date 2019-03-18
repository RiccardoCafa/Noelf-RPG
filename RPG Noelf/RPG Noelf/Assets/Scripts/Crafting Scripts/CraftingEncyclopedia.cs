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
            List<Slot> materiais = new List<Slot>();

            materiais.Add(new Slot(42, 2));
            materiais.Add(new Slot(3, 2));
            Recipe rec1 = new Recipe("Blueprint: Iron Sword", materiais)
            {
                generatedID = 13
            };
            crafting.Add(rec1.generatedID, rec1);//receita adicionada no crafting

            materiais = new List<Slot>();
            materiais.Add(new Slot(1, 5));
            Recipe rec2 = new Recipe("Recipe: Iron Ingot", materiais)
            {
                generatedID = 42
            };
            crafting.Add(rec2.generatedID,rec2);//adicionada no crafting


        }


        
     
                    


    }
}
