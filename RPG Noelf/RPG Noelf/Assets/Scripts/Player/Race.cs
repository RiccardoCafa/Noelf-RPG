using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Player
{
    abstract class Race : IAtributes
    {
        public Class Class { get; set; }
        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }
    }

    class Human : Race
    {
        public Human(Class @class)
        {
            Class = @class;
            Str = 6 + @class.Str;
            Spd = 4 + @class.Spd;
            Dex = 5 + @class.Dex;
            Con = 4 + @class.Con;
            Mnd = 6 + @class.Mnd;
        }
    }

    class Orc : Race
    {
        public Orc(Class @class)
        {
            Class = @class;
            Str = 9 + @class.Str;
            Spd = 2 + @class.Spd;
            Dex = 3 + @class.Dex;
            Con = 9 + @class.Con;
            Mnd = 2 + @class.Mnd;
        }
    }

    class Elf : Race
    {
        public Elf(Class @class)
        {
            Class = @class;
            Str = 2 + @class.Str;
            Spd = 7 + @class.Spd;
            Dex = 7 + @class.Dex;
            Con = 2 + @class.Con;
            Mnd = 7 + @class.Mnd;
        }
    }
}