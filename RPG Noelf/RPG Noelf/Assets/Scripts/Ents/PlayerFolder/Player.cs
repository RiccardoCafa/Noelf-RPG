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
using Windows.UI.Xaml.Input;
using RPG_Noelf.Assets.Scripts.Ents.PlayerFolder;

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
        private PlayerLoader _PlayerLoader;

        public string Id;
        

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

            Window.Current.CoreWindow.KeyUp += RunAttack;
        }

        public void Spawn(double x, double y)//cria o Player na tela
        {
            box = new PlayableSolid(x, y, 60, 120, Run);
            _PlayerLoader = new PlayerLoader(box, Id);
            _PlayerLoader.Load(parts, sides);
            box.MyEnt = this;
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

        public void RunAttack(object sender, KeyEventArgs e)
        {
            if(e.VirtualKey == Windows.System.VirtualKey.Z)
            {
                new Thread(() =>
                {
                    Attack(2);
                }).Start();
            }
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
