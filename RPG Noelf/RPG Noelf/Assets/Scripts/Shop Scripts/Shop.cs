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
        public List<Item> itens;

        public Item sellItem(Item selled, Bag playerbag)
        {

            if(playerbag.nGold > selled.goldValue && playerbag.canAddMore() == true)
            {
                playerbag.addToBag(selled);
                playerbag.cashOut(0);
                    return null;
            }

            return null;
        }
    }
}
