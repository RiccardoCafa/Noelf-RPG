using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    class Class : IAtributes
    {
        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }
    }

    class Warrior : Class
    {
        public Warrior()
        {
            Str = 5;
            Spd = 0;
            Dex = 0;
            Con = 5;
            Mnd = 0;
        }
    }

    class Ranger : Class
    {
        public Ranger()
        {
            Str = 0;
            Spd = 5;
            Dex = 5;
            Con = 0;
            Mnd = 0;
        }
    }

    class Wizard : Class
    {
        public Wizard()
        {
            Str = 0;
            Spd = 0;
            Dex = 0;
            Con = 0;
            Mnd = 10;
        }
    }
}