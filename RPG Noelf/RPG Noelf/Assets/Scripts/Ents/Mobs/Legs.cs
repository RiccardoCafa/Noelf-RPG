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
            mob.Str += (int)(1 + mob.level.actuallevel * 0.25);
            mob.Vulnerable.Add(Element.Ice);
        }
    }

    class KongLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Spd += (int)(10 + mob.level.actuallevel * 1.2);
        }
    }

    class LizardLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Mnd += (int)(1 + mob.level.actuallevel * 0.25);
            mob.Spd = (int)(1 + mob.level.actuallevel * 0.05);
        }
    }

    class BisonLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Con += (int)(1 + mob.level.actuallevel * 0.25);
            mob.Meek = true;
        }
    }

    class CatLegs : Legs
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Dex += (int)(1 + mob.level.actuallevel * 0.25);
            if (mob.Resistance.Contains(Element.Common)) mob.Resistance.Remove(Element.Common);
            else mob.Vulnerable.Add(Element.Common);
        }
    }
}