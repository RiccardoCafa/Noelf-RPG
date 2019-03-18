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
    enum BuffDebuffTypes//todos os tipos possiveis de efeitos
    {
        Res,//ok
        Dex,//ok
        Dmg,//ok
        Slow,//ok
        Broken,//ok
        Prison,//ok
        Silence,//ok
        Throw,//classe a parte...
        Critical,//ainda para ser feita
        Dash,//classe a parte
        Hidden,//classe a parte
        Double//ok
    }
    enum Element
    {
        fire,
        ice,
        none,
        poison
    }

    class SkillBuff : SkillGenerics //skills com efeitos
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
            if (player.Mp >= manaCost)
            {
                if (buffer == BuffDebuffTypes.Dex)
                {
                    player.Dex = (int)(player.Dex * (buff + Amplificator * Lvl));
                    return true;
                }
                else if (buffer == BuffDebuffTypes.Dmg)
                {
                    player.Damage = player.Damage * (buff + Amplificator * Lvl);
                    return true;
                }
                else if (buffer == BuffDebuffTypes.Res)
                {
                    player.Armor = player.Armor * (buff + Amplificator * Lvl);
                    return true;
                }
                if (buffer == BuffDebuffTypes.Slow)
                {
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Spd = (int)(Enemy.Spd * (buff + Amplificator * Lvl));
                    return true;
                }
                else if (buffer == BuffDebuffTypes.Silence)
                {
                    timer = timer + Amplificator * Lvl;
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Damage = 0;
                    return true;
                }
                else if (buffer == BuffDebuffTypes.Prison)
                {
                    timer = timer + Amplificator * Lvl;
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Spd = 0;
                    return true;
                }else if(buffer == BuffDebuffTypes.Double)
                {
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.BeHit(player.Hit(DamageBonus));
                    return true;
                }else if(buffer == BuffDebuffTypes.Critical)
                {
                    return true;
                }else if (buffer == BuffDebuffTypes.Broken)
                {
                    Enemy.ArmorBuff = (buff + Amplificator * Lvl) * -1;
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
}
