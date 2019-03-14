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
        silence
    }


    class SkillBuff : SkillGenerics
    {
        private double buff;

        private double timer;

        public BuffDebuffTypes buffer;


        public SkillBuff(double damage,double buff,double amplificador,double customana,double cooldown,double timer,int blocklvl,SkillTypeBuff tipobuff, BuffDebuffTypes buffer, SkillType tipoSkill, AtributBonus atrib, string name,string pathImage)
        {
            this.Damage = damage;
            this.pathImage = pathImage;
            this.manaCost = customana;
            this.block = blocklvl;
            this.Amplificator = amplificador;
            this.tipobuff = tipobuff;
            this.buff = buff;
            this.name = name;
            this.buffer = buffer;
            this.cooldown = cooldown;
            this.timer = timer;
            this.tipo = tipoSkill;
            this.atrib = atrib;
        }
        public override bool UseSkill(Player player,Player Enemy)
        {
            if(tipobuff == SkillTypeBuff.buff)
            {
                if(buffer == BuffDebuffTypes.dex)
                {
                    player.Dex = (int)(player.Dex * (buff + Amplificator * Lvl));
                    return true;
                }else if(buffer == BuffDebuffTypes.dmg)
                {
                    player.Damage = player.Damage * (buff + Amplificator * Lvl);
                    return true;
                }else if(buffer == BuffDebuffTypes.res)
                {
                    player.Armor = player.Armor * (buff + Amplificator*Lvl);
                    return true;
                }else
                {
                    return false;
                }
            }else if(tipobuff == SkillTypeBuff.debuff)
            {
                if(buffer == BuffDebuffTypes.slow)
                {
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Spd = (int)(Enemy.Spd * (buff + Amplificator * Lvl));
                    return true;
                }else if (buffer == BuffDebuffTypes.silence)
                {
                    timer = timer + Amplificator * Lvl;
                    CalcBonus(player);
                    Enemy.BeHit(player.Hit(DamageBonus));
                    Enemy.Damage = 0;
                    return true;
                }else if (buffer == BuffDebuffTypes.prison)
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

            return false;
        }
    }
}
