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
        public List<Slot> BuyingItems = new List<Slot>();
        public const int MaxBuyingItems = 16;
        // função para vender itens ao jogador
        public void SellItem(uint soldID, Bag playerBag, uint amount)
        {
            
            if (playerBag.Gold >= Encyclopedia.SearchFor(soldID).GoldValue * (int)amount)
            {
                if (playerBag.AddToBag(soldID, amount))
                {
                    int valor = Encyclopedia.SearchFor(soldID).GoldValue * (int)amount;
                    playerBag.Gold = playerBag.Gold - valor;
                }    
            }

            
        }


        //função para comprar itens do jogador
        public void BuyItem(Bag playerBag, Slot sack)
        {
            long valor = Encyclopedia.SearchFor(sack.ItemID).GoldValue;
            valor = valor * sack.ItemAmount;
            playerBag.AddGold((int)valor);
        }
        public void AddToBuyingItems(Slot slot)
        {
            if (BuyingItems.Count < MaxBuyingItems)
            {
                BuyingItems.Add(slot);
            }
        }

        public void RemoveFromBuyingItems(Slot slot)
        {
            if (BuyingItems.Contains(slot))
            {
                BuyingItems.Remove(slot);
            }
        }
        public void RemoveFromBuyingItems(int index)
        {
            if (BuyingItems.Count < index)
            {
                BuyingItems.RemoveAt(index);
            }
        }
    }
