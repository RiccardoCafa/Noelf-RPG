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

        //public new void Make(Image face, Image body, Image[,] arms, Image[,] legs) { base.Make(face, body, arms, legs); }
    }

    class DragonLegs : MobDecorator
    {
        public DragonLegs(Mob mob) : base(mob)
        {
            mob.Str += 1;
            mob.Vulnerable.Add(Element.Ice);
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/___0xd0.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/___0xd1.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/___0xe0.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/___0xe1.png"));
        }
    }

    class KongLegs : MobDecorator
    {
        public KongLegs(Mob mob) : base(mob)
        {
            mob.Spd += 10;
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/___1xd0.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/___1xd1.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/___1xe0.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/___1xe1.png"));
        }
    }

    class LizardLegs : MobDecorator
    {
        public LizardLegs(Mob mob) : base(mob)
        {
            mob.Mnd += 1;
            mob.Spd = 1;
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/___2xd0.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/___2xd1.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/___2xe0.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/___2xe1.png"));
        }
    }

    class BisonLegs : MobDecorator
    {
        public BisonLegs(Mob mob) : base(mob)
        {
            mob.Con += 1;
            mob.Meek = true;
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/___3xd0.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/___3xd1.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/___3xe0.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/___3xe1.png"));
        }
    }

    class CatLegs : MobDecorator
    {
        public CatLegs(Mob mob) : base(mob)
        {
            mob.Dex += 1;
            mob.Vulnerable.Add(Element.Common);
            MainPage.instance.images["leg_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/0/___4xd0.png"));
            MainPage.instance.images["leg_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/d/1/___4xd1.png"));
            MainPage.instance.images["leg_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/0/___4xe0.png"));
            MainPage.instance.images["leg_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/legs/e/1/___4xe1.png"));
        }
    }
}