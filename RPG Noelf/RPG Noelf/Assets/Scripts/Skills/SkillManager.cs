using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.PlayerFolder;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    public class SkillManager//adiministrador de skills no game
    {
        public List<SkillGenerics> SkillList { get; set; }
        public SkillGenerics[] SkillBar { get; set; }
        public SkillGenerics Passive { get; set; }
        public Player myPlayer;
        public Thread ManageSkill;
        public int SkillPoints { get; set; }
        public uint i = 0;

        public SkillManager(Player myPlayer)
        {
            SkillPoints = 0;
            this.myPlayer = myPlayer;
            SkillList = new List<SkillGenerics>();
            SkillBar = new SkillGenerics[4];
            ManageSkill = new Thread(ManageSkillThread);
            //ManageSkill.Start();
        }
        
        public void ManageSkillThread()
        {
            while(true)
            {
                Thread.Sleep(1000);
                MainPage.i++;
            }
        }

        public void SetWarriorPassive(string name, string pathImage)
        {
            Passive = new SkillBuff(name, pathImage)
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                Buff = 1.04,
                Buffer = BuffDebuffTypes.Dmg,
                cooldown = 0,
                Timer = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            };
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }
        public void SetArcherPassive(string name, string pathImage)//ainda tem que mexer
        {
            Passive = new SkillBuff(name, pathImage)
            {
                    Damage = 0,
                    manaCost = 0,
                    block = 1,
                    Amplificator = 0.01,
                    Buff = 1.04,
                    Buffer = BuffDebuffTypes.Dex,
                    cooldown = 0,
                    Timer = 0,
                    tipo = SkillType.passive,
                    atrib = AtributBonus.dex,
                    tipoatributo = Element.Common
            };
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }
        public void SetMagePassive(string name, string pathImage)//aqui tb
        {
            Passive = new SkillBuff(name, pathImage)
            {
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                Buff = 1.04,
                Buffer = BuffDebuffTypes.Dmg,
                cooldown = 0,
                Timer = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            };
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }
        public void AddSkillToBar(SkillGenerics s, int index)
        {
            if (s.tipobuff == SkillTypeBuff.normal)
            {
                SkillBar[index] = s as Skill;
            }
            else
            {
             
                 SkillBar[index] = s as SkillBuff;
                
            }
            
        }
        
        private bool TestLevelBlock(SkillGenerics skill)
        {
            if (myPlayer.level.actuallevel > skill.block)
            {
                return true;
            }

            return false;
        }

        public void ChangeSkill(SkillGenerics skill)
        {
            if (skill.tipo == SkillType.habilite)
            {
                foreach (SkillGenerics s in SkillBar)
                {
                    if (s != null)
                    {
                        if (s.Equals(skill))
                        {
                            return;
                        }
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (SkillBar[i] == null)
                    {
                        SkillBar[i] = skill;
                        break;
                    }
                }
            }
            else if (skill.tipo == SkillType.ultimate)
            {
                if (SkillBar[3] == null)
                {
                    SkillBar[3] = skill;
                }
            }
        }

        public bool UnlockSkill(int index)
        {
            if(myPlayer.level.actuallevel >= SkillList.ElementAt(index).block)
            {
                SkillList.ElementAt(index).Unlocked = true;
                SkillPoints--;
                return true;
            }
            return false;
        }

        public bool UnlockSkill(SkillGenerics skillToUnlock)
        {
            if (myPlayer.level.actuallevel >= skillToUnlock.block)
            {
                skillToUnlock.Unlocked = true;
                SkillPoints--;
                return true;
            }
            return false;
        }

        public bool UpSkill(SkillGenerics skill)
        {
            int MinimiumLevel = 0;

            if (SkillPoints <= 0 || !TestLevelBlock(skill))
            {
                return false;
            }
            
            if(!skill.Unlocked)
            {
                if(UnlockSkill(skill))
                {
                    SkillPoints--;
                    skill.Unlocked = true;
                    for (int i = 0; i < 3; i++)
                    {
                        if (SkillBar[i] == null)
                        {
                            AddSkillToBar(skill, i);
                            break;
                        }
                    }
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                if (skill.tipo == SkillType.ultimate)
                {
                    MinimiumLevel = 10;
                }
                else if (skill.tipo == SkillType.passive)
                {
                    MinimiumLevel = 15;
                }
                else if(skill.tipo == SkillType.habilite)
                {
                    MinimiumLevel = 25;
                }

                if (skill.Lvl < MinimiumLevel)
                {
                    skill.Lvl++;
                    SkillPoints--;
                    skill.Unlocked = true;
                    return true;
                } else
                {
                    return false;
                }
            }
        }
	}
}
    


