using RPG_Noelf.Assets.Scripts.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Face : IParts
    {
        virtual public void UpdateMob(Mob mob) { }

        public IParts Choose(int code)
        {
            switch (code)
            {
                case 0:
                    return new DragonFace();
                case 1:
                    return new KongFace();
                case 2:
                    return new LizardFace();
                case 3:
                    return new BisonFace();
                case 4:
                    return new CatFace();
                default:
                    return null;
            }
        }
    }

    class DragonFace : Face
    {
        Mob mob;

        public override void UpdateMob(Mob mob)
        {
            this.mob = mob;
            mob.Str += (int)(3 + mob.Level * 0.5);
            mob.Attacks.Add(Fireball);
            mob.attcks.Add("Fireball");
        }

        public void Fireball()//bola de fogo
        {
            Canvas hit = new Canvas
            {
                Background = new SolidColorBrush(Color.FromArgb(0, 150, 75, 0)),
                Width = 30, Height = 30
            };
            MainPage.ActualChunck.Children.Add(hit);
            
        }
    }

    class KongFace : Face
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Spd += (int)(3 + mob.Level * 0.5);
        }
    }

    class LizardFace : Face
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Mnd += (int)(3 + mob.Level * 0.5);
            mob.Attacks.Add(Lick);
            mob.attcks.Add("Lick");
        }

        public void Lick()//linguada
        {

        }
    }

    class BisonFace : Face
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Con += (int)(3 + mob.Level * 0.5);
            mob.Attacks.Add(Headache);
            mob.attcks.Add("Headache");
        }

        public void Headache()//cabeçada
        {

        }
    }

    class CatFace : Face
    {
        public override void UpdateMob(Mob mob)
        {
            mob.Dex += (int)(3 + mob.Level * 0.5);
        }
    }
}
