using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace RPG_Noelf.Assets.Scripts
{
    public enum Axis { horizontal, vertical }
    public enum Direction { up, down, right, left }

    public class Solid : Canvas//solido colidivel
    {
        public static List<Solid> solids = new List<Solid>();
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
            solids.Add(this);
        }
    }

    public class HitSolid : Solid//solido q causa dano
    {
        //public delegate void MoveHandler(Solid sender);
        //public event MoveHandler Moved;

        public HitSolid(double xi, double yi, double width, double height, byte dmg) : base(xi, yi, width, height)
        {
            Background = new SolidColorBrush(Color.FromArgb(50, dmg, 0, 0));
            solids.Remove(this);
            //Moved += Collision.OnMoved;
        }

        public void Interaciton()//o q este solido faz com os outros ao redor
        {
            const double margin = 20;
            foreach (Solid solid in solids)
            {
                if (solid is DynamicSolid)
                {

                }
                if (Equals(solid)) return;//se for comparar o solidMoving com ele msm, pule o teste
                if (Yf >= solid.Yi && Yf < solid.Yi + margin)//se o solidMoving esta no nivel de pisar em algum Solid
                {
                    if (Xi < solid.Xf && Xf > solid.Xi)//se o solidMoving esta colindindo embaixo
                    {
                        Yf = solid.Yi;
                        //freeDirections[Direction.down] = false;
                    }
                }
                if (Yi < solid.Yf && Yf > solid.Yi)//se o solid eh candidato a colidir nos lados do solidMoving
                {
                    if (Xf >= solid.Xi && Xf < solid.Xi + margin)//se o solidMoving esta colindindo a direita
                    {
                        Xf = solid.Xi;
                        //freeDirections[Direction.right] = false;
                    }
                    if (Xi <= solid.Xf && Xi > solid.Xf - margin)//se o solidMoving esta colindindo a esquerda
                    {
                        Xi = solid.Xf;
                        //freeDirections[Direction.left] = false;
                    }
                }
            }
        }
    }

    public class DynamicSolid : Solid//solido q se movimenta
    {
        public delegate void MoveHandler();
        public event MoveHandler Moved;

        public Dictionary<Direction, bool> freeDirections = new Dictionary<Direction, bool>() {
            { Direction.down, true }, { Direction.right, true }, { Direction.left, true } };

        public double speed;
        public double jumpSpeed;
        public double verticalSpeed;
        public double horizontalSpeed;
        public sbyte horizontalDirection = 0;
        public const double g = 1500;
        public bool moveRight, moveLeft;
        DateTime time;

        public DynamicSolid(double xi, double yi, double width, double height, double speed) : base(xi, yi, width, height)
        {
            this.speed = speed;
            jumpSpeed = speed * 150;
            Moved += OnMoved;
            horizontalSpeed = speed * 75;
            time = DateTime.Now;
            new Task(Update).Start();
        }

        public bool alive = true;
        public async void Update()//atualiza a td instante
        {
            TimeSpan span = DateTime.Now - time;
            while (alive)
            {
                time = DateTime.Now;
                if (freeDirections[Direction.down]) ApplyGravity(span.TotalSeconds);//se n ha chao
                else verticalSpeed = 0;
                if (moveRight) horizontalDirection = 1;//se esta se movimentando para direita
                else if (moveLeft) horizontalDirection = -1;//se esta se movimentando para esquerda
                else horizontalDirection = 0;//se n quer se mover pros lados
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Translate(Axis.vertical, span.TotalSeconds);
                    Translate(Axis.horizontal, span.TotalSeconds);
                });
                span = DateTime.Now - time;
            }
        }

        public virtual void Translate(Axis direction, double span)//translada o DynamicSolid
        {
            if (direction == Axis.vertical) Yi -= verticalSpeed * span;
            if (direction == Axis.horizontal) Xi += horizontalDirection * horizontalSpeed * span;
            if (verticalSpeed != 0 || horizontalDirection != 0) Move();//Interaction();//chama o evento
        }

        public void OnMoved()//o q este solido faz com os outros ao redor
        {
            const double margin = 20;
            freeDirections[Direction.down] =
            freeDirections[Direction.right] =
            freeDirections[Direction.left] = true;
            foreach (Solid solid in solids)
            {
                if (Equals(solid)) return;//se for comparar o solidMoving com ele msm, pule o teste
                if (Yf >= solid.Yi && Yf < solid.Yi + margin)//se o solidMoving esta no nivel de pisar em algum Solid
                {
                    if (Xi < solid.Xf && Xf > solid.Xi)//se o solidMoving esta colindindo embaixo
                    {
                        Yf = solid.Yi;
                        freeDirections[Direction.down] = false;
                    }
                }
                if (Yi < solid.Yf && Yf > solid.Yi)//se o solid eh candidato a colidir nos lados do solidMoving
                {
                    if (Xf >= solid.Xi && Xf < solid.Xi + margin)//se o solidMoving esta colindindo a direita
                    {
                        Xf = solid.Xi;
                        freeDirections[Direction.right] = false;
                    }
                    if (Xi <= solid.Xf && Xi > solid.Xf - margin)//se o solidMoving esta colindindo a esquerda
                    {
                        Xi = solid.Xf;
                        freeDirections[Direction.left] = false;
                    }
                }
            }
        }

        public void ApplyGravity(double span) => verticalSpeed -= g * span;//aplica a gravidade

        public void Move() => Moved?.Invoke();//metodo q dispara o event Moved
    }

    public class PlayableSolid : DynamicSolid//solido controlavel
    {
        public PlayableSolid(double xi, double yi, double width, double height, double speed) : base(xi, yi, width, height, speed)
        {
            Moved += OnMoved;
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
                case VirtualKey.Right:
                case VirtualKey.D://usuario soltou a direita
                    moveRight = false;
                    break;
                case VirtualKey.Left:
                case VirtualKey.A://usuario soltou a esquerda
                    moveLeft = false;
                    break;
            }
        }

        public override void Translate(Axis direction, double span)//translada o LevelScene
        {
            if (direction == Axis.vertical)
            {
                Yi -= verticalSpeed * span;
                //SetTop(Game.instance.scene1.layers[2], GetTop(Game.instance.scene1.layers[2]) + verticalSpeed * span * 0.075);
                //SetTop(Game.instance.scene1.layers[1], GetTop(Game.instance.scene1.layers[1]) + verticalSpeed * span * 0.15);
                //SetTop(Game.instance.scene1.layers[0], GetTop(Game.instance.scene1.layers[0]) + verticalSpeed * span * 0.3);
                //foreach (UIElement child in Game.instance.scene1.scene.chunck.Children)
                //{
                //    if (child is Solid) (child as Solid).Xi += verticalSpeed * span;
                //    else if (child is Image) SetTop(child, GetTop(child) + verticalSpeed * span);
                //}
            }
            if (direction == Axis.horizontal)
            {
                Xi += horizontalDirection * horizontalSpeed * span;
                //SetLeft(Game.instance.scene1.layers[2], GetLeft(Game.instance.scene1.layers[2]) - horizontalDirection * horizontalSpeed * span * 0.075);
                //SetLeft(Game.instance.scene1.layers[1], GetLeft(Game.instance.scene1.layers[1]) - horizontalDirection * horizontalSpeed * span * 0.15);
                //SetLeft(Game.instance.scene1.layers[0], GetLeft(Game.instance.scene1.layers[0]) - horizontalDirection * horizontalSpeed * span * 0.3);
                //foreach (Solid s in Game.instance.scene1.scene.ground) s.Xi -= horizontalDirection * horizontalSpeed * span;
            }
            //if (verticalSpeed != 0 || horizontalDirection != 0)
            //{
            Move();//chama o evento
            //}
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