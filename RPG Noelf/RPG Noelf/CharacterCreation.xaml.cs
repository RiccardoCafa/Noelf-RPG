using RPG_Noelf.Assets.Scripts;
using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_Noelf
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CharacterCreation : Page
    {

        public Dictionary<string, Image> PlayerImages;
        public Dictionary<string, Image> ClothesImages;
        Player CustomPlayer;
        int selectedSlot;

        const double xPlayerSpawn = 20;
        const double yPlayerSpawn = 20;

        public CharacterCreation()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            CustomPlayer = new Player("0000000");
            CustomPlayer.Spawn(xPlayerSpawn, yPlayerSpawn);
            PlayerCanvas.Children.Add(CustomPlayer.box);
            (CustomPlayer.box as DynamicSolid).alive = false; 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter is NewPlayer)
            {
                selectedSlot = (e.Parameter as NewPlayer).slot;
            }
        }

        private string ChangeCustom(char current, int range, bool isNext)//metodo auxiliar de ClickCustom()
        {
            int.TryParse(current.ToString(), out int x);
            if (isNext)
            {
                if (x == range - 1) x = 0;
                else x++;
            }
            else
            {
                if (x == 0) x = range - 1;
                else x--;
            }
            return x.ToString();
        }
        private void ClickCustom(object sender, RoutedEventArgs e)//gerencia a customizaçao do player (temporario)
        {
            string id = CustomPlayer.Id;
            if (sender == xEsqRace ||
                sender == xDirRace) id = ChangeCustom(id[0], 3, sender == xDirRace) + id.Substring(1, 6);
            else if (sender == xEsqClass ||
                     sender == xDirClass) id = id.Substring(0, 1) + ChangeCustom(id[1], 3, sender == xDirClass) + id.Substring(2, 5);
            else if (sender == xEsqSex ||
                     sender == xDirSex) id = id.Substring(0, 2) + ChangeCustom(id[2], 2, sender == xDirSex) + id.Substring(3, 4);
            else if (sender == xEsqSkinTone ||
                     sender == xDirSkinTone) id = id.Substring(0, 3) + ChangeCustom(id[3], 3, sender == xDirSkinTone) + id.Substring(4, 3);
            else if (sender == xEsqEyeColor ||
                     sender == xDirEyeColor) id = id.Substring(0, 4) + ChangeCustom(id[4], 3, sender == xDirEyeColor) + id.Substring(5, 2);
            else if (sender == xEsqHairStyle ||
                     sender == xDirHairStyle) id = id.Substring(0, 5) + ChangeCustom(id[5], 4, sender == xDirHairStyle) + id.Substring(6, 1);
            else if (sender == xEsqHairColor ||
                     sender == xDirHairColor) id = id.Substring(0, 6) + ChangeCustom(id[6], 3, sender == xDirHairColor);
            AppearPlayer(id);
            CodigoChar.Text = CustomPlayer.Id;
        }

        private void AppearPlayer(string id)
        {
            CustomPlayer.Id = id;
            // TODO Concertar roupas player
            //CustomPlayer.SetPlayer(CustomPlayer.playerImages);
            //CustomPlayer.SetClothes(CustomPlayer.clothesImages);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewId = 0;
            PlayerParams cParams = new PlayerParams(CustomPlayer.Id);
            SavePlayerData();
            var newView = CoreApplication.CreateNewView();
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(Game), cParams);
                Window.Current.Content = frame;

                viewId = ApplicationView.GetForCurrentView().Id;

                //ApplicationView.GetForCurrentView().Consolidated += App.;

                Window.Current.Activate();
            });
            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }
    }
}
