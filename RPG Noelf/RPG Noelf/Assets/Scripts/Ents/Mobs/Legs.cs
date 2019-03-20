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
    class Legs : MobDecorator
    {
        private Animal TpAnimal { get; set; }

        public Legs(Mob mob, int code) : base(mob)
        {
            TpAnimal = animalCode[code];
            mob.code += code * 10;
            switch (TpAnimal)
            {
                case Animal.dragon:
                    mob = new DragonArms(mob);
                    break;
                case Animal.kong:
                    mob = new KongArms(mob);
                    break;
                case Animal.lizard:
                    mob = new LizardArms(mob);
                    break;
                case Animal.bison:
                    mob = new BisonArms(mob);
                    break;
                case Animal.cat:
                    mob = new CatArms(mob);
                    break;
            }
        }
    }

    class DragonLegs : MobDecorator
    {
        public DragonLegs(Mob mob) : base(mob)
        {
            mob.Str += 1;
            mob.Vulnerable.Add(Element.Ice);
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/0.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/0.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/0.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/0.png"));
        }
    }

    class KongLegs : MobDecorator
    {
        public KongLegs(Mob mob) : base(mob)
        {
            mob.Spd += 10;
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/1.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/1.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/1.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/1.png"));
        }
    }

    class LizardLegs : MobDecorator
    {
        public LizardLegs(Mob mob) : base(mob)
        {
            mob.Mnd += 1;
            mob.Spd = 1;
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/2.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/2.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/2.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/2.png"));
        }
    }

    class BisonLegs : MobDecorator
    {
        public BisonLegs(Mob mob) : base(mob)
        {
            mob.Con += 1;
            mob.Meek = true;
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/3.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/3.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/3.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/3.png"));
        }
    }

    class CatLegs : MobDecorator
    {
        public CatLegs(Mob mob) : base(mob)
        {
            mob.Dex += 1;
            mob.Vulnerable.Add(Element.Common);
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/4.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/4.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/4.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/4.png"));
        }
    }
}