﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class SkillManager
    {
        public List<SkillGenerics> SkillList { get; set; }
        public SkillGenerics[] SkillBar { get; set; }
        public SkillGenerics Passive { get; set; }
        public Player myPlayer;

        public int SkillPoints { get; set; }

        public SkillManager(Player myPlayer)
        {
            SkillPoints = 0;
            this.myPlayer = myPlayer;
            SkillList = new List<SkillGenerics>();
            SkillBar = new Skill[4];
        }

        public void SetWarriorPassive(string pathImage, string name)
        {
            Passive = new SkillBuff(1.05,0,0,0,0,1,SkillTypeBuff.buff,BuffDebuffTypes.dmg,name, pathImage);
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }
        public void SetArcherPassive(string pathImage, string name)//ainda tem que mexer
        {
            Passive = new SkillBuff(1.05, 0, 0, 0, 0, 1, SkillTypeBuff.buff, BuffDebuffTypes.dmg, name, pathImage);
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }
        public void SetMagePassive(string pathImage, string name)//aqui tb
        {
            Passive = new SkillBuff(1.05, 0, 0, 0, 0, 1, SkillTypeBuff.buff, BuffDebuffTypes.dmg, name, pathImage);
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }

        public void MakeSkillType1(double damage, double manaCost, double cooldown, double Amplificator, int blockLevel, double BonusMultiplier, SkillType tipoSkill, AtributBonus atrib, string pathImage, string name)
        {
            SkillList.Add(new Skill(damage, manaCost, cooldown,Amplificator, blockLevel, BonusMultiplier, Type, atrib, pathImage, name));
        }
        public void MakeSkillType2(double buff, double amplificador, double customana, double cooldown, double timer, int blocklvl, SkillTypeBuff tipobuff, BuffDebuffTypes buffer, string name, string pathImage)
        {
            SkillList.Add(new SkillBuff(buff, amplificador, customana, cooldown, timer, blocklvl, tipobuff, buffer, name, pathImage));
        }
        public void AddSkillToBar(Skill s, int index)
        {
            SkillBar[index] = s;
        }
        
        private bool TestLevelBlock(Skill skill)
        {
            if (myPlayer.Level > skill.block)
            {
                return true;
            }

            return false;
        }

        public bool UnlockSkill(int index)
        {
            if(myPlayer.Level >= SkillList.ElementAt(index).block)
            {
                SkillList.ElementAt(index).Unlocked = true;
                SkillPoints--;
                return true;
            }
            return false;
        }

        public bool UpSkill(Skill skill)
        {
            int MinimiumLevel = 0;

            if (SkillPoints <= 0 || !TestLevelBlock(skill))
            {
                return false;
            }

            if (skill.tipo == SkillType.ultimate)
            {
                MinimiumLevel = 10;
            }
            else if (skill.tipo == SkillType.passive)
            {
                MinimiumLevel = 15;
            }
            else if(skill.tipo == SkillType.habilite)
            {
                MinimiumLevel = 25;
            }

            if (skill.Lvl < MinimiumLevel)
            {
                skill.Lvl++;
                SkillPoints--;
                skill.Unlocked = true;
                return true;
            } else
            {
                return false;
            }
        }
	}
}
    


