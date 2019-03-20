using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Skin : MobDecorator
    {
        private Animal TpAnimal { get; set; }

        public Skin(Mob mob, int code) : base(mob)
        {
            TpAnimal = animalCode[code];
            mob.code += code;
            switch (TpAnimal)
            {
                case Animal.dragon:
                    mob = new DragonLegs(mob);
                    break;
                case Animal.kong:
                    mob = new KongLegs(mob);
                    break;
                case Animal.lizard:
                    mob = new LizardLegs(mob);
                    break;
                case Animal.bison:
                    mob = new BisonLegs(mob);
                    break;
                case Animal.cat:
                    mob = new CatLegs(mob);
                    break;
            }
            Update(mob);
        }
    }
}
