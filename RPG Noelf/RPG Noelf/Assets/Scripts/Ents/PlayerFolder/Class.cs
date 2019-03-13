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
            
skillManager.MakeSkillType2(0,1.24,0.01,0,0,0,1,SkillTypeBuff.buff,BuffDebuffTypes.dmg,SkillType.habilite,AtributBonus.For, "Endurance", "/Assets/Images/Chao.jpg");
skillManager.MakeSkillType1(80, 0, 7,5,4, 0.10, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "Esfolar");
skillManager.MakeSkillType2(25, 0, 5, 0, 5,5,6,SkillTypeBuff.debuff,BuffDebuffTypes.prison, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "Cabeçada");
skillManager.MakeSkillType2(0, 1.39,0.01,0,35,6, 33, SkillTypeBuff.buff,BuffDebuffTypes.dmg, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Chao.jpg", "Até a morte");

skillManager.MakeSkillType2(50, 1.19,0.01,0,20,0,11,SkillTypeBuff.debuff,BuffDebuffTypes.slow, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "triunfar");
skillManager.MakeSkillType2(0, 1.13,0.02,0,12,5,22,SkillTypeBuff.buff,BuffDebuffTypes.dmg,SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "grito de guerra");
skillManager.MakeSkillType2(0, 0.08,0.02,0,20,5, 33,SkillTypeBuff.debuff,BuffDebuffTypes.broken, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "provocar");
skillManager.MakeSkillType2(0, 0.08, 0.02, 0, 20, 5, 45, SkillTypeBuff.debuff, BuffDebuffTypes.broken, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "bloqueio perfeito");
skillManager.MakeSkillType2(0, 0.08, 0.02, 0, 20, 5, 66, SkillTypeBuff.debuff, BuffDebuffTypes.broken, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Item2.jpg", "Armadura Santa");

skillManager.MakeSkillType2(0,08, 0.02, 0, 20, 5, 56, SkillTypeBuff.debuff, BuffDebuffTypes.broken, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "doble hit");
skillManager.MakeSkillType2(0, 0.08, 0.02, 0, 20, 5, 67, SkillTypeBuff.debuff, BuffDebuffTypes.broken, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item1.jpg", "a marca do duel");
skillManager.MakeSkillType2(0, 0.08, 0.02, 0, 20, 5, 78, SkillTypeBuff.debuff, BuffDebuffTypes.broken, SkillType.habilite, AtributBonus.For, "/Assets/Images/Item2.jpg", "berserk");
skillManager.MakeSkillType2(0, 0.08, 0.02, 0, 20, 5, 90, SkillTypeBuff.debuff, BuffDebuffTypes.broken, SkillType.habilite, AtributBonus.For, "/Assets/Images/Chao.jpg", "afastar");
skillManager.MakeSkillType2(0, 0.08, 0.02, 0, 20, 5, 99, SkillTypeBuff.debuff, BuffDebuffTypes.broken, SkillType.ultimate, AtributBonus.For, "/Assets/Images/Chao.jpg", "Sede de sangue");



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