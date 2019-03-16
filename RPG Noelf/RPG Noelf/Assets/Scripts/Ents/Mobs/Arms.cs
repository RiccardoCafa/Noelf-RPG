using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Arms : MobDecorator
    {
        public Arms(Mob mob) : base(mob) { }

        public new void Make() { base.Make(); }
    }

    class DragonArms : Arms
    {
        public DragonArms(Mob mob) : base(mob)
        {
            mob.Str += 1;
        }
    }

    class KongArms : Arms
    {
        public KongArms(Mob mob) : base(mob)
        {
            mob.Spd += 1;
            mob.attacks.Add(Poopoo);
        }

        public void Poopoo()
        {

        }
    }

    class LizardArms : Arms
    {
        public LizardArms(Mob mob) : base(mob)
        {
            mob.Mnd += 1;
        }
    }

    class BisonArms : Arms
    {
        public BisonArms(Mob mob) : base(mob)
        {
            mob.Con += 1;
        }
    }

    class CatArms : Arms
    {
        public CatArms(Mob mob) : base(mob)
        {
            mob.Dex += 1;
            mob.attacks.Add(Camouflage);
        }

        public void Camouflage()
        {

        }
    }
}
