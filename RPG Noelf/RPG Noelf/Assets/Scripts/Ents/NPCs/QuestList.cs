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

            Quest quest1 = new Quest("Here comes A new hero");




        }

    }
}
