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
        public TypeWeapon tpWeapon { get; set; }
        public Weapon(string name) :
        base(name)
        {
            IsStackable = false;
    
        }

        public string GetTypeWeapon()
        {
            switch (tpWeapon)
            {

                case TypeWeapon.Magical:
                    return "Magical";
                case TypeWeapon.Melee:
                    return "Melee";
                case TypeWeapon.Ranged:
                    return "Ranged";
            }
            return "";
        }



    }
}
