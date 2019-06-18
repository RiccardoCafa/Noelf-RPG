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
using RPG_Noelf.Assets.Scripts.Scenes;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.General;

namespace RPG_Noelf.Assets.Scripts
{
    public enum Axis { horizontal, vertical }
    public enum Direction { up, down, right, left }

    public class Block
    {
        public double size;
        protected double xi;
        public double Xi {
            get { return xi; }
            set { xi = value; }
        }
        protected double yi;
        public double Yi {
            get { return yi; }
            set { yi = value; }
        }
        public double Xf {
            get { return xi + size; }
            set { xi = value - size; }
        }
        public double Yf {
            get { return yi + size; }
            set { yi = value - size; }
        }
        public Block(double xi, double yi)
        {
            Xi = xi;
            Yi = yi;
            size = Matriz.scale;
            Solid.SetBlock(xi / Matriz.scale, yi / Matriz.scale, this);
        }
    }

    public class Solid : Canvas//solido colidivel
    {
        public static Block[,] blocks = new Block[115, 21];
        public static Block GetBlock(double x, double y)
        {
            for (int i = 0; i < 21; i++) { new Block(0, i * Matriz.scale); new Block(112 * Matriz.scale, i * Matriz.scale); }
            if (x + DynamicSolid.step / Matriz.scale >= 0 && y >= 0 && x + DynamicSolid.step / Matriz.scale < 115 && y < 21)
                return blocks[(int)(x + DynamicSolid.step / Matriz.scale), (int)y];
            else return null;
        }
        public static void SetBlock(double x, double y, Block s)
        {
            if (x >= 0 && y >= 0 && x < 115 && y < 21)
                blocks[(int)x, (int)y] = s;
        }
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

