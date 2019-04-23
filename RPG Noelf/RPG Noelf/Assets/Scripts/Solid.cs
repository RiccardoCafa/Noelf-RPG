using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts
{
    public enum Direction { horizontal, vertical }

    public abstract class Solid : Canvas
    {
        public const double g = 1.1;
        public DateTime time;

        public double Xi, Yi;
        public double Xf, Yf;

        public bool down, up, right, left;

        public Solid()
        {
            Xi = GetLeft(this);
            Yi = GetTop(this);
            Xf = GetLeft(this) + Width;
            Yf = GetTop(this) + Height;
            Collision.solids.Add(this);
        }
    }

    public class DynamicSolid : Solid
    {
        public delegate void MoveHandler(DynamicSolid sender, EventArgs args);
        public event MoveHandler Moved;

        public List<VirtualKey> lockedKeys = new List<VirtualKey>();

        public sbyte horizontalDirection = 0;
        public double speed;
        public double HorizontalSpeed;
        public double VerticalSpeed;
        public const double a = 0.1;
        public bool moveUp, moveRight, moveLeft;

        public DynamicSolid(double speed)
        {
            this.speed = speed;
            Moved += Collision.OnMoved;
            Window.Current.CoreWindow.KeyUp += Move;
            //time = DateTime.Now;
            Task.Run(Update);
        }

        public bool alive = true;
        public void Update()//atualiza a td instante
        {
            while (alive)
            {
                //TimeSpan secs = time - DateTime.Now;
                if (lockedKeys.Contains(VirtualKey.Down))//se ha chao
                {
                    VerticalSpeed = 0;
                    moveUp = false;
                }
                else ApplyGravity();
                if (moveUp) VerticalSpeed = speed;
                if (moveRight) horizontalDirection = 1;
                else if (moveLeft) horizontalDirection = -1;
                else horizontalDirection = 0;
                Translate(Direction.vertical);
                Translate(Direction.horizontal);
            }
        }

        public void ApplyGravity()//aplica a gravidade
        {
            VerticalSpeed -= a;
        }

        public void Move(CoreWindow sender, KeyEventArgs e)//ouve o comando do usuario de mover
        {
            if (lockedKeys.Contains(e.VirtualKey)) return;
            if (!moveUp && (e.VirtualKey == VirtualKey.Up || e.VirtualKey == VirtualKey.W)) moveUp = true;
            if (e.VirtualKey == VirtualKey.Right || e.VirtualKey == VirtualKey.D)
            {
                moveRight = true;
                moveLeft = false;
            }
            else if (e.VirtualKey == VirtualKey.Left || e.VirtualKey == VirtualKey.A)
            {
                moveRight = false;
                moveLeft = true;
            }
            else moveRight = moveLeft = false;
        }

        public void Translate(Direction direction)//translada o DynamicSolid
        {
            if (direction == Direction.vertical)
                SetTop(this, GetTop(this) - VerticalSpeed);
            if (direction == Direction.horizontal)
                SetLeft(this, GetLeft(this) + horizontalDirection * HorizontalSpeed);
            if (VerticalSpeed != 0 || HorizontalSpeed != 0) OnMoved();//chama o evento
        }

        public virtual void OnMoved() => Moved?.Invoke(this, EventArgs.Empty);//metodo q dispara o event Moved
    }
}