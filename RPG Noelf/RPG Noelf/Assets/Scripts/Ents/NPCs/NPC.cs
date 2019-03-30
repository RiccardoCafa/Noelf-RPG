using RPG_Noelf.Assets.Scripts.Shop_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    class NPC
    {
        public Trader _Trader { get; set; } = null;
        public Quester _Quester { get; set; } = null;
        

    }

    abstract class NPCFunctional
    {

    }

    sealed class Trader
    {
        public Shop shop;

        public Trader(Shop shop)
        {
            this.shop = shop;
        }

        public Trader()
        {
            shop = new Shop();
        }


    }

    sealed class Quester
    {

    }

    sealed class RuneMaster
    {

    }

}
