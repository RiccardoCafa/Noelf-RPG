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
    public enum SkillAtribute
    {
        buff,
        debuff,
        damage

    }
    public enum AtributBonus
    {
        For,
        Int,
        dex
    }

    class Skill
    {

        public float Damage { get; }
        
        private float BonusMultiplier;
        private float DamageBonus;
        
        public int Lvl { get; set; } = 1;
        public int block { get; }

        private int manaCost;

        public bool Unlocked { get; set; } = false;

        private bool area;

        public string pathImage { get; set; }
        public string name { get; set; }
        public string description { get; set; } = "";

        public SkillType tipo;
        private AtributBonus atrib;

        public Skill(float damage, int manaCost, int blockLevel, float BonusMultiplier, SkillType tipoSkill, AtributBonus atrib, string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.Damage = damage;
            this.manaCost = manaCost;
            this.block = blockLevel;
            this.BonusMultiplier = BonusMultiplier;
            this.tipo = tipoSkill;
            this.atrib = atrib;
            this.name = name;
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

        public float UseSkill(Player player, Player Enemy)
        {
            if (manaCost <= player.Mp)
            {
                float Damagetotal;
                player.Mp -= manaCost;
                CalcBonus(player);
                Damagetotal = (Damage + DamageBonus) - (Damage + DamageBonus) * Enemy.ArmoCalc();
                Enemy.Hp -= Damagetotal;
                return Damagetotal;
            }
            return 0f;
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
