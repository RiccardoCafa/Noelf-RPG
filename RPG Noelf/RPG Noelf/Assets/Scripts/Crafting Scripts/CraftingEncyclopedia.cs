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
        public static Dictionary<uint, Recipe> crafting = new Dictionary<uint, Recipe>();//o ID do crafting é o id do item criado

        public static void LoadCrafting()
        {
            Recipe IronBar = new Recipe(41);//receita de uma barra de ferro
            IronBar.Materials.Add(new Slot(1, 5));//um slot com 5 barras de ferro é nescessario
            crafting.Add(41, IronBar);



        }
        

        
     
                    


    }
}
