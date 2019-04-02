﻿using RPG_Noelf.Assets.Scripts.Ents.Mobs;
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
        // Player
        public static List<CharacterPlayer> players = new List<CharacterPlayer>();
        public static Player player;
        public static CharacterPlayer characterPlayer;

        // User Interface
        public static MainPage mainPage;
        public static InterfaceManager interfaceManager = new InterfaceManager();

        // Enviroment
        public static MainCamera mainCamera;
        public static DayNight dayNight;

        // Mobs
        public static CharacterMob mobTarget;

        // NPC's
        public static NPC npcTarget;
        public static Trader traderTarget;

        public static void InitializeGame()
        {
            interfaceManager.Inventario = MainPage.inventarioWindow;
            Encyclopedia.LoadItens();
        }

        public static void CreatePlayer()
        {
            /* Aqui vão ser implementados os métodos que irão criar o player
             * assim como fazer chamada pra main page e criá-lo graficamente
             */
        }

        public static void CreateNPC()
        {
            
        }

        public static void CreateMobs()
        {

        }

    }
}