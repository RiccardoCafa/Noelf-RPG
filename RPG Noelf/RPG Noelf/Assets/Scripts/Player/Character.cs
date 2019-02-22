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
        DateTime time;

        public const double GravityMultiplier = 0.9;

        public double hspeed { get; set; }
        public double vspeed { get; set; }

        private bool alive = true;
        private bool isFalling = false;
        private bool freeUp, freeDown, freeRight = true, freeLeft = true;
        private bool moveRight, moveLeft, jumping;

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
                    // Calcula a gravidade
                    if (isFalling)
                    {
                        if (!CheckGround())
                        {
                            TimeSpan secs = time - DateTime.Now;
                            MoveCharac(GravityMultiplier * Math.Pow(secs.TotalSeconds, 2), EDirection.top);
                        }
                    }
                    else
                    {
                        CheckGround();
                        time = DateTime.Now;
                    }

                    
                    if (moveLeft)
                    {
                        MoveCharac(-hspeed, EDirection.left);
                    }
                    if (moveRight)
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
            
            if(e.VirtualKey == Windows.System.VirtualKey.A)
            {
                if(freeLeft)
                {
                    //MoveCharac(-hspeed, EDirection.left);
                    moveLeft = true;
                }
            } else if(e.VirtualKey == Windows.System.VirtualKey.D)
            {
                if(freeRight)
                {
                    //MoveCharac(hspeed, EDirection.left);
                    moveRight = true;
                }
            } else if(e.VirtualKey == Windows.System.VirtualKey.W)
            {
                if(!isFalling)
                {
                    //MoveCharac(-vspeed, EDirection.top);
                    jumping = true;
                    Jump();
                }
                
            }
        }

        private void Charac_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {

            if (e.VirtualKey == Windows.System.VirtualKey.A)
            {
                
                moveLeft = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.D)
            {
                moveRight = false;
            } else if(e.VirtualKey == Windows.System.VirtualKey.W)
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
            isFalling = false;
        }

        public void Jump()
        {
            time = DateTime.Now;
            isFalling = true;
        }

        public void UpdateBlocks(Canvas blockCanvas)
        {
            collisionBlocks.Clear();
            collisionBlocks.Add(blockCanvas);
            /*foreach(UIElement block in blockCanvas.Children)
            {
                if(block is Canvas)
                {
                    collisionBlocks.Add(block);
                }
            }*/
        }

        public bool CheckGround()
        {
            foreach(Canvas bloco in collisionBlocks)
            {
                double _xoffset, _yoffset;
                double _Xblock = (double)bloco.GetValue(Canvas.LeftProperty);
                double _Yblock = (double)bloco.GetValue(Canvas.TopProperty);
                _xoffset = _Xblock - (double) characT.GetValue(Canvas.LeftProperty);
                _yoffset = _Yblock - (double) characT.GetValue(Canvas.TopProperty);

                if (_yoffset <= charac.Height && _yoffset > 0)
                {
                    if (_xoffset < charac.Width && _xoffset > -bloco.Width)
                    {
                        isFalling = false;
                        return true;
                    } else
                    {
                        isFalling = true;
                    }
                    freeDown = false;
                } else if (_yoffset >= -10  && _yoffset < 0)
                {
                    freeUp = false;
                }
            }
            return false;
        }

    }
}
