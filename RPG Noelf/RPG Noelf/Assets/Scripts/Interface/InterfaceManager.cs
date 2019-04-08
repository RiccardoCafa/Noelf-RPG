using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    public class InterfaceManager
    {
        public Canvas Inventario { get; set; }

        public bool InventarioOpen { get; set; }
        public bool ShopOpen { get; set; } = false;
        public bool ConvHasToClose { get; set; }
        public bool Conversation { get; set; } = false;

        public InterfaceManager()
        {
            Window.Current.CoreWindow.KeyUp += ManageKey;
        }

        private void ManageKey(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.I)
            {
                if (InventarioOpen)
                {
                    Inventario.Visibility = Visibility.Visible;
                    InventarioOpen = false;
                }
                else
                {
                    Inventario.Visibility = Visibility.Collapsed;
                    InventarioOpen = true;
                }
            }
        }

    }
}
