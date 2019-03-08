using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Player
{
    class Player
    {
        public Race Race { get; set; }
        public int Hp { get; set; }
        public int HpMax { get; set; }
        public int Xp { get; private set; }
        public int XpLim { get; private set; }
        public int Mp { get; set; }
        public int MpMax { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public int Level { get; private set; } = 1;
        public double AtkSpd { get; set; }
        public Bag inventory;

        public bool XpLevel(int xp)//responde se passou de nivel ou nao, alem de upar
        {
            Xp += xp;
            if(Xp >= XpLim)
            {
                Xp -= XpLim;
                Level++;
                return true;
            }
            return false;
        }

        public void LevelUpdate(int str, int spd, int dex, int con, int mnd)//atualiza os atributos ao upar
        {
            HpMax = Race.Con * 6 + Level * 2;
            Hp = HpMax;
            XpLim = Level * 100 + (int) Math.Pow(1.5, Level / 4);
            MpMax = Race.Mnd * 5 + Level;
            Mp = MpMax;
            AtkSpd = 2 - 1.75 * Race.Spd / 100;
        }
    }
}
