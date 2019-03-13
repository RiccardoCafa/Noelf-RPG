using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class PlayerSlot
    {
        public PlayerBag MyBag;

        public const uint maxStack = 99;
        public uint ItemID { get; set; }
        public uint ItemAmount { get; set; }

        public PlayerSlot(uint id, uint amount, PlayerBag myBag)
        {
            MyBag = myBag;
            ItemID = id;
            ItemAmount = amount;
        }

        // incrementar o monte e retornar o que sobrou
       public uint IncreaseAmount(uint amount)
        {
            if((ItemAmount + amount) >= maxStack)
            {
                return ItemAmount+ amount - maxStack;
            }
            else
            {
                ItemAmount = ItemAmount + amount;
                return 0;
            }
        }

        //retorna a quantidade final apos decrescer
        public uint DecreaseAmount(uint amount)
        {
            if((ItemAmount - amount)<= 0)
            {
                ItemAmount = 0;
                ItemID = 0;
                // TODO make mybag remove
                //MyBag.remove
                return 0;
            }
            else
            {
                ItemAmount = ItemAmount - amount;
                return 0;
            }
        }

        public bool StackableID(uint itemID)
        {
            if (Encyclopedia.encyclopedia.ContainsKey(itemID))
            {
                if (Encyclopedia.SearchStackID(itemID) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else throw new ArgumentOutOfRangeException();

        }

    }
}
