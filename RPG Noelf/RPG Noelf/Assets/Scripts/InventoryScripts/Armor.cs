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

        public Armor(int goldValue, int amount, string name, bool isStackable, Category categoria, int itemID, string pathImage) :
                            base(goldValue, amount, name, isStackable, categoria, itemID, pathImage)
        {
            //tpWeapon = tWeapon;
        }

        public int defense //armaduraTotal /armaduratotal + 100 
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
                    tpArmor = TypeArmor.Light;
                }
            }
        }


    }
       

    }

