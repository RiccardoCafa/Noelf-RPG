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

    public class Class// : IAtributes
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

    public class Warrior : Class
    {
        public Warrior(SkillManager skillManager) : base(skillManager)
        {
            Str = 5;
            Spd = 0;
            Dex = 0;
            Con = 5;
            Mnd = 0;

            skillManager.SetWarriorPassive("/Assets/Images/Item2.jpg", "Fúria do guerreiro");
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Endurance")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 20,
                Buff = 1.24,
                tipo = SkillType.habilite,
                Buffer = BuffDebuffTypes.Res,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "Esfolar")
            {
                Damage = 155,
                manaCost = 0,
                block = 1,
                Amplificator = 1.01,
                cooldown = 12,
                BonusMultiplier = 1.09,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Cabeçada")
            {
                Damage = 25,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 5,
                Timer = 2.9,
                Buffer = BuffDebuffTypes.Broken,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Até a morte")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 1.01,
                cooldown = 35,
                Timer = 6,
                Buff = 1.39,
                tipo = SkillType.ultimate,
                Buffer = BuffDebuffTypes.Dmg,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "triunfar")
            {
                Damage = 50,
                manaCost = 0,
                BonusMultiplier = 1.18,
                block = 1,
                Amplificator = 1.02,
                cooldown = 20,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "grito de guerra")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.02,
                cooldown = 12,
                Buffer = BuffDebuffTypes.Dmg,
                Buff = 1.15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "provocar")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 1.02,
                cooldown = 20,
                tipo = SkillType.habilite,
                Buff = 1.08,
                Buffer = BuffDebuffTypes.Broken,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "bloqueio perfeito")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.4,
                cooldown = 15,
                tipo = SkillType.habilite,
                Buffer = BuffDebuffTypes.Silence, 
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Armadura Santa")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 1.01,
                cooldown = 35,
                Timer = 3,
                Buff = 1.89,
                tipo = SkillType.ultimate,
                Buffer = BuffDebuffTypes.Res,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "doble hit")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.4,
                cooldown = 15,
                tipo = SkillType.passive,
                Buffer = BuffDebuffTypes.Double,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "a marca do duel")//***//
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "berserk")/**/
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillThrow("/Assets/Images/Chao.jpg", "afastar")
            {
                Damage = 25,
                manaCost = 0,
                block = 1,
                Amplificator = 1.02,
                cooldown = 15,
                Buff = 1.18,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Sede de sangue")/////
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                Timer = 6,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            className = "Guerreiro";
        }
    }

    public class Ranger : Class
    {
        public Ranger(SkillManager skillManager) : base(skillManager)
        {
            Str = 0;
            Spd = 5;
            Dex = 5;
            Con = 0;
            Mnd = 0;
            skillManager.SetArcherPassive("/Assets/Images/Skills/PassiveArcherSkill.jpg", "Headshot");
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Skills/ArcherSkill1.jpg", "Rajadas")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 0.02,
                Buff = 1.23,
                Buffer = BuffDebuffTypes.Dex,
                cooldown = 30,
                Timer = 15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Skills/ArcherSkill2.jpg", "Flecha de fogo")
            {
                Damage = 10,
                manaCost = 0,
                block = 2,
                Amplificator = 0.01,
                Buff = 1.09,
                Buffer = BuffDebuffTypes.Dmg,
                cooldown = 7,
                Timer = 6,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.Fire
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Skills/ArcherSkill3.jpg", "Trap")
            {
                Damage = 75,
                manaCost = 0,
                block = 2,
                Amplificator = 1.02,
                Buff = 1.18,
                Buffer = BuffDebuffTypes.Prison,
                cooldown = 7,
                Timer = 2,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillThrow("/Assets/Images/Skills/ArcherSkill4.jpg", "Abrealas")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 0.05,
                Buff = 0,
                cooldown = 7,
                Timer = 0.25,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "Flecha de gelo")
            {
                Damage = 20,
                manaCost = 0,
                block = 2,
                Amplificator = 1.025,
                Buff = 1.075,
                Buffer = BuffDebuffTypes.Slow,
                cooldown = 4,
                Timer = 1.5,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.Ice
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "Flechas de ferro")
            {
                Damage = 80,
                manaCost = 0,
                block = 2,
                Amplificator = 1.055,
                cooldown = 4,
                BonusMultiplier = 1.25,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Foco")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 1.01,
                Buff = 1.14,
                Buffer = BuffDebuffTypes.Critical,
                cooldown = 60,
                Timer = 30,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "TripleShot")
            {
                Damage = 90,
                manaCost = 0,
                block = 1,
                Amplificator = 1.025,
                cooldown = 10,
                BonusMultiplier = 1.175,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillDash("/Assets/Images/Item2.jpg", "Dash")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 0.28,
                cooldown = 10,
                Timer = 0,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillHidden("/Assets/Images/Item1.jpg", "camuflagem")
            {
                manaCost = 0,
                block = 2,
                Amplificator = 1.01,
                Buff = 1.09,
                cooldown = 10,
                Timer = 3,
                tipo = SkillType.habilite,
                atrib = AtributBonus.agl,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Expor fraqueza")
            {
                Damage = 50,
                manaCost = 0,
                block = 2,
                Amplificator = 1.04,
                Buff = 1.085,
                Buffer = BuffDebuffTypes.Broken,
                cooldown = 10,
                Timer = 5,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "TIRO aperfeiçoado")
            {
                Damage = 300,
                manaCost = 0,
                block = 1,
                Amplificator = 1.25,
                cooldown = 30,
                BonusMultiplier = 0.40,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "Dragon force")
            {
                Damage = 300,
                manaCost = 0,
                block = 1,
                Amplificator = 1.30,
                cooldown = 35,
                BonusMultiplier = 1.30,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "chuva de Mil flechas")
            {
                Damage = 250,
                manaCost = 0,
                block = 1,
                Amplificator = 1.25,
                cooldown = 25,
                BonusMultiplier = 1.01,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                area = true
            });
            className = "Arqueiro";
        }
    }

    public class Wizard : Class
    {

        public Wizard(SkillManager skillManager) : base(skillManager)
        {
            Str = 0;
            Spd = 0;
            Dex = 0;
            Con = 0;
            Mnd = 10;
            skillManager.SetMagePassive("/Assets/Images/Item2.jpg", "Manaflow");
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "bola de fogo")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.Fire
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "estilhaços")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Ice
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "aprisionar")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "bolhas")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "choque")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "nevasca")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Ice
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "ilusion")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "lava")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Fire
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "jorrada")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillHidden("/Assets/Images/Item1.jpg", "darkside")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "usurpar")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "Meteoro")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Fire
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "Tsunami")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "relógio do apocalipse")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            });
            className = "Mago";
        }
    }
}