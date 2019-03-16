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

    public enum Element
    {
        common, fire, poison, ice
    }

    class Skill : SkillGenerics
    {
       

        public Skill(string pathImage, string name)
        {
            this.pathImage = pathImage;
            this.name = name;
        }

        

        public override bool UseSkill(Player player, Player Enemy)
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


        

    }
}
