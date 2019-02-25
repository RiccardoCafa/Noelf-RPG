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
        public TextBlock textu { get; set; } 

        public const double GravityMultiplier = 0.8;

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
            
            if(e.VirtualKey == Windows.System.VirtualKey.A)
            {
                moveLeft = true;
            } else if(e.VirtualKey == Windows.System.VirtualKey.D)
            {
                moveRight = true;
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
            /* O player tem que estar em cima de alguma plataforma
             * Se não ele cai
             * 
             * Se na plataforma q ele estiver, a distancia vertical for baixa
             * Checar com os x
             */
            double xPlayer, yPlayer;
            double yMin, xMin;
            xPlayer = (double)characT.GetValue(Canvas.LeftProperty);
            yPlayer = (double)characT.GetValue(Canvas.TopProperty);
            foreach (Canvas bloco in collisionBlocks)
            {
                Canvas bottomBlock;

                double actualBlockX = (double) bloco.GetValue(Canvas.LeftProperty);
                double actualBlockY = (double) bloco.GetValue(Canvas.TopProperty);

                double xoffset = xPlayer - actualBlockX;
                double yoffset = yPlayer - actualBlockY;

                if(xPlayer >= actualBlockX && xPlayer < actualBlockX + bloco.Width)
                {
                        
                } 
            }
        }

        public void CheckGround02()
        {
            textu.Text = "";
            bool first = true;
            double _xoffset, _yoffset;
            double xLeft = 0, xRight = 0, yTop = 0, yBottom = 0;
            double xPlayer, yPlayer;
            xPlayer = (double)characT.GetValue(Canvas.LeftProperty);
            yPlayer = (double)characT.GetValue(Canvas.TopProperty);
            Canvas _xright, _xleft, _ytop, _ybottom;
            _xright = _xleft = _ytop = _ybottom = null;
            foreach (Canvas bloco in collisionBlocks)
            {
                double _Xblock = (double)bloco.GetValue(Canvas.LeftProperty);
                double _Yblock = (double)bloco.GetValue(Canvas.TopProperty);
                _xoffset = _Xblock - xPlayer;
                _yoffset = _Yblock - yPlayer;

                textu.Text += "_xoffset: " + _xoffset + "\n";
                

                double _absxoffset, _absyoffset;
                _absxoffset = Math.Abs(_xoffset);
                _absyoffset = Math.Abs(_yoffset);

                // Pegar a posição mais a direita do bloco a esquerda do player
                if(_xoffset < 0)
                {
                    if(_xleft == null)
                    {
                        _xleft = bloco;
                    } else
                    {
                        if (Math.Abs((double)_xleft.GetValue(Canvas.LeftProperty)) > _absxoffset)
                        {
                            _xleft = bloco;
                        }
                    }
                }

                // Pegar a posição mais a esquerda do bloco a direita do player
                if(_xoffset > 0)
                {
                    if (_xright == null)
                    {
                        _xright = bloco;
                    }
                    else
                    {
                        if (Math.Abs((double)_xright.GetValue(Canvas.LeftProperty)) > _absxoffset)
                        {
                            _xright = bloco;
                        }
                    }
                }

                // Pegar a posição mais baixa do bloco em cima do player
                if(_yoffset > 0)
                {
                    if (_ytop == null)
                    {
                        _ytop = bloco;
                    }
                    else
                    {
                        if (Math.Abs((double)_ytop.GetValue(Canvas.TopProperty)) > _absyoffset)
                        {
                            _ytop = bloco;
                        }
                    }
                }

                // Pegar a posição mais alta do bloco em baixo do player
                if(_yoffset < 0)
                {
                    if (_ybottom == null || Math.Abs((double)_ybottom.GetValue(Canvas.TopProperty)) > _absyoffset)
                    {
                        _ybottom = bloco;
                        textu.Text += _ybottom.Name;
                    }
                }

                // Checar em cada bloco nas vizinhaças se ele encosta

                // Para o bloco a baixo dele
                // Checar se o player está entre os dois X
                if(_ybottom != null)
                {
                    yTop = (double)_ybottom.GetValue(Canvas.TopProperty);
                    _yoffset = yTop - yPlayer;
                    xLeft = (double)_ybottom.GetValue(Canvas.LeftProperty);
                    xRight = (double)_ybottom.GetValue(Canvas.LeftProperty) + _ybottom.Width;
                    if (xPlayer >= xLeft && xPlayer <= xRight)
                    {
                        if(_yoffset <= charac.Height && _yoffset > 0)
                        {
                            isFalling = false;
                        } else { isFalling = true; }
                    } else
                    {
                        isFalling = true;
                    }
                }
                

            }
        }

        public void CheckGround_01()
        {
            textu.Text = "";
            bool flagx = true, flagy = true, first = true;
            double _minx = 0, _miny = 0;
            double _xoffset, _yoffset;
            double _yStanding = 0, _yNearest = 0;
            foreach (Canvas bloco in collisionBlocks)
            {
                double _Xblock = (double)bloco.GetValue(Canvas.LeftProperty);
                double _Yblock = (double)bloco.GetValue(Canvas.TopProperty);
                _xoffset = _Xblock - (double) characT.GetValue(Canvas.LeftProperty);
                _yoffset = _Yblock - (double) characT.GetValue(Canvas.TopProperty);

                if(_yoffset >= 0)
                {
                    _yNearest = _yoffset + charac.Height;
                }
                
                textu.Text += "_xoffset: " + _xoffset + "\n";
                if(!first)
                {
                    double valueABSX = Math.Abs(_xoffset);
                    double valueABSY = Math.Abs(_yoffset);
                    if (valueABSX >= 200) flagx = false; else
                    {
                        if (valueABSX < _minx)
                        {
                            _minx = valueABSX;
                            flagx = true;
                        }
                        else { flagx = false; }
                    }
                    if (valueABSY >= 200) flagy = false; else
                    {
                        if (valueABSY < _miny)
                        {
                            _miny = valueABSY;
                            flagy = true;
                        }
                        else { flagy = false; }
                    }
                    
                } else
                {
                    _minx = _xoffset;
                    _miny = _yoffset;
                }
                
                if(flagy)
                {
                    if (_yoffset <= charac.Height && _yoffset > 0)
                    {
                        if (_xoffset < charac.Width && _xoffset > -bloco.Width)
                        {
                            isFalling = false;
                            _yStanding = _Yblock;
                        }
                        else
                        {
                            isFalling = true;
                        }
                        freeDown = false;
                    }
                    else if (_yoffset >= -10 && _yoffset < 0)
                    {
                        freeUp = false;
                    }
                }

                if (flagx)
                {
                    //if(!isFalling) // Se tiver no ground
                    //{
                        if (_yStanding != _Yblock) 
                        // Se o bloco sendo checado estiver na mesma altura do bloco em que se encontra o player
                        {
                            if (_xoffset >= 0 && _xoffset < 1 && flagx)
                            {
                                freeRight = false;
                            }
                            else { freeRight = true; }

                            if (_xoffset < 0 && _xoffset >= -1 && flagx)
                            {
                                freeLeft = false;
                            }
                            else { freeLeft = true; }
                        }
                    //} else // Ele está voando
                    //{

                    //}
                }

            }
        }

    }
}
