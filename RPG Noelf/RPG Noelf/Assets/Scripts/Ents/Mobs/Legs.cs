using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Legs : MobDecorator
    {
        public Legs(Mob mob) : base(mob) { }

        public new void Make() { base.Make(); }
    }

    class DragonLegs : Legs
    {
        /* > Pernas
         *   - Mnd +1
         *   - armor -Ice
         */
        public DragonLegs(Mob mob) : base(mob) { }
    }

    class KongLegs : Legs
    {
        /* > Pernas
         *   - For +1
         *   - Spd +6
         */
        public KongLegs(Mob mob) : base(mob) { }
    }

    class LizardLegs : Legs
    {
        /* > Pernas
         *   - Spd =0
         */
        public LizardLegs(Mob mob) : base(mob) { }
    }

    class BisonLegs : Legs
    {
        /* > Pernas
         *   - Con +1
         *   - behavior +Meek
         */
        public BisonLegs(Mob mob) : base(mob) { }
    }

    class CatLegs : Legs
    {
        /* > Pernas
         *   - Dex +1
         *   - armor -Common
         */
        public CatLegs(Mob mob) : base(mob) { }
    }
}

