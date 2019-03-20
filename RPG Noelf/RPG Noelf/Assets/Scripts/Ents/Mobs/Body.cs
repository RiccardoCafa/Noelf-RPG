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
                    mob = new DragonFace();
                    break;
                case Animal.kong:
                    mob = new KongFace();
                    break;
                case Animal.lizard:
                    mob = new LizardFace();
                    break;
                case Animal.bison:
                    mob = new BisonFace();
                    break;
                case Animal.cat:
                    mob = new CatFace();
                    break;
            }
            Update(mob);
        }
    }

    class DragonBody : MobDecorator
    {
        public DragonBody(Mob mob) : base(mob)
        {
            Str = mob.Str + 2;
            Resistance.Add(Element.Fire);
            Resistance.Union(mob.Resistance);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/0.png"));
        }
    }

    class KongBody : MobDecorator
    {
        public KongBody(Mob mob) : base(mob)
        {
            Spd += mob.Spd + 2;
            Vulnerable.Add(Element.Fire);
            Vulnerable.Union(mob.Vulnerable);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/1.png"));
        }
    }

    class LizardBody : MobDecorator
    {
        public LizardBody(Mob mob) : base(mob)
        {
            Mnd = mob.Mnd + 2;
            Resistance.Add(Element.Poison);
            Resistance.Union(mob.Resistance);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/2.png"));
        }
    }

    class BisonBody : MobDecorator
    {
        public BisonBody(Mob mob) : base(mob)
        {
            Con = mob.Con + 2;
            Resistance.Add(Element.Common);
            Resistance.Union(mob.Resistance);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/3.png"));
        }
    }

    class CatBody : MobDecorator
    {
        public CatBody(Mob mob) : base(mob)
        {
            Dex = mob.Dex + 2;
            Attacks.Add(Camouflage);
            Attacks.Union(mob.Attacks);
            MainPage.instance.images["body"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/body/4.png"));
        }

        public void Camouflage()//camuflagem
        {

        }
    }
}
