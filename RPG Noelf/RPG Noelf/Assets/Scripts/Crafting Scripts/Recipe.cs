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

    }
}
