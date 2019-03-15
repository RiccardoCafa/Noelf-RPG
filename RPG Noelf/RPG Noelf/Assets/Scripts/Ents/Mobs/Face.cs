using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Face : MobDecorator
    {
        public Face(Mob mob) : base(mob) { }

        public new void Make() { base.Make(); }
    }

    class DragonFace : Face
    {
        /* > Face
         *   - Mnd +2
         *   - attack +Fireball()
         */
        public DragonFace(Mob mob) : base(mob) { }
    }

    class KongFace : Face
    {
        /* > Face
         *   - For +2
         */
        public KongFace(Mob mob) : base(mob) { }
    }

    class LizardFace : Face
    {
        /* > Face
         *   - Spd +2
         *   - attack +Lick()
         */
        public LizardFace(Mob mob) : base(mob) { }
    }

    class BisonFace : Face
    {
        /* > Face
         *   - Con +2
         *   - attack +Headache()
         */
        public BisonFace(Mob mob) : base(mob) { }
    }

    class CatFace : Face
    {
        /* > Face
         *   - Dex +2
         */
        public CatFace(Mob mob) : base(mob) { }
    }
}
