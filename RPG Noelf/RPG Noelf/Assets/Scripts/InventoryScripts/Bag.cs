using RPG_Noelf.Assets.Scripts.Crafting_Scripts;
using RPG_Noelf.Assets.Scripts.InventoryScripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    class Bag
    {
        public const uint maxStack = 99;
        public int freeSlots { get; set; } //numero de  espaços livres no inventário do player
        public int nGold { get; set; } // dinheiro do player;
        public List<SlotInventory> slots { get; set; } //espaços para guardar os itens
        public Bag() //precisa de construtor?
        {
            slots = new List<SlotInventory>();
            this.freeSlots = 30;
            
        }
        //resolvido o problema de adição na mochila com a criação de 3 funções extras
        public bool AddToBag(int itemID, int amount)
        {
            return true;
        }
        // atribuir o item para null é o suficiente para remove-lo, pois o GB ira eventualmente limpar
        public void RemoveFromBag(int itemID, int amount)//remove o item da bag
        {

        }
        public void AddGold(int coins) // incrementa o gold na mochila
        {
            nGold = nGold + coins;
        }
        public bool CashOut(int coins)//retorna o dinheiro que sobrou
        {
            if (nGold < coins)
            {
                //não tenho dinheiro suficiente
                return false;
            }
            else
            {
                nGold = nGold - coins;
                return true;
            }

        }
        public int AmountInBag(int itemID) //retorna a quantidade de um item especifico na mochila
        {
            int amt = 0;
            foreach(SlotInventory slot in slots)
            {
                if(slot.itemID == itemID)
                {
                    amt = slot.itemAmount;
                }
            }
            return amt;
        }
        public bool SearchID(int itemID)//verifica se o ID esta na mochila
        {
            foreach(SlotInventory sl in slots)
            {
                if(sl.itemID == itemID)
                {
                    return true;
                }
            }
            return false;            
        }
        // não estou muito certo se deve-se usar isso
        void Clearbag()
        {
            
        }

        public bool CanAddMore()
        {
            if (this.freeSlots > 0) return true;
            else return false;
        }

        //incrementa em um a quantidade de um item no inventario
        public void IncreaseItemNumber(int id, int amount)
        {
            

        }



    }
}
