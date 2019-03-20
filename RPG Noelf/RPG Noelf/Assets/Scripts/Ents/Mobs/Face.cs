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
    }

    class DragonFace : Mob
    {
        public DragonFace()
        {
            Str = 2;
            Attacks.Add(Fireball);
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/0.png"));
        }

        public void Fireball()//bola de fogo
        {

        }
    }

    class KongFace : Mob
    {
        public KongFace()
        {
            Spd = 2;
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/1.png"));
        }
    }

    class LizardFace : Mob
    {
        public LizardFace()
        {
            Mnd = 2;
            Attacks.Add(Lick);
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/2.png"));
        }

        public void Lick()//linguada
        {

        }
    }

    class BisonFace : Mob
    {
        public BisonFace()
        {
            Con = 2;
            Attacks.Add(Headache);
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/3.png"));
        }

        public void Headache()//cabeçada
        {

        }
    }

    class CatFace : Mob
    {
        public CatFace()
        {
            Dex = 2;
            MainPage.instance.images["face"].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, "/Assets/Images/mob/face/4.png"));
        }
    }
}
