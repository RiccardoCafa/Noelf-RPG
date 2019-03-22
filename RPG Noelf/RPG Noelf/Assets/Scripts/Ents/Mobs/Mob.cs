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
        public List<string> attcks { get; set; } = new List<string>();
        public List<Element> Resistance { get; set; } = new List<Element>();
        public List<Element> Vulnerable { get; set; } = new List<Element>();
        public bool Meek { get; set; } = false;

        private IParts[] Parts = { new Face(), new Body(), new Arms(), new Legs() };

        private string[] I = { "face", "body", "arms", "legs" };
        private string[] J = { "d", "e" };

        public string[] code = new string[4];

        public Mob()
        {
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int c = random.Next(0, 5);
                code[i] = "" + c;
                Parts[i] = Parts[i].Choose(c);
                Parts[i].UpdateMob(this);
            }
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
                            MainPage.instance.images[I[i] + j + k].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2 + path3));
                        }
                    }
                }
                else
                {
                    string path2 = "/" + code[i] + ".png";
                    MainPage.instance.images[I[i]].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2));
                }
            }
        }

        public string Status()
        {

            string text = "code " + code[0] + code[1] + code[2] + code[3]
                + "\nHP: " + Hp + "/" + HpMax
                + "\n str  " + Str
                + "\n spd  " + Spd
                + "\n dex  " + Dex
                + "\n con  " + Con
                + "\n mnd  " + Mnd
                + "\nres.: ";
            foreach(Element element in Resistance)
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
            text += Meek ? "\n..passive" : "\n..agressive";
            return text;
        }
    }
}
