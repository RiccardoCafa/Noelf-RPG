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
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Endurance")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 1.01,
                cooldown = 20,
                buff = 1.24,
                tipo = SkillType.habilite,
                buffer = BuffDebuffTypes.res,
                tipoatributo = Element.common
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
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Cabeçada")
            {
                Damage = 25,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 5,
                timer = 2.9,
                buffer = BuffDebuffTypes.broken,
                tipo = SkillType.habilite,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Até a morte")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 1.01,
                cooldown = 35,
                timer = 6,
                buff = 1.39,
                tipo = SkillType.ultimate,
                buffer = BuffDebuffTypes.dmg,
                tipoatributo = Element.common
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
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "grito de guerra")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.02,
                cooldown = 12,
                buffer = BuffDebuffTypes.dmg,
                buff = 1.15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "provocar")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 1.02,
                cooldown = 20,
                tipo = SkillType.habilite,
                buff = 1.08,
                buffer = BuffDebuffTypes.broken,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "bloqueio perfeito")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.4,
                cooldown = 15,
                tipo = SkillType.habilite,
                buffer = BuffDebuffTypes.silence, 
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Armadura Santa")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 1.01,
                cooldown = 35,
                timer = 3,
                buff = 1.89,
                tipo = SkillType.ultimate,
                buffer = BuffDebuffTypes.res,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "doble hit")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.4,
                cooldown = 15,
                tipo = SkillType.passive,
                buffer = BuffDebuffTypes.doble,
                tipoatributo = Element.common
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
                tipoatributo = Element.common
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
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "afastar")
            {
                Damage = 25,
                manaCost = 0,
                block = 1,
                Amplificator = 1.02,
                cooldown = 15,
                buff = 1.18,
                tipo = SkillType.habilite,
                buffer = BuffDebuffTypes.lancar,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Sede de sangue")/////
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                timer = 6,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
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
            skillManager.SetArcherPassive("/Assets/Images/Item2.jpg", "Headshot");
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Rajadas")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 0.02,
                buff = 1.23,
                buffer = BuffDebuffTypes.dex,
                cooldown = 30,
                timer = 15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "Flecha de fogo")
            {
                Damage = 10,
                manaCost = 0,
                block = 2,
                Amplificator = 0.01,
                buff = 1.09,
                buffer = BuffDebuffTypes.dmg,
                cooldown = 7,
                timer = 6,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.fire
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Trap")
            {
                Damage = 75,
                manaCost = 0,
                block = 2,
                Amplificator = 1.02,
                buff = 1.18,
                buffer = BuffDebuffTypes.prison,
                cooldown = 7,
                timer = 2,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Abrealas")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 0.05,
                buff = 0,
                buffer = BuffDebuffTypes.lancar,
                cooldown = 7,
                timer = 0.25,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "Flecha de gelo")
            {
                Damage = 20,
                manaCost = 0,
                block = 2,
                Amplificator = 1.025,
                buff = 1.075,
                buffer = BuffDebuffTypes.slow,
                cooldown = 4,
                timer = 1.5,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.ice
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
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Chao.jpg", "Foco")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 1.01,
                buff = 1.14,
                buffer = BuffDebuffTypes.critico,
                cooldown = 60,
                timer = 30,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.common
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
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Dash")
            {
                Damage = 0,
                manaCost = 0,
                block = 2,
                Amplificator = 0.28,
                buffer = BuffDebuffTypes.dash,
                cooldown = 10,
                timer = 0,
                tipo = SkillType.habilite,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item1.jpg", "camuflagem")
            {
                manaCost = 0,
                block = 2,
                Amplificator = 1.01,
                buff = 1.09,
                buffer = BuffDebuffTypes.hidden,
                cooldown = 10,
                timer = 3,
                tipo = SkillType.habilite,
                atrib = AtributBonus.agl,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new SkillBuff("/Assets/Images/Item2.jpg", "Expor fraqueza")
            {
                Damage = 50,
                manaCost = 0,
                block = 2,
                Amplificator = 1.04,
                buff = 1.085,
                buffer = BuffDebuffTypes.broken,
                cooldown = 10,
                timer = 5,
                tipo = SkillType.habilite,
                tipoatributo = Element.common
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
                tipoatributo = Element.common
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
                tipoatributo = Element.common
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
                tipoatributo = Element.common,
                area = true
            });
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
            skillManager.SetMagePassive("/Assets/Images/Item2.jpg", "Manaflow");
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "bola de fogo")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "estilhaços")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "aprisionar")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "bolhas")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "choque")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "nevasca")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "ilusion")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "lava")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "jorrada")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "darkside")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "usurpar")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "Meteoro")
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.common
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
                tipoatributo = Element.common
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
                tipoatributo = Element.common
            });
            className = "Mago";
        }
    }
}