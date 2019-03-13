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

        public static void LoadItens()
        {
            encyclopedia = new Dictionary<uint, Item>();

            // loaded Itens
            Item item1 = new Item("Iron Nugget")
            {
                IsStackable = true,
                ItemCategory = Category.Normal,            
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 1
            };
            encyclopedia.Add(1,item1);
            Item item2 = new Item("Coal")
            {
                IsStackable = true,
                ItemCategory = Category.Normal,          
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 1
            };
            encyclopedia.Add(2, item2);
            Item item3 = new Item("Wood")
            {
                IsStackable = true,
                ItemCategory = Category.Normal,            
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 1
            };
            encyclopedia.Add(3, item3);
            Item item4 = new Item("Leather")
            {
                IsStackable = true,
                ItemCategory = Category.Normal,
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 2
            };
            encyclopedia.Add(4, item4);
            Item item5 = new Item("Wool")
            {
                IsStackable = true,
                ItemCategory = Category.Normal,             
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 1
            };
            encyclopedia.Add(5, item5);
            Item item6 = new Item("Line")
            {
                IsStackable = true,
                ItemCategory = Category.Normal,             
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 2
            };
            encyclopedia.Add(6, item6);
            Item item7 = new Item("Magical Stone")
            {
                IsStackable = true,
                ItemCategory = Category.Normal,               
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 3
            };
            encyclopedia.Add(7, item7);
            Item item8 = new Item("Steel")
            {
                IsStackable = true,
                ItemCategory = Category.Uncommon,          
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 5
            };
            encyclopedia.Add(8, item8);
            Item item9 = new Item("Hardened Leather")
            {
                IsStackable = true,
                ItemCategory = Category.Uncommon,
               
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 4
            };
            encyclopedia.Add(9, item9);
            Item item10 = new Item("Hardened Line")
            {
                IsStackable = true,
                ItemCategory = Category.Uncommon,             
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 4
            };
            encyclopedia.Add(10, item10);
            Item item11 = new Item("Magic Wool")
            {
                IsStackable = true,
                ItemCategory = Category.Uncommon,               
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 4

            };
            encyclopedia.Add(11, item11);
            Item item12 = new Item("Improved Magical Stone")
            {
                IsStackable = true,
                ItemCategory = Category.Uncommon,              
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 5

            };
            encyclopedia.Add(12, item12);
            Weapon item13 = new Weapon("Iron Sword")
            {
                tpWeapon = TypeWeapon.Melee,
                bonusDamage = 10,
                armorPenetration = 1,
                ItemCategory = Category.Normal,              
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 20

            };
            encyclopedia.Add(13, item13);
            Weapon item14 = new Weapon("Iron Axe")
            {
                tpWeapon = TypeWeapon.Melee,
                bonusDamage = 10,
                armorPenetration = 1,
                ItemCategory = Category.Normal,               
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 25
            };
            encyclopedia.Add(14, item14);
            Weapon item15 = new Weapon("Long Bow")
            {
                tpWeapon = TypeWeapon.Ranged,
                bonusDamage = 10,
                armorPenetration = 1,
                ItemCategory = Category.Normal,                
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 20

            };
            encyclopedia.Add(15, item15);
            Weapon item16 = new Weapon("Magic Staff")
            {
                tpWeapon = TypeWeapon.Magical,
                bonusDamage = 10,
                armorPenetration = 1,
                ItemCategory = Category.Normal,             
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 25

            };
            encyclopedia.Add(16, item16);
            Weapon item17 = new Weapon("Steel Sword")
            {
                tpWeapon = TypeWeapon.Melee,
                bonusDamage = 10,
                armorPenetration = 1,
                ItemCategory = Category.Normal,          
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 40

            };
            encyclopedia.Add(17, item17);
            Weapon item18 = new Weapon("Steel Axe")
            {
                tpWeapon = TypeWeapon.Melee,
                bonusDamage = 10,
                armorPenetration = 1,
                ItemCategory = Category.Normal,            
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 45

            };
            encyclopedia.Add(18, item18);
            Weapon item19 = new Weapon("Crossbow")
            {
                tpWeapon = TypeWeapon.Ranged,
                bonusDamage = 10,
                armorPenetration = 3,
                ItemCategory = Category.Normal,           
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 45

            };
            encyclopedia.Add(19, item19);
             Weapon item20 = new Weapon("Improved Magic Staff")
            {
                tpWeapon = TypeWeapon.Magical,
                bonusDamage = 10,
                armorPenetration = 1,
                ItemCategory = Category.Normal,        
                PathImage = "/Assets/Images/Chao.jpg",
                GoldValue = 50

            };
            encyclopedia.Add(20, item20);
            Armor item21 = new Armor("Iron Chestplate")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 30,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(21, item21);
            Armor item22 = new Armor("Iron Legplates")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 20,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(22, item22);
            Armor item23 = new Armor("Iron Gauntlets")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 10,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(23, item23);
            Armor item24 = new Armor("Iron Helmet")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 15,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(24, item24);
            Armor item25 = new Armor("Iron Boots")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 10,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(25, item25);
            Armor item26 = new Armor("Steel Chestplate")
            {
                ItemCategory = Category.Uncommon,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 50,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(26, item26);
            Armor item27 = new Armor("Steel Legplates")
            {
                ItemCategory = Category.Uncommon,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 45,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(27, item27);
            Armor item28 = new Armor("Steel Gauntlets")
            {
                ItemCategory = Category.Uncommon,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 25,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(28, item28);
            Armor item29 = new Armor("Steel Boots")
            {
                ItemCategory = Category.Uncommon,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 22,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(29, item29);
            Armor item30 = new Armor("Steel Helmet")
            {
                ItemCategory = Category.Uncommon,
                defense = 0.5f,
                tpArmor = TypeArmor.Heavy,
                GoldValue = 25,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(30, item3);
            Armor item31 = new Armor("Leather Body")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Medium,
                GoldValue = 8,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(31, item31);
            Armor item32 = new Armor("Leather Chaps")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Medium,
                GoldValue = 5,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(32, item32);
            Armor item33 = new Armor("Leather Gloves")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Medium,
                GoldValue = 4,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(33, item33);
            Armor item34 = new Armor("Leather Hood")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Medium,
                GoldValue = 5,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(34, item34);
            Armor item35 = new Armor("Leather Boots")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Medium,
                GoldValue = 4,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(35, item35);
            Armor item36 = new Armor("Wool Gown")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Light,
                GoldValue = 3,
                PathImage = "/Assets/Images/Chao.jpg",
            };
            encyclopedia.Add(36, item36);
            Armor item37 = new Armor("Wool Hood")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Light,
                GoldValue = 3,
                PathImage = "/Assets/Images/Chao.jpg"
            };
            encyclopedia.Add(37, item37);
            Armor item38 = new Armor("Wool Gloves")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Light,
                GoldValue = 3,
                PathImage = "/Assets/Images/Chao.jpg"
            };
            encyclopedia.Add(38, item38);
            Armor item39 = new Armor("Shoes")
            {
                ItemCategory = Category.Normal,
                defense = 0.5f,
                tpArmor = TypeArmor.Light,
                GoldValue = 3,
                PathImage = "/Assets/Images/Chao.jpg"
            };
            encyclopedia.Add(39, item39);
            Consumable item40 = new Consumable("Minor Health Potion")
            {
                GoldValue = 20,
                ItemCategory = Category.Normal,
                Bonus = 20,
                PathImage = "/Assets/Images/Chao.jpg"

            };
            encyclopedia.Add(40, item40);
            Consumable item41 = new Consumable("Minor Mana Potion")
            {
                GoldValue = 20,
                ItemCategory = Category.Normal,
                Bonus = 20,
                PathImage = "/Assets/Images/Chao.jpg"

            };
            encyclopedia.Add(41, item41);
        }

        // procura um item especifico
        public static Item SearchFor(uint key)
        {
            if (encyclopedia.ContainsKey(key))
            {
                return encyclopedia[key];
            }
            else throw new ArgumentOutOfRangeException();
        }

        //procura se o ID é estacavel com boleana
        public static bool SearchStackID(uint key)
        {
            if(encyclopedia.ContainsKey(key))
            {
                return encyclopedia[key].IsStackable;
            }
            else throw new ArgumentOutOfRangeException();
        }

        //procura a defesa do items
        public static float SearchDefense(uint key)
        {
            if (encyclopedia.ContainsKey(key))
            {
                Armor armor = (Armor)encyclopedia[key];
                return armor.defense;
            }
            else throw new ArgumentOutOfRangeException();
        }

        //procura o bonus do Consumivel
        public static float SearchConsumableBonus(uint key)
        {
            if(encyclopedia.ContainsKey(key))
            {
                Consumable it = (Consumable) encyclopedia[key];
                return 0f;//it.giveBonus;
            }
            return 0f;
        }

        //procura o dano da Arma
        public static float SearchDamageWeapon(uint key)
        {
            if (encyclopedia.ContainsKey(key))
            {
                Weapon wep;
                wep = (Weapon)encyclopedia[key];
                return wep.bonusDamage;
            }
            else throw new ArgumentOutOfRangeException();
        }
    }
}
