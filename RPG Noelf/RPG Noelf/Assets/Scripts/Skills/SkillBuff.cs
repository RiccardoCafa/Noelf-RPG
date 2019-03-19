using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    enum SkillTypeBuff
    {
        debuff,
        buff,
        normal
    }
    enum BuffDebuffTypes
    {
        str,
        res,
        dex,
        dmg,
        slow,
        broken,
        prison,
        silence,
        lancar,
        critico,
        dash,
        hidden,
        doble
    }
    enum Element
    {
        Fire,
        Ice,
        Common,
        Poison
    }

    class SkillBuff : SkillGenerics
    {
        public double buff { get; set; }

        public double timer { get; set; }

        public BuffDebuffTypes buffer { get; set; }


        public SkillBuff(string name, string pathImage)
        {
            this.name = name;
            this.pathImage = pathImage;
        }
        public override bool UseSkill(Player player, Player Enemy)
        {

            if (buffer == BuffDebuffTypes.dex)
            {
                player.Dex = (int)(player.Dex * (buff + Amplificator * Lvl));
                return true;
            }
            else if (buffer == BuffDebuffTypes.dmg)
            {
                player.Damage = player.Damage * (buff + Amplificator * Lvl);
                return true;
            }
            else if (buffer == BuffDebuffTypes.res)
            {
                player.Armor = player.Armor * (buff + Amplificator * Lvl);
                return true;
            }
            if (buffer == BuffDebuffTypes.slow)
            {
                CalcBonus(player);
                Enemy.BeHit(player.Hit(DamageBonus));
                Enemy.Spd = (int)(Enemy.Spd * (buff + Amplificator * Lvl));
                return true;
            }
            else if (buffer == BuffDebuffTypes.silence)
            {
                timer = timer + Amplificator * Lvl;
                CalcBonus(player);
                Enemy.BeHit(player.Hit(DamageBonus));
                Enemy.Damage = 0;
                return true;
            }
            else if (buffer == BuffDebuffTypes.prison)
            {
                timer = timer + Amplificator * Lvl;
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
}
