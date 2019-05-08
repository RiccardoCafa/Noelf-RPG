using RPG_Noelf.Assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_Noelf
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class PlayerLobby : Page
    {
        List<PlayerParams> playerParams = new List<PlayerParams>();
        List<Image> slots = new List<Image>();

        public Dictionary<string, Image> PlayerImages;
        public Dictionary<string, Image> ClothesImages;

        public List<Dictionary<string, string>> playerData = new List<Dictionary<string, string>>();

        int slotSelected = -1;

        public PlayerLobby()
        {
            this.InitializeComponent();
            Start();
        }

        private void Start()
        {
            slots.Add(Slot_1);
            slots.Add(Slot_2);
            slots.Add(Slot_3);
            foreach (Image img in slots)
            {
                img.PointerPressed += SlotPressed;
            }

            LoadCharacters("root");

            PlayerImages = new Dictionary<string, Image>()
            {
                {"armsd0", xPlayerArm_d0 },
                {"armsd1", xPlayerArm_d1 },
                {"armse0", xPlayerArm_e0 },
                {"armse1", xPlayerArm_e1 },
                {"body", xPlayerBody },
                {"head", xPlayerHead },
                {"eye", xPlayerEye },
                {"hair", xPlayerHair },
                {"legsd0", xPlayerLeg_d0 },
                {"legsd1", xPlayerLeg_d1 },
                {"legse0", xPlayerLeg_e0 },
                {"legse1", xPlayerLeg_e1 }
            };
            ClothesImages = new Dictionary<string, Image>()
            {
                {"armsd0", xClothArm_d0 },
                {"armsd1", xClothArm_d1 },
                {"armse0", xClothArm_e0 },
                {"armse1", xClothArm_e1 },
                {"body", xClothBody },
                {"legsd0", xClothLeg_d0 },
                {"legsd1", xClothLeg_d1 },
                {"legse0", xClothLeg_e0 },
                {"legse1", xClothLeg_e1 }
            };

        }

        private void SlotPressed(object sender, PointerRoutedEventArgs e)
        {
            if(sender is Image)
            {
                Image me = (Image)sender;
                var slot = me.Name.Split("_");
                int.TryParse(slot[1], out int val);
                SelectSlot(val);
            }
        }

        private void LoadCharacters(string username)
        {
            if(username == "root")
            {
                // Load Root Characters
                for(int i = 0; i < 3; i++)
                {
                    string path = Path.Combine(Path.GetTempPath() + @"/Noelf/slot_" + i);
                    if (File.Exists(path))
                    {
                        Debug.WriteLine("Slot " + i + " found");
                        playerData.Add(new Dictionary<string, string>());
                        using(StreamReader rw = File.OpenText(path))
                        {
                            string r = "";
                            while((r = rw.ReadLine()) != null)
                            {
                                var b = r.Split(" ");
                                playerData[i].Add(b[0], b[1]);
                            }
                        }
                    }
                }
            } else
            {
                // Get from text

            }

            for(int i = 0; i < 3; i++)
            {
                if(playerData.Count > i)
                {
                    LoadPlayerImage(playerData[i].Values.ElementAt(0), i);
                    playerParams.Add(new PlayerParams());
                }
            }
        }

        private void SelectSlot(int number)
        {
            slotSelected = number;
        }

        private void QuitBtn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.Close();
        }

        private void PlayBtn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (slotSelected != -1)
            {
                if(playerParams.Count > slotSelected && playerParams[slotSelected] != null)
                {
                    // Tem Dados Salvos nesse slot
                    
                } else
                {
                    // Criar outro personagem
                    NewPlayer newP = new NewPlayer(slotSelected);
                    LoadCharacterCreationPage(newP);
                }
            }
            else return;
        }

        private async void LoadCharacterCreationPage(object param)
        {
            var viewId = 0;
            var newView = CoreApplication.CreateNewView();
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(CharacterCreation), param);
                Window.Current.Content = frame;

                viewId = ApplicationView.GetForCurrentView().Id;

                Window.Current.Activate();
            });
            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }

        private async void LoadGamePage(object param)
        {
            var viewId = 0;
            var newView = CoreApplication.CreateNewView();
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(Game), param);
                Window.Current.Content = frame;

                viewId = ApplicationView.GetForCurrentView().Id;

                Window.Current.Activate();
            });
            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }

        private async void LoadPlayerImage(string tid, int slot)
        {
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
            string playerPath = @"/Assets/Images/player/";

            // Fazer o corpo
            string bodyPath = playerPath + @"player/";
            xPlayerLeg_d0.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "legs/d/0/" + tid[0] + tid[2] + tid[3] + "___.png"));
            xPlayerLeg_d1.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "legs/d/1/" + tid[0] + tid[2] + tid[3] + "___.png"));
            
            xPlayerLeg_e0.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "legs/e/0/" + tid[0] + tid[2] + tid[3] + "___.png"));
            xPlayerLeg_e1.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "legs/e/1/" + tid[0] + tid[2] + tid[3] + "___.png"));

            xPlayerArm_e0.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "arms/e/0/" + tid[0] + tid[2] + tid[3] + "___.png"));
            xPlayerArm_e1.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "arms/e/1/" + tid[0] + tid[2] + tid[3] + "___.png"));

            xPlayerArm_d0.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "arms/d/0/" + tid[0] + tid[2] + tid[3] + "___.png"));
            xPlayerArm_d1.Source = new BitmapImage(new Uri("ms-appx://" + bodyPath + "arms/d/1/" + tid[0] + tid[2] + tid[3] + "___.png"));

            RenderTargetBitmap render = null;
            switch (slot)
            {
                case 0:
                    await GeradorFoto.MergeImages(PlayerCanvas, render, (int) SlotImage_1.Width, (int) SlotImage_1.Height);
                    SlotImage_1.Source = render;
                    EmptySlot1.Visibility = Visibility.Collapsed;
                    break;

                case 1:
                    await GeradorFoto.MergeImages(PlayerCanvas, render, (int)SlotImage_2.Width, (int)SlotImage_2.Height);
                    SlotImage_2.Source = render;
                    EmptySlot2.Visibility = Visibility.Collapsed;
                    break;

                case 2:
                    await GeradorFoto.MergeImages(PlayerCanvas, render, (int)SlotImage_3.Width, (int)SlotImage_3.Height);
                    SlotImage_3.Source = render;
                    EmptySlot3.Visibility = Visibility.Collapsed;
                    break;
            }
                
            
            //player/arms/e/1/000___.png"
        }
    }

    public class PlayerParams
    {

    }

    public class NewPlayer
    {
        int slot;

        public NewPlayer(int slot)
        {
            this.slot = slot;
        }
    }

}
