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
    class Body : IParts
    {
        virtual public void UpdateMob(Mob mob) { }

        public IParts Choose(int code)
        {
            switch (code)
            {
                case 0:
                    return new DragonBody();
                case 1:
                    return new KongBody();
                case 2:
                    return new LizardBody();
                case 3:
                    return new BisonBody();
                case 4:
                    return new CatBody();
                default:
                    return null;
            }
        }
    }

    class DragonBody : Body
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Str += (int)(3 + mob.level.actuallevel * 0.75);
            mob.Resistance.Add(Element.Fire);
        }
    }

    class KongBody : Body
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Spd += (int)(3 + mob.level.actuallevel * 0.75);
            mob.Vulnerable.Add(Element.Fire);
        }
    }

    class LizardBody : Body
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Mnd += (int)(3 + mob.level.actuallevel * 0.75);
            mob.Resistance.Add(Element.Poison);
        }
    }

    class BisonBody : Body
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Con += (int)(3 + mob.level.actuallevel * 0.75);
            mob.Resistance.Add(Element.Common);
        }
    }

    class CatBody : Body
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Dex += (int)(3 + mob.level.actuallevel * 0.75);
            mob.Attacks.Add(Camouflage);
            mob.attcks.Add("Camouflage");
        }

        public void Camouflage()//camuflagem
        {

        }
    }
}
