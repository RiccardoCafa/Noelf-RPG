using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Ents.NPCs
{
    public enum NPCStates {
        Interacting,
        Stop,
        Attacking,
        Following,
        Trading
    }

    class CharacterNPC : Character
    {
        public NPC MyNPC;
        private Thread Update;

        public CharacterNPC(Canvas T, NPC _NPC) : base(T)
        {
            MyNPC = _NPC;
            Update = new Thread(update);
            Update.Start();
        }

        private async void update()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

            });


        }

    }
}
