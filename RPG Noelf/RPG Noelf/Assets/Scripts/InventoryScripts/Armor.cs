using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    public enum TypeArmor
    {
        Heavy, Medium, Light
    }
    class Armor : Item
    {
        public TypeArmor tpArmor;
        public float defense;
        public Armor(string name) :
        base(name)
        {
            isStackable = false;
            
        }
        
        public string GetTypeArmor()
        {
            switch (tpArmor)
            {
                case TypeArmor.Heavy:
                    return "Heavy Armor";
                case TypeArmor.Light:
                    return "Light Armor";
                case TypeArmor.Medium:
                    return "Medium Armor";
                
            }

            return "";
        }



    }
 
       


    }
       

    }

