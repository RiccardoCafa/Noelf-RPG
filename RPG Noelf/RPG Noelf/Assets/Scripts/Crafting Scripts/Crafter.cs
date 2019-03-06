using RPG_Noelf.Assets.Images.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.InventoryScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{
    class Crafter
    {
       
        public Weapon craftWeapon(Recipe weaponRecipe,Bag playerBag, TypeWeapon tpWeapon)//pesquisando os materiais na mochila e criando a arma nova, removendo os materiais
        {
            bool isReady = false;
            Weapon newWeapon;
            foreach(Item item in playerBag.slots)
            {
                if(weaponRecipe.IDsMateriais.Contains(item.itemID) == true)
                {
                    playerBag.removeFromBag(item);
                    isReady = true;
                }
                else
                {
                  //string a =  nothing
                }
            }
            if (isReady ==  true)
            {
                newWeapon = new Weapon(tpWeapon);
                return newWeapon;
            }
            else
            {
                newWeapon = null;
                return newWeapon;
            }

        }

        public Armor craftArmor(Recipe armorRecipe, Bag playerBag)//pesquisando os materiais da armadura e os removendo
        {
            bool isReady = false;
            Armor newWeapon;
            foreach (Item item in playerBag.slots)
            {
                if (armorRecipe.IDsMateriais.Contains(item.itemID) == true)
                {
                    playerBag.removeFromBag(item);
                    isReady = true;
                }
                else
                {
                    //string a =  nothing
                }
            }
            if (isReady == true)
            {
                newWeapon = new Armor();
                return newWeapon;
            }
            else
            {
                newWeapon = null;
                return newWeapon;
            }
        }

        public Consumable craftConsumable(Recipe consumableRecipe, Bag playerBag)//pesquisando os materiais para os consumiveis e os removendo da mochila
        {
            bool ready = false;
            Consumable novoCP;
            foreach(Item item in playerBag.slots)
            {
                if (consumableRecipe.IDsMateriais.Contains(item.itemID) == true)
                {
                    playerBag.removeFromBag(item);
                    ready = true;
                }
                else
                {
                    //string a =  nothing
                }


            }
            if (ready)
            {
                novoCP = new Consumable();
                return novoCP;
            }
            else
            {
                novoCP = null;
                return novoCP;
            }

        }

        



    }
}
