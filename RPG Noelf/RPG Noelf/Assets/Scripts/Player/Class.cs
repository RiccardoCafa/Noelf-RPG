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

skillManager.SetPassive(0.05f, AtributBonus.For, "/Assets/Images/Item2.jpg", "Fúria do guerreiro");
            
skillManager.MakeSkill(0, 0, 2, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "Endurance");
skillManager.MakeSkill(0, 0, 4, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "Duro Duro");
skillManager.MakeSkill(0, 0, 6, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "Mole Mole");
skillManager.MakeSkill(0, 0, 33, 0.02f, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Chao.jpg", "Raio solar");

skillManager.MakeSkill(0, 0, 11, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "Coco de macaco");
skillManager.MakeSkill(0, 0, 22, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "Banana adormecida");
skillManager.MakeSkill(0, 0, 33, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "Rezende");
skillManager.MakeSkill(0, 0, 45, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "Caique");
skillManager.MakeSkill(0, 0, 66, 0.02f, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Item2.jpg", "Comunista");

skillManager.MakeSkill(0, 0, 56, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "Felipe");
skillManager.MakeSkill(0, 0, 67, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "Coms");
skillManager.MakeSkill(0, 0, 78, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "Sua formiga");
skillManager.MakeSkill(0, 0, 90, 0.02f, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "acontece");
skillManager.MakeSkill(0, 0, 99, 0.02f, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Chao.jpg", "tem dessas");
            

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