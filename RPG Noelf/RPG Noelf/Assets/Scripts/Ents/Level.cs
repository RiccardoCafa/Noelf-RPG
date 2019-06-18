using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents
{
    public class Level
    {
        public const int MaxLevel = 100;
        public int actuallevel { get; set; }
        public int actualEXP { get; set; }
        //public string xPBar = "imagem da barra de XP";
        public double bonusXP { get; set; }
        public bool ableToUp { get; set; }
        public int EXPlim { get; set; }
        private Player player;

        public Level(int init_level, Player player)
        {
            this.player = player;
            actualEXP = 0;
            actuallevel = init_level;
            bonusXP = 0;
            if (init_level < MaxLevel)
            {
                ableToUp = true;
            }
            else
            {
                ableToUp = false;
            }
            EXPlim = init_level * 100;
        }

        //função pra upar o level
        public bool UpLevel()
        {
            actualEXP = 0;
            if (actuallevel + 1 < MaxLevel)
            {
                actuallevel = actuallevel + 1;
                EXPlim = actuallevel * 100;
                if(actuallevel == MaxLevel)
                {
                    ableToUp = false;
                }
                if (player != null) player._SkillManager.SkillPoints++;
                if (player != null) player._Class.StatsPoints++;
            }
            return true;


        }

        //função pra ganhar XP
        public bool GainEXP(int qtdExp)
        {
            actualEXP += qtdExp;
            bool upou = false;
            while (actualEXP>=EXPlim)
            {
                int sobra = EXPlim - actualEXP;
                //actualEXP = 0;
                UpLevel();
                upou = true;
                actualEXP = sobra;
            }
            return upou;
        }
    }
}
