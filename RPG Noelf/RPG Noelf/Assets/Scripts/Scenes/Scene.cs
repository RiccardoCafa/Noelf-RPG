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
        public static double scale = 768.0 / 21.0;
    }

    public class Platform
    {
        public static Canvas chunck;
        public List<Solid> floor = new List<Solid>();
        public List<string> Blueprint { get; private set; } = new List<string>();
        int a = 0;
        uint n = 0;
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
            chunck.Width = (sizeX - 1) * Matriz.scale;
            chunck.Height = sizeY * Matriz.scale + Tile.VirtualSize[1] - Matriz.scale;
            Solid leftWall = new Solid(-20, 0, 20, chunck.Height);
            Solid rightWall = new Solid(chunck.Width, 0, 20, chunck.Height);
            floor.Add(leftWall);
            floor.Add(rightWall);
            //Map = new int[Blueprint.Count, 1000];
            for (int y = Blueprint.Count - 1; y >= 0; y--)
            {
                int x = 0;
                foreach (char block in Blueprint[y])
                {
                    if (block == 'p')
                    {
                        CreatePlayer(xScene, x, y);
                        //Map[y, x] = 1;
                        break;
                    }
                    x++;
                }
            }
            for (int y = Blueprint.Count - 1; y >= 0; y--)
            {
                int x = 0;
                foreach (char block in Blueprint[y])
                {
                    switch (block)
                    {
                        case '-': break;
                        case 'g':
                        case 'o':
                            Tile ground = new Tile(Tile.TileCode[block], x, y);
                            SetImage(ground.Path, Tile.VirtualSize[0], Tile.VirtualSize[1],
                                                  ground.VirtualPosition[0], ground.VirtualPosition[1]);
                            break;
                        case 'G':
                        case 'O':
                            Tile grass = new Tile(Tile.TileCode[block], x, y);
                            SetImage(grass.Path, Tile.VirtualSize[0], Tile.VirtualSize[1],
                                                 grass.VirtualPosition[0], grass.VirtualPosition[1]);
                            Block blk = new Block(Matriz.scale * x, Matriz.scale * y - 1);
                            //chunck.Children.Add(solid);
                            //floor.Add(solid);
                            //solid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
                            break;
                        case 'b':
                            Solid chest = InterfaceManager.instance.CreateChest(x * Tile.Size[0], y * Tile.Size[1], baus[a], Tile.Size[0], Tile.Size[1]);
                            a++;
                            floor.Add(chest);
                            break;
                        case 'm':
                            CreateMob(xScene, x, y);
                            break;
                        case 'n':
                            //new CharacterNPC()
                            n++;
                            NPC np = CreateNPCPhase(1, n);
                            CharacterNPC npc = new CharacterNPC(np, x * Matriz.scale, y * Matriz.scale, Matriz.scale, Matriz.scale * 2, 0);
                            floor.Add(npc.box);
                            break;
                        default: break;
                    }
                    x++;
                }
            }
        }

        private char GetBlueprint(int x, int y)
        {
            if (x >= 0 && y >= 0 && y < 21 && x < 114)
                return Blueprint[y].ToCharArray()[x];
            else
                return '-';
        }
        //List<NPC> npcs = new List<NPC>();
        private NPC CreateNPCPhase(int phase, uint number)
        {
            if (Encyclopedia.NonPlayerCharacters == null) Encyclopedia.LoadNPC();
            if(phase == 1)
            {
                return Encyclopedia.NonPlayerCharacters[number];
            }
            return null;
        }

        private void CreatePlayer(Canvas xScene, int x, int y)
        {
            GameManager.instance.player = new Player("0200000");
            GameManager.instance.player.Spawn(x * Matriz.scale, y * Matriz.scale);
            xScene.Children.Add(GameManager.instance.player.box);

            //GameManager.InitializeGame();
            InterfaceManager.instance.CreateInventory();
            InterfaceManager.instance.CreateChestWindow(350, 250);
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
            GameManager.instance.mobs.Add(mob);
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
