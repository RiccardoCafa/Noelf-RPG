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
        public Player myPlayer;

        public int SkillPoints { get; set; }

        public SkillManager(Player myPlayer)
        {
            SkillPoints = 0;
            this.myPlayer = myPlayer;
            SkillList = new List<Skill>();
        }

        public void MakeSkill(float damage, int manaCost, int blockLevel, float BonusMultiplier, SkillType Type, AtributBonus atrib, string pathImage, string name)
        {
            SkillList.Add(new Skill(damage, manaCost, blockLevel, BonusMultiplier, Type, atrib, pathImage, name));
        }
        
        private bool TestLevelUp(Skill skill)
        {
            if (myPlayer.Level > skill.block)
            {
                return true;
            }

            return false;
        }

        public bool UpSkill(Skill skill)
        {
            int MinimiumLevel;

            if (SkillPoints <= 0 || !TestLevelUp(skill))
            {
                return false;
            }
            else if (skill.tipo == SkillType.ultimate)

            if (skill.tipo == SkillType.ultimate)
            {
                MinimiumLevel = 10;
            }
            else if (skill.tipo == SkillType.passive)
            {
                MinimiumLevel = 15;
            }
            else
            {
                MinimiumLevel = 25;
            }

            if (skill.Lvl < MinimiumLevel)
            {
                skill.Lvl++;
                SkillPoints--;
                return true;
            } else
            {
                return false;
            }
        }

        public void ChangeSkill(Skill velha, Skill nova){
	        if(velha.tipo == SkillType.passive){
	            //n pode
	            }else{
	                int indexnew, indexold;
                    Skill olds, news;
                    indexold = SkillList.IndexOf(velha);
	                indexnew = SkillList.IndexOf(nova);
                    olds = velha;
	                news = nova;
                    SkillList.Insert(indexold, news);
                    SkillList.Insert(indexnew, olds);
	            }

        }
	}
}
    


