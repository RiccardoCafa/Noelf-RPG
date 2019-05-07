using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts
{
    public enum Axis { horizontal, vertical }
    public enum Direction { up, down, right, left }

    public class Solid : Canvas//solido colidivel
    {
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

    public class DynamicSolid : Solid//solido q se movimenta
    {
        public delegate void MoveHandler(DynamicSolid sender);
        public event MoveHandler Moved;

        public Dictionary<Direction, bool> freeDirections = new Dictionary<Direction, bool>() {
            { Direction.down, true }, { Direction.right, true }, { Direction.left, true } };

        public double speed;
        public double jumpSpeed;
        public double verticalSpeed;
        public double horizontalSpeed;
        public sbyte horizontalDirection = 0;
        public const double g = 0.5;
        public bool moveRight, moveLeft;
        DateTime time;

        public DynamicSolid(double xi, double yi, double width, double height, double speed) : base(xi, yi, width, height)
        {
            speed = 5;
            jumpSpeed = speed / 7;
            Moved += Collision.OnMoved;
            horizontalSpeed = speed / 20;
            time = DateTime.Now;
            new Task(Update).Start();
        }

        public bool alive = true;
        public async void Update()//atualiza a td instante
        {
            TimeSpan span = DateTime.Now - time;
            while (alive)
            {
                //Thread.Sleep(5);
                if (freeDirections[Direction.down]) ApplyGravity(span.TotalSeconds);//se n ha chao
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
            time = DateTime.Now;
        }

        public virtual void Translate(Axis direction)//translada o DynamicSolid
        {
            if (direction == Axis.vertical) Yi -= verticalSpeed;
            if (direction == Axis.horizontal) Xi += horizontalDirection * horizontalSpeed;
            if (verticalSpeed != 0 || horizontalDirection != 0) OnMoved();//chama o evento
        }

        public void ApplyGravity(double span) => verticalSpeed -= g*span;//aplica a gravidade

        public void OnMoved() => Moved?.Invoke(this);//metodo q dispara o event Moved
    }

    public class PlayableSolid : DynamicSolid//solido controlavel
    {
        public PlayableSolid(double xi, double yi, double width, double height, double speed) : base(xi, yi, width, height, speed)
        {
            Moved += Camera.OnMoved;
            Window.Current.CoreWindow.KeyDown += Move;
            Window.Current.CoreWindow.KeyUp += Stop;
        }

        public void Move(CoreWindow sender, KeyEventArgs e)//ouve o comando do usuario de mover
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Up:
                case VirtualKey.W://usuario quer pular
                    if (!freeDirections[Direction.down]) verticalSpeed = jumpSpeed;
                    break;
                case VirtualKey.Right:
                case VirtualKey.D://usuario quer mover p direita
                    moveRight = freeDirections[Direction.right];
                    break;
                case VirtualKey.Left:
                case VirtualKey.A://usuario quer mover p esquerda
                    moveLeft = freeDirections[Direction.left];
                    break;
            }
        }

        public void Stop(CoreWindow sender, KeyEventArgs e)//ouve o comando do usuario de parar
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Right: case VirtualKey.D://usuario soltou a direita
                    moveRight = false;
                    break;
                case VirtualKey.Left: case VirtualKey.A://usuario soltou a esquerda
                    moveLeft = false;
                    break;
            }
        }

        public override void Translate(Axis direction)//translada o LevelScene
        {
            if (direction == Axis.vertical)
            {
                Yi -= verticalSpeed;
                //SetTop(Game.instance.scene1.layers[2], GetTop(Game.instance.scene1.layers[2]) + verticalSpeed * 0.125);
                //SetTop(Game.instance.scene1.layers[1], GetTop(Game.instance.scene1.layers[1]) + verticalSpeed * 0.25);
                //SetTop(Game.instance.scene1.layers[0], GetTop(Game.instance.scene1.layers[0]) + verticalSpeed * 0.5);
                //SetTop(Game.instance.scene1.scene.chunck, GetTop(Game.instance.scene1.scene.chunck) + verticalSpeed);
            }
            if (direction == Axis.horizontal)
            {
                Xi += horizontalDirection * horizontalSpeed;
                //SetLeft(Game.instance.scene1.layers[2], GetLeft(Game.instance.scene1.layers[2]) - horizontalDirection * horizontalSpeed * 0.125);
                //SetLeft(Game.instance.scene1.layers[1], GetLeft(Game.instance.scene1.layers[1]) - horizontalDirection * horizontalSpeed * 0.25);
                //SetLeft(Game.instance.scene1.layers[0], GetLeft(Game.instance.scene1.layers[0]) - horizontalDirection * horizontalSpeed * 0.5);
                //SetLeft(Game.instance.scene1.scene.chunck, GetLeft(Game.instance.scene1.scene.chunck) - horizontalDirection * horizontalSpeed);
            }
            if (verticalSpeed != 0 || horizontalDirection != 0)
            {
                OnMoved();//chama o evento
            }
        }
    }

    public class LimitArgs
    {
        public Direction limit;
        public double speed;

        public LimitArgs(Direction limit, double speed)
        {
            this.limit = limit;
            this.speed = speed;
        }
    }

    public static class Camera
    {
        private const double boundX = 1366, boundY = 768;
        public delegate void ReachedLimitHandler(LimitArgs args);
        public static event ReachedLimitHandler ReachedLimit;

        public static void OnMoved(DynamicSolid solidMoving)
        {
            //if (solidMoving.Xi <= 350)
            //{
            //    OnReachedLimit(new LimitArgs(Direction.left, solidMoving.speed));
            //    solidMoving.freeDirections[Direction.left] = false;
            //}
            //if (solidMoving.Xf >= boundX - 350)
            //{
            //    OnReachedLimit(new LimitArgs(Direction.right, solidMoving.speed));
            //    solidMoving.freeDirections[Direction.right] = false;
            //}
            //if (solidMoving.Yi <= 300)
            //{
            //    OnReachedLimit(new LimitArgs(Direction.up, solidMoving.speed));
            //    solidMoving.freeDirections[Direction.up] = false;
            //}
            //if (solidMoving.Yi >= boundY - 200)
            //{
            //    OnReachedLimit(new LimitArgs(Direction.down, solidMoving.speed));
            //    solidMoving.freeDirections[Direction.down] = false;
            //}
        }

        public static void OnReachedLimit(LimitArgs args) => ReachedLimit?.Invoke(args);//metodo q dispara o event ReachedLimit
    }
}