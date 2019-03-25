using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents
{
    abstract class Ent
    {
        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }

        public double Hp { get; set; }
        public int HpMax { get; set; }
        public double AtkSpd { get; set; }
        public double Run { get; set; }
        public double TimeMgcDmg { get; set; }
        public double Damage { get; set; }
    }
}
