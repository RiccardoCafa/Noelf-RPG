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


        public int defense
        {
            get
            {
                return defense;
            }
            set
            {
                if (defense > 500)
                {
                    defense = 500;
                }
                else
                {
                    defense = value;
                }
                if (defense >= 300 && defense <= 500)
                {
                    tpArmor = TypeArmor.Heavy;
                } else if (defense >= 150 && defense <= 300)
                {
                    tpArmor = TypeArmor.Medium;
                }
                else
                {

                }
            }
        }


    }
       

    }

