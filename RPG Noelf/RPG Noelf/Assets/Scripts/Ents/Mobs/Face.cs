using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    abstract class Face : MobDecorator
    {
        public Face(Mob mob) : base(mob)
        {
        }

        public new void Make() { base.Make(); }
    }

    class DragonFace : Face
    {
        public DragonFace(Mob mob) : base(mob)
        {
            mob.Str += 2;
            mob.attacks.Add(Fireball);
        }

        public void Fireball()//bola de fogo
        {

        }
    }

    class KongFace : Face
    {
        public KongFace(Mob mob) : base(mob)
        {
            mob.Spd += 2;
        }
    }

    class LizardFace : Face
    {
        public LizardFace(Mob mob) : base(mob)
        {
            mob.Mnd += 2;
            mob.attacks.Add(Lick);
        }

        public void Lick()//linguada
        {

        }
    }

    class BisonFace : Face
    {
        public BisonFace(Mob mob) : base(mob)
        {
            mob.Con += 2;
            mob.attacks.Add(Headache);
        }

        public void Headache()//cabeçada
        {

        }
    }

    class CatFace : Face
    {
        public CatFace(Mob mob) : base(mob)
        {
            mob.Dex += 2;
        }
    }
}
