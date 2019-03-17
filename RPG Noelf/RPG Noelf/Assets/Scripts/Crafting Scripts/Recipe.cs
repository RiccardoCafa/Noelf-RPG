using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{
    class Recipe
    {
        public Slot[] Itens { get; set; }
        public uint generatedID { get; set; }
        public string RecipeName { get; set; }

        public Recipe(string name, int qtd)
        {
            RecipeName = name;
            Itens = new Slot[qtd];
        }



    }
}
