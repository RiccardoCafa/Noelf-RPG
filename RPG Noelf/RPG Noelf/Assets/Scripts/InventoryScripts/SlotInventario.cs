using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class SlotInventario
    {
        public const int maxStack = 99;
        public int itemID { get; set; }
        public int itemAmount { get; set; }

        public SlotInventario(int id, int amount)
        {
            itemID = id;
            itemAmount = amount;
        }

        //retorna a quantidade que sobrou caso estoure o tamanho da pilha
        public int increaseAmount(int qtd)
        {
            if (itemAmount >= maxStack)
            {
                return qtd ;
            }
            else
            {
                if ((itemAmount + qtd) > maxStack)
                {
                    itemAmount = maxStack;
                    return maxStack - (itemAmount + qtd);

                }
                else
                {
                    itemAmount = itemAmount + qtd;
                    return 0;
                }
            }
        }
        //decrementar a quantidade de um stack
        public void decreaseAmount(int amount)
        {
            if((itemAmount - amount) == 0)
            {
                itemAmount = 0;
                itemID = 0;
            }




        }
        


    }
}
