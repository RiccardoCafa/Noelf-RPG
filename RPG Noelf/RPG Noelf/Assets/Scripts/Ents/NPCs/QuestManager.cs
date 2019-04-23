using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    public class  QuestManager
    {
        private const int maxActiveQuests = 1;//numero máximo de quests ativas de um player
        public int numActiveQuests { get; set; }
        public List<Quest> allQuests = new List<Quest>();//lista com todas as quests, ativas ou não, excluindo completas
        public List<Quest> activeQuests = new List<Quest>();//lista com todas as quests ativas no momento
        public List<Quest> finishedQuests = new List<Quest>();//lista de todas as quest completas
        private Player player;
        public QuestManager(Player player)
        {
            numActiveQuests = 0;
            this.player = player;
        }

        //adicione uma quest nova ao player
        public void ReceiveNewQuest(Quest q)
        {
            int playerLevel = player.level.actuallevel;
            if(q.level <= playerLevel)//checa se o level do player é compativel com a quest
            {
                if (numActiveQuests >= maxActiveQuests)//checagem se o numero de quests ativas é o limite
                {
                    allQuests.Add(q);
                }
                else
                {
                    allQuests.Add(q);
                    activeQuests.Add(q);
                    numActiveQuests++;
                }
                
            }
        }
        public void ReceiveNewQuest(Quest q, uint genericID)
        {
            int playerLevel = player.level.actuallevel;
            if (q.level <= playerLevel && genericID == q.RequiredID)//checa se o level do player é compativel com a quest
            {
                if (numActiveQuests >= maxActiveQuests)//checagem se o numero de quests ativas é o limite
                {
                    allQuests.Add(q);
                }
                else
                {
                    allQuests.Add(q);
                    activeQuests.Add(q);
                    numActiveQuests++;
                }

            }
        }

        //completar uma quest
        public bool CheckQuestIsComplete(Quest q)
        {
            if (q.isComplete)
            {
                allQuests.Remove(q);
                activeQuests.Remove(q);
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
                player.level.GainEXP(q.GainedXP);
                player._Inventory.AddGold(q.GainedGold);
      
            }       

        }
        //setar uma quest para o status de Ativa
        //esta primeira seta a partir de uma quest especifica
        public void SetQuestToActive(Quest q)
        {
            if(allQuests.Contains(q) == true && activeQuests.Contains(q) == false && numActiveQuests < maxActiveQuests)
            {
                activeQuests.Add(q);
            }
        }
        //esta seta a partir de um ID de ativação
        public void SetQuestToActive(uint activatingID)
        {
            Quest generic = QuestList.SearchQuest(activatingID);
            allQuests.Add(generic);
            if(numActiveQuests <= maxActiveQuests)
            {
                activeQuests.Add(generic);
            }
            else
            {

            }

        }

        //setar uma quest para o status de finalizada
        public void SetQuestToFinished(Quest q)
        {
            if(allQuests.Contains(q) == true && activeQuests.Contains(q) == true  && q.isComplete == true)
            {
                allQuests.Remove(q);
                finishedQuests.Add(q);
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

        



    }


}
