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
        public List<Slot> itens { get; set; }
        public uint generatedID { get; set; }
        public string RecipeName { get; set; }

        public Recipe(string name, List<Slot> materiais)
        {
            RecipeName = name;
            itens = materiais;
        }



    }
}
