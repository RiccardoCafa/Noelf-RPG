using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Legs : MobDecorator
    {
        protected Legs(Mob mob) : base(mob) { }

        public new void Make() { base.Make(); }
    }

    class DragonLegs : Legs
    {
        public DragonLegs(Mob mob) : base(mob)
        {
            mob.Str += 1;
            mob.vulnerable.Add(Element.ice);
        }
    }

    class KongLegs : Legs
    {
        public KongLegs(Mob mob) : base(mob)
        {
            mob.Spd += 10;
        }
    }

    class LizardLegs : Legs
    {
        public LizardLegs(Mob mob) : base(mob)
        {
            mob.Mnd += 1;
            mob.Spd = 1;
        }
    }

    class BisonLegs : Legs
    {
        public BisonLegs(Mob mob) : base(mob)
        {
            mob.Con += 1;
            mob.meek = true;
        }
    }

    class CatLegs : Legs
    {
        public CatLegs(Mob mob) : base(mob)
        {
            mob.Dex += 1;
            mob.vulnerable.Add(Element.none);
        }
    }
}

