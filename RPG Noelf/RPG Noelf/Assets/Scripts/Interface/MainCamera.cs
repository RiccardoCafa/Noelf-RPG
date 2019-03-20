using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    class MainCamera
    {
        CharacterPlayer PlayerToFollow;
        Canvas Camera;
        Canvas Chunck;
        Canvas Tela;
        Thread UpdateThread;

        double xCamera;
        double yCamera;
        public static double CameraSpeed;

        public MainCamera(CharacterPlayer playerToFollow, Canvas Camera, Canvas Chunck)
        {
            this.PlayerToFollow = playerToFollow;
            this.Camera = Camera;
            this.Chunck = Chunck;
            this.Tela = MainPage.Telona;
            CameraSpeed = playerToFollow.MaxHSpeed;
            UpdateThread = new Thread(Update);

            UpdateThread.Start();
        }

        public async void Update()
        {
            while(true)
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                             
                    if (PlayerToFollow.IsWalking && !PlayerInsideWidthCamera())
                    {
                        if (PlayerToFollow.xCharacVal >= xCamera + Camera.Width && PlayerToFollow.moveRight)
                        {
                            if ((Chunck.Width - Tela.Width) >= (double)Chunck.GetValue(Canvas.LeftProperty) * -1)
                            {
                                Chunck.SetValue(Canvas.LeftProperty,
                                                (double)Chunck.GetValue(Canvas.LeftProperty) - CameraSpeed);
                                PlayerToFollow.CameraXOffSet = (double)Chunck.GetValue(Canvas.LeftProperty);
                                PlayerToFollow.CameraMovingLeft = true;
                            }
                            else Stop();
                        }
                        else if (PlayerToFollow.xCharacVal <= xCamera && PlayerToFollow.moveLeft)
                        {
                            if ((double)Chunck.GetValue(Canvas.LeftProperty) <= 0)
                            {
                                Chunck.SetValue(Canvas.LeftProperty,
                                                (double)Chunck.GetValue(Canvas.LeftProperty) + CameraSpeed);
                                PlayerToFollow.CameraXOffSet = (double)Chunck.GetValue(Canvas.LeftProperty);
                                PlayerToFollow.CameraMovingRight = true;
                            }
                            else Stop();
                        }
                    } else Stop();

                    if (!PlayerInsideHeightCamera())
                    {
                        /*if (PlayerToFollow.yCharacVal + PlayerToFollow.characT.Height >= yCamera)
                        {
                            if ((Chunck.Height - Tela.Height) >= 0TODO (double)Chunck.GetValue(Canvas.LeftProperty) * -1)
                            {
                                Chunck.SetValue(Canvas.TopProperty,
                                                (double)Chunck.GetValue(Canvas.TopProperty) - CameraSpeed);
                                Character.CameraYOffSet = (double)Chunck.GetValue(Canvas.TopProperty);
                                Character.CameraMovingUp = true;
                            }
                        }
                        else if (PlayerToFollow.yCharacVal <= yCamera + Camera.Height)
                        {
                            if ((double)Chunck.GetValue(Canvas.TopProperty) <= 0)
                            {
                                Chunck.SetValue(Canvas.TopProperty,
                                                (double)Chunck.GetValue(Canvas.TopProperty) + CameraSpeed);
                                Character.CameraYOffSet = (double)Chunck.GetValue(Canvas.TopProperty);
                                Character.CameraMovingDown = true;
                            }
                        }
                    } else
                    {
                        Character.CameraMovingUp = false;
                        Character.CameraMovingDown = false;
                    */
                    }
                });
            }
        }

        private void Stop()
        {
            PlayerToFollow.CameraMovingLeft = false;
            PlayerToFollow.CameraMovingRight = false;
        }

        public bool PlayerInsideHeightCamera()
        {
            yCamera = (double) Camera.GetValue(Canvas.TopProperty);
            if (PlayerToFollow.yCharacVal >= yCamera && 
                PlayerToFollow.yCharacVal + PlayerToFollow.characT.Height <= yCamera + Camera.Height)
            {
                return true;
            }
            return false;
        }

        public bool PlayerInsideWidthCamera()
        {
            xCamera = (double) Camera.GetValue(Canvas.LeftProperty);
            if (PlayerToFollow.xCharacVal >= xCamera &&
                PlayerToFollow.xCharacVal + PlayerToFollow.characT.Width <= xCamera + Camera.Width)
            {
                return true;
            }
            return false;
        }
    }
}
