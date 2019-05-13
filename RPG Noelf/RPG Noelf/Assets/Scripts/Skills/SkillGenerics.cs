﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    public abstract class SkillGenerics //atributos e funcoes genericas de skills 
    {
        public SkillType tipo { get; set; }
        public bool area;
        public AtributBonus atrib { get; set; }
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
        public double BonusMultiplier { get; set; }
        public double DamageBonus { get; set; }
        public SkillTypeBuff tipobuff { get; set; }
        public Element tipoatributo { get; set; }
        public double Buff { get; set; }
        public double Timer { get; set; }
        public double CountTime;
        public double CountBuffTime;
        public BuffDebuffTypes Buffer { get; set; }
        public bool Useabilite = true;
        public bool Usetroca = false;

        public void CalcBonus(Ent calcP)
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
        public string GetTypeString()
        {
            switch (tipo)
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
        public abstract bool UseSkill(Ent player, Ent Enemy);
        public abstract bool TurnBasicSkill(Ent player, Ent Enemy);
    }
}
