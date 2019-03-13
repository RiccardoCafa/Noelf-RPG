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
        habilite,
        ultimate
    }
    public enum AtributBonus
    {
        For,
        Int,
        dex,
        cons
    }

    class Skill : SkillGenerics
    {


        public double Damage { get; set; }
        
        private double BonusMultiplier;
        private double DamageBonus;
        

       
        
        private bool area;


        public SkillType tipo;
        private AtributBonus atrib;

        public Skill(double damage, double manaCost, double cooldown, double Amplificator, int blockLevel, double BonusMultiplier, SkillType tipoSkill, AtributBonus atrib, string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.Damage = damage;
            this.manaCost = manaCost;
            this.block = blockLevel;
            this.BonusMultiplier = BonusMultiplier;
            this.tipo = tipoSkill;
            this.atrib = atrib;
            this.name = name;
            this.cooldown = cooldown;
            this.Amplificator = Amplificator;
        }

        public string GetTypeString()
        {
            switch(tipo)
            {
                case SkillType.habilite:
                    return "Ativa";
                case SkillType.passive:
                    return "Passiva";
                case SkillType.ultimate:
                    return "Ultimate";
            }
            return "";
        }

        public bool UseSkill(Player player, Player Enemy)
        {
            if (manaCost <= player.Mp)
            {
                CalcBonus(player);
                Damage = Damage + Amplificator * Lvl;
                Enemy.BeHit(player.Hit(DamageBonus));
                return true;
            }
            return false;
        }


        public void CalcBonus(Player calcP)
        {
            if(atrib == AtributBonus.For)
            {
                DamageBonus = calcP.Str * BonusMultiplier; 
            }else if(atrib == AtributBonus.Int){
                DamageBonus = calcP.Mnd * BonusMultiplier;
            }else{
                DamageBonus = calcP.Dex * BonusMultiplier;
            }
        }

    }
}
