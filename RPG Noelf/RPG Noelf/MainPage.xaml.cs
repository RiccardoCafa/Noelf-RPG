using RPG_Noelf.Assets;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
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
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using RPG_Noelf.Assets.Scripts.InventoryScripts;

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
        Bag bag = new Bag();

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


            Item banana = new Item(5, 1, "Banana", true, Category.Legendary, "Comunista", "/Assets/Images/Item1.jpg");
            Item jorro = new Item(500000, 1, "Jorro", true, Category.Magical, "ComunistaJorrando", "/Assets/Images/Item2.jpg");
            Item espadona = new Item(2500000, 1, "Espadona", false, Category.Normal, "ComunistaComunismo", "/Assets/Images/Item1.jpg");
            Consumable potion = new Consumable(50, 1, "HP pot", true, Category.Normal, "PotHP", "/Assets/Images/Item2.jpg");

            bag.AddToBag(banana);
            bag.AddToBag(jorro);
            bag.AddToBag(banana);
            bag.AddToBag(jorro);
            bag.AddToBag(banana);
            bag.AddToBag(jorro);
            bag.AddToBag(banana);
            bag.AddToBag(jorro);

            bag.RemoveFromBag(jorro);
            bag.RemoveFromBag(jorro);
            bag.RemoveFromBag(jorro);
            bag.RemoveFromBag(jorro);

            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);

            bag.AddToBag(potion);
            bag.AddToBag(potion);
            bag.AddToBag(potion);

            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);

            bag.RemoveFromBag(espadona);
            bag.RemoveFromBag(espadona);

            bag.RemoveFromBag(potion);

            bag.RemoveFromBag(banana);

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                UpdateBag();
            });
            
            
        }

        public void UpdateBag()
        {
            for (int i = 0; i < bag.slots.Count; i++)
            {
                int column = i, row = i;
                row = i / 6;
                while (column > 5) column -= 6;

                var slotTemp = from element in InventarioGrid.Children
                             where (int)element.GetValue(Grid.ColumnProperty) == column && (int)element.GetValue(Grid.RowProperty) == row
                             select element;
                if(slotTemp != null)
                {
                    Image slot = (Image)slotTemp.ElementAt(0);
                    slot.Source = new BitmapImage(new Uri(this.BaseUri, bag.slots[i].pathImage));
                }
                
            }
        }
    }
}
