using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    abstract class Mob : Ent
    {
        public List<Action> attacks;
        public List<Element> resistance;
        public List<Element> vulnerable;
        public bool meek = false;

        abstract public void Make();
    }

    enum Animal
    {
        dragon, kong, lizard, bison, cat
    }

    class MobCore : Mob
    {
        public MobCore() { }

        override public void Make() { }
    }
}
