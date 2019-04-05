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
        public double BonusChanceCrit { get; set; } = 1;
        public double Armor { get; set; }

        public double DamageBuff { get; set; }
        public double ArmorBuff { get; set; }
        public double AtkSpeedBuff { get; set; }

        protected readonly string[] parts = { "eye", "hair", "head", "body", "arms", "legs" };
        protected readonly string[] sides = { "d", "e" };

        public double Hit(double bonusDamage)//golpeia
        {
            Random random = new Random();
            double dmg100 = random.NextDouble() * 100;
            if (dmg100 < 1 / Dex * 0.05) return 0;//errou
            else if (dmg100 < Dex * BonusChanceCrit * 0.1) return bonusDamage + Damage * dmg100;//acertou
            else return bonusDamage + Damage * dmg100 * 2;//critico
        }

        public void BeHit(double damage)//tratamento do dano levado
        {
            Hp -= damage / (1 + Con * 0.02 + Armor);
        }
    }
}
