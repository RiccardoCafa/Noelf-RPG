using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Crafting_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
   
    public enum TypeWeapon
    {
        Melee,Ranged,Magical 
    }
    class Weapon:Item
    {
        public int bonusDamage { get; set; }
        public int bonusDefense { get; set; }
        public double armorPenetration { get; set; }// tArmor - ArmorPenetration =  new armadura
        //public Recipe weaponRecipe;
        public TypeWeapon tpWeapon { get; }
        public Weapon(TypeWeapon tWeapon, int goldValue, int amount, string name, bool isStackable, Category categoria, int itemID, string pathImage) :
                            base(goldValue, amount, name, isStackable, categoria, itemID, pathImage)
        {
            tpWeapon = tWeapon;
        }




    }
}
