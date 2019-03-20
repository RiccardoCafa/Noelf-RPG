using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    
    abstract class MobDecorator : Mob
    {
        public Mob Mob { get; set; }
        public static Animal[] animalCode = { Animal.dragon, Animal.kong, Animal.lizard, Animal.bison, Animal.cat };

        protected MobDecorator(Mob mob)
        {
            Mob = mob;
        }
    }
}
