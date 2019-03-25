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
      public double chanceEffect { get; set; }
      public Element typeRune { get; set; }
      public Skill GenericSkill { get; set; }
    
        public Rune(Element tpRune, int runeLevel)
        {
            typeRune = tpRune;
            
        }


       




    }



    
}
