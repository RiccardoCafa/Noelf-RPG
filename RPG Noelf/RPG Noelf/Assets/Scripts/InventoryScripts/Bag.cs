using RPG_Noelf.Assets.Scripts.Crafting_Scripts;
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
        public int freeSlots { get; set; } //numero de  espaços livres no inventário do player
        public int nGold { get; set; } // dinheiro do player;
        public List<Item> slots { get; set; } //espaços para guardar os itens
        public Recipe possibleCrafting { get; set; }
        public Bag()// precisa de construtor?
        {
            slots = new List<Item>();
            this.freeSlots = 30;
            possibleCrafting = new Recipe();
        }
        // atribuir o item para null é o suficiente para remove-lo, pois o GB ira eventualmente limpar
        public void RemoveFromBag(Item removed)//remove o item da bag
        {
            if (slots.Contains(removed) && removed.isStackable && removed.amount > 1)//se for stackavel e tiver mais de um, retira apenas um
            {
                removed.amount--;
            }
            else if (slots.Contains(removed) && removed.isStackable == false)//remove se não for stackavel e se so tiver um
            {
                slots.Remove(removed);
                freeSlots++;//incrementa o numero de espaços livres
                possibleCrafting.IDsMateriais.Remove(removed.itemID);
                removed = null;
            }
            else if (slots.Contains(removed) && removed.isStackable && removed.amount == 1)//se so houver um e for stackavel, remove total
            {
                slots.Remove(removed);
                freeSlots++;
                possibleCrafting.IDsMateriais.Remove(removed.itemID);
                removed = null;
            }
        }
        public void AddGold(int coins) // incrementa o gold na mochila
        {
            nGold = nGold + coins;
        }
        public void CashOut(int coins)//retorna o dinheiro que sobrou
        {
            if (nGold < coins)
            {
                nGold = 0;
            }
            else
            {
                nGold = nGold - coins;
            }

        }
        public int AmountInBag(Item item) //retorna a quantidade de um item especifico na mochila
        {
            return item.amount;
        }
        public bool SearchID(string id)//verifica se o ID esta na mochila
        {
            foreach (Item item in slots)
            {
                if (item.itemID == id)
                {
                    return true;           
                }
            }
            return false;
        }
        // não estou muito certo se deve-se usar isso
        void Clearbag()
        {
            slots.Clear();
        }

        public bool CanAddMore()
        {
            if (this.freeSlots > 0) return true;
            else return false;
        }
        //resolvido o problema de adição na mochila com a criação de 3 funções extras
        public void AddToBag(Item item)
        {
            if (freeSlots != 0 && item.isStackable == false)//adiciona na proxima posição se o item não for stackavel
            {
                slots.Add(item);
                freeSlots--;
                Debug.WriteLine("Item non-stackable added!");
            }
            if (freeSlots == 0 && SearchID(item.itemID) == true && item.isStackable == true)//se não tiver espaço, porem ter um item do mesmo tipo e este for stackavel, incrementar
            {
                IncreaseItemNumber(item.itemID);
                Debug.WriteLine("Item existing and stackable but without space in the bag incremented!");
            }
            else if (freeSlots != 0 && SearchID(item.itemID) == true && item.isStackable == true)
            {//ter espaço e ainda assim ter um mesmo item, incrementar caso seja stackavel

                IncreaseItemNumber(item.itemID);
                Debug.WriteLine("Item existing and stackable but with space in the bag incremented!");
            } else if(freeSlots > 0) {
                slots.Add(item);
                freeSlots--;
            }
            if (freeSlots == 0 && SearchID(item.itemID) == false)//não ter espaço e não ter um item
            {
                //o player não pode pegar
            }


        }

        public void IncreaseItemNumber(string id)//incrementa em um a quantidade de um item no inventario
        {
            foreach (Item item in slots)
            {
                if (SearchID(id) == true)
                {
                    if (item.itemID == id && item.isStackable == true)
                    {
                        item.amount++;
                        break;
                    }
                }
            }


        }
    }
}
