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
        private Slot slotInOffer;

        public const int MaxBuyingItems = 16;

        public Shop()
        {
            BuyingItems.MaxSlots = MaxBuyingItems;
        }

        public Slot SlotInOffer
        {
            get
            {
                return slotInOffer;
            }
            set
            {
                if (BuyingItems.FreeSlots > 0)
                {
                    slotInOffer = value;
                }
            }
        }

        // função para vender itens ao jogador
        public void SellItem(uint soldID,uint amount, Bag playerBag)
        {
            if (playerBag.CanAddMore())
            {
                playerBag.AddToBag(soldID,amount);
                int valor = Encyclopedia.SearchFor(soldID).GoldValue;
                playerBag.Gold -= valor;
                
            }

        }


        //função para comprar itens do jogador
        public void BuyItem(Bag playerBag)
        {
            foreach (Slot sack in BuyingItems.Slots)
            {
                long valor = Encyclopedia.SearchFor(sack.ItemID).GoldValue;
                valor = valor * sack.ItemAmount;
                playerBag.AddGold((int)valor);
            }
            BuyingItems.Slots.Clear();

        }

        public bool AddToBuyingItems(Slot slot)
        {
            if (BuyingItems.CanAddMore())
            {
                return BuyingItems.AddToBag(slot.ItemID, slot.ItemAmount);
            }
            return false;
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
