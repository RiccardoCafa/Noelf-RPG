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
    public class MainCamera
    {
        public static MainCamera instance;
        CharacterPlayer PlayerToFollow;
        Canvas Camera;
        Canvas Chunck;
        Canvas Tela;
        Thread UpdateThread;

        public bool CameraMovingLeft { get; set; } = false;
        public bool CameraMovingRight { get; set; } = false;
        public bool CameraMovingUp { get; set; } = false;
        public bool CameraMovingDown { get; set; } = false;

        public double CameraXOffSet = 0, CameraYOffSet = 0;

        double xCamera;
        double yCamera;
        public static double CameraSpeed;
        public static double CameraJump;

        public MainCamera(CharacterPlayer playerToFollow, Canvas Camera, Canvas Chunck)
        {
            instance = this;
            this.PlayerToFollow = playerToFollow;
            this.Camera = Camera;
            this.Chunck = Chunck;
            this.Tela = MainPage.Telona;
            CameraSpeed = playerToFollow.MaxHSpeed;
            CameraJump = 0.2;
            UpdateThread = new Thread(Update);

            UpdateThread.Start();
        }

        public async void Update()
        {
            while(true)
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
                {
                    CameraYOffSet = (double)Chunck.GetValue(Canvas.TopProperty);
                    if (PlayerToFollow.IsWalking && !PlayerInsideWidthCamera())
                    {
                        if (PlayerToFollow.xCharacVal >= xCamera + Camera.Width && PlayerToFollow.moveRight)
                        {
                            if ((Chunck.Width - Tela.Width) >= (double)Chunck.GetValue(Canvas.LeftProperty) * -1)
                            {
                                Chunck.SetValue(Canvas.LeftProperty,
                                                (double)Chunck.GetValue(Canvas.LeftProperty) - CameraSpeed);
                                CameraXOffSet = (double)Chunck.GetValue(Canvas.LeftProperty);
                                CameraMovingLeft = true;
                            }
                            else StopLeft();
                        }
                        else if (PlayerToFollow.xCharacVal <= xCamera && PlayerToFollow.moveLeft)
                        {
                            if ((double)Chunck.GetValue(Canvas.LeftProperty) <= 0)
                            {
                                Chunck.SetValue(Canvas.LeftProperty,
                                                (double)Chunck.GetValue(Canvas.LeftProperty) + CameraSpeed);
                                CameraXOffSet = (double)Chunck.GetValue(Canvas.LeftProperty);
                                CameraMovingRight = true;
                            }
                            else StopLeft();
                        }
                    } else StopLeft();

                });
            }
        }

        private void StopLeft()
        {
            CameraMovingLeft = false;
            CameraMovingRight = false;
        }

        private void StopTop()
        {
            CameraMovingUp = false;
            CameraMovingDown = false;
            PlayerToFollow.moveDown = false;
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
