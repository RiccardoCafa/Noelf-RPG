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
using System.IO;
using System.Linq;
using System.Diagnostics;
using RPG_Noelf.Assets.Scripts.Interface;

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
                case '0': _Class = new Warrior(_SkillManager); Ranged = false; break;
                case '1': _Class = new Wizard(_SkillManager); Ranged = true;  break;
                case '2': _Class = new Ranger(_SkillManager); Ranged = true; break;
            }

            Id = id;
            Str = Race.Str + _Class.Str;
            Spd = Race.Spd + _Class.Spd;
            Dex = Race.Dex + _Class.Dex;
            Con = Race.Con + _Class.Con;
            Mnd = Race.Mnd + _Class.Mnd;
            level = new Level(1, this);
            LevelUpdate(0, 0, 0, 0, 0);
            ApplyDerivedAttributes();
            attkDelay = DateTime.Now;
            Window.Current.CoreWindow.KeyUp += RunAttack;

            //_Inventory.BagUpdated += InterfaceManager.instance.UpdateBagEvent;
            //Equipamento.EquipUpdated += InterfaceManager.instance.UpdateEquipEvent;
            //PlayerUpdated += UpdatePlayerInfo
        }

        public void Spawn(double x, double y)//cria o Player na tela
        {
            box = new PlayableSolid(x, y, 60 * 0.6, 120 * 0.6, Run);
            _PlayerLoader = new PlayerLoader(box, Id);
            _PlayerLoader.Load(parts, sides);
            box.MyEnt = this;
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

        public void RunAttack(object sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Z)
            {
                new Thread(() =>
                {
                    Attack();
                }).Start();
            }
        }

        public void LevelUpdate(int str, int spd, int dex, int con, int mnd)//atualiza os atributos ao upar
        {
            
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
            //OnPlayerUpdate();
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
            //OnPlayerUpdate();
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
            //OnPlayerUpdate();
        }

        public virtual void OnPlayerUpdate()
        {
            PlayerUpdated?.Invoke(this, EventArgs.Empty);
        }

        public override void Die()
        {
            Debug.WriteLine("Player died");
        }
    }
}

