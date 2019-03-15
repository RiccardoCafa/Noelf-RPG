using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Body : MobDecorator
    {
        public Body(Mob mob) : base(mob) { }

        public new void Make() { base.Make(); }
    }

    class DragonBody : Body
    {
        /* > Tronco
         *   - Mnd +2
         *   - armor +Fire
         */
        public DragonBody(Mob mob) : base(mob) { }
    }

    class KongBody : Body
    {
        /* > Tronco
         *   - For +2
         *   - armor -Fire
         */
        public KongBody(Mob mob) : base(mob) { }
    }

    class LizardBody : Body
    {
        /* > Tronco
         *   - Spd +2
         *   - armor +Poison
         */
        public LizardBody(Mob mob) : base(mob) { }
    }

    class BisonBody : Body
    {
        /* > Tronco
         *   - Con +2
         *   - armor +Common
         */
        public BisonBody(Mob mob) : base(mob) { }
    }

    class CatBody : Body
    {
        /* > Tronco
         *   - Dex +2
         *   - behavior +Camouflage
         */
        public CatBody(Mob mob) : base(mob) { }
    }
}
