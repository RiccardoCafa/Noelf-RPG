using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    public enum IClasses
    {
        Warrior,
        Ranger,
        Wizard
    }

    class Class : IAtributes
    {
        public int Str { get; set; }
        public int Spd { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Mnd { get; set; }
        public int StatsPoints { get; set; }

        protected string className;

        protected SkillManager _skillManager;

        public string ClassName {
            get {
                return className;
            }
        }

        public Class(SkillManager skillManager)
        {
            StatsPoints = 0;
            this._skillManager = skillManager;
        }

    }

    class Warrior : Class
    {
        public Warrior(SkillManager skillManager) : base(skillManager)
        {
            Str = 5;
            Spd = 0;
            Dex = 0;
            Con = 5;
            Mnd = 0;

skillManager.SetWarriorPassive("/Assets/Images/Item2.jpg", "Fúria do guerreiro");
            
skillManager.MakeSkill(0,0,20, 2, 0.25, SkillType.habilite, AtributBonus.cons, "/Assets/Images/Chao.jpg", "Endurance");
skillManager.MakeSkill(80, 0, 7,4, 0.10, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "Esfolar");
skillManager.MakeSkill(25, 0, 5, 6, 0, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "Cabeçada");
skillManager.MakeSkill(0, 0,35, 33, 0, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Chao.jpg", "Até a morte");

skillManager.MakeSkill(50, 0,20, 11, 0.20, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "triunfar");
skillManager.MakeSkill(0, 0,12, 22, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "grito de guerra");
skillManager.MakeSkill(0, 0,0, 33, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "provocar");
skillManager.MakeSkill(0, 0,0, 45, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "bloqueio perfeito");
skillManager.MakeSkill(0, 0,0, 66, 0.02f, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Item2.jpg", "Armadura Santa");

skillManager.MakeSkill(0, 0,0, 56, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "doble hit");
skillManager.MakeSkill(0, 0,0, 67, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "a marca do duel");
skillManager.MakeSkill(0, 0,0, 78, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "berserk");
skillManager.MakeSkill(0, 0,0, 90, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "afastar");
skillManager.MakeSkill(0, 0,0, 99, 0.02f, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Chao.jpg", "Sede de sangue");



            className = "Guerreiro";
        }
    }

    class Ranger : Class
    {
        public Ranger(SkillManager skillManager) : base(skillManager)
        {
            Str = 0;
            Spd = 5;
            Dex = 5;
            Con = 0;
            Mnd = 0;

            className = "Arqueiro";
        }
    }

    class Wizard : Class
    {
        public Wizard(SkillManager skillManager) : base(skillManager)
        {
            Str = 0;
            Spd = 0;
            Dex = 0;
            Con = 0;
            Mnd = 10;

            className = "Bruxo";
        }
    }
}