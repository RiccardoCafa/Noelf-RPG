using System;

public class Skill
{
    private float damage;
    private float manac;
    private int Lvl = 1;
    private int block;
    private float bonusD;
    private char type;

    public void UseSkill(Player p, Skill a)
    {
        if (a.manac <= p.mana)
        {
            //p.alguma coisa -
        }



    }
    public Skill(float d, float m, int b, float bd, char t)
    {
        this.damage = d;
        this.manac = m;
        this.block = b;
        this.bonusD = bd;
        this.type = t;
    }
}
