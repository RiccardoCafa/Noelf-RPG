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

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    class Player : Ent
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

        private string Sex;
        private string SkinTone;
        private string EyeColor;
        private string Haircut;
        private string HairColor;

        Dictionary<string, string> images = new Dictionary<string, string>()
        {
            { "0_ 0 ____", "man" }, { "1_ 0 ____", "orc male" }, { "2_ 0 ____", "elf male" },
            { "0_ 1 ____", "woman" }, { "1_ 1 ____", "orc female" }, { "2_ 1 ____", "elf female" },

            { "0_ _ 0___", "white" }, { "1_ _ 0___", "turquoise" }, { "2_ _ 0___", "albino" },
            { "0_ _ 1___", "brown" }, { "1_ _ 1___", "green" }, { "2_ _ 1___", "white" },
            { "0_ _ 2___", "black" }, { "1_ _ 2___", "emerald" }, { "2_ _ 2___", "bronze" },

            { "0_ _ _0__", "green" }, { "1_ _ _0__", "cyan" }, { "2_ _ _0__", "sky blue" },
            { "0_ _ _1__", "blue" }, { "1_ _ _1__", "yellow" }, { "2_ _ _1__", "green" },
            { "0_ _ _2__", "brown" }, { "1_ _ _2__", "red" }, { "2_ _ _2__", "purple" },

            { "0_ 0 __0_", "messy" }, { "1_ 0 __0_", "mohican" }, { "2_ 0 __0_", "samurai" },
            { "0_ 0 __1_", "ponytail" }, { "1_ 0 __1_", "rabbit tail" }, { "2_ 0 __1_", "long" },
            { "0_ 0 __2_", "soldier" }, { "1_ 0 __2_", "beard" }, { "2_ 0 __2_", "long samurai" },
            { "0_ _ __3_", "grated" }, { "1_ _ __3_", "grated" }, { "2_ _ __3_", "grated" },

            { "0_ 1 __0_", "messy" }, { "1_ 1 __0_", "mohican" }, { "2_ 1 __0_", "samurai" },
            { "0_ 1 __1_", "ponytail" }, { "1_ 1 __1_", "rabbit tail" }, { "2_ 1 __1_", "long" },
            { "0_ 1 __2_", "soldier" }, { "1_ 1 __2_", "beard" }, { "2_ 1 __2_", "long samurai" }
        };

        public Player(string id)
        {
            /* ID: rc x kysh
             *  0 r -> raça  0-2
             *  1 c -> classe  0-2
             *  3 x -> sexo  0,1
             *  5 k -> cor de pele  0-2
             *  6 y -> cor do olho  0-2
             *  7 s -> tipo de cabelo  0-3
             *  8 h -> cor de cabelo  0-2
             */

            Id = id;

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

            switch (Id.ToCharArray()[3])
            {
                case '0': Sex = "male"; break;
                case '1': Sex = "female"; break;
            }

            switch (Id.ToCharArray()[5])
            {
                case '0': SkinTone = "light"; break;
                case '1': SkinTone = "medium"; break;
                case '2': SkinTone = "dark"; break;
            }

            switch (Id.ToCharArray()[6])
            {
                case '0': EyeColor = ""; break;
                case '1': SkinTone = "medium"; break;
                case '2': SkinTone = "dark"; break;
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
        }

        public void Status(TextBlock textBlock)//exibe as informaçoes (temporario)
        {
            string text = "\nHP: " + Hp + "/" + HpMax
                        + "\n str[" + Str + "]" + "  spd[" + Spd + "]" + "  dex[" + Dex + "]"
                        + "\n     con[" + Con + "]" + "  mnd[" + Mnd + "]";
            text += "\nattkSpd-> " + AtkSpd + " s"
                  + "\nrun-> " + Run + " m/s"
                  + "\ntimeMgcDmg-> " + TimeMgcDmg + " s"
                  + "\ndmg-> " + Damage;
            text += "\nsex " + Sex
                  + "\nskin tone " + SkinTone
                  + "\neye color " + EyeColor
                  + "\nhaircut " + Haircut
                  + "\nhair color " + HairColor;
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
