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
            Item item1 = new Item("Iron Nugget")
            {
                goldValue = 1
            };
            encyclopedia.Add(1,item1);
            encyclopedia.Add(2, new Item("Coal"));
            encyclopedia.Add(3, new Item("Wood"));
            encyclopedia.Add(4, new Item("Leather"));
            encyclopedia.Add(5, new Item("Wool"));
            encyclopedia.Add(6, new Item("Line"));
            encyclopedia.Add(7, new Item("Magic Stone"));
            encyclopedia.Add(8, new Item("Steel"));
            encyclopedia.Add(9, new Item("Hardened Leather"));
            encyclopedia.Add(10, new Item("Hardened Line"));
            encyclopedia.Add(11, new Item("Magic Wool"));
            encyclopedia.Add(12, new Item("Improved Magic Stone"));
            encyclopedia.Add(13, new Weapon("Iron Sword"));
            encyclopedia.Add(14, new Weapon("Iron Axe"));
            encyclopedia.Add(15, new Weapon("Long Bow"));
            encyclopedia.Add(16, new Weapon("Magic Staff"));
            encyclopedia.Add(17, new Weapon("Steel Sword"));
            encyclopedia.Add(18, new Weapon("Steel Axe"));
            encyclopedia.Add(19, new Weapon("Crossbow"));
            encyclopedia.Add(20, new Weapon("Improved Magic Staff"));
            encyclopedia.Add(21, new Armor("Iron Chestplate"));
            encyclopedia.Add(22, new Armor("Iron Legplates"));
            encyclopedia.Add(23, new Armor("Iron Gauntlets"));
            encyclopedia.Add(24, new Armor("Iron Helmet"));
            encyclopedia.Add(25, new Armor("Iron Boots"));
            encyclopedia.Add(26, new Armor("Steel Chestplate"));
            encyclopedia.Add(27, new Armor("Steel Legplates"));
            encyclopedia.Add(28, new Armor("Steel Gauntlets"));
            encyclopedia.Add(29, new Armor("Steel Boots"));
            encyclopedia.Add(30, new Armor("Steel Helmet"));
            encyclopedia.Add(31, new Armor("Leather Body"));
            encyclopedia.Add(32, new Armor("Leather Chaps"));
            encyclopedia.Add(33, new Armor("Leather Gloves"));
            encyclopedia.Add(34, new Armor("Leather Hood"));
            encyclopedia.Add(35, new Armor("Wool Gown"));
            encyclopedia.Add(36, new Armor("Wool Hood"));
            encyclopedia.Add(37, new Armor("Wool Gloves"));
            encyclopedia.Add(38, new Armor("Fancy Shoes"));

        }


        
        // procura um item especifico
        public static Item SearchFor(uint key)
        {
            Item generic;
            generic = encyclopedia[key];
            return generic;
        }

        //procura se o ID é estacavel com boleana
        public static bool SearchStackID(uint key)
        {
            Item item = encyclopedia[key];
            if(item.isStackable == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //procura a defesa do items
        public static float SearchDefense(uint key)
        {
            Armor armor = (Armor)encyclopedia[key];
            return armor.defense;

        }

        //procura o bonus do Consumivel
        public static float SearchConsumableBonus(uint key)
        {
            Consumable it = (Consumable) encyclopedia[key];
            return it.giveBonus;

        }

        //procura o dano da Arma
        public static float SearchDamageWeapon(uint key)
        {
            Weapon wep;
            wep = (Weapon) encyclopedia[key];
            return wep.bonusDamage;
        }
        
    }
}
