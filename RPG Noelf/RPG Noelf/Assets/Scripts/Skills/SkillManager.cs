using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    public class SkillManager//adiministrador de skills no game
    {
        public static SkillManager instance;
        public List<SkillGenerics> SkillList { get; set; }
        private List<SkillGenerics> skilltime = new List<SkillGenerics>();//lista de skills que foram usadas

        public SkillGenerics[] SkillBar { get; set; }
        public SkillGenerics Passive { get; set; }

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public Player myPlayer;

        public int SkillPoints { get; set; }
        public uint i = 0;

        private double RealTime = 0;

        public SkillManager(Player myPlayer)
        {
            instance = this;
            SkillPoints = 0;
            this.myPlayer = myPlayer;
            SkillList = new List<SkillGenerics>();
            SkillBar = new SkillGenerics[4];
            //DispatcherSetup();
        }

        public void BeAbleSkill(int index)//tipobuff
        {
            if (RealTime == 0) DispatcherSetup();
            if(SkillBar[index]!= null && SkillBar[index].locked == true)
            {
                if (SkillBar[index].tipobuff == SkillTypeBuff.debuff || SkillBar[index].tipobuff == SkillTypeBuff.normal)
                {
                    myPlayer.Attack(SkillBar[index]);
                }
                else
                {
                    SkillBar[index].UseSkill(myPlayer, myPlayer);
                    SkillBar[index].CountBuffTime = RealTime + SkillBar[index].timer;
                }

                SkillBar[index].CountTime = RealTime + SkillBar[index].cooldown;
                skilltime.Add(SkillBar[index]);
                if (skilltime.Count == 0)
                {
                    dispatcherTimer.Stop();
                    RealTime = 0;
                }
                SkillBar[index].locked = false;
            }
        }
        private void DispatcherSetup()
        {
            dispatcherTimer.Tick += Timer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void Timer(object sender, object e)
        {
            SkillGenerics[] removeSkills = new SkillGenerics[skilltime.Count];
            int count = 0;
            foreach (SkillGenerics habilite in skilltime)//para verificar se as skills ja acabaram seus tempos de CD
            {
                if (habilite.CountTime <= RealTime)
                {
                    habilite.CountTime = 0;
                    habilite.Active = true;
                    habilite.locked = true;
                    removeSkills[count] = habilite;
                    count++;
                    //skilltime.Remove(habilite);
                }
                if (habilite.CountBuffTime <= RealTime)
                {
                    habilite.CountBuffTime = 0;
                    habilite.RevertSkill(myPlayer);
                }
            }
            foreach(SkillGenerics s in removeSkills)
            {
                skilltime.Remove(s);
            }
            RealTime++;
        }

        public void SetWarriorPassive(string name, string pathImage)
        {
            Passive = new SkillDmgBuff(name, pathImage)
            {
                ID = 0,
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                Buff = 1.04,
                cooldown = 0,
                timer = 0,
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
            Passive = new SkillDex(name, pathImage)
            {
                    ID = 15,
                    Damage = 0,
                    manaCost = 0,
                    block = 1,
                    Amplificator = 0.01,
                    Buff = 1.04,
                    cooldown = 0,
                    timer = 0,
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
            Passive = new SkillDmgBuff(name, pathImage)
            {
                ID = 30,
                Damage = 0,
                manaCost = 0,
                block = 1,
                Amplificator = 0.01,
                Buff = 1.04,
                cooldown = 0,
                timer = 0,
                tipo = SkillType.passive,
                atrib = AtributBonus.For,
                tipoatributo = Element.Common
            };
            Passive.description = "Skill Passiva que faz coisas de skill passiva. Essa é uma descrição POG e XGH";
            Passive.Unlocked = true;
            SkillList.Add(Passive);
        }
        
        public void UpdateEncyclopedia()
        {
            uint count = 0;
            foreach(SkillGenerics s in SkillList)
            {
                Encyclopedia.skillsImages.Add(count, new BitmapImage(new Uri("ms-appx://" + s.pathImage)));
                count++;
            }
        }

        public void AddSkillToBar(SkillGenerics s, int index)
        {
            SkillBar[index] = s;
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
                SkillBar[3] = skill;
                //if (SkillBar[3] == null)
                //{
                //}
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
        public SkillGenerics FindSkill(int id)
        {
            foreach (SkillGenerics item in SkillList)
            {
                if (item.ID == id) return item;
            }
            return null;
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
    