using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace RPG_Noelf.Assets.Scripts.Enviroment
{
    public class ChestEventArgs : EventArgs
    {
        public Bau chest { get; set; }
    }

    class ChestBody : Canvas
    {
        public delegate void ChestHandler(object sender, ChestEventArgs chestEventArgs);
        public event ChestHandler ChestOpen;
        Bau Chest;

        public ChestBody(double x, double y, Bau Chest)
        {
            this.Chest = Chest;
            SetTop(this, y);
            SetLeft(this, x);
        }

        public void RecreateChest()
        {
            Chest.CrateCreation();
        }

        public void OnChestPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var prop = e.GetCurrentPoint(this).Properties;
            if(e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                if(prop.IsLeftButtonPressed)
                {
                    OnChestOpened();
                }
            }
        }

        public void OnChestOpened()
        {
            ChestOpen?.Invoke(this, new ChestEventArgs() { chest = Chest });
        }
    }
}
