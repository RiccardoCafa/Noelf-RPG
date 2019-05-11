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
        Critical,//ok
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
        
        public double oldstatus;
        public SkillBuff(string pathImage, string name)
        {
            this.name = name;
            this.pathImage = pathImage;
        }

        public override bool TurnBasicSkill(Ent player, Ent Enemy)
        {
            if (Buffer == BuffDebuffTypes.Dex)
            {
                player.Dex = (int)oldstatus;
                return true;
            }
            else if (Buffer == BuffDebuffTypes.Dmg)
            {
                player.Damage = oldstatus;
                return true;
            }
            else if (Buffer == BuffDebuffTypes.Res)
            {
                player.Armor = oldstatus;
                return true;
            }
            if (Buffer == BuffDebuffTypes.Slow)
            {

                Enemy.Spd = (int)oldstatus;
                return true;
            }
            else if (Buffer == BuffDebuffTypes.Silence)
            {
                Enemy.Damage = oldstatus;
                return true;
            }
            else if (Buffer == BuffDebuffTypes.Prison)
            {
                Enemy.Spd =(int)oldstatus;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool UseSkill(Ent player, Ent Enemy)
        {
               
            if (player.Mnd >= manaCost)
            {
                if (Buffer == BuffDebuffTypes.Dex)
                {
                    oldstatus = player.Dex;
                    player.Dex = (int)(player.Dex * (Buff + Amplificator * Lvl));
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Dmg)
                {
                    oldstatus = player.Damage;
                    player.Damage = player.Damage * (Buff + Amplificator * Lvl);
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Res)
                {
                    oldstatus = player.Armor;
                    player.Armor = player.Armor * (Buff + Amplificator * Lvl);
                    return true;
                }
                if (Buffer == BuffDebuffTypes.Slow)
                {
                    oldstatus = Enemy.Spd;
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Spd = (int)(Enemy.Spd * (Buff + Amplificator * Lvl));
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Silence)
                {
                    oldstatus = Enemy.Damage;
                    Timer = Timer + Amplificator * Lvl;
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Damage = 0;
                    return true;
                }
                else if (Buffer == BuffDebuffTypes.Prison)
                {
                    oldstatus = Enemy.Spd;
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
            return false;
        }
        
    }
    class SkillDash : SkillGenerics
    {
        public SkillDash(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override bool TurnBasicSkill(Ent player, Ent Enemy)
        {
            throw new NotImplementedException();
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
        public override bool TurnBasicSkill(Ent player, Ent Enemy)
        {
            return false;
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
        public override bool TurnBasicSkill(Ent player, Ent Enemy)
        {
            return false;
        }
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            return false;
        }
    }

}


