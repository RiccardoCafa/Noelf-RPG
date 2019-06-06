using RPG_Noelf.Assets.Scripts.Ents.Mobs;
using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
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
    public class Platform
    {
        public Canvas chunck;
        public List<Solid> floor = new List<Solid>();
        public List<string> Blueprint { get; private set; } = new List<string>();

        public Platform(Canvas xScene)//constroi o cenario, com os tiles e os canvas
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
            chunck.Width = (sizeX - 1) * Tile.Size[0];
            chunck.Height = sizeY * Tile.Size[1] + Tile.VirtualSize[1] - Tile.Size[1];
            for (int y = Blueprint.Count - 1; y >= 0; y--)
            {
                List<int> platX = new List<int>();
                int x = 0;
                foreach (char block in Blueprint[y])
                {
                    switch (block)
                    {
                        case '-': break;
                        case 'g':
                            Tile ground = new Tile(Tile.TileCode[block], x, y);
                            SetImage(ground.Path, Tile.VirtualSize[0], Tile.VirtualSize[1],
                                                  ground.VirtualPosition[0], ground.VirtualPosition[1]);
                            break;
                        case 'G':
                            Tile grass = new Tile(Tile.TileCode[block], x, y);
                            SetImage(grass.Path, Tile.VirtualSize[0], Tile.VirtualSize[1],
                                                 grass.VirtualPosition[0], grass.VirtualPosition[1]);
                            platX.Add(x);
                            if (Blueprint[y].ToArray()[x + 1] != 'G')
                            {
                                Solid solid = new Solid(Tile.Size[0] * platX.First(), Tile.Size[1] * y - 1,
                                                        Tile.Size[0] * (platX.Last() - platX.First() + 1), Tile.Size[1]);
                                chunck.Children.Add(solid);
                                floor.Add(solid);
                                solid.Name = "plat" + platX.First() + y;
                                //solid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
                                platX.Clear();
                            }
                            break;
                        default: break;
                    }
                    x++;
                }
                x = 0;
                foreach (char block in Blueprint[y])
                {
                    switch (block)
                    {
                        case '-': break;
                        case 'p':
                            GameManager.player = new Player("0000000");
                            GameManager.player.Spawn(x * Tile.Size[0], y * Tile.Size[1]);
                            xScene.Children.Add(GameManager.player.box);
                            break;
                        case 'b':
                            Solid chest = new Solid(x * Tile.Size[0], y * Tile.Size[1], Tile.Size[0], Tile.Size[1]);
                            //chest.Background = new SolidColorBrush(Color.FromArgb(255, 255, 127, 0));
                            chest.Children.Add(new Image() { Width = Tile.Size[0], Height = Tile.Size[0], Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/tiles/chest.png")) });
                            xScene.Children.Add(chest);
                            break;
                        case 'm':
                            GameManager.mob = new Mob(level: 2);
                            GameManager.mob.Spawn(250, 20);
                            break;
                        default: break;
                    }
                    x++;
                }
            }
        }

        private void SetImage(string source, double width, double height, double x, double y)
        {
            Image image = new Image();
            chunck.Children.Add(image);
            image.Source = new BitmapImage(new Uri(Game.instance.BaseUri, source));
            image.Width = width;
            image.Height = height;
            image.SetValue(Canvas.LeftProperty, x);
            image.SetValue(Canvas.TopProperty, y);
        }
    }
}
