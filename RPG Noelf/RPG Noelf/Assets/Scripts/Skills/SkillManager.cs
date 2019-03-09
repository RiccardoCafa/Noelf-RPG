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
        List<Skill> SkillList;
        public  SkillManager(){
            SkillList = new List<Skill>();

        }
        
        public void TestLvl(Player a, Skill b)
        {
            if (a.Level > b.block)
            {
                UpSkill(b);
            }
            else
            {
                //mensagem
            }
        }
        public void UpSkill(Skill b)
        {
            if (b.type == 'P')
            {
                if (b.Lvl <= 15)
                {
                    b.Lvl++;
                }
            }
            else if (b.type == 'R')
            {
                if (b.Lvl < 10)
                {
                    b.Lvl++;
                }
            }
            else
            {
                if (b.Lvl < 25)
                {
                    b.Lvl++;
                }
            }

        }
        public void TrocaSkill(Skill velha, Skill nova){
	        if(velha.type == 'P'){
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
    


