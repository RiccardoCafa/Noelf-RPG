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
        Rectangle charac;
        Canvas characT;
        Canvas LastBlock;
        DateTime time;
        public RotateTransform rotation { get; set; }

        // Apagar depois
        public TextBlock textBlock { get; set; }
        public Canvas blocoLeftx, blocoRightx, blocoBottomx;

        public double diferenca= 0;
        public double xPlayerVal = 0, yPlayerVal = 0;

        public const double GravityMultiplier = 0.8;

        public double Hspeed { get; set; }
        public double Vspeed { get; set; }

        private bool alive = true;
        private bool isFalling = false;
        private bool freeRight = true, freeLeft = true;
        private bool moveRight, moveLeft, jumping;
        private bool prepRight, prepLeft;

        private Thread update;

        private List<Canvas> collisionBlocks = new List<Canvas>();

        private enum EDirection {
            top,
            left
        }

        public Character(Rectangle character, Canvas T)
        {
            // Getting character image and Canvas for control
            charac = character;
            characT = T;

            // Setting horizontal and vertical speed
            Hspeed = 0.1;
            Vspeed = 80;

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
                        //rotation.Angle += 2;
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
                        MoveCharac(-Hspeed, EDirection.left);
                        //rotation.Angle -= 0.5;
                    }
                    if (freeRight && moveRight)
                    {
                        MoveCharac(Hspeed, EDirection.left);
                        //rotation.Angle += 0.5;
                    }

                    if(jumping)
                    {
                        MoveCharac(-Vspeed, EDirection.top);
                        jumping = false;
                    }
                    /*
                    textBlock.Text = "Player [X1,X2,Y1,Y2]: [" + string.Format("{0:N2}", xPlayerVal) 
                    + "," + string.Format("{0:N2}", (xPlayerVal + charac.Width)) + "," +
                    string.Format("{0:N2}", yPlayerVal) + "," + string.Format("{0:N2}", (yPlayerVal + charac.Height)) + "]\n";

                    if (blocoLeftx != null)
                        textBlock.Text += "Bloco LeftX: [" + GetCanvasLeft(blocoLeftx) + "," +
                        (GetCanvasLeft(blocoLeftx) + blocoLeftx.Width) + "," + GetCanvasTop(blocoLeftx) + ","
                        + (GetCanvasTop(blocoLeftx) + blocoLeftx.Height) + "]\n";

                    if (blocoRightx != null)
                        textBlock.Text += "Bloco RightX: [" + GetCanvasLeft(blocoRightx) + "," +
                        (GetCanvasLeft(blocoRightx) + blocoRightx.Width) + "," + GetCanvasTop(blocoRightx) + ","
                        + (GetCanvasTop(blocoRightx) + blocoRightx.Height) + "]\n";

                    if(blocoBottomx != null)
                        textBlock.Text += "Bloco BottomX: [" + GetCanvasLeft(blocoBottomx) + "," +
                        (GetCanvasLeft(blocoBottomx) + blocoBottomx.Width) + "," + GetCanvasTop(blocoBottomx) + ","
                        + (GetCanvasTop(blocoBottomx) + blocoBottomx.Height) + "\n";

                    textBlock.Text += "\n Diferenca: " + diferenca + "\n";*/
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
            blocoLeftx = null;
            blocoRightx = null;
            xPlayer = (double)characT.GetValue(Canvas.LeftProperty);
            yPlayer = (double)characT.GetValue(Canvas.TopProperty);
            xPlayerVal = xPlayer;
            yPlayerVal = yPlayer;
            foreach (Canvas bloco in collisionBlocks)
            {

                double actualBlockX = GetCanvasLeft(bloco);
                double actualBlockY = GetCanvasTop(bloco);

                // Get the nearest block on the bottom
                if (xPlayer + charac.Width >= actualBlockX && xPlayer < actualBlockX + bloco.Width
                    && actualBlockY - (yPlayer + charac.Height) <= 0 && actualBlockY - (yPlayer + charac.Height) > -2)
                {
                    bottomBlock = bloco;
                }

                // Get the distant
                double dif = actualBlockX - (xPlayer + charac.Width);
                double dif02 = xPlayer - (actualBlockX + bloco.Width);
                if(dif > 0 && dif < 10) diferenca = dif;
                // Pegar os blocos a direita e esquerda mais proximos do player
                if (bloco != LastBlock)
                {
                    if (xPlayer > actualBlockX + bloco.Width && dif02 <= 2 && dif02 > 0)
                    {
                        blocoLeftx = bloco;
                    }

                    if (xPlayer + charac.Width <= actualBlockX && dif <= 2 && dif > 0)
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
                    isFalling = ydist <= charac.Height ? isFalling = false : isFalling = true;
                }
                else
                {
                    isFalling = true;
                }

                if(blocoLeftx != null)
                {
                    //yvalue = actualBlockY - yPlayer;
                    freeLeft = (yPlayer + charac.Height >= GetCanvasTop(blocoLeftx) &&
                                    yPlayer <= GetCanvasTop(blocoLeftx) + blocoLeftx.Height) ? false : true;
                } else
                {
                    freeLeft = true;
                }

                if (blocoRightx != null)
                {
                    //yvalue = actualBlockY - yPlayer;
                    freeRight = (yPlayer + charac.Height >= GetCanvasTop(blocoRightx) &&
                                    yPlayer <= GetCanvasTop(blocoRightx) + blocoRightx.Height) ? false : true;
                } else
                {
                    freeRight = true;
                }
            }
        }
    }
}
