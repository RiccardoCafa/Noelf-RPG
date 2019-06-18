using RPG_Noelf.Assets.Scripts.Ents.PlayerFolder;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    public enum NPCStates {
        Interacting,
        Stop,
        Attacking,
        Following,
        Trading
    }

    public class CharacterNPC : Ent
    {
        public NPC MyNPC;
        public Trigger trigger;
        private PlayerLoader _PlayerLoader;
        public string id = "0000000";
        
        public CharacterNPC(NPC _NPC, double xi, double yi, double width, double height, double speed)
        {
            box = new DynamicSolid(xi, yi, width, height, speed);
            MyNPC = _NPC;
            box.PointerPressed += InteractWith;
            InterfaceManager.instance.CanvasChunck01.Children.Add(box);
            Load();
        }
        /* ID: rcxkysh
             *  0 r -> raça  0-2
             *  1 c -> classe  0-2
             *  2 x -> sexo  0,1
             *  3 k -> cor de pele  0-2
             *  4 y -> cor do olho  0-2
             *  5 s -> tipo de cabelo  0-3
             *  6 h -> cor de cabelo  0-2
             *  
             *  clothes: xc.png
             *  player/head,body,arms,legs: rxk___.png
             *  player/eye: rx_y__.png
             *  player/hair: rx__sh.png
             */
        private string CalculateRandomNPC()
        {
            Random rnd = new Random();
            string idnpc = "";

            idnpc += rnd.Next(0, 3).ToString();
            idnpc += rnd.Next(0, 3).ToString();
            idnpc += rnd.Next(0, 2).ToString();
            idnpc += rnd.Next(0, 3).ToString();
            idnpc += rnd.Next(0, 3).ToString();
            idnpc += rnd.Next(0, 4).ToString();
            idnpc += rnd.Next(0, 3).ToString();

            return idnpc;
        }

        public void Load()
        {
            id = CalculateRandomNPC();
            _PlayerLoader = new PlayerLoader(box, id);
            _PlayerLoader.Load(parts, sides);
        }

        private void InteractWith(object sender, PointerRoutedEventArgs e)
        {
            if(e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var eprop = e.GetCurrentPoint(Game.instance).Properties;
                if(eprop.IsLeftButtonPressed)
                {
                    //if(trigger.Triggering())
                    //{
                        MyNPC.StartConversation();
                    //}
                }
            }
        }

        public override void Die(Ent someone)
        {
            
        }
    }
}
