using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace RPG_Noelf.Assets.Scripts.Enviroment
{
    public class LootBody : Canvas
    {

        Slot MySlot;

        public LootBody(Slot slot)
        {
            MySlot = slot;
            PointerPressed += OnLootPointerPressed;
        }

        public void OnLootPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (Character.GetDistance(GameManager.player.box.Xi, GameManager.player.box.Yi, (double)GetLeft(this), (double)GetTop(this)) < 65)
            {
                if(e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
                {
                    var prop = e.GetCurrentPoint(this).Properties;
                    if(prop.IsLeftButtonPressed)
                    {
                        if(GameManager.player._Inventory.AddToBag(MySlot))
                        {
                            if(MySlot.ItemAmount <= 0)
                            {
                                Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
        }
    }
}
