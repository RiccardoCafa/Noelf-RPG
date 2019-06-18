
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Scenes;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    public class Mob : Ent
    {
        Player player;

        public List<Action> Attacks = new List<Action>();
        public List<string> attcks = new List<string>();//(temporario)
        public List<Element> Resistance = new List<Element>();
        public List<Element> Vulnerable = new List<Element>();
        public Bag MobBag { get; } = new Bag();
        public bool Meek = false;
        public int xpSolta;
        public int GoldSolta;

        private IParts[] Parts = { new Face(), new Body(), new Arms(), new Legs() };

        public string[] code = new string[4];

        public Dictionary<string, string> N = new Dictionary<string, string>()
        {
            {"0", "Dr" }, {"1", "M" }, {"2", "L" }, {"3", "B" }, {"4", "J" }
        };
        public Dictionary<string, string> a = new Dictionary<string, string>()
        {
            {"0", "a" }, {"1", "on" }, {"2", "i" }, {"3", "u" }, {"4", "a" }
        };
        public Dictionary<string, string> m = new Dictionary<string, string>()
        {
            {"0", "g" }, {"1", "k" }, {"2", "zar" }, {"3", "fall" }, {"4", "gu" }
        };
        public Dictionary<string, string> e = new Dictionary<string, string>()
        {
            {"0", "on" }, {"1", "ey" }, {"2", "d" }, {"3", "o" }, {"4", "ar" }
        };

        static double prop = 0.6;
        public Dictionary<string, Image> mobImages = new Dictionary<string, Image>()
        {
            {"armsd0", new Image() { Width = 60 * prop, Height = 65 * prop } },
            {"armsd1", new Image() { Width = 70 * prop, Height = 80 * prop } },
            {"legsd1", new Image() { Width = 75 * prop, Height = 55 * prop } },
            {"legsd0", new Image() { Width = 65 * prop, Height = 70 * prop } },
            {"body", new Image() { Width = 160 * prop, Height = 135 * prop } },
            {"head", new Image() { Width = 100 * prop, Height = 100 * prop } },
            {"legse1", new Image() { Width = 75 * prop, Height = 55 * prop } },
            {"legse0", new Image() { Width = 60 * prop, Height = 65 * prop } },
            {"armse0", new Image() { Width = 60 * prop, Height = 70 * prop } },
            {"armse1", new Image() { Width = 65 * prop, Height = 80 * prop } }
        };

        public TextBlock hpTxt;

        public Mob(int level, Player player)//cria um mob novo, aleatoriamente montado
        {
            this.player = player;
            #region montagem
            this.level = new Level(level, null);
            Str = 2;
            Spd = 2;
            Dex = 2;
            Con = 2;
            Mnd = 2;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int c = random.Next(0, 5);
                code[i] = "" + c;
                Parts[i] = Parts[i].Choose(c);
                Parts[i].UpdateMob(this);
            }
            #endregion
            ApplyDerivedAttributes();
            SetMob(mobImages);
            xpSolta = level * 20 + (int)Damage / 2;
            Random rnd = new Random();
            GoldSolta = rnd.Next(level * 10, level * 50);

            int numberOfItems = rnd.Next(0, 3);

            for(int i = 0; i < numberOfItems; i++)
            {
                int itemID = rnd.Next(1, Encyclopedia.encycloImages.Count);
                MobBag.AddToBag(new Slot((uint)itemID, (uint)rnd.Next(1, 90)));
            }

        }

        public void Start()
        {
            box.Start();
        }
        public void Update()
        {
            
            if (Math.Abs(player.box.Xi - box.Xi) < 300)
            {
                if((!box.freeDirections[Direction.left] || !box.freeDirections[Direction.right]) && !box.freeDirections[Direction.down])
                {
                    box.Yi -= box.jumpSpeed / 5;//verticalSpeed = jumpSpeed * 10;
                    box.OnMoved();//verticalSpeed = jumpSpeed;
                    box.time = DateTime.Now;
                }
                if (player.box.Xi < box.Xi)
                {
                    box.moveLeft = box.freeDirections[Direction.left];
                }
                else if (player.box.Xi > box.Xi)
                {
                    box.moveRight = box.freeDirections[Direction.right];
                }
                else
                {
                    box.moveLeft = box.moveRight = false;
                }
                if(Math.Abs(player.box.Xi - box.Xi) < 20)
                {
                    Attacking = true;
                }
            }
            else
            {
                box.moveLeft = box.moveRight = false;
            }
            box.Update();
        }

        private void Load()
        {
            foreach (string key in mobImages.Keys)
            {
                box.Children.Add(mobImages[key]);
            }
            hpTxt = new TextBlock()
            {
                Text = Hp.ToString() + "/" + HpMax.ToString(),
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                FontSize = 12,
                FontStyle = Windows.UI.Text.FontStyle.Italic
            };
            box.Children.Add(hpTxt);
            Canvas.SetTop(hpTxt, -50);
            double prop = 0.6;
            Canvas.SetLeft(mobImages["armsd0"], 4 * prop); Canvas.SetTop(mobImages["armsd0"], 18 * prop);
            Canvas.SetLeft(mobImages["armsd1"], -21 * prop); Canvas.SetTop(mobImages["armsd1"], 49 * prop);
            Canvas.SetLeft(mobImages["legsd1"], 32 * prop); Canvas.SetTop(mobImages["legsd1"], 76 * prop);
            Canvas.SetLeft(mobImages["legsd0"], 20 * prop); Canvas.SetTop(mobImages["legsd0"], 45 * prop);
            Canvas.SetLeft(mobImages["body"], 8 * prop); Canvas.SetTop(mobImages["body"], -10 * prop);
            Canvas.SetLeft(mobImages["head"], -16 * prop); Canvas.SetTop(mobImages["head"], -34 * prop);
            Canvas.SetLeft(mobImages["legse1"], 64 * prop); Canvas.SetTop(mobImages["legse1"], 75 * prop);
            Canvas.SetLeft(mobImages["legse0"], 55 * prop); Canvas.SetTop(mobImages["legse0"], 47 * prop);
            Canvas.SetLeft(mobImages["armse0"], 33 * prop); Canvas.SetTop(mobImages["armse0"], 18 * prop);
            Canvas.SetLeft(mobImages["armse1"], 12 * prop); Canvas.SetTop(mobImages["armse1"], 50 * prop);

            Attacked += UpdateHpText;
        }//monta as imagens na box do Mob
        public void Spawn(double x, double y)//cria o mob na tela
        {
            box = new DynamicSolid(x, y, Matriz.scale * 2, Matriz.scale * 2, Run);
            box.MyEnt = this;
            Load();
        }

        public void UpdateHpText(object sender, EntEvent ent)
        {
            hpTxt.Text = Hp.ToString() + "/" + HpMax.ToString();
        }

        public override void Die(Ent WhoKilledMe)
        {
            if (MobBag.Slots.Count > 0)
            {
                foreach (Slot mobS in MobBag.Slots)
                {
                    InterfaceManager.instance.CreateDrop(box.Xi + (box.Width / 2), box.Yi + (box.Height / 2), mobS);
                }
            }
            if(WhoKilledMe is Player)
            {
                Player p = WhoKilledMe as Player;
                p.level.GainEXP(xpSolta);
                p._Inventory.AddGold(GoldSolta);
            }
            //Solid.solids.Remove(box);
            box.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //Debug.WriteLine("Mob died");
        }

        private void SetMob(Dictionary<string, Image> images)//aplica as imagens das caracteristicas fisicas do mob
        {
            for (int i = 2; i < 6; i++)
            {
                string path1 = "/Assets/Images/mob/" + parts[i];
                if (parts[i] == "arms" || parts[i] == "legs")
                {
                    foreach (string side in sides)
                    {
                        string path2 = "/" + side;
                        for (int bit = 0; bit < 2; bit++)
                        {
                            string path3 = "/" + bit + "/" + code[i - 2] + ".png";
                            images[parts[i] + side + bit].Source = new BitmapImage(new Uri(Game.instance.BaseUri, path1 + path2 + path3));
                        }
                    }
                }
                else
                {
                    string path2 = "/" + code[i - 2] + ".png";
                    images[parts[i]].Source = new BitmapImage(new Uri(Game.instance.BaseUri, path1 + path2));
                }
            }

        }

        public void Status(TextBlock textBlock)//exibe as informaçoes (temporario)
        {
            string text = "name: " + N[code[0]] + a[code[1]] + m[code[2]] + e[code[3]]
                + "   (code " + code[0] + code[1] + code[2] + code[3] + ")"
                + "\nHP: " + Hp + "/" + HpMax
                + "\n str[" + Str + "]" + "  spd[" + Spd + "]" + "  dex[" + Dex + "]"
                + "\n     con[" + Con + "]" + "  mnd[" + Mnd + "]"
                + "\nres.: ";
            foreach (Element element in Resistance) { text += element.ToString() + "  "; }
            text += "\nvul.: ";
            foreach (Element element in Vulnerable) { text += element.ToString() + "  "; }
            text += "\nattks.: ";
            foreach (string word in attcks) { text += word + "  "; }
            text += "\nattkSpd-> " + AtkSpd + " s"
                  + "\nrun-> " + Run + " m/s"
                  + "\ntimeMgcDmg-> " + TimeMgcDmg + " s"
                  + "\ndmg-> " + Damage;
            text += Meek ? "\n..passive" : "\n..agressive";
            textBlock.Text = text;
        }
    }
}
