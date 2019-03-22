using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Scenes
{
    class Scene
    {
        public string[] Blueprint { get; private set; } = new string[15];

        public Scene(Canvas xScene)//constroi o cenario, com os tiles e os canvas
        {
            Blueprint[00] = "                                                                                                     ";
            Blueprint[01] = "                                                                                                     ";
            Blueprint[02] = "                                                                                                     ";
            Blueprint[03] = "                                                                                                     ";
            Blueprint[04] = "                                                                                                     ";
            Blueprint[05] = "                                                  GGGGGGGGGGGGGGGGGG                                 ";
            Blueprint[06] = "                                                                     G                               ";
            Blueprint[07] = "                                                                       G                             ";
            Blueprint[08] = "                                                                         GGGGG                 GGGGG ";
            Blueprint[09] = "                                                                                GGGGG       GGGggggg ";
            Blueprint[10] = "                                                                                          GGgggggggg ";
            Blueprint[11] = "                                                                                         Ggggggggggg ";
            Blueprint[12] = "               GGGGGGGGGGGGGGGGGGGG                                                    GGggggggggggg ";
            Blueprint[13] = "GGGGGGGGGGGGGGGggggggggggggggggggggG                           GGGGGGGGGGGGGGGGGGGGGGGGggggggggggggg ";
            Blueprint[14] = "ggggggggggggggggggggggggggggggggggggGGGGGGGGGGGGGGGGGGGGGGGGGGGggggggggggggggggggggggggggggggggggggg ";
            int num = 1;
            for (int y = Blueprint.Length - 1; y >= 0; y--)
            {
                List<int> platX = new List<int>();
                int x = 0;
                foreach (char block in Blueprint[y])
                {
                    if (block != ' ')
                    {
                        Tile tile = new Tile(Tile.TileCode[block], x, y);
                        Image image = new Image();
                        xScene.Children.Add(image);
                        image.Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, tile.Path));
                        image.Width = tile.VirtualSize[0];
                        image.Height = tile.VirtualSize[1];
                        image.SetValue(Canvas.LeftProperty, tile.VirtualPosition[0]);
                        image.SetValue(Canvas.TopProperty, tile.VirtualPosition[1]);
                        if (block == 'G')
                        {
                            platX.Add(x);
                            if (Blueprint[y].ToArray()[x + 1] != 'G')
                            {
                                Canvas canvas = new Canvas();
                                xScene.Children.Add(canvas);
                                canvas.Width = tile.Size[0] * (platX.Last() - platX.First() + 1);
                                canvas.Height = tile.Size[1];
                                canvas.SetValue(Canvas.LeftProperty, tile.Size[0] * platX.First());
                                canvas.SetValue(Canvas.TopProperty, tile.Size[1] * y - 1);
                                canvas.Name = "plat" + platX.First() + y;
                                canvas.Background = new SolidColorBrush(Colors.Blue);
                                canvas.Opacity = 0.5;
                                platX.Clear();
                                num++;
                            }
                        }
                    }
                    x++;
                }
            }
        }
    }
}
