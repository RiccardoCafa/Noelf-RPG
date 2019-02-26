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

namespace RPG_Noelf.Assets.Scripts.Player
{
    /// <summary>
    /// The Character class that will manage gravity, movement, jump, animations...
    /// </summary>
    class Character
    {
        Image charac;
        Canvas characT;
        Canvas LastBlock;
        DateTime time;
        public TextBlock textu { get; set; } 

        public const double GravityMultiplier = 0.8;

        public double hspeed { get; set; }
        public double vspeed { get; set; }

        private double lastY;
        private double distancia;

        private bool alive = true;
        private bool isFalling = false;
        private bool freeUp, freeDown, freeRight = true, freeLeft = true;
        private bool moveRight, moveLeft, jumping;
        private bool prepRight, prepLeft;

        private Thread update;

        private List<Canvas> collisionBlocks = new List<Canvas>();

        private enum EDirection {
            top,
            left
        }

        public Character(Image character, Canvas T)
        {
            // Getting character image and Canvas for control
            charac = character;
            characT = T;

            // Setting horizontal and vertical speed
            hspeed = 0.1;
            vspeed = 80;

            // Setting Key events
            Window.Current.CoreWindow.KeyDown += Charac_KeyDown;
            Window.Current.CoreWindow.KeyUp += Charac_KeyUp;

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
            while(alive)
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                    CheckGround();
                    // Calcula a gravidade
                    if (isFalling)
                    {
                        TimeSpan secs = time - DateTime.Now;
                        MoveCharac(GravityMultiplier * Math.Pow(secs.TotalSeconds, 2), EDirection.top);
                    }
                    else
                    {
                        time = DateTime.Now;
                        if(prepLeft)
                        {
                            moveLeft = true;
                            prepLeft = false;
                        }
                        if(prepRight)
                        {
                            prepRight = false;
                            moveRight = true;
                        }
                    }

                    
                    if (freeLeft && moveLeft)
                    {
                        MoveCharac(-hspeed, EDirection.left);
                    }
                    if (freeRight && moveRight)
                    {
                        MoveCharac(hspeed, EDirection.left);
                    }
                    if(jumping)
                    {
                        MoveCharac(-vspeed, EDirection.top);
                        jumping = false;
                    }
                }); 
            }
        }

        private void Charac_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            if(!isFalling)
            {
                if(e.VirtualKey == Windows.System.VirtualKey.A || e.VirtualKey == Windows.System.VirtualKey.Left)
                {
                    moveLeft = true;
                } else if(e.VirtualKey == Windows.System.VirtualKey.D || e.VirtualKey == Windows.System.VirtualKey.Right)
                {
                    moveRight = true;
                } else if(e.VirtualKey == Windows.System.VirtualKey.W || e.VirtualKey == Windows.System.VirtualKey.Up)
                {
                    //MoveCharac(-vspeed, EDirection.top);
                    jumping = true;
                    Jump();
                }
            } else
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
                if(isFalling) prepLeft = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.D || e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                moveRight = false;
                if (isFalling) prepRight = false;
            } else if(e.VirtualKey == Windows.System.VirtualKey.W || e.VirtualKey == Windows.System.VirtualKey.Up)
            {
                jumping = false;
            }
            
        }

        private void MoveCharac(double hspeed, double vspeed)
        {
            characT.SetValue(Canvas.LeftProperty,
                              (double)characT.GetValue(Canvas.LeftProperty) + hspeed);
            characT.SetValue(Canvas.TopProperty,
                              (double)characT.GetValue(Canvas.TopProperty) + vspeed);
        }

        private void MoveCharac(double speed, EDirection dir)
        {
            switch(dir)
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
            //collisionBlocks.Add(blockCanvas);
            foreach(UIElement block in blockCanvas.Children)
            {
                if(block is Canvas)
                {
                    collisionBlocks.Add((Canvas)block);
                }
            }
        }

        public void CheckGround()
        {
            double xPlayer, yPlayer;
            Canvas bottomBlock = null;
            xPlayer = (double)characT.GetValue(Canvas.LeftProperty);
            yPlayer = (double)characT.GetValue(Canvas.TopProperty);

            foreach (Canvas bloco in collisionBlocks)
            {

                double actualBlockX = GetCanvasLeft(bloco);
                double actualBlockY = GetCanvasTop(bloco);

                // Get the nearest block on the bottom
                if (xPlayer + charac.Width >= actualBlockX && xPlayer < actualBlockX + bloco.Width
                    && actualBlockY - yPlayer <= 50)
                {
                    bottomBlock = bloco;
                }

                // Get the distant
                double xvalue, yvalue;
                double dif = xPlayer - actualBlockX;
                // Ver a altura do player
                if(bloco != LastBlock || isFalling)
                {
                    // Checar primeiro depois de qual ponta o player se encontra
                    if (xPlayer >= actualBlockX + bloco.Width && dif > 0) // Se (player) encontra no lado direito
                    {
                        xvalue = xPlayer - (actualBlockX + bloco.Width);
                        yvalue = actualBlockY - yPlayer;

                        if(xvalue <= 2 && yPlayer + charac.Height >= actualBlockY &&
                            yPlayer <= actualBlockY + bloco.Width)
                        {
                            freeLeft = false;
                        } else
                        {
                            freeLeft = true;
                        }
                    }
                    else if (xPlayer + charac.Width <= actualBlockX && dif <= 0) // Se (player) encontra no lado esquerdo
                    {
                        xvalue = actualBlockX - (xPlayer + charac.Width);
                        yvalue = actualBlockY - yPlayer;

                        if (xvalue <= 2 && yPlayer + charac.Height >= actualBlockY &&
                            yPlayer <= actualBlockY + bloco.Width)
                        {
                            freeRight = false;
                        }
                        else
                        {
                            freeRight = true;
                        }
                    }
                }
            }

            if (bottomBlock != null)
            {
                double ydist = GetCanvasTop(bottomBlock) - yPlayer;
                //lastY = GetCanvasTop(bottomBlock);
                LastBlock = bottomBlock;
                isFalling = ydist <= 50 ? isFalling = false : isFalling = true;
            } else
            {
                isFalling = true;
            }
        }
    }
}
