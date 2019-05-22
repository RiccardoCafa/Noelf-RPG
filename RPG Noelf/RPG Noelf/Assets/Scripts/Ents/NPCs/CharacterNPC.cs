using RPG_Noelf.Assets.Scripts.Ents.PlayerFolder;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
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
            Game.TheScene.Children.Add(box);
            Load();
        }

        public void Load()
        {
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

    }
}
