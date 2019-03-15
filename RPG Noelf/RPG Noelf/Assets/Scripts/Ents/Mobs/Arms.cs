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
        /* > Braços
         *   - Mnd +1
         */
        public DragonArms(Mob mob) : base(mob) { }
    }

    class KongArms : Arms
    {
        /* > Braços
         *   - For +1
         *   - attack +Poopoo()
         */
        public KongArms(Mob mob) : base(mob) { }
    }

    class LizardArms : Arms
    {
        /* > Braços
         *   - Spd +1
         */
        public LizardArms(Mob mob) : base(mob) { }
    }

    class BisonArms : Arms
    {
        /* > Braços
         *   - Con +1
         */
        public BisonArms(Mob mob) : base(mob) { }
    }

    class CatArms : Arms
    {
        /* > Braços
         *   - Dex +1
         *   - attack +Scratch()
         */
        public CatArms(Mob mob) : base(mob) { }
    }
}
