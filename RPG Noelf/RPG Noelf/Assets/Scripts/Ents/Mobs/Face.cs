using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Face : Mob
    {
        private Animal TpAnimal { get; set; }

        public Face() { }

        //public override void Make(Image face, Image body, Image[,] arms, Image[,] legs) { }
    }

    class DragonFace : MobDecorator
    {
        public DragonFace(Mob mob) : base(mob)
        {
            mob.Str += 2;
            mob.Attacks.Add(Fireball);
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/0___.png"));
        }

        public void Fireball()//bola de fogo
        {

        }
    }

    class KongFace : MobDecorator
    {
        public KongFace(Mob mob) : base(mob)
        {
            mob.Spd += 2;
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/1___.png"));
        }
    }

    class LizardFace : MobDecorator
    {
        public LizardFace(Mob mob) : base(mob)
        {
            mob.Mnd += 2;
            mob.Attacks.Add(Lick);
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/2___.png"));
        }

        public void Lick()//linguada
        {

        }
    }

    class BisonFace : MobDecorator
    {
        public BisonFace(Mob mob) : base(mob)
        {
            mob.Con += 2;
            mob.Attacks.Add(Headache);
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/3___.png"));
        }

        public void Headache()//cabeçada
        {

        }
    }

    class CatFace : MobDecorator
    {
        public CatFace(Mob mob) : base(mob)
        {
            mob.Dex += 2;
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/4___.png"));
        }
    }
}
