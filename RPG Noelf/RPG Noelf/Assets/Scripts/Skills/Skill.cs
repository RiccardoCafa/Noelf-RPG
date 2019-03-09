using RPG_Noelf.Assets.Scripts.PlayerFolder;
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
        private int manac;
        public int Lvl { get; set; } = 1;
        public int block { get; }
        private float bonusD;
        private float DamageBonus;
        public char type { get; }
        private bool area;
        private char atrib;
        
        public void UseSkill(Player p, Skill a,Player Enemy)
        {
            if (a.manac <= p.Mp)
            {
                float Damagetotal;
                p.Mp -= a.manac;
                CalcBonus(p);
                p.ArmoCalc();
                Damagetotal = (damage + bonusD) * p.Armor;
                Enemy.Hp -= Damagetotal;
            }
            


        }
        public Skill(float d, int m, int b, float bd, char t,char a)
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
                DamageBonus = calcP.Str * bonusD; 
            }else if(atrib == 'I'){
                DamageBonus = calcP.Mnd * bonusD;
            }else{
                DamageBonus = calcP.Dex * bonusD;
            }
        }
    }
}
