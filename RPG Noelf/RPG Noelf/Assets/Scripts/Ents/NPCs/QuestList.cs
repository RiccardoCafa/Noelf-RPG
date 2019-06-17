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

        public static void LoadQuests()
        {
            allquests = new Dictionary<uint, Quest>();

            SpeakQuest quest1 = new SpeakQuest((uint)1)
            {
                QUEST_ID = 1,
                name = "Chega ai parceiro",
                Description = "Fale com Lapa",
                RewardDescription = "Aprovação em LP II",
                GainedGold = 50,
                GainedXP = 100
            };
            allquests.Add(1, quest1);

            SpeakQuest quest2 = new SpeakQuest((uint)2)
            {
                QUEST_ID = 2,
                name = "Conheça o mundo",
                Description = "Veja a Quimera debaixo da plataforma",
                RewardDescription = "Ouro e Joias",
                GainedGold = 10,
                GainedXP = 1000,
                GainedItem = new Inventory_Scripts.Slot(7, 10)
            };
            allquests.Add(2, quest2);



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
