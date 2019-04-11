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
        public uint GeneratedID { get; set; }
        public List<Slot> Materials { get; set; }

        public Recipe(uint NewID)
        {
            GeneratedID = NewID;
            Materials = new List<Slot>();
        }

       //checar se a mochila tem os materiais
        public bool CheckRecipe(Bag playerbag)
        {
            foreach(Slot slots in Materials)
            {
                //função lambda ou Predicate
                Slot s = playerbag.Slots.Find(x => x.ItemID == slots.ItemID && x.ItemAmount >= slots.ItemAmount);
                if(s == null)
                {
                    return false;
                }
            }
            return true;
        }
      //checar se isso é material
        public bool IsMaterial(Slot item)
        {
           foreach(Slot slot in Materials)
            {
                if(slot.ItemAmount == item.ItemAmount && slot.ItemID == item.ItemID)
                {
                    return true;
                }
            }
            return false;
        }

        


    }
}
