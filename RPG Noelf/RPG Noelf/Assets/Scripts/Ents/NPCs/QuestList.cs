using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    class QuestList
    {
        public static Dictionary<int, Quest> allquests;

        public static void load_quests()
        {
            allquests = new Dictionary<int, Quest>();

            SpeakQuest quest1 = new SpeakQuest("Here comes a new hero",(uint) 1,(uint) 0);




        }

    }
}
