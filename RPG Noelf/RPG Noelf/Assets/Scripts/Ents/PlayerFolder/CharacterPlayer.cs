using RPG_Noelf.Assets.Scripts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    class CharacterPlayer : Character
    {
        private MainCamera ActualCam = MainCamera.instance;
        private Thread updatePlayer;
        public Player Player;

        public CharacterPlayer(Canvas T, Player player) : base(T)
        {
            // Getting character image and Canvas for control
            characT = T;

            updatePlayer = new Thread(UpdatePlayer);
            updatePlayer.Start();

            Player = player;

            // Setting Key events
            Window.Current.CoreWindow.KeyDown += Charac_KeyDown;
            Window.Current.CoreWindow.KeyUp += Charac_KeyUp;
        }

        public async void UpdatePlayer()
        {
            if (ActualCam == null) ActualCam = MainCamera.instance;
            while (alive)
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (ActualCam != null)
                    {
                        if (ActualCam.CameraMovingLeft)
                        {
                            Hspeed = 0;//MoveCharac(-MainCamera.CameraSpeed, EDirection.left);
                        }
                        else if (ActualCam.CameraMovingRight)
                        {
                            Hspeed = 0;//MoveCharac(MainCamera.CameraSpeed, EDirection.left);
                        }
                        else Hspeed = MaxHSpeed;

                    }
                    else
                    {
                        ActualCam = MainCamera.instance;
                    }
                });
            }

        }

        private void Charac_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            if (!isFalling)
            {
                if (e.VirtualKey == Windows.System.VirtualKey.A || e.VirtualKey == Windows.System.VirtualKey.Left)
                {
                    moveLeft = true;
                }
                else if (e.VirtualKey == Windows.System.VirtualKey.D || e.VirtualKey == Windows.System.VirtualKey.Right)
                {
                    moveRight = true;
                }

                if (e.VirtualKey == Windows.System.VirtualKey.W || e.VirtualKey == Windows.System.VirtualKey.Up)
                {
                    Jump();
                }
            }
            else
            {
                if (e.VirtualKey == Windows.System.VirtualKey.A || e.VirtualKey == Windows.System.VirtualKey.Left)
                {
                    prepLeft = true;
                }
                else if (e.VirtualKey == Windows.System.VirtualKey.D || e.VirtualKey == Windows.System.VirtualKey.Right)
                {
                    prepRight = true;
                }
            }
        }

        private void Charac_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {

            if (e.VirtualKey == Windows.System.VirtualKey.A || e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                moveLeft = false;
                IsWalking = false;
                if (isFalling) prepLeft = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.D || e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                moveRight = false;
                IsWalking = false;
                if (isFalling) prepRight = false;
            }

        }
    }
}
