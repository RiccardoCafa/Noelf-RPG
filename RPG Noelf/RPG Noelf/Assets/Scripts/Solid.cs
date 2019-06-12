using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Linq;

namespace RPG_Noelf.Assets.Scripts
{
    public enum Axis { horizontal, vertical }
    public enum Direction { up, down, right, left }

    public class Solid : Canvas//solido colidivel
    {
        public static List<Solid> solids = new List<Solid>();
        public Ent MyEnt;
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

        public double GetDistance(double xref, double yref)
        {
            return Math.Sqrt(Math.Pow(xref - Xi, 2) + Math.Pow(yref - Yi, 2));
        }
    }

    public class HitSolid : DynamicSolid//solido q causa dano
    {
        //public delegate void MoveHandler(Solid sender);
        //public event MoveHandler Moved;

        public DynamicSolid Who;
        public DispatcherTimer timer;
        private int TimesTicked = 0;
        public int TimesToTick = 1;

        public HitSolid(double xi, double yi, double width, double height, DynamicSolid who, double spd) : base(xi, yi, width, height, spd)
        {
            Background = new SolidColorBrush(Color.FromArgb(50, 50, 0, 0));
            solids.Remove(this);
            g = 0;
            Who = who;
            g = 0;
        }

        public void DispatcherTimeSetup()
        {
            timer = new DispatcherTimer();
            timer.Tick += DispatcherTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            //TimesTicked = 0;
            timer.Start();
        }

        protected virtual void DispatcherTimer_Tick(object sender, object a)
        {
            TimesTicked++;
            if (TimesTicked >= TimesToTick)
            {
                if (Who != null)
                {
                    Visibility = Visibility.Collapsed;
                    Who.MyEnt.HitPool.AddToPool(this);
                    timer.Stop();
                }
            }
        }

        public DynamicSolid Interaction()//o q este solido faz com os outros ao redor
        {
            DynamicSolid dynamicFound = null;
            var dinamics = from dinm in solids where dinm is DynamicSolid select dinm;

            foreach (DynamicSolid solid in dinamics)
            {
                if (solid.Equals(Who)) continue;
                if (Yi < solid.Yf && Yf > solid.Yi && Xi < solid.Xf && Xf > solid.Xi)//se o solid eh candidato a colidir nos lados do solidMoving
                {
                    dynamicFound = solid as DynamicSolid;
                    break;
                }
            }
            DispatcherTimeSetup();
            return dynamicFound;
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
        public sbyte lastHorizontalDirection = 1;
        public double g = 1500;
        public bool moveRight, moveLeft;
        DateTime time;

        public DynamicSolid(double xi, double yi, double width, double height, double speed) : base(xi, yi, width, height)
        {
            Background = new SolidColorBrush(Color.FromArgb(50, 50, 0, 0));
            this.speed = speed;
            jumpSpeed = speed * 150;
            Moved += OnMoved;
            horizontalSpeed = speed * 75;
            Window.Current.CoreWindow.KeyDown += Start;
        }

        Task task;
        public void Start(CoreWindow sender, KeyEventArgs e)
        {
            if (task == null)
            {
                time = DateTime.Now;
                task = new Task(Update);
                task.Start();
            }
        }

        public bool alive = true;
        public async void Update()//atualiza a td instante
        {
            TimeSpan span = DateTime.Now - time;
            while (alive)
            {
                time = DateTime.Now;
                if (freeDirections[Direction.down])
                {
                    ApplyGravity(span.TotalSeconds);
                }//se n ha chao
                else verticalSpeed = 0;
                if (moveRight) lastHorizontalDirection = horizontalDirection = 1;//se esta se movimentando para direita
                else if (moveLeft) lastHorizontalDirection = horizontalDirection = -1;//se esta se movimentando para esquerda
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
            const double margin = 15;
            freeDirections[Direction.down] =
            freeDirections[Direction.right] =
            freeDirections[Direction.left] = true;
            foreach (Solid solid in solids)
            {
                if (!(solid is DynamicSolid))
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
        }

        public void ApplyGravity(double span) => verticalSpeed -= g * span / 0.6;//aplica a gravidade

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
            }
            if (direction == Axis.horizontal)
            {
                Xi += horizontalDirection * horizontalSpeed * span;
                if ((Xi + Xf) / 2 >= 1366)
                {
                    Xi -= 1366;
                    SetLeft(Game.instance.scene1.layers[2], GetLeft(Game.instance.scene1.layers[2]) - 1366 * 0.075);
                    SetLeft(Game.instance.scene1.layers[1], GetLeft(Game.instance.scene1.layers[1]) - 1366 * 0.15);
                    SetLeft(Game.instance.scene1.layers[0], GetLeft(Game.instance.scene1.layers[0]) - 1366 * 0.3);
                    SetLeft(Game.instance.scene1.scene.chunck, GetLeft(Game.instance.scene1.scene.chunck) - 1366);
                    foreach (Solid s in Game.instance.scene1.scene.floor) s.Xi -= 1366;
                }
                if ((Xi + Xf) / 2 <= 0)
                {
                    Xi += 1366;
                    SetLeft(Game.instance.scene1.layers[2], GetLeft(Game.instance.scene1.layers[2]) + 1366 * 0.075);
                    SetLeft(Game.instance.scene1.layers[1], GetLeft(Game.instance.scene1.layers[1]) + 1366 * 0.15);
                    SetLeft(Game.instance.scene1.layers[0], GetLeft(Game.instance.scene1.layers[0]) + 1366 * 0.3);
                    SetLeft(Game.instance.scene1.scene.chunck, GetLeft(Game.instance.scene1.scene.chunck) + 1366);
                    foreach (Solid s in Game.instance.scene1.scene.floor) s.Xi += 1366;
                }
            }
            if (verticalSpeed != 0 || horizontalDirection != 0)
            {
                Move();//chama o evento
            }
        }
    }
}