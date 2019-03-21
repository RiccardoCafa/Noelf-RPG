using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Runes
{
   

    class Rune
    {
      public string runename { get; set; }
      public Level actualRuneLevel {get;set;}
      public double chanceEffect { get; set; }
      public Element typeRune { get; set; }
      public double damage { get; set; }  
    
        public Rune(Element tpRune, int runeLevel)
        {
            typeRune = tpRune;
            actualRuneLevel = new Level(runeLevel);
        }


       




    }



    
}
