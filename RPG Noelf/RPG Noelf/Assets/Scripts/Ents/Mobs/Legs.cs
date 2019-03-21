using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Legs : IParts
    {
        virtual public void UpdateMob(Mob mob) { }

        public IParts Choose(int code)
        {
            switch (code)
            {
                case 0:
                    return new DragonLegs();
                case 1:
                    return new KongLegs();
                case 2:
                    return new LizardLegs();
                case 3:
                    return new BisonLegs();
                case 4:
                    return new CatLegs();
                default:
                    return null;
            }
        }
    }

    class DragonLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Str += 1;
            mob.Vulnerable.Add(Element.Ice);
        }
    }

    class KongLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Spd += 10;
        }
    }

    class LizardLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Mnd += 1;
            mob.Spd = 0;
        }
    }

    class BisonLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Con += 1;
            mob.Meek = true;
        }
    }

    class CatLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Dex += 1;
            mob.Vulnerable.Add(Element.Common);
        }
    }
}