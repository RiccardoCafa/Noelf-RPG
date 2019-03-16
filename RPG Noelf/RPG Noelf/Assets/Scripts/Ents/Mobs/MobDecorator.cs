using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    abstract class MobDecorator : Mob
    {
        public Mob Mob { get; set; }

        protected MobDecorator(Mob mob)
        {
            Mob = mob;
        }

        override public void Make()
        {
            Mob.Make();
        }
    }
}
