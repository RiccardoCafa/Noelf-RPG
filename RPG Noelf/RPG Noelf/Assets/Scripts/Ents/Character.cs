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
using RPG_Noelf.Assets.Scripts.Interface;

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
        
        public double xCharacVal = 0, yCharacVal = 0;
        protected double diferenca = 0;

        protected const double Gravity = 1.1;
        protected double GravityMultiplier = 1;

        public double Hspeed { get; set; }
        public double Vspeed { get; set; }
        public double CameraVerticalSpeed { get; set; }

        public double MaxHSpeed { get; set; }
        public double MaxVSpeed { get; set; }

        public bool IsWalking { get; set; } = false;
        public bool moveRight { get; set; }
        public bool moveLeft { get; set; }
        public bool moveDown { get; set; }
        public bool jumping { get; set; }
        public bool isFalling { get; set; } = false;
        public bool freeRight { get; set; } = true;
        public bool freeLeft { get; set; } = true;
        public bool freeDown { get; set; } = true;

        protected bool alive = true;
        protected bool loaded = false;
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
            MaxHSpeed = 0.4;
            MaxVSpeed = 80;
            Hspeed = MaxHSpeed;
            Vspeed = MaxVSpeed;

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
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (DispatchedHandler)(() =>
                {
                    CheckGround();
                    // Calcula a gravidade
                    if (isFalling)
                    {
                        TimeSpan secs = time - DateTime.Now;
                        MoveCharac((GravityMultiplier * Character.Gravity * Math.Pow(secs.TotalSeconds, 2)), Canvas.TopProperty);
                        blocoLeftx = blocoRightx = null;
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
                        MoveCharac(-Hspeed, Canvas.LeftProperty);
                        IsWalking = true;
                    } else if (moveLeft && !freeLeft)
                    {
                        IsWalking = false;
                    }
                    if (freeRight && moveRight)
                    {
                        MoveCharac(Hspeed, Canvas.LeftProperty);
                        IsWalking = true;
                        //rotation.Angle += 0.5;
                    } else if(moveRight && !freeRight)
                    {
                        IsWalking = false;
                    }
                    /*if(freeDown && moveDown)
                    {
                        isFalling = false;
                        GravityMultiplier = 0;
                        MoveCharac(-MainCamera.CameraJump, EDirection.top);
                    }*/
                    
                    if (jumping && !isFalling)
                    {
                        MoveCharac(-Vspeed, Canvas.TopProperty);
                        jumping = false;
                    }

                }));
            }
        }

        public void MoveCharac(double hspeed, double vspeed)
        {
            characT.SetValue(Canvas.LeftProperty,
                              (double)characT.GetValue(Canvas.LeftProperty) + hspeed);
            characT.SetValue(Canvas.TopProperty,
                              (double)characT.GetValue(Canvas.TopProperty) + vspeed);
        }

        public void MoveCharac(double speed, DependencyProperty dir)
        {
            characT.SetValue(dir,
                            (double)characT.GetValue(dir) + speed);
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
            jumping = true;
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

        private void IntersectWith(Canvas left, Canvas right)
        {
            if(right != null && xCharacVal + characT.Width >= GetCanvasLeft(right) && xCharacVal + characT.Width <= GetCanvasLeft(right) + right.Width)
            {
                Jump();
            }
            if(left != null && xCharacVal <= GetCanvasLeft(left) + left.Width && xCharacVal >= GetCanvasLeft(left))
            {
                Jump();
            }
        }

        public void CheckGround()
        {
            double xPlayer, yPlayer;
            Canvas bottomBlock = null;
            blocoLeftx = null;
            blocoRightx = null;
            xPlayer = (double)characT.GetValue(Canvas.LeftProperty);
            yPlayer = (double)characT.GetValue(Canvas.TopProperty);
            double XPlayerW = xPlayer + characT.Width;
            double YPlayerH = yPlayer + characT.Height;
            xCharacVal = xPlayer;
            yCharacVal = yPlayer;
            foreach (Canvas bloco in collisionBlocks)
            {
                double actualBlockX;
                double actualBlockY;
                if (this is CharacterPlayer)
                {
                    actualBlockX = GetCanvasLeft(bloco) + MainCamera.instance.CameraXOffSet;
                    actualBlockY = GetCanvasTop(bloco) + MainCamera.instance.CameraYOffSet;
                } else
                {
                    actualBlockX = GetCanvasLeft(bloco);
                    actualBlockY = GetCanvasTop(bloco);
                }

                // Get the nearest block on the bottom
                if (XPlayerW >= actualBlockX && xPlayer < actualBlockX + bloco.Width
                    && actualBlockY - YPlayerH <= 0 && actualBlockY - YPlayerH > -2)
                {
                    bottomBlock = bloco;
                }

                // Get the distant
                double dif = actualBlockX - (XPlayerW);
                double dif02 = xPlayer - (actualBlockX + bloco.Width);
                //if (dif > 0 && dif < 10) diferenca = dif;
                // Pegar os blocos a direita e esquerda mais proximos do player
                if (bloco != LastBlock)
                {
                    if (xPlayer > actualBlockX + bloco.Width && dif02 <= 10 && dif02 > 0)
                    {
                        blocoLeftx = bloco;
                    }

                    if (XPlayerW <= actualBlockX && dif <= 10 && dif > 0)
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
                    freeLeft = (YPlayerH >= GetCanvasTop(blocoLeftx) &&
                                    yPlayer <= GetCanvasTop(blocoLeftx) + blocoLeftx.Height) ? false : true;
                }
                else
                {
                    freeLeft = true;
                }

                if (blocoRightx != null)
                {
                    //yvalue = actualBlockY - yPlayer;
                    freeRight = (YPlayerH >= GetCanvasTop(blocoRightx) &&
                                    yPlayer <= GetCanvasTop(blocoRightx) + blocoRightx.Height) ? false : true;
                }
                else
                {
                    freeRight = true;
                }

                if(!(this is CharacterPlayer)) IntersectWith(blocoLeftx, blocoRightx);
            }
        }

        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
