using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Shop_Scripts
{
    class Shop
    {
        public List<Item> selled;
        public List<Item> purchased;


        public Shop()
        {
            selled = new List<Item>();
            purchased = new List<Item>();
        } 
        

        // função para vender itens ao jogador
        public bool sellItem(Item sell, Bag player)
        {
            if (selled.Contains(sell) == true && player.CanAddMore() == true && player.nGold >= sell.goldValue)
            {
                selled.Remove(sell);
                if (player.AddToBag(sell) == true)
                {
                    player.CashOut(sell.goldValue);
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        //função para comprar itens do jogador

        public bool BuyItem(Item item, Bag player)
        {
            if(item != null && player != null)
            {
                player.RemoveFromBag(item);
                purchased.Add(item);
                player.AddGold(item.goldValue);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
