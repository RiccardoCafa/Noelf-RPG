using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class SkillManager
    {
        public List<Skill> SkillList { get; set; }
        public Skill[] SkillBar { get; set; }
        public Skill Passive { get; set; }
        public Player myPlayer;

        public int SkillPoints { get; set; }

        public SkillManager(Player myPlayer)
        {
            SkillPoints = 0;
            this.myPlayer = myPlayer;
            SkillList = new List<Skill>();
            SkillBar = new Skill[4];
        }

        public void SetPassive(float BonusMultiplier, AtributBonus atrib, string pathImage, string name)
        {
            Passive = new Skill(0, 0, 1, BonusMultiplier, SkillType.passive, atrib, pathImage, name);
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }

        public void MakeSkill(float damage, int manaCost, int blockLevel, float BonusMultiplier, SkillType Type, AtributBonus atrib, string pathImage, string name)
        {
            SkillList.Add(new Skill(damage, manaCost, blockLevel, BonusMultiplier, Type, atrib, pathImage, name));
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
    


