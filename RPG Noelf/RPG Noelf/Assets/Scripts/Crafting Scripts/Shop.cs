using RPG_Noelf.Assets.Images.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts
{
    class Shop 
    {
        
        public List<Item> sell; // lista de itens a serem vendidos
        public List<Item> buy; // lista de itens comprados
        public Shop()
        {
            sell = new List<Item>();
            buy = new List<Item>();
            
        }


      
        public void SellItem(Bag g,Item sellit)//vender item ao jogador
        {
            if (g.freeSlots == 30)
            {
//responder o vagabundo
            }
            else
            {
                if (g.nGold >= sellit.goldValue)
                {
                    if (sell.Contains(sellit) == true)
                    {
                        g.addToBag(sellit);
                        g.cashOut(sellit.goldValue);

                    }
                }
                else
                {
                    //responder o vagabundo
                }
            }
        }
        public void BuyItem(Bag g,Item buyit)//comprar item do jogador
        {
            g.removeFromBag(buyit);
            g.nGold += (buyit.goldValue * 65)/100;
            sell.Add(buyit);
        }


    }
}
