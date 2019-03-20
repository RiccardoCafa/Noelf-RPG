using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Arms : MobDecorator
    {
        private Animal TpAnimal { get; set; }

        public Arms(Mob mob, int code) : base(mob)
        {
            TpAnimal = animalCode[code];
            mob.code += code * 100;
            switch (TpAnimal)
            {
                case Animal.dragon:
                    mob = new DragonBody(mob);
                    break;
                case Animal.kong:
                    mob = new KongBody(mob);
                    break;
                case Animal.lizard:
                    mob = new LizardBody(mob);
                    break;
                case Animal.bison:
                    mob = new BisonBody(mob);
                    break;
                case Animal.cat:
                    mob = new CatBody(mob);
                    break;
            }
        }
    }

    class DragonArms : MobDecorator
    {
        public DragonArms(Mob mob) : base(mob)
        {
            mob.Str += 1;
            MainPage.instance.images["arm_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/0/0.png"));
            MainPage.instance.images["arm_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/1/0.png"));
            MainPage.instance.images["arm_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/0/0.png"));
            MainPage.instance.images["arm_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/1/0.png"));
        }
    }

    class KongArms : MobDecorator
    {
        public KongArms(Mob mob) : base(mob)
        {
            mob.Spd += 1;
            mob.Attacks.Add(Poopoo);
            MainPage.instance.images["arm_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/0/1.png"));
            MainPage.instance.images["arm_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/1/1.png"));
            MainPage.instance.images["arm_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/0/1.png"));
            MainPage.instance.images["arm_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/1/1.png"));
        }

        public void Poopoo()
        {

        }
    }

    class LizardArms : MobDecorator
    {
        public LizardArms(Mob mob) : base(mob)
        {
            mob.Mnd += 1;
            MainPage.instance.images["arm_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/0/2.png"));
            MainPage.instance.images["arm_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/1/2.png"));
            MainPage.instance.images["arm_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/0/2.png"));
            MainPage.instance.images["arm_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/1/2.png"));
        }
    }

    class BisonArms : MobDecorator
    {
        public BisonArms(Mob mob) : base(mob)
        {
            mob.Con += 1;
            MainPage.instance.images["arm_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/0/3.png"));
            MainPage.instance.images["arm_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/1/3.png"));
            MainPage.instance.images["arm_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/0/3.png"));
            MainPage.instance.images["arm_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/1/3.png"));
        }
    }

    class CatArms : MobDecorator
    {
        public CatArms(Mob mob) : base(mob)
        {
            mob.Dex += 1;
            mob.Attacks.Add(Camouflage);
            MainPage.instance.images["arm_d0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/0/4.png"));
            MainPage.instance.images["arm_d1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/d/1/4.png"));
            MainPage.instance.images["arm_e0"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/0/4.png"));
            MainPage.instance.images["arm_e1"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/arms/e/1/4.png"));
        }

        public void Camouflage()
        {

        }
    }
}
