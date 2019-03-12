using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Skills
{

    public enum SkillType
    {
        passive,
        active
    }

    class Skill
    {
        public double Damage { get; }
        private int manaCost;
        public int Lvl { get; set; } = 1;
        public int block { get; }
        private double BonusMultiplier;
        private double DamageBonus;
        public char Type { get; }
        private bool area;
        private char atrib;
        public string pathImage { get; set; }
        public string name { get; set; }
        
        public double UseSkill(Player player, Player Enemy)
        {
            if (manaCost <= player.Mp)
            {
                double Damagetotal;
                player.Mp -= manaCost;
                CalcBonus(player);
                //Enemy.ArmoCalc();
                Damagetotal = (Damage + DamageBonus) - (Damage + DamageBonus) * Enemy.Armor;
                Enemy.Hp -= Damagetotal;
                return Damagetotal;
            }
            return 0f;
        }

        public Skill(double damage, int manaCost, int blockLevel, double BonusMultiplier, char Type,char atrib, string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.Damage = damage;
            this.manaCost = manaCost;
            this.block = blockLevel;
            this.BonusMultiplier = BonusMultiplier;
            this.Type = Type;
            this.atrib = atrib;
            this.name = name;
        }

        public void CalcBonus(Player calcP)
        {
            if(atrib == 'F')
            {
                DamageBonus = calcP.Str * BonusMultiplier; 
            }else if(atrib == 'I'){
                DamageBonus = calcP.Mnd * BonusMultiplier;
            }else{
                DamageBonus = calcP.Dex * BonusMultiplier;
            }
        }

    }
}
