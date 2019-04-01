using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Skills;
using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Search;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    public class Player : Ent
    {
        public Race Race { get; set; }
        public Class _Class { get; set; }
        public SkillManager _SkillManager { get; }
        public Bag _Inventory { get; }
        public Equips Equipamento { get; }

        public int Level { get; private set; }

        public string Id { get; set; }

        public int Xp { get; private set; }
        public int XpLim { get; private set; }

        public int Mp { get; set; }
        public int MpMax { get; set; }

        public double Armor { get; set; }
        public double Damage { get; set; }

        public double DamageBuff { get; set; }
        public double ArmorBuff { get; set; }
        public double AtkSpeedBuff { get; set; }
        public double BonusChanceCrit { get; set; } = 1;

        public Player(string id, Dictionary<string, Image> playerImages, Dictionary<string, Image> clothesImages)
        {
            /* ID: rcxkysh
             *  0 r -> raça  0-2
             *  1 c -> classe  0-2
             *  2 x -> sexo  0,1
             *  3 k -> cor de pele  0-2
             *  4 y -> cor do olho  0-2
             *  5 s -> tipo de cabelo  0-3
             *  6 h -> cor de cabelo  0-2
             */

            Id = id;
            //Id.ToCharArray(0, 1) = "1";
            _SkillManager = new SkillManager(this);
            _Inventory = new Bag();
            Equipamento = new Equips(this);

            switch (Id.ToCharArray()[0])
            {
                case '0': Race = new Human(); break;
                case '1': Race = new Orc(); break;
                case '2': Race = new Elf(); break;
            }

            switch (Id.ToCharArray()[1])
            {
                case '0': _Class = new Warrior(_SkillManager); break;
                case '1': _Class = new Ranger(_SkillManager); break;
                case '2': _Class = new Wizard(_SkillManager); break;
            }

            Id = id;
            Str = Race.Str + _Class.Str;
            Spd = Race.Spd + _Class.Spd;
            Dex = Race.Dex + _Class.Dex;
            Con = Race.Con + _Class.Con;
            Mnd = Race.Mnd + _Class.Mnd;
            Level = 1;
            XpLim = Level * 100;
            LevelUpdate(0, 0, 0, 0, 0);

            SetPlayer(playerImages);
            SetClothes(clothesImages);
        }

        private void SetPlayer(Dictionary<string, Image> playerImages)
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
                                playerImages[parts[i] + side + bit].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2 + path3));
                            }
                        }
                        break;
                    case "hair":
                        if(Id[5] == '3') path2 = "/" + Id[0] + Id[2] + "__" + Id[5] + "_.png";
                        else path2 = "/" + Id[0] + Id[2] + "__" + Id.Substring(5, 2) + ".png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2));
                        break;
                    case "eye":
                        path2 = "/" + Id[0] + Id[2] + "_" + Id[4] + "__.png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2));
                        break;
                    default:
                        path2 = "/" + Id[0] + Id.Substring(2, 2) + "___.png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2));
                        break;
                }
            }
        }

        private void SetClothes(Dictionary<string, Image> clothesImages)
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
                            clothesImages[parts[i] + side + bit].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2 + path3));
                        }
                    }
                }
                else
                {
                    string path2 = "/" + Id[2] + Id[1] + ".png";
                    clothesImages[parts[i]].Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, path1 + path2));
                }
            }
        }

        public void Status(TextBlock textBlock)//exibe as informaçoes (temporario)
        {
            string text = "\nHP: " + Hp + "/" + HpMax
                        + "\n str[" + Str + "]" + "  spd[" + Spd + "]" + "  dex[" + Dex + "]"
                        + "\n     con[" + Con + "]" + "  mnd[" + Mnd + "]";
            text += "\nattkSpd-> " + AtkSpd + " s"
                  + "\nrun-> " + Run + " m/s"
                  + "\ntimeMgcDmg-> " + TimeMgcDmg + " s"
                  + "\ndmg-> " + Damage
                  + "\n" + Id;
            textBlock.Text = text;
        }

        public bool XpLevel(int xp)//responde se passou de nivel ou nao, alem de upar (necessario chamar LevelUpdate() em seguida)
        {
            Xp += xp;
            if (Xp >= XpLim)
            {
                Xp -= XpLim;
                Level++;
                _SkillManager.SkillPoints++;
                _Class.StatsPoints++;
                XpLim = Level * 100;
                return true;
            }
            return false;
        }

        public void LevelUpdate(int str, int spd, int dex, int con, int mnd)//atualiza os atributos ao upar
        {
            Str += str;
            Spd += spd;
            Dex += dex;
            Con += con;
            Mnd += mnd;
            HpMax = Con * 6 + Level * 2;
            Hp = HpMax;
            MpMax = Mnd * 5 + Level;
            Mp = MpMax;
            AtkSpd = 2 - 1.75 * Spd / 100;
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
        }

        public double Hit(double bonusDamage)//golpeia
        {
            Random random = new Random();
            double dmg100 = random.NextDouble() * 100;
            if (dmg100 < 1 / Dex * 0.05) return 0;//errou
            else if (dmg100 < Dex * BonusChanceCrit * 0.1) return bonusDamage + Damage * dmg100;//acertou
            else return bonusDamage + Damage * dmg100 * 2;//critico
        }

        public void BeHit(double damage)//tratamento do dano levado
        {
            Hp -= damage / (1 + Con * 0.02 + Armor);
        }
    }
}
