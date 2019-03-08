using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Player
{
    abstract class Race : IAtributes
    {
        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }
    }

    class Human : Race
    {
        public Human()
        {
            Str = 6;
            Spd = 4;
            Dex = 5;
            Con = 4;
            Mnd = 6;
        }
    }

    class Orc : Race
    {
        public Orc()
        {
            Str = 9;
            Spd = 2;
            Dex = 3;
            Con = 9;
            Mnd = 2;
        }
    }

    class Elf : Race
    {
        public Elf()
        {
            Str = 2;
            Spd = 7;
            Dex = 7;
            Con = 2;
            Mnd = 7;
        }
    }
}