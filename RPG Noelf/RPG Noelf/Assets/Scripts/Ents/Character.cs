using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Xaml.Shapes;
using System.Diagnostics;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    /// <summary>
    /// The Character class that will manage gravity, movement, jump, animations...
    /// </summary>
    abstract class Character
    {
        public Canvas characT;
        protected Canvas LastBlock;
        protected DateTime time;

        protected Canvas blocoLeftx, blocoRightx, blocoBottomx;

        public static double CameraXOffSet, CameraYOffSet;
        public double xCharacVal = 0, yCharacVal = 0;
        protected double diferenca = 0;

        protected const double GravityMultiplier = 0.8;

        public double Hspeed { get; set; }
        public double Vspeed { get; set; }

        public static bool CameraMovingLeft = false;
        public static bool CameraMovingRight = false;
        public static bool CameraMovingUp = false;
        public static bool CameraMovingDown = false;
        public bool IsWalking { get; set; } = false;

        protected bool alive = true;
        protected bool loaded = false;
        protected bool isFalling = false;
        protected bool freeRight = true, freeLeft = true;
        protected bool moveRight, moveLeft, jumping;
        protected bool prepRight, prepLeft;

        protected List<Canvas> collisionBlocks = new List<Canvas>();

        private Thread update;

        public enum EDirection
        {
            top,
            left
        }

        public Character(Canvas T)
        {
            // Getting character image and Canvas for control
            characT = T;

            // Setting horizontal and vertical speed
            Hspeed = 0.2;
            Vspeed = 80;

            // Getting first instance of x and y
            xCharacVal = GetCanvasLeft(characT);
            yCharacVal = GetCanvasTop(characT);

            // Initialize Class
            Start();
        }

        private void Start()
        {
            // Get the actual time
            time = DateTime.Now;
            // Creates a loop while alive Thread for update
            update = new Thread(Update);
            update.Start();
        }

        private async void Update()
        {
            while (alive)
            {
                //if (!loaded) continue;
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    CheckGround();
                    // Calcula a gravidade
                    if (isFalling)
                    {
                        TimeSpan secs = time - DateTime.Now;
                        MoveCharac(GravityMultiplier * Math.Pow(secs.TotalSeconds, 2), EDirection.top);
                        blocoLeftx = blocoRightx = null;
                        //rotation.Angle += 2;
                    }
                    else
                    {
                        time = DateTime.Now;
                        if (prepLeft)
                        {
                            moveLeft = true;
                            prepLeft = false;
                        }
                        if (prepRight)
                        {
                            prepRight = false;
                            moveRight = true;
                        }
                    }

                    if (freeLeft && moveLeft)
                    {
                        MoveCharac(-Hspeed, EDirection.left);
                        IsWalking = true;
                        //rotation.Angle -= 0.5;
                    }
                    if (freeRight && moveRight)
                    {
                        MoveCharac(Hspeed, EDirection.left);
                        IsWalking = true;
                        //rotation.Angle += 0.5;
                    }
                    if(!moveRight && !moveLeft && !freeLeft && !freeRight)
                    {
                        IsWalking = false;
                    }
                    if (jumping && !isFalling)
                    {
                        MoveCharac(-Vspeed, EDirection.top);
                        jumping = false;
                    }
                    if(CameraMovingLeft)
                    {
                        MoveCharac(-Hspeed, EDirection.left);
                    }
                    else if(CameraMovingRight)
                    {
                        MoveCharac(Hspeed, EDirection.left);
                    }
                });
            }
        }

        public void MoveCharac(double hspeed, double vspeed)
        {
            characT.SetValue(Canvas.LeftProperty,
                              (double)characT.GetValue(Canvas.LeftProperty) + hspeed);
            characT.SetValue(Canvas.TopProperty,
                              (double)characT.GetValue(Canvas.TopProperty) + vspeed);
        }

        public void MoveCharac(double speed, EDirection dir)
        {
            switch (dir)
            {
                case EDirection.left:
                    characT.SetValue(Canvas.LeftProperty,
                              (double)characT.GetValue(Canvas.LeftProperty) + speed);
                    break;
                case EDirection.top:
                    characT.SetValue(Canvas.TopProperty,
                              (double)characT.GetValue(Canvas.TopProperty) + speed);
                    break;
            }
        }

        public void ResetPosition(int X, int Y)
        {
            characT.SetValue(Canvas.LeftProperty, X);
            characT.SetValue(Canvas.TopProperty, Y);
        }

        public double GetCanvasTop(Canvas c)
        {
            return (double)c?.GetValue(Canvas.TopProperty);
        }

        public double GetCanvasLeft(Canvas c)
        {
            return (double)c?.GetValue(Canvas.LeftProperty);
        }

        public void Jump()
        {
            time = DateTime.Now;
            isFalling = true;
        }

        public void UpdateBlocks(Canvas blockCanvas)
        {
            collisionBlocks.Clear();
            
            foreach (UIElement block in blockCanvas.Children)
            {
                if (block is Canvas)
                {
                    collisionBlocks.Add((Canvas)block);
                }
            }
            loaded = true;
        }

        public void CheckGround()
        {
            double xPlayer, yPlayer;
            Canvas bottomBlock = null;
            blocoLeftx = null;
            blocoRightx = null;
            xPlayer = (double)characT.GetValue(Canvas.LeftProperty);
            yPlayer = (double)characT.GetValue(Canvas.TopProperty);
            xCharacVal = xPlayer;
            yCharacVal = yPlayer;
            foreach (Canvas bloco in collisionBlocks)
            {

                double actualBlockX = GetCanvasLeft(bloco) + CameraXOffSet;
                double actualBlockY = GetCanvasTop(bloco) + CameraYOffSet;

                // Get the nearest block on the bottom
                if (xPlayer + characT.Width >= actualBlockX && xPlayer < actualBlockX + bloco.Width
                    && actualBlockY - (yPlayer + characT.Height) <= 0 && actualBlockY - (yPlayer + characT.Height) > -2)
                {
                    bottomBlock = bloco;
                }

                // Get the distant
                double dif = actualBlockX - (xPlayer + characT.Width);
                double dif02 = xPlayer - (actualBlockX + bloco.Width);
                if (dif > 0 && dif < 10) diferenca = dif;
                // Pegar os blocos a direita e esquerda mais proximos do player
                if (bloco != LastBlock)
                {
                    if (xPlayer > actualBlockX + bloco.Width && dif02 <= 2 && dif02 > 0)
                    {
                        blocoLeftx = bloco;
                    }

                    if (xPlayer + characT.Width <= actualBlockX && dif <= 2 && dif > 0)
                    {
                        blocoRightx = bloco;
                    }
                }

                if (bottomBlock != null)
                {
                    double ydist = GetCanvasTop(bottomBlock) - yPlayer;
                    //lastY = GetCanvasTop(bottomBlock);
                    LastBlock = bottomBlock;
                    blocoBottomx = bottomBlock;
                    isFalling = ydist <= characT.Height ? isFalling = false : isFalling = true;
                }
                else
                {
                    isFalling = true;
                }

                if (blocoLeftx != null)
                {
                    //yvalue = actualBlockY - yPlayer;
                    freeLeft = (yPlayer + characT.Height >= GetCanvasTop(blocoLeftx) &&
                                    yPlayer <= GetCanvasTop(blocoLeftx) + blocoLeftx.Height) ? false : true;
                }
                else
                {
                    freeLeft = true;
                }

                if (blocoRightx != null)
                {
                    //yvalue = actualBlockY - yPlayer;
                    freeRight = (yPlayer + characT.Height >= GetCanvasTop(blocoRightx) &&
                                    yPlayer <= GetCanvasTop(blocoRightx) + blocoRightx.Height) ? false : true;
                }
                else
                {
                    freeRight = true;
                }
            }
        }

        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
