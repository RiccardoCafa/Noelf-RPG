using RPG_Noelf.Assets.Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_Noelf
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class PlayerLobby : Page
    {
        List<Image> slots = new List<Image>();

        public Dictionary<int, PlayerParams> playerParams = new Dictionary<int, PlayerParams>(3);
        public List<Dictionary<string, string>> playerData = new List<Dictionary<string, string>>(3);

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

        }

        private void SlotPressed(object sender, PointerRoutedEventArgs e)
        {
            if(sender is Image)
            {
                Image me = (Image)sender;
                var slot = me.Name.Split("_");
                int.TryParse(slot[1], out int val);
                SelectSlot(val - 1);
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
                        playerData.Add(new Dictionary<string, string>());
                        playerData[i].Add("slot", i.ToString());
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
                // Get from database

            }

            foreach(Dictionary<string, string> data in playerData)
            {
                int.TryParse(data["slot"], out int slot);
                LoadPlayerImage(playerData[slot].Values.ElementAt(1), slot);
                playerParams.Add(slot, new PlayerParams(id: playerData[slot].FirstOrDefault(x => x.Key == "id").Value));
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
                if(playerParams.Keys.Contains(slotSelected))
                {
                    // Tem Dados Salvos nesse slot
                    LoadGamePage(playerParams[slotSelected]);
                } else
                {
                    // Criar outro personagem
                    NewPlayer newP = new NewPlayer(slotSelected);
                    LoadCharacterCreationPage(newP);
                }
            }
            else return;
        }

        private void LoadPlayerImage(string tid, int slot)
        {
            Player player = new Player(tid);
            player.Spawn(35, 33);
            switch (slot)
            {
                case 0: SlotImage_1.Children.Add(player.box); EmptySlot1.Visibility = Visibility.Collapsed; break;
                case 1: SlotImage_2.Children.Add(player.box); EmptySlot2.Visibility = Visibility.Collapsed; break;
                case 2: SlotImage_3.Children.Add(player.box); EmptySlot3.Visibility = Visibility.Collapsed; break;
            }
            (player.box as DynamicSolid).alive = false;
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

    }

    public class PlayerParams
    {
        public string idPlayer { get; set; }

        public PlayerParams(string id)
        {
            idPlayer = id;
        }
    }

    public class NewPlayer
    {
        public int slot { get; set; }

        public NewPlayer(int slot)
        {
            this.slot = slot;
        }
    }

}