        //public double wScale = 114 / 1366, hScale = 21 / 768;
        public Solid(double xi, double yi, double width, double height)
        {
            Xi = xi;
            Yi = yi;
            Width = width;
            Height = height;
            //SetSolid(xi / Matriz.scale, yi / Matriz.scale, this);
            //if (!(this is DynamicSolid)) Matriz.matriz[(int)(xi * wScale), (int)(yi * hScale)] = true;
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
        public Solid Affected;
        public DispatcherTimer timer;
        private int TimesTicked = 0;
        public int TimesToTick = 1;
        public double bonusDamage = 0;

        public HitSolid(double xi, double yi, double width, double height, DynamicSolid who, double spd) : base(xi, yi, width, height, spd)
        {
            Background = new SolidColorBrush(Color.FromArgb(50, 50, 0, 0));
            //solids.Remove(this);
            g = 0;
            Who = who;
        }

        public void DispatcherTimeSetup()
        {
            timer = new DispatcherTimer();
            timer.Tick += DispatcherTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        protected virtual void DispatcherTimer_Tick(object sender, object a)
        {
            TimesTicked++;
            if (TimesTicked >= TimesToTick)
            {
                if (speed != 0)
                {
                    Affected = Interaction();
                    if (Affected != null && Affected.MyEnt != null)
                    {
                        Affected.MyEnt.Hit(Who.MyEnt.Hit(bonusDamage));
                        speed = 0;
                    }
                }
                else if (TimesTicked >= 5f || Affected != null)
                {
                    if (Who != null)
                    {
                        Visibility = Visibility.Collapsed;
                        Who.MyEnt.HitPool.AddToPool(this);
                        alive = false;
                        timer.Stop();
                    }
                }
                TimesTicked = 0;
            }
        }

        public Solid Interaction()//o q este solido faz com os outros ao redor
        {
            DynamicSolid dynamicFound = null;
            var dinamics = DynamicSolid.DynamicSolids;// from dinm in solids where dinm is DynamicSolid select dinm;

            foreach (DynamicSolid solid in dinamics)
            {
                if (solid.Equals(Who)) continue;
                if (Yi < solid.Yf && Yf > solid.Yi && Xi < solid.Xf && Xf > solid.Xi)//se o solid eh candidato a colidir nos lados do solidMoving
                {
                    dynamicFound = solid;
                    break;
                }
            }
            DispatcherTimeSetup();
            return dynamicFound;
            //return new Solid(0, 0, 0, 0);
        }
    }

    public class DynamicSolid : Solid//solido q se movimenta
    {
        public delegate void MoveHandler();
        public event MoveHandler Moved;

        public static List<DynamicSolid> DynamicSolids = new List<DynamicSolid>();

        public Dictionary<Direction, bool> freeDirections = new Dictionary<Direction, bool>() {
            { Direction.up, true }, { Direction.down, true }, { Direction.right, true }, { Direction.left, true } };

        public double speed;
        public double jumpSpeed;
        public double verticalSpeed;
        public double horizontalSpeed;
        public sbyte horizontalDirection = 0;
        public sbyte lastHorizontalDirection = 1;
        public double g = 1500;
        public bool moveRight, moveLeft;
        public bool alive = false;
        protected DateTime time;

        public DynamicSolid(double xi, double yi, double width, double height, double speed) : base(xi + 0, yi - 0, width, height)
        {
            Background = new SolidColorBrush(Color.FromArgb(50, 50, 0, 0));
            DynamicSolids.Add(this);
            this.speed = speed;
            jumpSpeed = speed * 150;
            Moved += OnMoved;
            horizontalSpeed = speed * 250;
            Window.Current.CoreWindow.KeyDown += Start;
        }
        TimeSpan span;
        public Task task;
        public void Start(CoreWindow sender, KeyEventArgs e)
        {
            alive = true;
            time = DateTime.Now;
            span = DateTime.Now - time;
        }

        public void Update()//atualiza a td instante
        {
            if (alive)
            {
                time = DateTime.Now;
                if (g != 0 && freeDirections[Direction.down])
                {
                    ApplyGravity(span.TotalSeconds);
                }//se n ha chao
                else verticalSpeed = 0;
                if (moveRight) lastHorizontalDirection = horizontalDirection = 1;//se esta se movimentando para direita
                else if (moveLeft) lastHorizontalDirection = horizontalDirection = -1;//se esta se movimentando para esquerda
                else horizontalDirection = 0;//se n quer se mover pros lados
                Translate(Axis.vertical, span.TotalSeconds);
                Translate(Axis.horizontal, span.TotalSeconds);
                span = DateTime.Now - time;
            }
        }

        public virtual void Translate(Axis direction, double span)//translada o DynamicSolid
        {
            if (direction == Axis.vertical) Yi -= verticalSpeed * span;
            if (direction == Axis.horizontal) Xi += horizontalDirection * horizontalSpeed * span;
            if (verticalSpeed != 0 || horizontalDirection != 0) Move();//Interact/ion();//chama o evento
        }
        List<Block> slds = new List<Block>();
        public void OnMoved()//o q este solido faz com os outros ao redor
        {
            freeDirections[Direction.down] =
            freeDirections[Direction.right] =
            freeDirections[Direction.left] = true;
            const double margin = 15;
            slds.Clear();
            #region nao abrir, risco de morte por hemorragia ocular
            if (GetBlock(Xi / Matriz.scale + step, Yi / Matriz.scale) != null) slds.Add(GetBlock(Xi / Matriz.scale, Yi / Matriz.scale));

            if (GetBlock(Xi / Matriz.scale - 1, Yi / Matriz.scale) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, Yi / Matriz.scale));
            if (GetBlock(Xi / Matriz.scale + 1, Yi / Matriz.scale) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, Yi / Matriz.scale));
            if (GetBlock(Xi / Matriz.scale, Yi / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale, Yi / Matriz.scale + 1));
            //GetSolid(Xi / Matriz.scale,Yi / Matriz.scale - 1),

            //GetSolid(Xi / Matriz.scale - 1,Yi / Matriz.scale - 1),
            if (GetBlock(Xi / Matriz.scale - 1, Yi / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, Yi / Matriz.scale + 1));
            if (GetBlock(Xi / Matriz.scale + 1, Yi / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, Yi / Matriz.scale + 1));
            //GetSolid(Xi / Matriz.scale + 1,Yi / Matriz.scale - 1),

            if (GetBlock((Xf - 1) / Matriz.scale, (Yf - 1) / Matriz.scale) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale, (Yf - 1) / Matriz.scale));

