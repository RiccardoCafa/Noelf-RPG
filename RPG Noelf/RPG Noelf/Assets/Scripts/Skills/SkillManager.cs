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
        
        public  SkillManager(){

            SkillList = new List<Skill>();

        }

        public void MakeSkill(float damage, int manaCost, int blockLevel, float BonusMultiplier, char Type, char atrib, string pathImage, string name)
        {
            SkillList.Add(new Skill(damage, manaCost, blockLevel, BonusMultiplier, Type, atrib, pathImage, name));
        }
        
        public bool TestLevelUp(Player player, Skill skill)
        {
            if (player.Level > skill.block)
            {
                return UpSkill(skill);
            }

            return false;
        }

        private bool UpSkill(Skill skill)
        {
            if (skill.Type == 'P')
            {
                if (skill.Lvl < 15)
                {
                    skill.Lvl++;
                    return true;
                }
            }
            else if (skill.Type == 'R')
            {
                if (skill.Lvl < 10)
                {
                    skill.Lvl++;
                    return true;
                }
            }
            else
            {
                if (skill.Lvl < 25)
                {
                    skill.Lvl++;
                    return true;
                }
            }
            return false;
        }

        public void ChangeSkill(Skill velha, Skill nova){
	        if(velha.Type == 'P'){
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
    

