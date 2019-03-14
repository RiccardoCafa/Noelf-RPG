using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.InventoryScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Shop_Scripts
{
    class Shop
    {
        public List<int> TradingItems = new List<int>();
        public Bag BuyingItems = new Bag();

        public const int MaxBuyingItems = 16;

        // função para vender itens ao jogador
        public void SellItem(uint soldID, Bag playerBag)
        {
            
        }


        //função para comprar itens do jogador
        public void BuyItem(Bag playerBag)
        {
            foreach(Slot sack in BuyingItems.Slots)
            {
                long valor = Encyclopedia.SearchFor(sack.ItemID).GoldValue;
                valor = valor * sack.ItemAmount;
                playerBag.AddGold((int)valor);
            }
            
        }
        
        public void AddToBuyingItems(Slot slot)
        {
            BuyingItems.AddToBag(slot.ItemID, slot.ItemAmount);
        }

        public void RemoveFromBuyingItems(Slot slot)
        {
            BuyingItems.RemoveFromBag(slot);
        }

        public void RemoveFromBuyingItems(uint index, uint amount)
        {
            BuyingItems.RemoveFromBag(index, amount);
        }
    }

}
