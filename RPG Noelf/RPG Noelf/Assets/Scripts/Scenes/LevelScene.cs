using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Scenes
{
    public class LevelScene
    {
        public Canvas Father;
        public Platform scene;
        public Image[] layers;

        public LevelScene(Canvas xScene)
        {
            layers = new Image[3] { new Image(), new Image(), new Image() };
            xScene.Children.Add(layers[2]);
            xScene.Children.Add(layers[1]);
            xScene.Children.Add(layers[0]);
            scene = new Platform(xScene);
            Father = xScene;
            layers[2].Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/layer3.png"));
            layers[1].Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/layer2.png"));
            layers[0].Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/layer1.png"));
            Canvas.SetTop(layers[2], 100);
            Canvas.SetTop(layers[1], 320);
            Canvas.SetTop(layers[0], 400);
            Canvas.SetLeft(layers[2], -50);
            Canvas.SetLeft(layers[1], -50);
            Canvas.SetLeft(layers[0], -50);
            //Camera.ReachedLimit += OnReachedLimit;
        }

        //public void OnReachedLimit(LimitArgs e)
        //{
        //    switch (e.limit)
        //    {
        //        case Direction.up:
        //            Canvas.SetTop(layers[2], Canvas.GetTop(layers[2]) + e.speed * 0.125);
        //            Canvas.SetTop(layers[1], Canvas.GetTop(layers[1]) + e.speed * 0.25);
        //            Canvas.SetTop(layers[0], Canvas.GetTop(layers[0]) + e.speed * 0.5);
        //            Canvas.SetTop(scene.chunck, Canvas.GetTop(scene.chunck) + e.speed);
        //            break;
        //        case Direction.down:
        //            Canvas.SetTop(layers[2], Canvas.GetTop(layers[2]) - e.speed * 0.125);
        //            Canvas.SetTop(layers[1], Canvas.GetTop(layers[1]) - e.speed * 0.25);
        //            Canvas.SetTop(layers[0], Canvas.GetTop(layers[0]) - e.speed * 0.5);
        //            Canvas.SetTop(scene.chunck, Canvas.GetTop(scene.chunck) - e.speed);
        //            break;
        //        case Direction.right:
        //            Canvas.SetLeft(layers[2], Canvas.GetLeft(layers[2]) - e.speed * 0.125);
        //            Canvas.SetLeft(layers[1], Canvas.GetLeft(layers[1]) - e.speed * 0.25);
        //            Canvas.SetLeft(layers[0], Canvas.GetLeft(layers[0]) - e.speed * 0.5);
        //            Canvas.SetLeft(scene.chunck, Canvas.GetLeft(scene.chunck) - e.speed);
        //            break;
        //        case Direction.left:
        //            Canvas.SetLeft(layers[2], Canvas.GetLeft(layers[2]) + e.speed * 0.125);
        //            Canvas.SetLeft(layers[1], Canvas.GetLeft(layers[1]) + e.speed * 0.25);
        //            Canvas.SetLeft(layers[0], Canvas.GetLeft(layers[0]) + e.speed * 0.5);
        //            Canvas.SetLeft(scene.chunck, Canvas.GetLeft(scene.chunck) + e.speed);
        //            break;
        //    }
        //}
    }
}
