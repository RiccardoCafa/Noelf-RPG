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
        private const int maxActiveQuests = 5;//numero máximo de quests ativas de um player
        public int numActiveQuests { get; set; }
        public List<Quest> allQuests = new List<Quest>();//lista com todas as quests, ativas ou não, excluindo completas
        public List<Quest> activeQuests = new List<Quest>();//lista com todas as quests ativas no momento
        public List<Quest> finishedQuests = new List<Quest>();//lista de todas as quest completas
        public QuestManager()
        {
            numActiveQuests = 0;
        }

        //adicione uma quest nova ao player
        public void ReceiveNewQuest(Quest q, int playerLevel)
        {
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
        public void GainRewards(Player p, Quest q)
        {

        }
        //setar uma quest para o status de Ativa
        public void SetQuestToActive(Quest q)
        {

        }

    }


}
