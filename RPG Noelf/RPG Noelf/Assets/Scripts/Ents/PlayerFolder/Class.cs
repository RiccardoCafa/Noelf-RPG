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
            skillManager.SkillList.Add(new SkillResbuff("/Assets/Images/Chao.jpg", "Endurance")
            {
                Damage = 0,
                manaCost = 35,
                block = 2,
                Amplificator = 0.01,
                cooldown = 20,
                Buff = 1.24,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "Esfolar")
            {
                Damage = 155,
                manaCost = 25,
                block = 6,
                Amplificator = 1.01,
                cooldown = 12,
                BonusMultiplier = 1.09,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });

            skillManager.SkillBar[0] = skillManager.SkillList[2];

            skillManager.SkillList.Add(new SkillBroken("/Assets/Images/Item2.jpg", "Cabeçada")
            {
                Damage = 25,
                manaCost = 15,
                block = 8,
                Amplificator = 0.01,
                cooldown = 5,
                timer = 2.9,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillDmgBuff("/Assets/Images/Chao.jpg", "Até a morte")
            {
                Damage = 0,
                manaCost = 200,
                block = 33,
                Amplificator = 1.01,
                cooldown = 35,
                timer = 6,
                Buff = 1.39,
                tipo = SkillType.ultimate,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "triunfar")
            {
                Damage = 50,
                manaCost = 125,
                BonusMultiplier = 1.18,
                block = 11,
                Amplificator = 1.02,
                cooldown = 20,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillDmgBuff("/Assets/Images/Item2.jpg", "grito de guerra")
            {
                Damage = 0,
                manaCost = 50,
                block = 22,
                Amplificator = 0.02,
                cooldown = 12,
                Buff = 1.15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillBroken("/Assets/Images/Chao.jpg", "provocar")
            {
                Damage = 0,
                manaCost = 45,
                block = 33,
                Amplificator = 1.02,
                description = "Quebra a armadura do seu inimigo",
                cooldown = 20,
                tipo = SkillType.habilite,
                Buff = 1.08,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillSilence("/Assets/Images/Item1.jpg", "bloqueio perfeito")
            {
                Damage = 0,
                manaCost = 75,
                block = 45,
                description = "Ignora um ataque",
                Amplificator = 0.4,
                cooldown = 15,
                tipo = SkillType.habilite, 
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillResbuff("/Assets/Images/Item2.jpg", "Armadura Santa")
            {
                Damage = 0,
                manaCost = 200,
                block = 66,
                description = "Ganha Imunidade por um breve periodo de tempo",
                Amplificator = 1.01,
                cooldown = 35,
                timer = 3,
                Buff = 1.89,
                tipo = SkillType.ultimate,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillDobleHit("/Assets/Images/Item1.jpg", "doble hit")
            {
                Damage = 0,
                manaCost = 60,
                description = "Da dois ataques seguidos",
                block = 56,
                Amplificator = 0.4,
                cooldown = 15,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "a marca do duel")//***//
            {
                Damage = 0,
                manaCost = 100,
                block = 90,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "berserk")/**/
            {
                Damage = 0,
                manaCost = 45,
                block = 67,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Chao.jpg", "afastar")
            {
                Damage = 25,
                manaCost = 50,
                block = 78,
                Amplificator = 1.02,
                cooldown = 15,
                Buff = 1.18,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillDmgBuff("/Assets/Images/Chao.jpg", "Sede de sangue")/////
            {
                Damage = 0,
                manaCost = 200,
                block = 99,
                Amplificator = 0.01,
                cooldown = 0,
                timer = 6,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
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
            skillManager.SkillList.Add(new SkillDex("/Assets/Images/Skills/ArcherSkill1.jpg", "Rajadas")
            {
                Damage = 0,
                manaCost = 20,
                block = 2,
                Amplificator = 0.02,
                Buff = 1.23,
                cooldown = 30,
                timer = 15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillDmgBuff("/Assets/Images/Skills/ArcherSkill2.jpg", "Flecha de fogo")
            {
                Damage = 10,
                manaCost = 25,
                block = 6,
                Amplificator = 0.01,
                Buff = 1.09,
                cooldown = 7,
                timer = 6,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.Fire,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Skills/ArcherSkill3.jpg", "Trap")
            {
                Damage = 75,
                manaCost = 45,
                block = 8,
                Amplificator = 1.02,
                Buff = 1.18,
                cooldown = 7,
                timer = 2,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Skills/ArcherSkill4.jpg", "Abrealas")
            {
                Damage = 0,
                manaCost = 40,
                block = 11,
                Amplificator = 0.05,
                Buff = 0,
                cooldown = 7,
                timer = 0.25,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillSlowbuff("/Assets/Images/Item1.jpg", "Flecha de gelo")
            {
                Damage = 20,
                manaCost = 30,
                block = 45,
                Amplificator = 1.025,
                Buff = 1.075,
                cooldown = 4,
                timer = 1.5,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.Ice,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "Flechas de ferro")
            {
                Damage = 80,
                manaCost = 90,
                block = 22,
                Amplificator = 1.055,
                cooldown = 4,
                BonusMultiplier = 1.25,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillCritical("/Assets/Images/Chao.jpg", "Foco")
            {
                Damage = 0,
                manaCost = 30,
                block = 78,
                Amplificator = 1.01,
                Buff = 1.14,
                cooldown = 60,
                timer = 30,
                tipo = SkillType.habilite,
                atrib = AtributBonus.Int,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "TripleShot")
            {
                Damage = 90,
                manaCost = 60,
                block = 33,
                Amplificator = 1.025,
                cooldown = 10,
                BonusMultiplier = 1.175,
                tipo = SkillType.habilite,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillDash("/Assets/Images/Item2.jpg", "Dash")
            {
                Damage = 0,
                manaCost = 45,
                block = 90,
                Amplificator = 0.28,
                cooldown = 10,
                timer = 0,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillHidden("/Assets/Images/Item1.jpg", "camuflagem")
            {
                manaCost = 70,
                block = 67,
                Amplificator = 1.01,
                Buff = 1.09,
                cooldown = 10,
                timer = 3,
                tipo = SkillType.habilite,
                atrib = AtributBonus.agl,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillBroken("/Assets/Images/Item2.jpg", "Expor fraqueza")
            {
                Damage = 50,
                manaCost = 65,
                block = 56,
                Amplificator = 1.04,
                Buff = 1.085,
                cooldown = 10,
                timer = 5,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "TIRO aperfeiçoado")
            {
                Damage = 300,
                manaCost = 200,
                block = 33,
                Amplificator = 1.25,
                cooldown = 30,
                BonusMultiplier = 0.40,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "Dragon force")
            {
                Damage = 300,
                manaCost = 200,
                block = 66,
                Amplificator = 1.30,
                cooldown = 35,
                BonusMultiplier = 1.30,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "chuva de Mil flechas")
            {
                Damage = 250,
                manaCost = 200,
                block = 99,
                Amplificator = 1.25,
                cooldown = 25,
                BonusMultiplier = 1.01,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.dex,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal,
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
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "bola de fogo")
            {
                Damage = 0,
                manaCost = 50,
                block = 2,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipobuff = SkillTypeBuff.normal,
                tipoatributo = Element.Fire
            });
            skillManager.SkillList.Add(new SkillSlowbuff("/Assets/Images/Item1.jpg", "estilhaços")
            {
                Damage = 0,
                manaCost = 40,
                block = 6,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Ice,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Item2.jpg", "aprisionar")
            {
                Damage = 0,
                manaCost = 60,
                block = 8,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "bolhas")
            {
                Damage = 0,
                manaCost = 35,
                block = 11,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "choque")
            {
                Damage = 0,
                manaCost = 45,
                block = 45,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillSlowbuff("/Assets/Images/Item2.jpg", "nevasca")
            {
                Damage = 0,
                manaCost = 80,
                block = 22,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Ice,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillSilence("/Assets/Images/Chao.jpg", "ilusion")
            {
                Damage = 0,
                manaCost = 180,
                block = 78,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "lava")
            {
                Damage = 0,
                manaCost = 230,
                block = 33,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Fire,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "jorrada")
            {
                Damage = 0,
                manaCost = 300,
                block = 90,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillHidden("/Assets/Images/Item1.jpg", "darkside")
            {
                Damage = 0,
                manaCost = 150,
                block = 67,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillSilence("/Assets/Images/Item1.jpg", "usurpar")
            {
                Damage = 0,
                manaCost = 200,
                block = 56,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "Meteoro")
            {
                Damage = 0,
                manaCost = 400,
                block = 33,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Fire,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "Tsunami")
            {
                Damage = 0,
                manaCost = 450,
                block = 66,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "relógio do apocalipse")
            {
                Damage = 0,
                manaCost = 750,
                block = 99,
                Amplificator = 0.01,
                cooldown = 0,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            className = "Mago";
        }
    }
}