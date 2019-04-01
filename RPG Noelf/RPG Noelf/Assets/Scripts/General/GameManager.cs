using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using RPG_Noelf.Assets.Scripts.Enviroment;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Mobs;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using RPG_Noelf.Assets.Scripts.Shop_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.General
{
    public static class GameManager
    {
        public static MainPage mainPage;
        public static List<CharacterPlayer> players = new List<CharacterPlayer>();
        public static InterfaceManager interfaceManager = new InterfaceManager();
        public static CharacterPlayer player;
        public static CharacterMob mob;
        public static MainCamera mainCamera;
        public static Player p1, p2;

        // Enviroment
        public static DayNight dayNight;

        // NPC's
        public static NPC npc;

        public static void InitializeGame()
        {
            interfaceManager.Inventario = MainPage.inventarioWindow;
            Encyclopedia.LoadItens();
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

    }
}
