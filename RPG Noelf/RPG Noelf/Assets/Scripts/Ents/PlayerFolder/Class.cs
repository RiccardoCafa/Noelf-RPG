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
                description = "aumenta a resistência em 25%, por 5seg",
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
                description = "causa dano bônus 10% da força",
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
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Item2.jpg", "Cabeçada")
            {
                description = "atordoa o adversário por 0.1",
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
                description = "Aumenta agilidade em 40%",
                Damage = 0,
                manaCost = 200,
                block = 33,
                Amplificator = 1.01,
                cooldown = 35,
                timer = 6,
                atrib = AtributBonus.agl,
                Buff = 1.39,
                tipo = SkillType.ultimate,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Item1.jpg", "triunfar")
            {
                description = "ausa dano equivalente a 20% da força e dando um stun de 0.5s",
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
                description = "aumenta a dano em 15%",
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
                description = "diminui a armadura do adversário  em 10%",
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
                description = "ignora o próximo hit",
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
                description = "fica invuneravel por 3 seg",
                Amplificator = 1.01,
                cooldown = 35,
                timer = 3,
                Buff = 1.99,
                tipo = SkillType.ultimate,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            skillManager.SkillList.Add(new SkillDobleHit("/Assets/Images/Item1.jpg", "doble hit")
            {
                Damage = 0,
                manaCost = 60,
                description = "Causa o dobro de dano",
                block = 56,
                Amplificator = 0.4,
                cooldown = 15,
                tipo = SkillType.habilite,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "A marca do duel")//***//
            {
                description = "causa mais dano na pessoa marcada por 4seg escala equivalente a 20% da forca",
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
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "Golpe do berserk")/**/
            { 
                description = "Causa um Dano Brutal contra o adversario",
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
                description = "Imobiliza o adversário causando dano físico equivalente a 20% da forca",
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
                description = "Durante 6 seg seus ataques seus ataques causam mais 60% dano",
                Damage = 0,
                manaCost = 200,
                block = 99,
                Buff = 1.60,
                Amplificator = 0.01,
                cooldown = 35,
                timer = 6,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.buff
            });
            className = "Guerreiro";
            skillManager.UpdateEncyclopedia();

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
                description = "aumenta a agilidade em 25%",
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
            skillManager.SkillList.Add(new Skill("/Assets/Images/Skills/ArcherSkill2.jpg", "Flecha de fogo")
            {
                description = "causa dano equivalente a 10% da int",
                Damage = 50,
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
                description = "prende o adversário causando dano equivalente a 20% dex",
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
                description = "Atordoa o adversario 0.25s",
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
                description = "causa dano e lentidão equivalente a 10% da int",
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
                description = "causa dano equivalente a 25% da dex",
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
                description = "aumenta a chance de acerto crítico em 15% Por 30s",
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
                description = "causa dano físico de 20% da dex",
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
                description = "avança para alguma lado",
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
                description = "fica invisível por 3 seg e ao sair da camuflagem ganha 10%de agilidade",
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
                description = "diminui a resistência em 25%",
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
                description = "causa 40% de sua dextreza",
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
                description = "causa dano fisico/mágico equivalente a 30% da destreza e int",
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
                description = "causa dano físico em área igual a 30% da destreza",
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
            });
            className = "Arqueiro";
            skillManager.UpdateEncyclopedia();

            skillManager.SkillBar[0] = skillManager.SkillList[1];
            skillManager.SkillBar[1] = skillManager.SkillList[2];
            skillManager.SkillBar[2] = skillManager.SkillList[3];
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
                description = "causa dano mágico equivalente a 25% da int",
                Damage = 25,
                manaCost = 50,
                block = 2,
                Amplificator = 0.01,
                cooldown = 5,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipobuff = SkillTypeBuff.normal,
                tipoatributo = Element.Fire
            });
            skillManager.SkillList.Add(new SkillSlowbuff("/Assets/Images/Item1.jpg", "estilhaços")
            {
                description = "causa dano mágico equivalente a 15% da int E causa 20% de lentidão",
                Damage = 25,
                manaCost = 40,
                block = 6,
                Amplificator = 0.01,
                cooldown = 7,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Ice,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Item2.jpg", "aprisionar")
            {
                description = "prende o cara por 2 segs",
                Damage = 0,
                manaCost = 60,
                block = 8,
                Amplificator = 0.01,
                cooldown = 12,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "bolhas")
            {
                description = "causa dano mágico equivalente a 15% da int",
                Damage = 15,
                manaCost = 35,
                block = 11,
                Amplificator = 0.01,
                cooldown = 7,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "choque")
            {
                description = "causa dano mágico equivalente a 25% da int",
                Damage = 50,
                manaCost = 45,
                block = 45,
                Amplificator = 0.01,
                cooldown = 8,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillSlowbuff("/Assets/Images/Item2.jpg", "nevasca")
            {
                description = "causa dano mágico equivalente a 10% da int E causa 25% de lentidão",
                Damage = 35,
                manaCost = 80,
                block = 22,
                Amplificator = 0.01,
                cooldown = 15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Ice,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new SkillPrison("/Assets/Images/Chao.jpg", "ilusion")
            {
                description = "atordoa por 0.1seg",
                Damage = 0,
                manaCost = 180,
                block = 78,
                Amplificator = 0.01,
                cooldown = 12,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item1.jpg", "lava")
            {
                description = "causa dano mágico equivalente a 25% da int",
                Damage = 65,
                manaCost = 230,
                block = 33,
                Amplificator = 0.01,
                cooldown = 20,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Fire,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "jorrada")
            {
                description = "causa dano mágico equivalente a 30% da int",
                Damage = 45,
                manaCost = 300,
                block = 90,
                Amplificator = 0.01,
                cooldown = 15,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillHidden("/Assets/Images/Item1.jpg", "darkside")
            {
                description = "fica invisível por 1 seg",
                Damage = 0,
                manaCost = 150,
                block = 67,
                Amplificator = 0.01,
                cooldown = 12,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new SkillSilence("/Assets/Images/Item1.jpg", "usurpar")
            {
                description = "silencia por 2 segundos",
                Damage = 0,
                manaCost = 200,
                block = 56,
                Amplificator = 0.01,
                cooldown = 20,
                tipo = SkillType.habilite,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.debuff
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Item2.jpg", "Meteoro")
            {
                description = "causa dano mágico em área equivalente a 65% da int",
                Damage = 500,
                manaCost = 400,
                block = 33,
                Amplificator = 0.01,
                cooldown = 45,
                xoffset = 150,
                yoffset = -200,
                gravity = 1000,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Fire,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "Tsunami")
            {
                description = "causa dano mágico equivalente a 40% da int e da 20% de lentidão",
                Damage = 450,
                manaCost = 450,
                block = 66,
                Amplificator = 0.01,
                cooldown = 45,
                spd = 20,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            skillManager.SkillList.Add(new Skill("/Assets/Images/Chao.jpg", "relógio do apocalipse")
            {
                description = "dano magico equivalente a 90% da int",
                Damage = 1000,
                manaCost = 750,
                block = 99,
                Amplificator = 0.01,
                cooldown = 60,
                tipo = SkillType.ultimate,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common,
                tipobuff = SkillTypeBuff.normal
            });
            className = "Mago";
            skillManager.UpdateEncyclopedia();
        }
    }
}