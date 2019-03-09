using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class SkillManager
    {
        List<Skill> SkillList;
        /*public void TestLvl(player a, Skill b)
        {
            if (a.lvl > b.block)
            {
                UpSkill(b);
            }
            else
            {
                //mensagem
            }
        }*/
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
        /*public void TrocaSkill(Player perso, Skill velha, Skill nova){
	        if(velha.type == 'P'){
	            //n pode
	            }else{
	                int indexnew, indexold;
                    Skill olds, news;
                    indexold = velha.FindIndex(a=>a.Prop == aProp);
	                indexnew = nova.FindIndex(a=>a.Prop == aProp);
                    olds = velha;
	                news = nova;
	                perso.SkillList.insert(news, indexold);
	                perso.SkillList.insert(olds, indexnew);
	            }

        }*/
	}
}
    

