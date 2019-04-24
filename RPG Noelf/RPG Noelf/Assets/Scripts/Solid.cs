using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;

namespace RPG_Noelf.Assets.Scripts
{
    public enum Axis { horizontal, vertical }

    public class Solid : Canvas
    {
        public DateTime time;

        protected double xi;
        public double Xi {
            get { return xi; }
            set { SetLeft(this, value); xi = value; }
        }
        protected double yi;
        public double Yi {
            get { return yi; }
            set { SetTop(this, value); yi = value; }
        }
        public double Xf {
            get { return xi + Width; }
            set { xi = value - Width; }
        }
        public double Yf {
            get { return yi + Height; }
            set { yi = value - Height; }
        }
        public bool down, up, right, left;

        public Solid(double xi, double yi, double width, double height)
        {
            Xi = xi;
            Yi = yi;
            Width = width;
            Height = height;
            Collision.solids.Add(this);
        }
    }

    public enum Direction { down, right, left }

    public class DynamicSolid : Solid
    {
        public delegate void MoveHandler(DynamicSolid sender);
        public event MoveHandler Moved;

        //public List<VirtualKey> lockedKeys = new List<VirtualKey>();
        public Dictionary<Direction, bool> freeDirections = new Dictionary<Direction, bool>() {
            { Direction.down, true }, { Direction.right, true }, { Direction.left, true } };

        public double speed;
        public const double jumpSpeed = 0.5;
        public double verticalSpeed;
        public double horizontalSpeed;
        public sbyte horizontalDirection = 0;
        public const double g = 0.001;
        public bool jump, moveRight, moveLeft;

        public DynamicSolid(double xi, double yi, double width, double height, double speed) : base(xi, yi, width, height)
        {
            this.speed = 0.5;
            Moved += Collision.OnMoved;
            horizontalSpeed = speed / 20;
            Window.Current.CoreWindow.KeyDown += Move;
            Window.Current.CoreWindow.KeyUp += Stop;
            Task.Run(Update);
        }

        public bool alive = true;
        public async void Update()//atualiza a td instante
        {
            while (alive)
            {
                if (freeDirections[Direction.down]) ApplyGravity();//se n ha chao
                else verticalSpeed = 0;
                if (moveRight) horizontalDirection = 1;//se esta se movimentando para direita
                else if (moveLeft) horizontalDirection = -1;//se esta se movimentando para esquerda
                else horizontalDirection = 0;//se n quer se mover pros lados
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Translate(Axis.vertical);
                    Translate(Axis.horizontal);
                });
            }
        }

        public void ApplyGravity() => verticalSpeed = verticalSpeed < 5 ? verticalSpeed - g : 5;//aplica a gravidade

        public void Stop(CoreWindow sender, KeyEventArgs e)//ouve o comando do usuario de parar
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Up: case VirtualKey.W://usuario soltou o pular
                    jump = false;
                    break;
                case VirtualKey.Right: case VirtualKey.D://usuario soltou a direita
                    moveRight = false;
                    break;
                case VirtualKey.Left: case VirtualKey.A://usuario soltou a esquerda
                    moveLeft = false;
                    break;
            }
        }

        public void Move(CoreWindow sender, KeyEventArgs e)//ouve o comando do usuario de mover
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Up: case VirtualKey.W://usuario quer pular
                    if (!freeDirections[Direction.down]) verticalSpeed = jumpSpeed;
                    break;
                case VirtualKey.Right: case VirtualKey.D://usuario quer mover p direita
                    moveRight = freeDirections[Direction.right];
                    break;
                case VirtualKey.Left: case VirtualKey.A://usuario quer mover p esquerda
                    moveLeft = freeDirections[Direction.left];
                    break;
            }
        }

        public void Translate(Axis direction)//translada o DynamicSolid
        {
            if (direction == Axis.vertical) Yi -= verticalSpeed;
            if (direction == Axis.horizontal) Xi += horizontalDirection * horizontalSpeed;
            if (verticalSpeed != 0 || horizontalSpeed != 0) OnMoved();//chama o evento
        }

        public void OnMoved() => Moved?.Invoke(this);//metodo q dispara o event Moved
    }
}