using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.PlayerFolder;


namespace RPG_Noelf.Assets.Scripts.Skills
{
    public enum SkillTypeBuff
    {
        debuff,
        buff,
        normal
    }
    public enum BuffDebuffTypes//todos os tipos possiveis de efeitos
    {
        Res,//ok
        Dex,//ok
        Dmg,//ok
        Slow,//ok
        Broken,//ok
        Prison,//ok
        Silence,//ok
        Throw,//classe a parte...
        Critical,//ok
        Dash,//classe a parte
        Hidden,//classe a parte
        Double//ok
    }
    public enum Element
    {
        Fire,
        Ice,
        Common,
        Poison
    }

    public class SkillBuff : SkillGenerics //skills com efeitos
    {
       
        

        public SkillBuff(string pathImage, string name)
        {
            this.name = name;
            this.pathImage = pathImage;
        }
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            return false;
            /*if (player.Mp >= manaCost)
            {
                if (Buffer == BuffDebuffTypes.Dex)
                {
                    player.Dex = (int)(player.Dex * (Buff + Amplificator * Lvl));
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Dmg)
                {
                    player.Damage = player.Damage * (Buff + Amplificator * Lvl);
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Res)
                {
                    player.Armor = player.Armor * (Buff + Amplificator * Lvl);
                    return true;
                }
                if (Buffer == BuffDebuffTypes.Slow)
                {
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Spd = (int)(Enemy.Spd * (Buff + Amplificator * Lvl));
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Silence)
                {
                    Timer = Timer + Amplificator * Lvl;
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Damage = 0;
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Prison)
                {
                    Timer = Timer + Amplificator * Lvl;
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Spd = 0;
                    return true;
                }else if(Buffer == BuffDebuffTypes.Double)
                {
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.BeHit(player.Hit(DamageBonus));
                    return true;
                }else if(Buffer == BuffDebuffTypes.Critical)
                {
                    player.BonusChanceCrit = Buff + Amplificator * Lvl;
                    return true;
                }else if (Buffer == BuffDebuffTypes.Broken)
                {
                    Enemy.ArmorBuff = (Buff + Amplificator * Lvl) * -1;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;*/
        }
        
    }
    class SkillDash : SkillGenerics
    {
        public SkillDash(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }
        public override bool UseSkill(Ent player, Ent Enemy)

        {

            return false;
        }
    }
    class SkillHidden : SkillGenerics
    {
        public SkillHidden(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }
        public override bool UseSkill(Ent player, Ent Enemy)

        {
            return false;
        }
    }
    class SkillThrow : SkillGenerics
    {
        public SkillThrow(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override bool UseSkill(Ent player, Ent Enemy)
        {
            return false;
        }
    }

}


