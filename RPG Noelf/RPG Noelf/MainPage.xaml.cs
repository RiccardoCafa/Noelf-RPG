using RPG_Noelf.Assets;
using RPG_Noelf.Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml.Media;
using RPG_Noelf.Assets.Scripts.Interface;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPG_Noelf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Thread Start;
        Character player;
        InterfaceManager interfaceManager = new InterfaceManager();

        public MainPage()
        {
            this.InitializeComponent();
            
            Start = new Thread(start);
            Start.Start();
        }

        public async void start()
        {
            // Settando Janelas de Interface
            interfaceManager.Inventario = InventarioWindow;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                // Settando o player
                player = new Character(Player, PlayerCanvas);
                player.UpdateBlocks(Chunck01);
                player.ResetPosition(320, 40);
                //player.textBlock = Texticulu;
                player.rotation = Rotation;
            });
        }
    }
}
