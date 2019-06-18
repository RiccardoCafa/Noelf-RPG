using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Shop_Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            this.EventoFala += GameManager.instance.player._Questmanager.EventoFalaComNPCDaQuest; 
        }

        public void StartConversation()
        {
            InterfaceManager.instance.CallConversationBox(this);
            InterfaceManager.instance.ConvHasToClose = false;
            InterfaceManager.instance.Conversation = true;
            OnEventoFalar();
        }

        public void EndConversation()
        {
            foreach(NPCFunction f in Functions.Values)
            {
                f.EndFunction();
            }
            InterfaceManager.instance.ConvHasToClose = true;
        }

        public void AddFunction(NPCFunction Function)
        {
            Functions.Add(Function.GetFunctionName(), Function);
        } 

        public NPCFunction GetFunction(string key)
        {
            if (Functions.ContainsKey(key))
                return Functions[key];
            else return null;
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
            GameManager.instance.traderTarget = this;
            GameManager.instance.OpenShop();
        }
        public void EndFunction()
        {
            InterfaceManager.instance.CloseShop();
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
            GameManager.instance.questerTarget = this;
            InterfaceManager.instance.OpenQuest();
            
        }
        
        //GameManager.player._Questmanager.ReceiveNewQuest(myQuest);
        public void AcceptQuest()
        {
            if(GameManager.instance.player._Questmanager.CheckQuestLevel(myQuest))
            {
                GameManager.instance.player._Questmanager.ReceiveNewQuest(myQuest);
            }
        }

        public void EndFunction()
        {
            InterfaceManager.instance.CloseQuest();// .CloseQuestWindow();
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
