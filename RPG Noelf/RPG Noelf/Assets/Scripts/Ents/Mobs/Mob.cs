using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Mob : Ent
    {
        public List<Action> Attacks { get; set; } = new List<Action>();
        public List<string> attcks { get; set; } = new List<string>();//(temporario)
        public List<Element> Resistance { get; set; } = new List<Element>();
        public List<Element> Vulnerable { get; set; } = new List<Element>();
        public bool Meek { get; set; } = false;

        public int Level;

        private IParts[] Parts = { new Face(), new Body(), new Arms(), new Legs() };

        private string[] I = { "face", "body", "arms", "legs" };
        private string[] J = { "d", "e" };

        public string[] code = new string[4];

        public Dictionary<string, char> N = new Dictionary<string, char>()
        {
            {"0", 'D' }, {"1", 'G' }, {"2", 'L' }, {"3", 'B' }, {"4", 'C' },
        };
        public Dictionary<string, string> a = new Dictionary<string, string>()
        {
            {"0", "ar" }, {"1", "e" }, {"2", "hi" }, {"3", "on" }, {"4", "uo" },
        };
        public Dictionary<string, char> m = new Dictionary<string, char>()
        {
            {"0", 'g' }, {"1", 'r' }, {"2", 'z' }, {"3", 'n' }, {"4", 't' },
        };
        public Dictionary<string, string> e = new Dictionary<string, string>()
        {
            {"0", "a" }, {"1", "eo" }, {"2", "il" }, {"3", "o" }, {"4", "u" },
        };

        public Mob(Dictionary<string, Image> images, int level)
        {
            #region montagem
            Level = level;
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
            #region atributos derivados
            HpMax = Con * 6 + Level * 2;
            Hp = HpMax;
            AtkSpd = 2 - (1.25 * Dex + 1.5 * Spd) / 100;
            Run = 1 + 0.075 * Spd;
            TimeMgcDmg = 9.0 * Mnd / 10;
            Damage = Str;
            #endregion
            #region imagens
            for (int i = 0; i < 4; i++)
            {
                string path1 = "/Assets/Images/mob/" + I[i];
                if (i == 2 || i == 3)
                {
                    foreach (string j in J)
                    {
                        string path2 = "/" + j;
                        for (int k = 0; k < 2; k++)
                        {
                            string path3 = "/" + k + "/" + code[i] + ".png";
                            images[I[i] + j + k].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2 + path3));
                        }
                    }
                }
                else
                {
                    string path2 = "/" + code[i] + ".png";
                    MainPage.instance.images[I[i]].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2));
                }
            }
            #endregion
        }

        public void Status(TextBlock textBlock)//exibe as informaçoes (temporario)
        {
            string text = "name: " + N[code[0]] + a[code[1]] + m[code[2]] + e[code[3]]
                + "   (code " + code[0] + code[1] + code[2] + code[3] + ")"
                + "\nHP: " + Hp + "/" + HpMax
                + "\n str[" + Str + "]" + "  spd[" + Spd + "]" + "  dex[" + Dex + "]"
                + "\n     con[" + Con + "]" + "  mnd[" + Mnd + "]"
                + "\nres.: ";
            foreach (Element element in Resistance)
            {
                text += element.ToString() + "  ";
            }
            text += "\nvul.: ";
            foreach (Element element in Vulnerable)
            {
                text += element.ToString() + "  ";
            }
            text += "\nattks.: ";
            foreach (string word in attcks)
            {
                text += word + "  ";
            }
            text += "\nattkSpd-> " + AtkSpd + " s"
                  + "\nrun-> " + Run + " m/s"
                  + "\ntimeMgcDmg-> " + TimeMgcDmg + " s"
                  + "\ndmg-> " + Damage;
            text += Meek ? "\n..passive" : "\n..agressive";
            textBlock.Text = text;
        }
    }
}
