using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
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

    public class CharacterNPC : Character
    {
        public NPC MyNPC;
        public Trigger trigger;
        //private Thread Updates;

        public CharacterNPC(Canvas T, NPC _NPC) : base(T)
        {
            
            MyNPC = _NPC;
            trigger = new Trigger(this);
            T.PointerPressed += InteractWith;
            //Updates = new Thread(updateNPC);
            //Updates.Start();
        }

        private void InteractWith(object sender, PointerRoutedEventArgs e)
        {
            if(e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var eprop = e.GetCurrentPoint(Game.instance).Properties;
                if(eprop.IsLeftButtonPressed)
                {
                    if(trigger.Triggering())
                    {
                        MyNPC.StartConversation();
                    }
                }
            }
        }

    }
}
