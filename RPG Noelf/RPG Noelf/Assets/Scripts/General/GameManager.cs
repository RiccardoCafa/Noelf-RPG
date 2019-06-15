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
using Windows.UI.Xaml.Controls;

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
        public static InterfaceManager interfaceManager;

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
        
        public static void InitializeGame(Canvas Tela)
        {
            TStart = new Task(delegate { Start(Tela); } );
            TStart.Start();
        }

        public static async void Start(Canvas Tela)
        {
            // Instanciar player

            // Interface
            interfaceManager = new InterfaceManager(Tela);

            // Banco de dados
            Encyclopedia.LoadEncyclopedia();
            CraftingEncyclopedia.LoadCraftings();

            // Quests
            QuestList.load_quests();
            player._Questmanager.ReceiveNewQuest(QuestList.allquests[1]);

            // Crafting
            CraftingStation = new Crafting();

            // Carrega interface
            

            // Update
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TUpdate.Start();
            });
        }

        /*_str = _spd = _dex = _con = _mnd = 0;
            
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                bagWindow = BagWindow;
                //mobStatus = xMobStatus;
                Window.Current.CoreWindow.KeyDown += Skill_KeyDown;
                scene1 = new LevelScene(xScene);//criaçao do cenario

                ////CreatePlayer();
                //GameManager.InitializeGame();
                //CreateInventory(BagWindow);
                //CreateChestWindow(350, 250);
                //CreateMob();
                CreateCraftingWindow();
                CreateConversationLayout();
                CreateSkill(WindowTreeSkill);

                GameManager.player._Inventory.BagUpdated += UpdateBagEvent;
                GameManager.player.Equipamento.EquipUpdated += UpdateEquipEvent;
                GameManager.player.Equipamento.EquipUpdated += UpdatePlayerInfoEvent;
                GameManager.player.PlayerUpdated += UpdatePlayerInfoEvent;
                GameManager.player._Questmanager.QuestUpdated += UpdateQuestManagerEvent;
                Conversation.PointerPressed += EndConversation;

                UpdateBag();
                UpdateSkillTree();
                UpdatePlayerInfo();
                UpdateSkillBar();
                UpdateShopInfo();

                SetEventForSkillBar();
                SetEventForSkillTree();
                SetEventForShopItem();
                SetEventForEquip();

                ShopWindow.Visibility = Visibility.Collapsed;
                _InventarioCanvas.Visibility = Visibility.Collapsed;
                Atributos.Visibility = Visibility.Collapsed;
                WindowEquipamento.Visibility = Visibility.Collapsed;
                WindowTreeSkill.Visibility = Visibility.Collapsed;


                GameManager.player._Questmanager.ReceiveNewQuest(QuestList.allquests[1]);
                GameManager.player._Questmanager.ReceiveNewQuest(QuestList.allquests[2]);
                GameManager.player._Questmanager.actualQuest = GameManager.player._Questmanager.allQuests[1];
                

                //CreateChest(100, 600, new Bau(Category.Legendary, 15));
                //CreateChest(350, 600, new Bau(Category.Normal, 10));
            });*/

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
            InterfaceManager.instance.OpenShop();
        }

        public static void CloseShop()
        {
            InterfaceManager.instance.CloseShop();
        }

        public static void CloseQuestWindow()
        {
            if(questerTarget != null)
            {
                InterfaceManager.instance.CloseQuest();
            }
        } 

        public static void OpenQuestWindow()
        {
            if (questerTarget != null)
            {
                //InterfaceManager.instance.Quest OpenQuest();
            }
            
        }

    }
}
