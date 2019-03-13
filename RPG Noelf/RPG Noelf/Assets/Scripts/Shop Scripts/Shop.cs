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
        public List<int> selled;
        public List<int> purchased;


        public Shop()
        {
            selled = new List<int>();
            purchased = new List<int>();
        } 
        

        // função para vender itens ao jogador
        public bool sellItem(int soldID, Bag player)
        {
           
        }


        //função para comprar itens do jogador
        public bool BuyItem(int purchasedID, Bag player)
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
