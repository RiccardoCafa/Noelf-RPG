using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class Skill
    {
        public float damage { get; }
        private float manac;
        public int Lvl { get; set; } = 1;
        public int block { get; }
        private float bonusD;
        private float DamageBonus;
        public char type { get; }
        private Boolean area;
        private char atrib;

        public void UseSkill(Player p, Skill a,Player Enemy)
        {
            if (a.manac <= p.mana)
            {
                p.mana -= a.manac;
                Enemy.Hp -= damage + bonusD;
            }
            


        }
        public Skill(float d, float m, int b, float bd, char t,char a)
        {
            this.damage = d;
            this.manac = m;
            this.block = b;
            this.bonusD = bd;
            this.type = t;
            this.atrib = a;
        }
        public void CalcBonus(Player calcP)
        {
            if(atrib == 'F')
            {
                DamageBonus = calcP.force * bonusD; 
            }
        }
    }
}
