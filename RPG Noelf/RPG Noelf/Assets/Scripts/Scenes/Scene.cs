using RPG_Noelf.Assets.Scripts.Ents.Mobs;
using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using RPG_Noelf.Assets.Scripts.Enviroment;
using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
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
    public static class Matriz
    {
        private static bool[,] upWall = new bool[114, 21];
        private static bool[,] downWall = new bool[114, 21];
        private static bool[,] rightWall = new bool[114, 21];
        private static bool[,] leftWall = new bool[114, 21];
        public static double scale = 768.0 / 21.0;

        public static bool GetUpWall(double x, double y)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) return upWall[(int)x, (int)y];
            else return true;
        }
        public static void SetUpWall(double x, double y, bool a)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) upWall[(int)x, (int)y] = a;
        }
        public static bool GetDownWall(double x, double y)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) return downWall[(int)x, (int)y];
            else return true;
        }
        public static void SetDownWall(double x, double y, bool a)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) downWall[(int)x, (int)y] = a;
        }
        public static bool GetRightWall(double x, double y)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) return rightWall[(int)x, (int)y];
            else return true;
        }
        public static void SetRightWall(double x, double y, bool a)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) rightWall[(int)x, (int)y] = a;
        }
        public static bool GetLeftWall(double x, double y)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) return leftWall[(int)x, (int)y];
            else return true;
        }
        public static void SetLeftWall(double x, double y, bool a)
        {
            if (x >= 0 && x < 114 && y >= 0 && y < 21) leftWall[(int)x, (int)y] = a;
        }
    }

    public class Platform
    {
        public static Canvas chunck;
        public List<Solid> floor = new List<Solid>();
        public List<string> Blueprint { get; private set; } = new List<string>();
        int a = 0;
        private List<Bau> baus;

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
            Solid leftWall = new Solid(-20, 0, 20, chunck.Height);
            Solid rightWall = new Solid(chunck.Width, 0, 20, chunck.Height);
            floor.Add(leftWall);
            floor.Add(rightWall);
            for (int y = Blueprint.Count - 1; y >= 0; y--)
            {
                int x = 0;
                foreach (char block in Blueprint[y])
                {
                    if (block == 'p')
                    {
                        CreatePlayer(xScene, x, y);
                        break;
                    }
                    x++;
                }
            }
            for (int y = Blueprint.Count - 1; y >= 0; y--)
            {
                //List<int> platX = new List<int>();
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
                            if (GetBlueprint(x, y + 1) != 'G' &&
                                GetBlueprint(x, y + 1) != 'g') Matriz.SetUpWall(x, y, true);
                            if (GetBlueprint(x, y - 1) != 'G' &&
                                GetBlueprint(x, y - 1) != 'g') Matriz.SetDownWall(x, y, true);
                            if (GetBlueprint(x + 1, y) != 'G' &&
                                GetBlueprint(x + 1, y) != 'g') Matriz.SetRightWall(x, y, true);
                            if (GetBlueprint(x - 1, y) != 'G' &&
                                GetBlueprint(x - 1, y) != 'g') Matriz.SetLeftWall(x, y, true);
                            Tile grass = new Tile(Tile.TileCode[block], x, y);
                            SetImage(grass.Path, Tile.VirtualSize[0], Tile.VirtualSize[1],
                                                 grass.VirtualPosition[0], grass.VirtualPosition[1]);
                            //Solid solid = new Solid(x * Matriz.scale, (y - 1) * Matriz.scale, Matriz.scale, Matriz.scale);
                            //Solid solid = new Solid(x * Solid.wScale, y * Solid.hScale - 1, Solid.wScale, Solid.hScale);
                            //chunck.Children.Add(solid);
                            //floor.Add(solid);
                            //solid.Name = "plat" + platX.First() + y;
                            //solid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
                            break;
                        case 'b':
                            Solid chest = Game.instance.CreateChest(x * Matriz.scale, y * Matriz.scale, baus[a], Matriz.scale, Matriz.scale);
                            a++;
                            floor.Add(chest);
                            break;
                        case 'm':
                            CreateMob(xScene, x, y);
                            break;
                        case 'n':
                            CharacterNPC npc = new CharacterNPC(new NPC(), x * Matriz.scale, y * Matriz.scale, Matriz.scale, Matriz.scale * 2, 0);
                            floor.Add(npc.box);
                            break;
                        default: break;
                    }
                    x++;
                }
            }
            //for (int i = 0; i < 114; i++)
            //{
            //    for (int j = 0; j < 21; j++)
            //    {
            //        if (Matriz.GetDownWall(i, j))
            //        {
            //            Solid solid = new Solid(i * Matriz.scale, j * Matriz.scale, Matriz.scale, Matriz.scale);
            //            chunck.Children.Add(solid);
            //            //floor.Add(solid);
            //            //solid.Name = "plat" + platX.First() + y;
            //            solid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
            //        }
            //    }
            //}
            for (int i = 0; i < 114; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (Matriz.GetUpWall(i, j))
                    {
                        Solid solid = new Solid(i * 36.57, j * 36.57 + 36.57 / 2, 36.57, 36.57 / 2);
                        chunck.Children.Add(solid);
                        floor.Add(solid);
                        solid.Background = new SolidColorBrush(Color.FromArgb(127, 255, 0, 127));
                    }
                    if (Matriz.GetDownWall(i, j))
                    {
                        Solid solid = new Solid(i * 36.57, j * 36.57, 36.57, 36.57 / 2);
                        chunck.Children.Add(solid);
                        floor.Add(solid);
                        solid.Background = new SolidColorBrush(Color.FromArgb(127, 255, 127, 0));
                    }
                    if (Matriz.GetRightWall(i, j))
                    {
                        Solid solid = new Solid(i * 36.57 + 36.57 / 2, j * 36.57, 36.57 / 2, 36.57);
                        chunck.Children.Add(solid);
                        floor.Add(solid);
                        solid.Background = new SolidColorBrush(Color.FromArgb(127, 0, 255, 127));
                    }
                    if (Matriz.GetLeftWall(i, j))
                    {
                        Solid solid = new Solid(i * 36.57, j * 36.57, 36.57 / 2, 36.57);
                        chunck.Children.Add(solid);
                        floor.Add(solid);
                        solid.Background = new SolidColorBrush(Color.FromArgb(127, 127, 255, 0));
                    }
                }
            }
            //chunck = new Canvas();
            //xScene.Children.Add(chunck);
            //System.IO.StreamReader file = new System.IO.StreamReader("Assets/Scripts/Scenes/scenario.txt");
            //string bp;
            //int sizeX = 0;
            //int sizeY = 0;
            //while ((bp = file.ReadLine()) != null)
            //{
            //    Blueprint.Add(bp);
            //    sizeX = sizeX <= bp.Length ? bp.Length : sizeX;
            //    sizeY++;
            //}
            //file.Close();
            //chunck.Width = (sizeX - 1) * Tile.Size[0];
            //chunck.Height = sizeY * Tile.Size[1] + Tile.VirtualSize[1] - Tile.Size[1];
            //Solid leftWall = new Solid(-20, 0, 20, chunck.Height);
            //Solid rightWall = new Solid(chunck.Width, 0, 20, chunck.Height);
            //floor.Add(leftWall);
            //floor.Add(rightWall);
            //for (int y = Blueprint.Count - 1; y >= 0; y--)
            //{
            //    int x = 0;
            //    foreach (char block in Blueprint[y])
            //    {
            //        if (block == 'p')
            //        {
            //            CreatePlayer(xScene, x, y);
            //            break;
            //        }
            //        x++;
            //    }
            //}
            //for (int y = Blueprint.Count - 1; y >= 0; y--)
            //{
            //    List<int> platX = new List<int>();
            //    int x = 0;
            //    foreach (char block in Blueprint[y])
            //    {
            //        switch (block)
            //        {
            //            case '-': break;
            //            case 'g':
            //                Tile ground = new Tile(Tile.TileCode[block], x, y);
            //                SetImage(ground.Path, Tile.VirtualSize[0], Tile.VirtualSize[1],
            //                                      ground.VirtualPosition[0], ground.VirtualPosition[1]);
            //                break;
            //            case 'G':
            //                Tile grass = new Tile(Tile.TileCode[block], x, y);
            //                SetImage(grass.Path, Tile.VirtualSize[0], Tile.VirtualSize[1],
            //                                     grass.VirtualPosition[0], grass.VirtualPosition[1]);
            //                platX.Add(x);
            //                if (Blueprint[y].ToArray()[x + 1] != 'G')
            //                {
            //                    Solid solid = new Solid(Tile.Size[0] * platX.First(), Tile.Size[1] * y - 1,
            //                                            Tile.Size[0] * (platX.Last() - platX.First() + 1), Tile.Size[1]);
            //                    chunck.Children.Add(solid);
            //                    floor.Add(solid);
            //                    solid.Name = "plat" + platX.First() + y;
            //                    //solid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
            //                    platX.Clear();
            //                }
            //                break;
            //            case 'b':
            //                Solid chest = Game.instance.CreateChest(x * Tile.Size[0], y * Tile.Size[1], baus[a], Tile.Size[0], Tile.Size[1]);
            //                a++;
            //                floor.Add(chest);
            //                break;
            //            case 'm':
            //                CreateMob(xScene, x, y);
            //                break;
            //            case 'n':
            //                CharacterNPC npc = new CharacterNPC(new NPC(), x * Tile.Size[0], y * Tile.Size[1], 60 * 0.6, 120 * 0.6, 0);
            //                floor.Add(npc.box);
            //                break;
            //            default: break;
            //        }
            //        x++;
            //    }
            //}
            //foreach (Mob mob in GameManager.mobs) mob.box.Run();
        }

        private char GetBlueprint(int x, int y)
        {
            if (x >= 0 && y >= 0 && y < 21 && x < 114)
                return Blueprint[y].ToCharArray()[x];
            else
                return '-';
        }

        private void CreatePlayer(Canvas xScene, int x, int y)
        {
            GameManager.player = new Player("0000000");
            GameManager.player.Spawn(x * Matriz.scale, y * Matriz.scale);
            xScene.Children.Add(GameManager.player.box);


            GameManager.InitializeGame();
            Game.instance.CreateInventory(Game.instance.bagWindow);
            Game.instance.CreateChestWindow(350, 250);
            baus = new List<Bau>()
            {
                new Bau(Category.Normal, 10),
                new Bau(Category.Normal, 10),
                new Bau(Category.Legendary, 15),
                new Bau(Category.Normal, 10)
            };
        }

        private void CreateMob(Canvas xScene, int x, int y)
        {
            Mob mob = new Mob(level: 2);
            GameManager.mobs.Add(mob);
            mob.Spawn(x * Matriz.scale, y * Matriz.scale);
            xScene.Children.Add(mob.box);
            floor.Add(mob.box);
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
