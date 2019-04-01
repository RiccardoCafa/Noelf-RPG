using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    public abstract class  QuestManager
    {
        public List<Quest> managedQuests { get; set;}

        public abstract int CheckQuestStatus(Quest q);
        public abstract bool CheckQuestIsComplete(Quest q);





    }


    public class CountQuestManager : QuestManager
    {
        public override bool CheckQuestIsComplete(Quest q)
        {
            if(q is CountQuest)
            {
                CountQuest temporary = q as CountQuest;
                if(temporary.countMobs == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }
    
        //checar o status da quest atual
        public override int CheckQuestStatus(Quest q)
        {       
                if(q.isComplete != true)
                {
                    return q.QuestStatus;
                }
                else
                {
                    return 0;
                }
        }


    }
}
