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

        public void StartConversation()
        {
            
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

    }

    public interface NPCFunction
    {
        string GetFunctionName();
        void MyFunction(object sender, RoutedEventArgs e);
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
            
        }

        public string GetFunctionName()
        {
            return "Trader";
        }
    }

    public sealed class Quester : NPCFunction
    {
        public string GetFunctionName()
        {
            return "Quester";
        }

        public void MyFunction(object sender, RoutedEventArgs e)
        {
            
        }
    }

    public sealed class RuneMaster
    {

    }

}
