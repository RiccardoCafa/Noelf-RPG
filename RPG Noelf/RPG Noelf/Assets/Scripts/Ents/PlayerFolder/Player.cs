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
using RPG_Noelf.Assets.Scripts.Ents.NPCs;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    public class Player : Ent
    {
        public Race Race { get; set; }
        public Class _Class { get; set; }
        public SkillManager _SkillManager { get; }
        public Bag _Inventory { get; }
        public Equips Equipamento { get; }
        public QuestManager _Questmanager { get; }
        public Level level { get; }
       

        public string Id { get; set; }

        public int Xp { get; private set; }
        public int XpLim { get; private set; }

        public int Mp { get; set; }
        public int MpMax { get; set; }
        
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

            SetPlayer(playerImages);
            SetClothes(clothesImages);
        }

        private void SetPlayer(Dictionary<string, Image> playerImages)//aplica as imagens das caracteristicas fisicas do player
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
                        if (Id[5] == '3') path2 = "/" + Id[0] + Id[2] + "__" + Id[5] + "_.png";
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

        private void SetClothes(Dictionary<string, Image> clothesImages)//aplica as imagens das roupas do player (classe)
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
    }
}
