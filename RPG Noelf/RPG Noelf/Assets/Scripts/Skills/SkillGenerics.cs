using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    abstract class SkillGenerics
    {
        public SkillType tipo;
        private bool area;
        public AtributBonus atrib;
        public int Lvl { get; set; } = 1;
        public double Damage { get; set; }
        public int block { get; set; }
        public double Amplificator { get; set; }
        public double manaCost { get; set; }
        public double cooldown { get; set; }
        public bool Unlocked { get; set; } = false;
        public string pathImage { get; set; }
        public string name { get; set; }
        public string description { get; set; } = "";
        public double BonusMultiplier;
        public double DamageBonus;


        public void CalcBonus(Player calcP)
        {
            if (atrib == AtributBonus.For)
            {
                DamageBonus = calcP.Str * BonusMultiplier;
            }
            else if (atrib == AtributBonus.Int)
            {
                DamageBonus = calcP.Mnd * BonusMultiplier;
            }
            else
            {
                DamageBonus = calcP.Dex * BonusMultiplier;
            }
        }
    }
}
