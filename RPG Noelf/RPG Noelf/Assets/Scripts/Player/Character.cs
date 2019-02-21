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
        Rectangle colliderBox;
        Canvas actualBlock;
        DateTime time;
        //Remove after
        TextBox playerPos;
        TextBox floorPos;

        public double constant { get; set; }

        private bool alive = true;
        private bool isFalling = false;
        private bool up, down, right, left;

        private Thread update;

        private List<Canvas> collisionBlocks = new List<Canvas>();

        public Character(Image character, Canvas T)
        {
            charac = character;
            characT = T;
            //colliderBox = box;
            constant = 0.4;

            charac.KeyDown += Charac_KeyDown;

            Start();
        }

        public void setPlayerPosText(TextBox textBox) { playerPos = textBox; }
        public void setFloorPos(TextBox textBox) { floorPos = textBox; }

        private void Start()
        {
            time = DateTime.Now;
            update = new Thread(Update);
            update.Start();
        }

        private async void Update()
        {
            while(alive)
            {
                
                // Calcula a gravidade
                if(isFalling)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                        CheckCollision();
                        // Getting the difference between the time from the start
                        //Task.Delay(1000);
                        if (isFalling)
                        {
                            TimeSpan secs = time - DateTime.Now;
                            characT.SetValue(Canvas.TopProperty, 
                                (double)characT.GetValue(Canvas.TopProperty) + 1.2 * Math.Pow(secs.TotalSeconds, 2));
                        }
                    });
                }
            }
        }

        private void Charac_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            playerPos.Text = "MOVENDO KEYDOWN";
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

        public void CheckCollision()
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
                        break;
                    } else
                    {
                        isFalling = true;
                    }
                    down = false;
                } else if (_yoffset >= -charac.Height  && _yoffset < 0)
                {
                    up = false;
                }
            }
        }

    }
}
