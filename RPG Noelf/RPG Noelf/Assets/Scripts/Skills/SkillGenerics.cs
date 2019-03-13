using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    abstract class SkillGenerics
    {
        public int Lvl { get; set; } = 1;
        public int block { get; set; }
        public double Amplificator { get; set; }
        public double manaCost { get; set; }
        public double cooldown { get; set; }
        public bool Unlocked { get; set; } = false;
        public string pathImage { get; set; }
        public string name { get; set; }
        public string description { get; set; } = "";
    }
}
