using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class Path : Atributes
    {
        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }
        public int Hp { get; set; }
    }

    class Warrior : Path
    {
        public Warrior()
        {
            Str = 12;
            Spd = 2;
            Dex = 7;
            Con = 9;
            Mnd = 0;
            Hp = 72;
        }
    }

    class Ranger : Path
    {
        public Ranger()
        {
            Str = 0;
            Spd = 14;
            Dex = 10;
            Con = 3;
            Mnd = 3;
            Hp = 24;
        }
    }

    class Wizard : Path
    {
        public Wizard()
        {
            Str = 0;
            Spd = 3;
            Dex = 4;
            Con = 0;
            Mnd = 23;
            Hp = 0;
        }
    }
}
