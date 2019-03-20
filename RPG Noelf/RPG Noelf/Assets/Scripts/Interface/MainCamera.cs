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
        Canvas MobChunck;
        Canvas Tela;
        Thread UpdateThread;

        double xCamera;
        double yCamera;
        double CameraSpeed;

        public MainCamera(CharacterPlayer playerToFollow, Canvas Camera, Canvas Chunck)
        {
            this.PlayerToFollow = playerToFollow;
            this.Camera = Camera;
            this.Chunck = Chunck;
            this.Tela = MainPage.Telona;
            CameraSpeed = 0.2;
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
                        if (PlayerToFollow.xCharacVal >= xCamera + Camera.Width)
                        {
                            if ((Chunck.Width - Tela.Width) >= 0/*(double)Chunck.GetValue(Canvas.LeftProperty) * -1*/)
                            {
                                Chunck.SetValue(Canvas.LeftProperty,
                                                (double)Chunck.GetValue(Canvas.LeftProperty) - CameraSpeed);
                                Character.CameraXOffSet = (double)Chunck.GetValue(Canvas.LeftProperty);
                                Character.CameraMovingLeft = true;
                            }
                        }
                        else if (PlayerToFollow.xCharacVal <= xCamera)
                        {
                            if ((double)Chunck.GetValue(Canvas.LeftProperty) <= 0)
                            {
                                Chunck.SetValue(Canvas.LeftProperty,
                                                (double)Chunck.GetValue(Canvas.LeftProperty) + CameraSpeed);
                                Character.CameraXOffSet = (double)Chunck.GetValue(Canvas.LeftProperty);
                                Character.CameraMovingRight = true;
                            }
                        }
                    } else
                    {
                        Character.CameraMovingLeft = false;
                        Character.CameraMovingRight = false;
                    }

                    if (PlayerInsideHeightCamera())
                    {

                    }
                });
            }
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
