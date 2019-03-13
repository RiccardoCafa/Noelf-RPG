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
        public float bonusDamage { get; set; }
        public float armorPenetration { get; set; }// tArmor - ArmorPenetration =  new armadura
        //public Recipe weaponRecipe;
        public string pathImage { get; set; }
        public TypeWeapon tpWeapon { get; }
        public Weapon(string name) :
        base(name)                 
        {

            tpWeapon = tWeapon;
            itemType = "Weapon";
        }




    }
}
