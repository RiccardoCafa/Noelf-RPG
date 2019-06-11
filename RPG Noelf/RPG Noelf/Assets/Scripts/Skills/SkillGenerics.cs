using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Skills
{//como faz o hit box e onde faz
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
        public bool Active = true;
        public double xoffset = 0;
        public double yoffset = 0;
        public double spd;
        public double gravity = 0;

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
        public abstract void RevertSkill(Ent ent);

        public void UpdateThrow(HitSolid hit, Ent ent)
        {
            double x, y;
            double width = 30;

            if (ent.box.lastHorizontalDirection == 1)
            {
                x = ent.box.Xf + xoffset;
                y = ent.box.Yi + 20 - yoffset;

            }
            else
            {
                x = ent.box.Xi - width - xoffset;
                y = ent.box.Yi + 20 - yoffset;
            }

            hit.Xi = x;
            hit.Yi = y;
            hit.Width = 20;
            hit.Who = ent.box as DynamicSolid;
            hit.speed = spd;
            
            if (spd != 0)
            {
                if (ent.box.lastHorizontalDirection == 1)
                {
                    hit.moveRight = true;
                }
                else
                {
                    hit.moveLeft = true;
                }
            }

            hit.g = gravity;
        }

        public HitSolid Throw (Ent ent)
        {
            double x, y;
            double width = 30;
            HitSolid myHit;
            if (ent.box.lastHorizontalDirection == 1)
            {
                x = ent.box.Xf + xoffset;
                y = ent.box.Yi + 20 - yoffset;

            }
            else
            {
                x = ent.box.Xi - width - xoffset;
                y = ent.box.Yi + 20 - yoffset;
            }

            myHit = new HitSolid(x, y, width, 20, ent.box as DynamicSolid, spd);

            if (spd != 0)
            {
                if (ent.box.lastHorizontalDirection == 1)
                {
                    myHit.moveRight = true;
                }
                else
                {
                    myHit.moveLeft = true;
                }
            }

            myHit.g = gravity;

            return myHit;
        }

    }
}
