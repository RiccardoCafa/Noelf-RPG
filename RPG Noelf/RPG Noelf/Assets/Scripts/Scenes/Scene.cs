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
        public List<string> Blueprint { get; private set; } = new List<string>();

        public Scene(Canvas xScene)//constroi o cenario, com os tiles e os canvas
        {
            System.IO.StreamReader file = new System.IO.StreamReader("Assets/Scripts/Scenes/scenario.txt");
            string bp;
            while ((bp = file.ReadLine()) != null)
            {
                Blueprint.Add(bp);
            }
            file.Close();
            for (int y = Blueprint.Count - 1; y >= 0; y--)
            {
                List<int> platX = new List<int>();
                int x = 0;
                foreach (char block in Blueprint[y])
                {
                    if (block != '-')
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
                            if (/*x + 1 < Blueprint[y].Length || */Blueprint[y].ToArray()[x + 1] != 'G')
                            {
                                Canvas canvas = new Canvas();
                                xScene.Children.Add(canvas);
                                canvas.Width = tile.Size[0] * (platX.Last() - platX.First() + 1);
                                canvas.Height = tile.Size[1] / 4;
                                canvas.SetValue(Canvas.LeftProperty, tile.Size[0] * platX.First());
                                canvas.SetValue(Canvas.TopProperty, tile.Size[1] * y - 1);
                                canvas.Name = "plat" + platX.First() + y;
                                platX.Clear();
                            }
                        }
                    }
                    x++;
                }
            }
        }
    }
}
