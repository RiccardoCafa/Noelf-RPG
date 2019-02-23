using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    abstract class Race : Atributes
    {
        public Path path { get; set; }
        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }
        public int Hp { get; set; }
        public Bag playerInventory;
    }

    class Human : Race
    {
        public Human(Path path)
        {
            this.path = path;
            Str = 16 + path.Str;
            Spd = 14 + path.Spd;
            Dex = 15 + path.Dex;
            Con = 14 + path.Con;
            Mnd = 16 + path.Mnd;
            Hp = 112 + path.Hp;
            playerInventory = new Bag();
        }
    }

    class Orc : Race
    {
        public Orc(Path path)
        {
            this.path = path;
            Str = 24 + path.Str;
            Spd = 10 + path.Spd;
            Dex = 11 + path.Dex;
            Con = 21 + path.Con;
            Mnd = 9 + path.Mnd;
            Hp = 168 + path.Hp;
            playerInventory = new Bag();
        }
    }

    class Elf : Race
    {
        public Elf(Path path)
        {
            this.path = path;
            Str = 10 + path.Str;
            Spd = 20 + path.Spd;
            Dex = 18 + path.Dex;
            Con = 11 + path.Con;
            Mnd = 16 + path.Mnd;
            Hp = 88 + path.Hp;
            playerInventory = new Bag();
        }
    }
}
