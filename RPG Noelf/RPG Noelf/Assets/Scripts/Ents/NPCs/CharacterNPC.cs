using RPG_Noelf.Assets.Scripts.General;
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

    public class CharacterNPC : Solid
    {
        public NPC MyNPC;
        public Trigger trigger;

        public CharacterNPC(NPC _NPC, double x, double y, double width, double height) : base(x, y, width, height)
        {
            MyNPC = _NPC;
            trigger = new Trigger(this);
            PointerPressed += InteractWith;
        }

        public CharacterNPC(NPC _NPC, Canvas canvas) : 
                        base(GetLeft(canvas), GetTop(canvas), (double)canvas.GetValue(WidthProperty), (double) canvas.GetValue(HeightProperty))
        {

            MyNPC = _NPC;
            trigger = new Trigger(this);
            PointerPressed += InteractWith;
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
