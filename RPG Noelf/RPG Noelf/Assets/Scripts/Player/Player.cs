using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
﻿using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    class Player : IAtributes
    {
        public Race Race { get; set; }
        public Class _Class { get; set; }
        public SkillManager _SkillManager { get; }
        public Bag _Inventory { get; }

        public int Level { get; private set; }

        public string Id { get; set; }

        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }

        public float Hp { get; set; }
        public int HpMax { get; set; }

        public int Xp { get; private set; }
        public int XpLim { get; private set; }

        public int Mp { get; set; }
        public int MpMax { get; set; }

        public float Armor { get; set; }
        public int Damage { get; set; }
        public double AtkSpd { get; set; }

        public Player(string id, IRaces race, IClasses _class)
        {
            /* ID: rc_x.kysh
             *  r -> raça  0-2
             *  c -> classe  0-2
             *  x -> sexo  0,1
             *  k -> cor de pele  0-2
             *  y -> cor do olho  0-2
             *  s -> tipo de cabelo  0-3
             *  h -> cor de cabelo  0-2
             */

            _SkillManager = new SkillManager(this);
            _Inventory = new Bag();

            switch (race)
            {
                case IRaces.Human:
                    Race = new Human();
                    break;
                case IRaces.Orc:
                    Race = new Orc();
                    break;
                case IRaces.Elf:
                    Race = new Elf();
                    break;
            }

            switch (_class)
            {
                case IClasses.Warrior:
                    _Class = new Warrior(_SkillManager);
                    break;
                case IClasses.Ranger:
                    _Class = new Ranger(_SkillManager);
                    break;
                case IClasses.Wizard:
                    _Class = new Wizard(_SkillManager);
                    break;
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
            if(Mp + MP >= MpMax)
            {
                Mp = MpMax;
            } else
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

        public float ArmoCalc()
        {
            Armor = Armor / (100 + Armor);
            return Armor;
        }
    }
}
