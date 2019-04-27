using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    class QuestList
    {
        public static Dictionary<uint, Quest> allquests;

        public static void load_quests()
        {
            allquests = new Dictionary<uint, Quest>();
            SpeakQuest quest1 = new SpeakQuest((uint) 1);
            quest1.name = "Chega ai parceiro";
            quest1.Description = "Fale com Lapa";
            quest1.RewardDescription = "Aprovação em LP II";
            allquests.Add(1, quest1);



        }
        //procurar a quest no dicionario de Quests
        public static Quest SearchQuest(uint genericID)
        {
            if(allquests.ContainsKey(genericID) == true)
            {
                return allquests[genericID];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            

        }



    }
}
