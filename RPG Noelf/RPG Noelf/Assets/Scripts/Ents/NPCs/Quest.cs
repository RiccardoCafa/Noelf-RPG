using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
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
    public enum QuestStatus
    {
        Complete,Failed,inProgress
    }

    public abstract class Quest
    {
        public string name { get; set; }//nome da quest
        public string Description { get; set; }//descrição da quest
        public TypeQuest type { get; set; }//tipo de quest
        public int QuestPart { get; set; }//progresso da quest
        public uint GainedXP { get; set; }//EXP recebido
        public int GainedGold { get; set; }//ouro recebido
        public QuestStatus QuestStatus { get; set; }
        public bool isComplete { get; set; }//está completa sim ou não

        public Quest(string name)
        {
            this.name = name;
            QuestStatus = QuestStatus.inProgress;
        }
    }

    public class CountQuest:Quest
    {
        public int countMobs { get; set; }
        public CountQuest(string name, int mobs):base(name)
        {
            countMobs = mobs;
        }


    }
    class SpeakQuest : Quest
    {
        public NPC targetedGuy { get; set; }
        public uint NedeedItem { get; set; }
        public SpeakQuest(string name) : base(name)
        {

        } 

    }

     class HuntingQuest : Quest
    {
        public int HuntedID { get; set; }
        public int HuntAmount { get; set; }
        public HuntingQuest(string name) : base(name)
        {

        }

    }



}
