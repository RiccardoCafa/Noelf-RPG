using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    abstract class Mob : Ent
    {
        public List<Action> Attacks { get; set; } = new List<Action>();
        public List<Element> Resistance { get; set; } = new List<Element>();
        public List<Element> Vulnerable { get; set; } = new List<Element>();
        public bool Meek { get; set; } = false;

        public Image Face { get; set; }
        public Image Body { get; set; }
        public Image[,] Arms { get; set; } = new Image[2, 2];
        public Image[,] Legs { get; set; } = new Image[2, 2];

        public int code;

        //abstract public void Make(Image face, Image body, Image[,] arms, Image[,] legs);
    }

    enum Animal
    {
        dragon, kong, lizard, bison, cat
    }
    /*
    class MobCore : Mob
    {
        public MobCore() { }

        override public void Make() { }

    }*/
}
