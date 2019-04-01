using RPG_Noelf.Assets.Scripts.Shop_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    class NPC
    {
        private Dictionary<string, NPCFunction> Functions = new Dictionary<string, NPCFunction>();

        public string Name { get; set; }
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public Level MyLevel { get; set; }

        public void StartConversation()
        {
            
        }

        public void AddFunction(NPCFunction Function)
        {
            Functions.Add(Function.GetFunctionName(), Function);
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

    }

    interface NPCFunction
    {
        string GetFunctionName();
        void MyFunction();
    }

    sealed class Trader : NPCFunction
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

        public void MyFunction()
        {
            
        }

        public string GetFunctionName()
        {
            return "Trader";
        }
    }

    sealed class Quester
    {

    }

    sealed class RuneMaster
    {

    }

}
