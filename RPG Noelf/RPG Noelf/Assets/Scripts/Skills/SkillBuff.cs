using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.PlayerFolder;


namespace RPG_Noelf.Assets.Scripts.Skills
{
    public enum SkillTypeBuff {
        normal,
        buff,
        debuff
    }
    public enum Element
    {
        Fire,
        Ice,
        Common,
        Poison
    }
    public class SkillCritical : SkillGenerics //skills com efeitos
    {
        public SkillCritical(string pathImage, string name)
        {
            this.name = name;
            this.pathImage = pathImage;
        }
        public override void RevertSkill(Ent ent)
        {
            ent.BonusChanceCrit -= Buff + Amplificator * Lvl;
        }
        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                player.BonusChanceCrit += Buff + Amplificator * Lvl;
                return  0;   
            }
            return 0;
        }

    }
    class SkillDash : SkillGenerics
    {
        public SkillDash(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }
        public override void RevertSkill(Ent ent)
        {
            
        }
        public override double UseSkill(Ent player, Ent Enemy)

        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                return DamageBonus + Damage;
            }
            return 0;
        }
    }
    class SkillHidden : SkillGenerics
    {
        public SkillHidden(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }
        public override void RevertSkill(Ent ent)
        {
            
        }
        public override double UseSkill(Ent player, Ent Enemy)

        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                return DamageBonus + Damage;
            }
            return 0;
        }
    }
    class SkillBroken : SkillGenerics
    {
        public SkillBroken(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            ent.ArmorBuff += Buff + Amplificator * Lvl;
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                Enemy.ArmorBuff -= (Buff + Amplificator * Lvl);
                return DamageBonus + Damage;
            }
            else
            {
                return 0;
            }
        }
    }
    class SkillPrison : SkillGenerics
    {
        public SkillPrison(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            ent.Spd = (int)oldstatus;
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                oldstatus = Enemy.Spd;
                timer = timer + Amplificator * Lvl;
                CalcBonus(player);
                Enemy.Spd = 0;
                return DamageBonus + Damage;
            }
            else
            {
                return 0;
            }
               
        }
    }
    class SkillSilence : SkillGenerics
    {
        public SkillSilence(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            ent.Damage = oldstatus;
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                oldstatus = Enemy.Damage;
                timer = timer + Amplificator * Lvl;
                CalcBonus(player);
                Enemy.Damage = 0;
                return DamageBonus + Damage;
            }
            else
            {
                return 0;
            }
               
        }
       
    }
    class SkillDex : SkillGenerics
    {
        public SkillDex(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            ent.Dex = (int)oldstatus;
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                oldstatus = player.Dex;
                player.Dex += (int)(player.Dex * (Buff + Amplificator * Lvl));
                return 0;
            }
            else
            {
                return 0;
            }
                
        }
        
    }
    class SkillDmgBuff : SkillGenerics
    {
        public SkillDmgBuff(string pathImage,string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            ent.DamageBuff -= ent.Damage * (Buff + Amplificator * Lvl);
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                player.DamageBuff += player.Damage * (Buff + Amplificator * Lvl);
                return 0;
            }
            else
            {
                return 0;
            }
                
        }
    }
    class SkillResbuff : SkillGenerics
    {
        public SkillResbuff(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            ent.ArmorBuff -= (Buff + Amplificator * Lvl);
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                player.ArmorBuff += (Buff + Amplificator * Lvl);
                return 0;
            }
            else
            {
                return 0;
            }
                
        }
    }
    class SkillSlowbuff : SkillGenerics
    {
        public SkillSlowbuff(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            ent.Spd = (int)oldstatus;
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                oldstatus = Enemy.Spd;
                CalcBonus(player);
                Enemy.Spd = (int)(Enemy.Spd * (Buff + Amplificator * Lvl));
                return DamageBonus + Damage;
            }
            else
            {
                return 0;
            }
            
        }
    }
    class SkillDobleHit : SkillGenerics
    {
        public SkillDobleHit(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        public override void RevertSkill(Ent ent)
        {
            
        }

        public override double UseSkill(Ent player, Ent Enemy)
        {
            if (!(player is Player)) return 0;
            if (manaCost <= (player as Player).Mp)
            {
                player.Mp -= manaCost;
                return Damage * 2;
            }
            else
            {
                return 0;
            }
            
        }
    }
}


