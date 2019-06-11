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
        
        public double oldstatus;
        public SkillCritical(string pathImage, string name)
        {
            this.name = name;
            this.pathImage = pathImage;
        }

        public override bool UseSkill(Ent player, Ent Enemy)
        {
               
            if (player.Mnd >= manaCost)
            {
                player.BonusChanceCrit = Buff + Amplificator * Lvl;
                return true;
                
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

     

        public override bool UseSkill(Ent player, Ent Enemy)

        {
            if (player.Mnd > manaCost)
            {
                return false;
            }
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
            if (player.Mnd > manaCost)
            {
                return false;
            }
            return false;
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
            ent.Armor += Buff + Amplificator * Lvl;
        }

        public override bool UseSkill(Ent player, Ent Enemy)
        {
            if (player.Mnd > manaCost)
            {
                Enemy.ArmorBuff -= (Buff + Amplificator * Lvl);
                return true;
            }
            else
            {
                return false;
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
        
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            if (player.Mnd > manaCost)
            {
                Timer = Timer + Amplificator * Lvl;
                CalcBonus(player);
                Enemy.BeHit(player.Hit(DamageBonus));
                Enemy.Spd = 0;
                return true;
            }
            else
            {
                return false;
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
       
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            if (player.Mnd > manaCost)
            {
                Timer = Timer + Amplificator * Lvl;
                CalcBonus(player);
                Enemy.BeHit(player.Hit(DamageBonus));
                Enemy.Damage = 0;
                return true;
            }
            else
            {
                return false;
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
    
        public override bool UseSkill(Ent player, Ent Enemy)
        {

            if (player.Mnd > manaCost)
            {
                player.Dex = (int)(player.Dex * (Buff + Amplificator * Lvl));
                return true;
            }
            else
            {
                return false;
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
       
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            if (player.Mnd > manaCost)
            {
                player.Damage = player.Damage * (Buff + Amplificator * Lvl);
                return true;
            }
            else
            {
                return false;
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
        
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            if (player.Mnd > manaCost)
            {
                player.Armor = player.Armor * (Buff + Amplificator * Lvl);
                return true;
            }
            else
            {
                return false;
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
      
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            if (player.Mnd > manaCost)
            {
                CalcBonus(player);
                Enemy.BeHit(player.Hit(DamageBonus));
                Enemy.Spd = (int)(Enemy.Spd * (Buff + Amplificator * Lvl));
                return true;
            }
            else
            {
                return false;
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
       
        public override bool UseSkill(Ent player, Ent Enemy)
        {
            if(player.Mnd > manaCost)
            {
                Enemy.BeHit(player.Hit(DamageBonus*2));
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}


