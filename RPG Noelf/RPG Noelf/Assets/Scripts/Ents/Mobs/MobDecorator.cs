using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class MobDecorator : IMob
    {
        public Mob mob;

        public MobDecorator(Mob mob)
        {
            this.mob = mob;
        }

        public void Make()
        {
            mob.Make();
        }
    }
}
