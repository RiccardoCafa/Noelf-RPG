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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPG_Noelf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Character player;

        public MainPage()
        {
            this.InitializeComponent();
            
        }
        
        private void MakeHappen_Click(object sender, RoutedEventArgs e)
        {
            GeradorFoto.MergeImages(Personagem, 197, 202, RenderedImage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player.Jump();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (player == null)
            {
                player = new Character(Player, PlayerCanvas);
                player.setFloorPos(BlockPos);
                player.setPlayerPosText(PlayerPos);
            }
            player.UpdateBlocks(Chunck01);
            player.ResetPosition(320, 40);
        }

        /* private void Animation_Begin(object sender, RoutedEventArgs e)
        {
            myStoryboard.Begin();
        }
        private void Animation_Pause(object sender, RoutedEventArgs e)
        {
            myStoryboard.Pause();
        }
        private void Animation_Resume(object sender, RoutedEventArgs e)
        {
            myStoryboard.Resume();
        }
        private void Animation_Stop(object sender, RoutedEventArgs e)
        {
            myStoryboard.Stop();
        }*/

        //private void MyRectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    TimeLine.Begin();
        //}
    }
}
