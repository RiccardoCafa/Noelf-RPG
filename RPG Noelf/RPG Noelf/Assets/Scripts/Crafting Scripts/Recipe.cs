using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{
    class Recipe { 
    
      public string ItemNome { get; set; }
      public List<Slot> ListaMaterial;
      public int NumMaterials { get; set; }  
      public Recipe(string nome)
      {
        this.ItemNome = nome;
        ListaMaterial = new List<Slot>();
      }
      public void Add(Slot t)
        {
            if(t != null)
            {
                ListaMaterial.Add(t);
                NumMaterials = ListaMaterial.Count;
            }
        } 
        /*
        public bool IsMaterial(uint ID)
        {
            var material = from itens in ListaMaterial
                           where itens.ItemID == ID
                           select itens;
            //LINQ eh top
            return material != null;
        }

        public int GetMaterialAmount(uint id)
        {
            if (IsMaterial(id))
            {
               var item = from slots in ListaMaterial
                           where slots.ItemID == id
                           select slots;
               if(item != null)
                {
                    Slot slot = item.FirstOrDefault();
                    return (int) slot.ItemAmount;
                }

            }
            return 0;
        }
        */

    }
}