            if (GetBlock((Xf - 1) / Matriz.scale - 1, (Yf - 1) / Matriz.scale) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale - 1, (Yf - 1) / Matriz.scale));
            if (GetBlock((Xf - 1) / Matriz.scale + 1, (Yf - 1) / Matriz.scale) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale + 1, (Yf - 1) / Matriz.scale));
            if (GetBlock((Xf - 1) / Matriz.scale, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock((Xf - 1) / Matriz.scale, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale, (Yf - 1) / Matriz.scale - 1));

            if (GetBlock((Xf - 1) / Matriz.scale - 1, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale - 1, (Yf - 1) / Matriz.scale - 1));
            if (GetBlock((Xf - 1) / Matriz.scale - 1, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale - 1, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock((Xf - 1) / Matriz.scale + 1, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale + 1, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock((Xf - 1) / Matriz.scale + 1, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock((Xf - 1) / Matriz.scale + 1, (Yf - 1) / Matriz.scale - 1));

            if (GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale));
            if (GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale));
            if (GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale - 1));

            if (GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale - 1));
            if (GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale - 1));

            if (GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale));
            if (GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale));
            if (GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock(Xi / Matriz.scale, (Yf - 1) / Matriz.scale - 1));

            if (GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale - 1));
            if (GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale - 1, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale + 1) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale + 1));
            if (GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale - 1) != null) slds.Add(GetBlock(Xi / Matriz.scale + 1, (Yf - 1) / Matriz.scale - 1));
            #endregion
            foreach (Block block in slds)
            {
                if (freeDirections[Direction.down] && Yf >= block.Yi && Yf < block.Yi + margin)//se o solidMoving esta no nivel de pisar em algum Solid
                {
                    if (Xi + step < block.Xf && Xf + step > block.Xi)//se o solidMoving esta colindindo embaixo
                    {
                        Yf = block.Yi;
                        freeDirections[Direction.down] = false;
                    }
                }
                if (Yi < block.Yf && Yf > block.Yi)//se o solid eh candidato a colidir nos lados do solidMoving
                {
                    if (freeDirections[Direction.right] && Xf + step >= block.Xi && Xf + step < block.Xi + margin)//se o solidMoving esta colindindo a direita
                    {
                        Xf = block.Xi - step;
                        freeDirections[Direction.right] = false;
                    }
                    if (freeDirections[Direction.left] && Xi + step <= block.Xf && Xi + step > block.Xf - margin)//se o solidMoving esta colindindo a esquerda
                    {
                        Xi = block.Xf - step;
                        freeDirections[Direction.left] = false;
                    }
                }
            }
        }
        
        public void ApplyGravity(double span) => verticalSpeed -= g * span / 0.06;//aplica a gravidade
        
        public static double step = 0;
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
                    if (!freeDirections[Direction.down])
                    {
                        Yi -= jumpSpeed / 5;//verticalSpeed = jumpSpeed * 10;
                        OnMoved();//verticalSpeed = jumpSpeed;
                        time = DateTime.Now;
                    }
                        //verticalSpeed = 700;
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
                    SetLeft(GameManager.instance.scene.layers[2], GetLeft(GameManager.instance.scene.layers[2]) - 1366 * 0.075);
                    SetLeft(GameManager.instance.scene.layers[1], GetLeft(GameManager.instance.scene.layers[1]) - 1366 * 0.15);
                    SetLeft(GameManager.instance.scene.layers[0], GetLeft(GameManager.instance.scene.layers[0]) - 1366 * 0.3);
                    SetLeft(Platform.chunck, GetLeft(Platform.chunck) - 1366);
                    foreach (Solid s in GameManager.instance.scene.scene.floor) s.Xi -= 1366;
                    step += 1366;
                    //foreach (Block block in blocks)
                    //{
                    //    if (block != null)
                    //    {
                    //        Solid a = new Solid(block.Xi, block.Yi, Matriz.scale, Matriz.scale)
                    //        {
                    //            Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0))
                    //        };
                    //        Platform.chunck.Children.Add(a);
                    //    }
                    //}
                    //Solid b = new Solid(Xi + step, Yi, Matriz.scale, Matriz.scale)
                    //{
                    //    Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0))
                    //};
                    //Platform.chunck.Children.Add(b);
                }
                if ((Xi + Xf) / 2 <= 0)
                {
                    Xi += 1366;
                    SetLeft(GameManager.instance.scene.layers[2], GetLeft(GameManager.instance.scene.layers[2]) + 1366 * 0.075);
                    SetLeft(GameManager.instance.scene.layers[1], GetLeft(GameManager.instance.scene.layers[1]) + 1366 * 0.15);
                    SetLeft(GameManager.instance.scene.layers[0], GetLeft(GameManager.instance.scene.layers[0]) + 1366 * 0.3);
                    SetLeft(Platform.chunck, GetLeft(Platform.chunck) + 1366);
                    foreach (Solid s in GameManager.instance.scene.scene.floor) s.Xi += 1366;
                    step -= 1366;
                }
            }
            if (verticalSpeed != 0 || horizontalDirection != 0)
            {
                Move();//chama o evento
            }
        }
    }
}