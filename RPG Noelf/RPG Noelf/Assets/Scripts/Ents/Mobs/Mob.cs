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
    public class Mob : Ent
    {
        public List<Action> Attacks { get; set; } = new List<Action>();
        public List<string> attcks { get; set; } = new List<string>();//(temporario)
        public List<Element> Resistance { get; set; } = new List<Element>();
        public List<Element> Vulnerable { get; set; } = new List<Element>();
        public bool Meek { get; set; } = false;

        public int Level;

        private IParts[] Parts = { new Face(), new Body(), new Arms(), new Legs() };

        //private string[] I = { "head", "body", "arms", "legs" };
        

        public string[] code = new string[4];

        public Dictionary<string, string> N = new Dictionary<string, string>()
        {
            {"0", "Dr" }, {"1", "M" }, {"2", "L" }, {"3", "B" }, {"4", "J" },
        };
        public Dictionary<string, string> a = new Dictionary<string, string>()
        {
            {"0", "a" }, {"1", "on" }, {"2", "i" }, {"3", "u" }, {"4", "a" },
        };
        public Dictionary<string, string> m = new Dictionary<string, string>()
        {
            {"0", "g" }, {"1", "k" }, {"2", "zar" }, {"3", "fall" }, {"4", "gu" },
        };
        public Dictionary<string, string> e = new Dictionary<string, string>()
        {
            {"0", "on" }, {"1", "ey" }, {"2", "d" }, {"3", "o" }, {"4", "ar" },
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
            TimeMgcDmg = 0.45 * Mnd;
            Damage = Str;
            #endregion
            #region imagens
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
                            images[parts[i] + side + bit].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2 + path3));
                        }
                    }
                }
                else
                {
                    string path2 = "/" + code[i - 2] + ".png";
                    MainPage.instance.MobImages[parts[i]].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2));
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
