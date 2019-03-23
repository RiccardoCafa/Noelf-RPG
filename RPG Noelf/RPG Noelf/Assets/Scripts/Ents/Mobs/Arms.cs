using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Arms : IParts
    {
        virtual public void UpdateMob(Mob mob) { }

        public IParts Choose(int code)
        {
            switch (code)
            {
                case 0:
                    return new DragonArms();
                case 1:
                    return new KongArms();
                case 2:
                    return new LizardArms();
                case 3:
                    return new BisonArms();
                case 4:
                    return new CatArms();
                default:
                    return null;
            }
        }
    }

    class DragonArms : Arms
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Str += (int)(1 + mob.Level * 0.25);
        }
    }

    class KongArms : Arms
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Spd += (int)(1 + mob.Level * 0.25);
            mob.Attacks.Add(Poopoo);
            mob.attcks.Add("Poopoo");
        }

        public void Poopoo()
        {

        }
    }

    class LizardArms : Arms
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Mnd += (int)(1 + mob.Level * 0.25);
        }
    }

    class BisonArms : Arms
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Con += 2;
        }
    }

    class CatArms : Arms
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Dex += (int)(1 + mob.Level * 0.25);
            mob.Attacks.Add(Scratch);
            mob.attcks.Add("Scratch");
        }

        public void Scratch()
        {

        }
    }
}
