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
using RPG_Noelf.Assets.Scripts.Scenes;

namespace RPG_Noelf.Assets.Scripts.General
{
    public class GameManager
    {
        // SINGLETON
        public static GameManager instance;

        // Thread
        public Task TStart;
        public Task TUpdate;
        public Task TDraw;
        public bool Running = true;
        public bool CanGo = false;

        // Player
        public List<CharacterPlayer> players = new List<CharacterPlayer>();
        public Player player;
        
        // User Interface
        public InterfaceManager interfaceManager;

        // Enviroment
        public List<Character> characters = new List<Character>();
        public MainCamera mainCamera;
        public DayNight dayNight;
        public LevelScene scene;

        // Mobs
        public List<Mob> mobs = new List<Mob>();
        public CharacterMob mobTarget;

        // NPC's
        public Crafting CraftingStation;
        public CharacterNPC npcCharacter;
        public NPC npcTarget;
        public Trader traderTarget;
        public Quester questerTarget;
        
        public GameManager()
        {
            instance = this;
        }

        public void InitializeGame(Canvas Tela)
        {
            TStart = new Task(delegate { Start(Tela); } );
            TStart.Start();
        }

        public async void Start(Canvas Tela)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //Debug.WriteLine("Criando HuD");
                // Interface
                interfaceManager = new InterfaceManager(Tela);
                //Debug.WriteLine("HuD Criada");

                // Carregando itens
                Encyclopedia.LoadEncyclopedia();
                QuestList.LoadQuests();
                
                // Criar o scenario e instanciar o player
                scene = new LevelScene(interfaceManager.CanvasChunck01);

                // Banco de dados
                //Debug.WriteLine("Carregando banco de dados");
                QuestList.LoadQuests();
                Encyclopedia.LoadNPC();
                CraftingEncyclopedia.LoadCraftings();
                // Quests
                player._Questmanager.ReceiveNewQuest(QuestList.allquests[1]);
                player._Questmanager.ReceiveNewQuest(QuestList.allquests[2]);
                player._Questmanager.actualQuest = player._Questmanager.allQuests[1];

                // Criando HuD
                interfaceManager.GenerateHUD();

                // Crafting
                CraftingStation = new Crafting();

                player._Inventory.AddToBag(new Slot(2, 90));
                player._Inventory.AddToBag(new Slot(13, 1));
                player._Inventory.AddToBag(new Slot(21, 1));
                player._Inventory.AddToBag(new Slot(22, 1));
                player._Inventory.AddToBag(new Slot(23, 1));
                player._Inventory.AddToBag(new Slot(24, 1));
                player._Inventory.AddToBag(new Slot(18, 2));


                // Update
                TUpdate = new Task(Update);
                TUpdate.Start();

            });
            // Carrega interface



            // Draw
            //TDraw = new Task(Draw);
            //TDraw.Start();
        }

        /*  UpdateBag();
            UpdateSkillTree();
            UpdatePlayerInfo();
            UpdateSkillBar();
            UpdateShopInfo();*/

        public async void Update()
        {
            while(Running)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    interfaceManager.UpdateBag();

                    interfaceManager.UpdateSkillBar();

                    interfaceManager.UpdatePlayerInfo();

                    interfaceManager.UpdateEquip();

                    interfaceManager.UpdateActualQuestManager();

                    interfaceManager.UpdateQuestList();

                    interfaceManager.UpdateShopInfo();

                    foreach(DynamicSolid dyn in DynamicSolid.DynamicSolids)
                    {
                        dyn.Update();
                    }

                    foreach(Ent ent in Ent.Entidades)
                    {
                        ent.Update();
                    }

                    //instance.player.box.Update();
                    /*Parallel.ForEach(DynamicSolid.DynamicSolids, (current) =>
                    {
                        current.Update();
                    });*/

                    Task.Delay(1000 / 60);
                });

            }
        }

        public void Draw()
        {
            while(Running)
            {

            }
        }

        public void OpenShop()
        {
            if (traderTarget == null) return;
            interfaceManager.ShopOpen = true;
            InterfaceManager.instance.OpenShop();
        }

        public void CloseShop()
        {
            InterfaceManager.instance.CloseShop();
        }

        public void CloseQuestWindow()
        {
            if(questerTarget != null)
            {
                InterfaceManager.instance.CloseQuest();
            }
        } 

        public void OpenQuestWindow()
        {
            if (questerTarget != null)
            {
                InterfaceManager.instance.OpenQuest();
                //InterfaceManager.instance.Quest OpenQuest();
            }
            
        }

    }
}
