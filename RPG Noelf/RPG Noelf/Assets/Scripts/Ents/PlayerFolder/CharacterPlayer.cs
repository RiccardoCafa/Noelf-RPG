using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    class CharacterPlayer : Character
    {
        Rectangle charac;
        Canvas characT;
        Canvas LastBlock;
        DateTime time;
        public RotateTransform rotation { get; set; }

        private enum EDirection
        {
            top,
            left
        }

        public CharacterPlayer(Canvas T) : base(T)
        {
            // Getting character image and Canvas for control
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
        }

        private void Charac_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            if (!isFalling)
            {
                if (e.VirtualKey == Windows.System.VirtualKey.A || e.VirtualKey == Windows.System.VirtualKey.Left)
                {
                    moveLeft = true;
                }
                else if (e.VirtualKey == Windows.System.VirtualKey.D || e.VirtualKey == Windows.System.VirtualKey.Right)
                {
                    moveRight = true;
                }
                else if (e.VirtualKey == Windows.System.VirtualKey.W || e.VirtualKey == Windows.System.VirtualKey.Up)
                {
                    //MoveCharac(-vspeed, EDirection.top);
                    jumping = true;
                    Jump();
                }
            }
            else
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
                if (isFalling) prepLeft = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.D || e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                moveRight = false;
                if (isFalling) prepRight = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.W || e.VirtualKey == Windows.System.VirtualKey.Up)
            {
                jumping = false;
            }

        }
    }
}
