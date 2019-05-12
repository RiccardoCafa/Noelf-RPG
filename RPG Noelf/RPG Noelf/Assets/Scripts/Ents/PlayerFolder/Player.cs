using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Skills;
using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    public class Player : Ent
    {
        public event EventHandler PlayerUpdated;

        public Race Race;
        public Class _Class;
        public readonly SkillManager _SkillManager;
        public readonly Bag _Inventory;
        public readonly Equips Equipamento;
        public readonly QuestManager _Questmanager;

        public string Id;
        public Dictionary<string, Image> playerImages = new Dictionary<string, Image>()
        {
            {"armse0", new Image() { Width = 51, Height = 57 } },
            {"armse1", new Image() { Width = 53, Height = 41 } },
            {"legse1", new Image() { Width = 54, Height = 57 } },
            {"legse0", new Image() { Width = 45, Height = 45 } },
            {"body", new Image() { Width = 69, Height = 84 } },
            {"head", new Image() { Width = 78, Height = 78 } },
            {"eye", new Image() { Width = 78, Height = 78 } },
            {"hair", new Image() { Width = 78, Height = 78 } },
            {"legsd1", new Image() { Width = 51, Height = 51 } },
            {"legsd0", new Image() { Width = 42, Height = 48 } },
            {"armsd0", new Image() { Width = 60, Height = 54 } },
            {"armsd1", new Image() { Width = 45, Height = 48 } }
        };
        public Dictionary<string, Image> clothesImages = new Dictionary<string, Image>()
        {
            {"armse0", new Image() { Width = 51, Height = 57 } },
            {"armse1", new Image() { Width = 53, Height = 41 } },
            {"legse1", new Image() { Width = 54, Height = 57 } },
            {"legse0", new Image() { Width = 45, Height = 45 } },
            {"body", new Image() { Width = 69, Height = 84 } },
            {"legsd1", new Image() { Width = 51, Height = 51 } },
            {"legsd0", new Image() { Width = 42, Height = 48 } },
            {"armsd0", new Image() { Width = 60, Height = 54 } },
            {"armsd1", new Image() { Width = 45, Height = 48 } }
        };

        public int Xp { get; private set; }
        public int XpLim { get; private set; }

        public int Mp;
        public int MpMax;

        DateTime attkDelay;

        public Player(string id) : base()
        {
            /* ID: rcxkysh
             *  0 r -> raça  0-2
             *  1 c -> classe  0-2
             *  2 x -> sexo  0,1
             *  3 k -> cor de pele  0-2
             *  4 y -> cor do olho  0-2
             *  5 s -> tipo de cabelo  0-3
             *  6 h -> cor de cabelo  0-2
             *  
             *  clothes: xc.png
             *  player/head,body,arms,legs: rxk___.png
             *  player/eye: rx_y__.png
             *  player/hair: rx__sh.png
             */

            Id = id;
            _SkillManager = new SkillManager(this);
            _Inventory = new Bag();
            Equipamento = new Equips(this);
            _Questmanager = new QuestManager(this);

            switch (Id[0])
            {
                case '0': Race = new Human(); break;
                case '1': Race = new Orc(); break;
                case '2': Race = new Elf(); break;
            }

            switch (Id[1])
            {
                case '0': _Class = new Warrior(_SkillManager); break;
                case '1': _Class = new Wizard(_SkillManager); break;
                case '2': _Class = new Ranger(_SkillManager); break;
            }

            Id = id;
            Str = Race.Str + _Class.Str;
            Spd = Race.Spd + _Class.Spd;
            Dex = Race.Dex + _Class.Dex;
            Con = Race.Con + _Class.Con;
            Mnd = Race.Mnd + _Class.Mnd;
            level = new Level(1);
            LevelUpdate(0, 0, 0, 0, 0, 0);
            ApplyDerivedAttributes();
            //Window.Current.CoreWindow.KeyDown += Attack;
            attkDelay = DateTime.Now;
        }

        private void Load()
        {
            foreach (string key in playerImages.Keys)
            {
                box.Children.Add(playerImages[key]);
                if (key != "head" && key != "eye" && key != "hair") box.Children.Add(clothesImages[key]);
            }
            Canvas.SetLeft(playerImages["armse0"], 16); Canvas.SetTop(playerImages["armse0"], 24);
            Canvas.SetLeft(playerImages["armse1"], 26); Canvas.SetTop(playerImages["armse1"], 42);
            Canvas.SetLeft(playerImages["legse1"], 17); Canvas.SetTop(playerImages["legse1"], 73);
            Canvas.SetLeft(playerImages["legse0"], 15); Canvas.SetTop(playerImages["legse0"], 58);
            Canvas.SetLeft(playerImages["body"], -9); Canvas.SetTop(playerImages["body"], 9);
            Canvas.SetLeft(playerImages["head"], -5); Canvas.SetTop(playerImages["head"], -27);
            Canvas.SetLeft(playerImages["eye"], -5); Canvas.SetTop(playerImages["eye"], -27);
            Canvas.SetLeft(playerImages["hair"], -5); Canvas.SetTop(playerImages["hair"], -27);
            Canvas.SetLeft(playerImages["legsd1"], -17); Canvas.SetTop(playerImages["legsd1"], 78);
            Canvas.SetLeft(playerImages["legsd0"], -5); Canvas.SetTop(playerImages["legsd0"], 59);
            Canvas.SetLeft(playerImages["armsd0"], -24); Canvas.SetTop(playerImages["armsd0"], 18);
            Canvas.SetLeft(playerImages["armsd1"], -17); Canvas.SetTop(playerImages["armsd1"], 39);
            Canvas.SetLeft(clothesImages["armse0"], 16); Canvas.SetTop(clothesImages["armse0"], 24);
            Canvas.SetLeft(clothesImages["armse1"], 26); Canvas.SetTop(clothesImages["armse1"], 42);
            Canvas.SetLeft(clothesImages["legse1"], 17); Canvas.SetTop(clothesImages["legse1"], 73);
            Canvas.SetLeft(clothesImages["legse0"], 15); Canvas.SetTop(clothesImages["legse0"], 58);
            Canvas.SetLeft(clothesImages["body"], -9); Canvas.SetTop(clothesImages["body"], 9);
            Canvas.SetLeft(clothesImages["legsd1"], -17); Canvas.SetTop(clothesImages["legsd1"], 78);
            Canvas.SetLeft(clothesImages["legsd0"], -5); Canvas.SetTop(clothesImages["legsd0"], 59);
            Canvas.SetLeft(clothesImages["armsd0"], -24); Canvas.SetTop(clothesImages["armsd0"], 18);
            Canvas.SetLeft(clothesImages["armsd1"], -17); Canvas.SetTop(clothesImages["armsd1"], 39);
        }//monta as imagens na box do Player
        public void Spawn(double x, double y)//cria o Player na tela
        {
            box = new PlayableSolid(x, y, 60, 120, Run);
            SetPlayer(playerImages);
            SetClothes(clothesImages);
            Load();
        }

        //public void Attack(CoreWindow sender, KeyEventArgs e)
        //{
        //    if (e.VirtualKey == Windows.System.VirtualKey.Z && attkDelay.Millisecond - DateTime.Now.Millisecond <= AtkSpd * 1000)
        //    {
        //        Task.Run(() =>
        //        {
        //            HitSolid atk = new HitSolid(100, 20, 40, 50, 240);
        //            box.Children.Add(atk);
        //            //TextBlock txt = new TextBlock();
        //            //txt.Text = "" + (DateTime.Now.Millisecond - attkDelay.Millisecond) + ">=" + (AtkSpd * 1000);
        //            //atk.Children.Add(txt);
        //            //Canvas.SetTop(txt, -20);
        //            //Thread.Sleep(500);
        //            //box.Children.Remove(atk);
        //            attkDelay = DateTime.Now;
        //        });
        //    }
        //}

        public void SetPlayer(Dictionary<string, Image> playerImages)//aplica as imagens das caracteristicas fisicas do player
        {
            for (int i = 0; i < 6; i++)
            {
                string path1 = "/Assets/Images/player/player/" + parts[i];
                string path2;
                switch (parts[i])
                {
                    case "arms":
                    case "legs":
                        foreach (string side in sides)
                        {
                            path2 = "/" + side;
                            for (int bit = 0; bit < 2; bit++)
                            {
                                string path3 = "/" + bit + "/" + Id[0] + Id.Substring(2, 2) + "___.png";
                                playerImages[parts[i] + side + bit].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2 + path3));
                            }
                        }
                        break;
                    case "hair":
                        if (Id[5] == '3') path2 = "/" + Id[0] + Id[2] + "__" + Id[5] + "_.png";
                        else path2 = "/" + Id[0] + Id[2] + "__" + Id.Substring(5, 2) + ".png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                        break;
                    case "eye":
                        path2 = "/" + Id[0] + Id[2] + "_" + Id[4] + "__.png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                        break;
                    default:
                        path2 = "/" + Id[0] + Id.Substring(2, 2) + "___.png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                        break;
                }
            }
        }

        //private void SetPlayer(List<Image> playerImages)
        //{
        //    DirectoryInfo di = new DirectoryInfo("/Assets/Images/player/");
        //    List<FileInfo> files = di.GetFiles("*.png").Where(file => (file.Name[0] == Id[0] || file.Name[0] == '_') &&
        //                                                              (file.Name[1] == Id[2] || file.Name[1] == '_') &&
        //                                                              (file.Name[2] == Id[3] || file.Name[2] == '_') &&
        //                                                              (file.Name[3] == Id[4] || file.Name[3] == '_') &&
        //                                                              (file.Name[4] == Id[5] || file.Name[4] == '_') &&
        //                                                              (file.Name[5] == Id[6] || file.Name[5] == '_'))
        //                                               .Select(file => file).ToList();
        //    foreach (FileInfo file in files)
        //    {
        //        playerImages[0].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, file.FullName));
        //    }
        //}

        public void SetClothes(Dictionary<string, Image> clothesImages)//aplica as imagens das roupas do player (classe)
        {
            for (int i = 3; i < 6; i++)
            {
                string path1 = "/Assets/Images/player/clothes/" + parts[i];
                if (parts[i] == "arms" || parts[i] == "legs")
                {
                    foreach (string side in sides)
                    {
                        string path2 = "/" + side;
                        for (int bit = 0; bit < 2; bit++)
                        {
                            string path3 = "/" + bit + "/" + Id[2] + Id[1] + ".png";
                            clothesImages[parts[i] + side + bit].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2 + path3));
                        }
                    }
                }
                else
                {
                    string path2 = "/" + Id[2] + Id[1] + ".png";
                    clothesImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                }
            }
        }

        public void Status(TextBlock textBlock)//exibe as informaçoes (temporario)
        {
            string text = "\nHP: " + Hp + "/" + HpMax
                        + "\n str[" + Str + "]" + "  spd[" + Spd + "]" + "  dex[" + Dex + "]"
                        + "\n     con[" + Con + "]" + "  mnd[" + Mnd + "]"
                        + "\nattkSpd-> " + AtkSpd + " s"
                        + "\nrun-> " + Run + " m/s"
                        + "\ntimeMgcDmg-> " + TimeMgcDmg + " s"
                        + "\ndmg-> " + Damage;
            textBlock.Text = text;
        }


        public void LevelUpdate(int str, int spd, int dex, int con, int mnd, int exp)//atualiza os atributos ao upar
        {
            level.GainEXP(exp);
            Str += str;
            Spd += spd;
            Dex += dex;
            Con += con;
            Mnd += mnd;
            HpMax = Con * 6 + level.actuallevel * 2;
            Hp = HpMax;
            MpMax = Mnd * 5 + level.actuallevel;
            Mp = MpMax;
            AtkSpd = 2 - (1.25 * Dex + 1.5 * Spd) / 100;
            Run = 1 + 0.075 * Spd;
            TimeMgcDmg = 0.45 * Mnd;
            Damage = Str;
            Armor = ArmorBuff + ArmorEquip;
            OnPlayerUpdate();
        }

        public void AddMP(int MP)
        {
            if (Mp + MP >= MpMax)
            {
                Mp = MpMax;
            }
            else
            {
                Mp += MP;
            }
            OnPlayerUpdate();
        }

        public void AddHP(int HP)
        {
            if (Hp + HP >= HpMax)
            {
                Hp = HpMax;
            }
            else
            {
                Hp += HP;
            }
            OnPlayerUpdate();
        }

        public virtual void OnPlayerUpdate()
        {
            PlayerUpdated?.Invoke(this, EventArgs.Empty);
        }

    }
}
