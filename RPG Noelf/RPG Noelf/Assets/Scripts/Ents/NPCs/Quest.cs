using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    public enum TypeQuest
    {
        Collecting,Hunting,Boss,Searching,Speaking

    }
    public enum TypeCounting
    {
        MobCount, ItemCount
    }



    public class Quest
    {
        public string name { get; set; }
        public string Description { get; set; }
        public TypeQuest type { get; set; }
        public int QuestStatus { get; set; }
        public uint GainedXP { get; set; }
        public int GainedGold { get; set; }
        public bool isComplete { get; set; }

        public Quest(string name)
        {
            this.name = name;
        }



    }

    public class CountQuest:Quest
    {
        int countMobs { get; set; }
        public CountQuest(string name):base(name)
        {

        }

    }
    class SpeakQuest : Quest
    {
        public NPC targetedGuy { get; set; }
        public SpeakQuest(string name) : base(name)
        {

        } 
    }




}
