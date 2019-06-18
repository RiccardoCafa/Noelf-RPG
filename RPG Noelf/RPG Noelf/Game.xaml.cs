using RPG_Noelf.Assets.Scripts.General;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
namespace RPG_Noelf
{
    /// <summary>
    /// Game page.
    /// </summary>
    public partial class Game : Page
    {
        public static Game instance;
        public GameManager gameManager;

        public Game()
        {
            instance = this;
            this.InitializeComponent();
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            gameManager = new GameManager();
            gameManager.InitializeGame(Tela);
        }
        /*
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter is PlayerParams)
            {
                var parames = (PlayerParams)e.Parameter;
                PlayerCreated = new Player(parames.idPlayer);
            }
        }*/

    }
}