using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    public class QuestEventArgs
    {
        public Quest quest { get; set; }
    }

    public class  QuestManager
    {
        public delegate void QuestHandler(object sender, QuestEventArgs e);
        public event QuestHandler QuestUpdated;
        
        public List<Quest> allQuests = new List<Quest>();//lista com todas as quests, excluindo completas
        public List<Quest> finishedQuests = new List<Quest>();//lista de todas as quest completas
        public Quest actualQuest { get; set; }
        private Player player;

        public QuestManager(Player player)
        {
            this.player = player;
        }

        public void SetActualQuest(Quest quest)
        {
            if (quest == null) return;
            actualQuest = quest;
            //OnQuestUpdated();
        }

        //adicione uma quest nova ao player
        public void ReceiveNewQuest(Quest q)
        {
            int playerLevel = player.level.actuallevel;
            if(q.level <= playerLevel)//checa se o level do player é compativel com a quest
            {
                allQuests.Add(q);
                //OnQuestUpdated();
            }
            
        }
        public void ReceiveNewQuest(Quest q, uint genericID)
        {
            int playerLevel = player.level.actuallevel;
            if (q.level <= playerLevel && genericID == q.RequiredID)//checa se o level do player é compativel com a quest
            {
                allQuests.Add(q);
                //OnQuestUpdated();
            }
        }

        //completar uma quest
        public bool CheckQuestIsComplete(Quest q)
        {
            if (q.isComplete)
            {
                allQuests.Remove(q);
                finishedQuests.Add(q);
                return true;
            }
            else
            {
                return false;
            }

        }

        //receber recompensas da Quest
        public void GainRewards(Quest q)
        {
            if(q.isComplete == true)
            {
                if(q.GainedItem != null)
                {
                    player._Inventory.AddToBag(q.GainedItem);
                }
                player.level.GainEXP(q.GainedXP);
                player._Inventory.AddGold(q.GainedGold);
            }       

        }

        //setar uma quest para o status de finalizada
        public void SetQuestToFinished(Quest q)
        {
            if(allQuests.Contains(q) == true && q.isComplete == true)
            {
                allQuests.Remove(q);
                finishedQuests.Add(q);
                //OnQuestUpdated();
            }
        }

        //checar o level da quest, se é possivel adquiri-la ou não
        public bool CheckQuestLevel(Quest required)
        {
            int playerLevel = player.level.actuallevel;
            Quest quest = required;
            if (quest.level > playerLevel)
            {
                return false;
            }else if(quest.level <= playerLevel)
            {
                return true;
            }
            return false;
        }

        public void RemoveQuest(Quest q)
        {
            allQuests.Remove(q);
            //OnQuestUpdated();
        }

        public void GiveUpActualQuest()
        {
            allQuests.Remove(actualQuest);
            if(allQuests.Count > 0)
            {
                actualQuest = allQuests[0];
            } else
            {
                actualQuest = null;
            }
            //OnQuestUpdated();
        }

        public void OnQuestUpdated()
        {
            QuestUpdated?.Invoke(this, new QuestEventArgs() { quest = actualQuest });
        } 

        public void EventoFalaComNPCDaQuest(object source, EventArgs arg, uint id)
        {   
            SpeakQuest generic = new SpeakQuest(id);
            foreach(Quest q in allQuests)
            {
                if(q.QUEST_ID == generic.QUEST_ID)
                {
                    generic = (SpeakQuest) q;
                    generic.isComplete = true;
                    this.actualQuest = generic;
                }
            }
        }
    }
}
