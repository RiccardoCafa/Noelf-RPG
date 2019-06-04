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

namespace RPG_Noelf.Assets.Scripts.General
{
    public static class GameManager
    {
        // Player
        public static List<CharacterPlayer> players = new List<CharacterPlayer>();
        public static Player player;
        //public static CharacterPlayer characterPlayer;
        
        // User Interface
        public static InterfaceManager interfaceManager = new InterfaceManager();

        // Enviroment
        public static List<Character> characters = new List<Character>();
        public static MainCamera mainCamera;
        public static DayNight dayNight;

        // Mobs
        public static Mob mob;
        public static CharacterMob mobTarget;

        // NPC's
        public static Crafting CraftingStation;
        public static CharacterNPC npcCharacter;
        public static NPC npcTarget;
        public static Trader traderTarget;
        public static Quester questerTarget;
        
        // Audio


        public static void InitializeGame()
        {
            interfaceManager.Inventario = Game.inventarioWindow;
            QuestList.load_quests();
            Encyclopedia.LoadEncyclopedia();

            npcCharacter = new CharacterNPC(Encyclopedia.NonPlayerCharacters[1], 650, 150, 120, 60);
            npcCharacter.trigger.AddTrigger(player.box);
            
            player._Inventory.AddToBag(new Slot(3, Bag.MaxStack - 20));
            //player._Inventory.AddToBag(new Slot(21, 1));
            //player._Inventory.AddToBag(new Slot(22, 1));
            //player._Inventory.AddToBag(new Slot(24, 1));
            //player._Inventory.AddToBag(new Slot(25, 1));
            //player._Inventory.AddToBag(new Slot(26, 1));
            //player._Inventory.AddToBag(new Slot(27, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(24, 1));
            player._Inventory.AddToBag(new Slot(42, 999));
            //player._Inventory.AddToBag(new Slot(1, 999));
            CraftingEncyclopedia.LoadCraftings();
            CraftingStation = new Crafting();

            //npcCharacter = new CharacterNPC(Game.instance.CreateCharacterNPC(), Encyclopedia.NonPlayerCharacters[1]);
            //npcCharacter.UpdateBlocks(Game.TheScene);
            //npcCharacter.trigger.AddTrigger(characterPlayer);
            GameManager.player._Questmanager.ReceiveNewQuest(QuestList.allquests[1]);
            //player._Questmanager.ReceiveNewQuest(QuestList.allquests[1]);
            //npcTarget.EventoFala += player._Questmanager.EventoFalaComNPCDaQuest;
            //MainPage.instance.OpenQuest();

            //player._Inventory.AddToBag(new Slot(3, 1));
            //player._Inventory.AddToBag(new Slot(21, 1));
            //player._Inventory.AddToBag(new Slot(22, 1));
            //player._Inventory.AddToBag(new Slot(24, 1));
          //  player._Inventory.AddToBag(new Slot(25, 1));
            player._Inventory.AddToBag(new Slot(42, 6));
            player._Inventory.AddToBag(new Slot(1, 6));
            //characters.Add(mobTarget);
            //characters.Add(characterPlayer);
            //Parallel.Invoke(() => characters[0].Update(), () => characters[1].Update());
        }

        public static void InitializePlayer()
        {

        }

        public static void CreatePlayer()
        {
            /* Aqui vão ser implementados os métodos que irão criar o player
              assim como fazer chamada pra main page e criá-lo graficamente */
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
