using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    class MobSlot : Slot
    {
        public Random DropRate = new Random();
        public double ChanceDrop { get; set; }
        
        
        public MobSlot(uint IDkey, uint amount, double chanceDrop) : base(IDkey, amount)
        {
            this.ChanceDrop = chanceDrop;
        }
    }
}
