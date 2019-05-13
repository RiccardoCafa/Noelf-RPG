using RPG_Noelf.Assets.Scripts.Interface;
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
    public class Scene
    {
        public Canvas chunck;
        public List<Solid> ground = new List<Solid>();
        public List<string> Blueprint { get; private set; } = new List<string>();

        public Scene(Canvas xScene)//constroi o cenario, com os tiles e os canvas
        {
            chunck = new Canvas();
            xScene.Children.Add(chunck);
            System.IO.StreamReader file = new System.IO.StreamReader("Assets/Scripts/Scenes/scenario.txt");
            string bp;
            int sizeX = 0;
            int sizeY = 0;
            while ((bp = file.ReadLine()) != null)
            {
                Blueprint.Add(bp);
                sizeX = sizeX <= bp.Length ? bp.Length : sizeX;
                sizeY++;
            }
            file.Close();
            Tile t = new Tile(TypeTile.grass, 0, 0);
            chunck.Width = (sizeX - 1) * t.Size[0];
            chunck.Height = sizeY * t.Size[1] + t.VirtualSize[1] - t.Size[1];
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
                        chunck.Children.Add(image);
                        image.Source = new BitmapImage(new Uri(Game.instance.BaseUri, tile.Path));
                        image.Width = tile.VirtualSize[0];
                        image.Height = tile.VirtualSize[1];
                        image.SetValue(Canvas.LeftProperty, tile.VirtualPosition[0]);
                        image.SetValue(Canvas.TopProperty, tile.VirtualPosition[1]);
                        if (block == 'G')
                        {
                            platX.Add(x);
                            if (Blueprint[y].ToArray()[x + 1] != 'G')
                            {
                                Solid solid = new Solid(tile.Size[0] * platX.First(), tile.Size[1] * y - 1,
                                                        tile.Size[0] * (platX.Last() - platX.First() + 1), tile.Size[1]);
                                chunck.Children.Add(solid);
                                ground.Add(solid);
                                solid.Name = "plat" + platX.First() + y;
                                solid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
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
