using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Shop_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    public class NPC
    {
        private Dictionary<string, NPCFunction> Functions = new Dictionary<string, NPCFunction>();

        public Level MyLevel { get; set; }
        public uint IDnpc { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public delegate void EventoFalar(object source, EventArgs arg, uint id);
        public event EventoFalar EventoFala;
        
        public NPC()
        {
            this.EventoFala += GameManager.player._Questmanager.EventoFalaComNPCDaQuest;
        }

        public void StartConversation()
        {
            MainPage.instance.CallConversationBox(this);
            GameManager.interfaceManager.ConvHasToClose = false;
            GameManager.interfaceManager.Conversation = true;
            GameManager.player._Questmanager.PrintActualQuestStatus();
            OnEventoFalar();
        }

        public void EndConversation()
        {
            foreach(NPCFunction f in Functions.Values)
            {
                f.EndFunction();
            }
            GameManager.interfaceManager.ConvHasToClose = true;
        }

        public void AddFunction(NPCFunction Function)
        {
            Functions.Add(Function.GetFunctionName(), Function);
        } 

        public NPCFunction GetFunction(string key)
        {
            return Functions[key];
        }

        public List<string> GetFunctionsString()
        {
            List<string> funcList = new List<string>();
            foreach(string s in Functions.Keys)
            {
                funcList.Add(s);
            }
            return funcList;
        }

        public int GetFunctionSize()
        {
            return Functions.Count;
        }

        protected virtual void OnEventoFalar()
        {
            EventoFala?.Invoke(this, EventArgs.Empty, this.IDnpc);
        }

    }

    public interface NPCFunction
    {
        string GetFunctionName();
        void MyFunction(object sender, RoutedEventArgs e);
        void EndFunction();
    }

    public sealed class Trader : NPCFunction
    {
        public Shop shop;
        public Trader(Shop shop)
        {
            this.shop = shop;
        }

        public Trader()
        {
            shop = new Shop();
        }
        public void MyFunction(object sender, RoutedEventArgs e)
        {
            GameManager.traderTarget = this;
            GameManager.OpenShop();
        }
        public void EndFunction()
        {
            if (GameManager.interfaceManager.ShopOpen) GameManager.CloseShop();
        }
        public string GetFunctionName()
        {
            return "Trader";
        }
    }
    public sealed class Quester : NPCFunction
    {
        public Quest myQuest { get; set; }
       
        public Quester(uint quest)
        {
            myQuest = QuestList.allquests[quest];
        }
        public void MyFunction(object sender, RoutedEventArgs e)
        {
            GameManager.questerTarget = this;
            MainPage.instance.OpenQuest();
            
        }
        
        //GameManager.player._Questmanager.ReceiveNewQuest(myQuest);
        public void AcceptQuest()
        {
            if(GameManager.player._Questmanager.CheckQuestLevel(myQuest))
            {
                GameManager.player._Questmanager.ReceiveNewQuest(myQuest);
            }
        }

        public void EndFunction()
        {
            if (GameManager.player._Questmanager.activeQuests.Contains(myQuest))
            {
                GameManager.CloseQuestWindow();
            }
        }

        public string GetFunctionName()
        {
            return "Quester";
        }

        public uint GetQuestID()
        {
            return QuestList.allquests.FirstOrDefault(x => x.Value.Equals(myQuest)).Key;
            /*
             Função do tipo predicar, foi chamada a função FirstOrDefault, que retorna um valor, e dentro desta função
             se deu um parametro para indentificar o valor a ser procurado, no final da senteça, retorna-se a chave do dicionario
             referente a este valor             
             */
        }
    }

    


    public sealed class RuneMaster
    {
        //TODO
    }

}
