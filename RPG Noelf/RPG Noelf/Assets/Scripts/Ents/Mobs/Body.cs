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
    class Body : MobDecorator
    {
        private Animal TpAnimal { get; set; }

        public Body(Mob mob, int code) : base(mob)
        {
            TpAnimal = animalCode[code];
            mob.code = code * 1000;
            switch (TpAnimal)
            {
                case Animal.dragon:
                    mob = new DragonFace(mob);
                    break;
                case Animal.kong:
                    mob = new KongFace(mob);
                    break;
                case Animal.lizard:
                    mob = new LizardFace(mob);
                    break;
                case Animal.bison:
                    mob = new BisonFace(mob);
                    break;
                case Animal.cat:
                    mob = new CatFace(mob);
                    break;
            }
        }

        //public new void Make(Image face, Image body, Image[,] arms, Image[,] legs) { base.Make(face, body, arms, legs); }
    }

    class DragonBody : MobDecorator
    {
        public DragonBody(Mob mob) : base(mob)
        {
            mob.Str += 2;
            mob.Resistance.Add(Element.Fire);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/_0__.png"));
        }
    }

    class KongBody : MobDecorator
    {
        public KongBody(Mob mob) : base(mob)
        {
            mob.Spd += 2;
            mob.Vulnerable.Add(Element.Fire);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/_1__.png"));
        }
    }

    class LizardBody : MobDecorator
    {
        public LizardBody(Mob mob) : base(mob)
        {
            mob.Mnd += 2;
            mob.Resistance.Add(Element.Poison);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/_2__.png"));
        }
    }

    class BisonBody : MobDecorator
    {
        public BisonBody(Mob mob) : base(mob)
        {
            mob.Con += 2;
            mob.Resistance.Add(Element.Common);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/_3__.png"));
        }
    }

    class CatBody : MobDecorator
    {
        public CatBody(Mob mob) : base(mob)
        {
            mob.Dex += 2;
            mob.Attacks.Add(Camouflage);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/_4__.png"));
        }

        public void Camouflage()//camuflagem
        {

        }
    }
}
