using RPG_Noelf.Assets.Images.Inventory_Scripts;
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
        public float armorPenetration { get; set; }// tArmor - ArmorPenetration =  new armadura

        public TypeWeapon tpWeapon { get; }
        public Weapon(TypeWeapon tWeapon)
        {
            tpWeapon = tWeapon;
        }




    }
}
