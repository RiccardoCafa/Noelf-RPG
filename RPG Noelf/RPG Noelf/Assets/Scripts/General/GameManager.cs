using RPG_Noelf.Assets.Scripts.Crafting_Scripts;
using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.Ents.Mobs;
using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using RPG_Noelf.Assets.Scripts.Enviroment;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Mobs;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using Windows.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace RPG_Noelf.Assets.Scripts.General
{
    public static class GameManager
    {
        // Thread
        public static Task TStart;
        public static Task TUpdate;
        public static bool Running = true;

        // Player
        public static List<CharacterPlayer> players = new List<CharacterPlayer>();
        public static Player player;
        
        // User Interface
        public static InterfaceManager interfaceManager = new InterfaceManager();

        // Enviroment
        public static List<Character> characters = new List<Character>();
        public static MainCamera mainCamera;
        public static DayNight dayNight;

        // Mobs
        public static List<Mob> mobs = new List<Mob>();
        public static CharacterMob mobTarget;

        // NPC's
        public static Crafting CraftingStation;
        public static CharacterNPC npcCharacter;
        public static NPC npcTarget;
        public static Trader traderTarget;
        public static Quester questerTarget;
        
        public static void InitializeGame()
        {
            TStart.Start();
        }

        public static async void Start()
        {
            // Banco de dados
            Encyclopedia.LoadEncyclopedia();
            CraftingEncyclopedia.LoadCraftings();

            // Quests
            QuestList.load_quests();
            player._Questmanager.ReceiveNewQuest(QuestList.allquests[1]);

            // Crafting
            CraftingStation = new Crafting();

            // Carrega interface
            interfaceManager.Inventario = Game.inventarioWindow;

            // Update
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TUpdate.Start();
            });
        }

        public static void Update()
        {
            while(Running)
            {

            }
        }

        public static void InitializePlayer()
        {

        }

        public static void CreatePlayer()
        {
           
        }

        public static void CreateNPC()
        {
            
        }

        public static void CreateMobs()
        {

        }

        public static void OpenShop()
        {
            if (traderTarget == null) return;
            interfaceManager.ShopOpen = true;
            Game.instance.OpenShop();
        }

        public static void CloseShop()
        {
            Game.instance.CloseShop();
        }

        public static void CloseQuestWindow()
        {
            if(questerTarget != null)
            {
                Game.instance.CloseQuest();
            }
        } 

        public static void OpenQuestWindow()
        {
            if (questerTarget != null)
            {
                Game.instance.OpenQuest();
            }
            
        }

    }
}
