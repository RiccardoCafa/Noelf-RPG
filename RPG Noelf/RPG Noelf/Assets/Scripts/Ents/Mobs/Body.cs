using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Body : MobDecorator
    {
        protected Body(Mob mob) : base(mob) { }

        public new void Make() { base.Make(); }
    }

    class DragonBody : Body
    {
        public DragonBody(Mob mob) : base(mob)
        {
            mob.Str += 2;
            mob.resistance.Add(Element.Fire);
        }
    }

    class KongBody : Body
    {
        public KongBody(Mob mob) : base(mob)
        {
            mob.Spd += 2;
            mob.vulnerable.Add(Element.Fire);
        }
    }

    class LizardBody : Body
    {
        public LizardBody(Mob mob) : base(mob)
        {
            mob.Mnd += 2;
            mob.resistance.Add(Element.Poison);
        }
    }

    class BisonBody : Body
    {
        public BisonBody(Mob mob) : base(mob)
        {
            mob.Con += 2;
            mob.resistance.Add(Element.Common);
        }
    }

    class CatBody : Body
    {
        public CatBody(Mob mob) : base(mob)
        {
            mob.Dex += 2;
            mob.attacks.Add(Camouflage);
        }

        public void Camouflage()//camuflagem
        {

        }
    }
}
