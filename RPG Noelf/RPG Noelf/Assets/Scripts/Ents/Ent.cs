using System;

namespace RPG_Noelf.Assets.Scripts.Ents
{
    public abstract class Ent
    {
        public DynamicSolid box;

        public int Str;
        public int Spd;
        public int Dex;
        public int Con;
        public int Mnd;

        public double Hp;
        public int HpMax;
        public double AtkSpd;
        public double Run;
        public double TimeMgcDmg;
        public double Damage;
        public double BonusChanceCrit = 1;
        public double Armor;
        public Level level;

        public double DamageBuff;
        public double ArmorBuff;
        public double ArmorEquip;
        public double AtkSpeedBuff;

        protected readonly string[] parts = { "eye", "hair", "head", "body", "arms", "legs" };
        protected readonly string[] sides = { "d", "e" };

        public void ApplyDerivedAttributes()
        {
            HpMax = Con * 6 + level.actuallevel * 2;
            Hp = HpMax;
            AtkSpd = 2 - (1.25 * Dex + 1.5 * Spd) / 100;
            Run = Math.Pow(Spd, 0.333) * 4 / 5 + 3;
            TimeMgcDmg = 0.45 * Mnd;
            Damage = Str;
        }

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
