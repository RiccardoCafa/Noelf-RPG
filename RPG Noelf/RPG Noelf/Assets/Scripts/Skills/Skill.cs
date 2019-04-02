using RPG_Noelf.Assets.Scripts.Ents;
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
        agl
    }


    class Skill : SkillGenerics
    {
       //Skill que apenas causam dano

        public Skill(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        

        public override bool UseSkill(Ent player, Ent Enemy)
        {
            return false;
            /*
            if (manaCost <= player.Mp)
            {
                CalcBonus(player);
                Damage = Damage + Amplificator * Lvl;
                Enemy.BeHit(player.Hit(DamageBonus));
                return true;
            }
            return false;*/
        }


        

    }
}
