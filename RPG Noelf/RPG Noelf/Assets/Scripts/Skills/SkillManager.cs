using System;

public class SkillManager
{

    public void TestLvl(Player a, Skill b)
    {
        if (a.lvl > b.block)
        {
            UpSkill(b);
        }
        else
        {
            //mensagem
        }
    }
    public void Upskill(Skill b)
    {
        if (b.type == "P")
        {
            if (b.lvl <= 15)
            {
                b.lvl++;
            }
        }
        else if (b.type == "R")
        {
            if (b.lvl < 10)
            {
                b.lvl++;
            }
        }
        else
        {
            if (b.lvl < 25)
            {
                b.lvl++;
            }
        }

    }

    public void TrocaSkill(Player perso, Skill old, Skill new){
	if(old.type == "P"){
	//n pode
	    }else{
	        int indexnew, indexold;
    Skill olds, news;
    indexold = old.FindIndex(a=>a.Prop == aProp);
	        indexnew = new.FindIndex(a=>a.Prop == aProp);
    olds = old;
	        news = new;
	        perso.SkillList.insert(news, indexold);
	        perso.SkillList.insert(olds, indexnew);
	} 


}



}
