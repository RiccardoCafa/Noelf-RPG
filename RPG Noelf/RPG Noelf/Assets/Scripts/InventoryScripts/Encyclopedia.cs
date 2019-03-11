using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
     class Encyclopedia
    {
        public static Dictionary<uint, Item> encyclopedia;

        public static void loadItens()
        {
            encyclopedia = new Dictionary<uint, Item>();
            encyclopedia.Add(1,new Item());
            encyclopedia.Add(2, new Item());
            encyclopedia.Add(3, new Item());
            encyclopedia.Add(4, new Item());
            encyclopedia.Add(5, new Item());
            encyclopedia.Add(6, new Item());
            encyclopedia.Add(7, new Item());
            encyclopedia.Add(8, new Item());
            encyclopedia.Add(9, new Item());
            encyclopedia.Add(10, new Item());
            encyclopedia.Add(11, new Item());
            encyclopedia.Add(12, new Item());
            encyclopedia.Add(13, new Weapon(TypeWeapon.Melee));
            encyclopedia.Add(14, new Weapon(TypeWeapon.Melee));
            encyclopedia.Add(15, new Weapon(TypeWeapon.Ranged));
            encyclopedia.Add(16, new Weapon(TypeWeapon.Magical));
            encyclopedia.Add(17, new Weapon(TypeWeapon.Melee));
            encyclopedia.Add(18, new Weapon(TypeWeapon.Melee));
            encyclopedia.Add(19, new Weapon(TypeWeapon.Ranged));
            encyclopedia.Add(20, new Weapon(TypeWeapon.Magical));
            encyclopedia.Add(21, new Armor(TypeArmor.Heavy));
            encyclopedia.Add(22, new Armor(TypeArmor.Medium));
            encyclopedia.Add(23, new Armor(TypeArmor.Light));
            encyclopedia.Add(24, new Armor(TypeArmor.Light));
            encyclopedia.Add(25, new Armor(TypeArmor.Light, 0.02f));
        }


        

        public static Item searchFor(uint key)
        {
            Item generic;
            generic = encyclopedia[key];
            return generic;

        }

        
    }
}
