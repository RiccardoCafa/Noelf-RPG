using RPG_Noelf.Assets.Scripts;
using RPG_Noelf.Assets.Scripts.Ents.Mobs;
using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using RPG_Noelf.Assets.Scripts.Enviroment;
using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Mobs;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using RPG_Noelf.Assets.Scripts.Scenes;
using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.IO;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPG_Noelf
{
    /// <summary>
    /// Game page.
    /// </summary>
    public partial class Game : Page
    {
        public static Game instance;

        private Thread Start;
        private Player PlayerCreated;
        

        public TextBlock mobStatus;
        public TextBlock dayText;
        public TextBlock ChestName;
        public string MobText;

        public List<Image> PlayerImagesTest;
        public Dictionary<string, Image> MobImages;
        public Dictionary<string, Image> PlayerImages;
        public Dictionary<string, Image> ClothesImages;

        public static Canvas Telona;
        public static Canvas ActualChunck;
        public static Canvas inventarioWindow;
        public static Canvas TheScene;
        public static Canvas _InventarioCanvas;
        public static Grid _InventarioGrid;
        public static Grid SkillGrid;
        public static Grid ChestGrid;
        public LevelScene scene1;

        public static TextBlock texticulus;
        public static int i;

        public string test;


        public Game()
        {
            instance = this;
            this.InitializeComponent();

            Telona = Tela;
            //dayText = DayText;
            TheScene = xScene;
            Application.Current.DebugSettings.EnableFrameRateCounter = true;
            Window.Current.CoreWindow.KeyUp += WindowControl;
            Start = new Thread(start);
            Start.Start();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter is PlayerParams)
            {
                var parames = (PlayerParams)e.Parameter;
                PlayerCreated = new Player(parames.idPlayer);
            }
        }
        public Canvas bagWindow;
        public async void start()
        {
            _str = _spd = _dex = _con = _mnd = 0;
            
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                bagWindow = BagWindow;
                //mobStatus = xMobStatus;
                Window.Current.CoreWindow.KeyDown += Skill_KeyDown;
                scene1 = new LevelScene(xScene);//criaçao do cenario

                ////CreatePlayer();
                //GameManager.InitializeGame();
                //CreateInventory(BagWindow);
                //CreateChestWindow(350, 250);
                //CreateMob();
                CreateCraftingWindow();
                CreateConversationLayout();
                CreateSkill(WindowTreeSkill);

                GameManager.player._Inventory.BagUpdated += UpdateBagEvent;
                GameManager.player.Equipamento.EquipUpdated += UpdateEquipEvent;
                GameManager.player.Equipamento.EquipUpdated += UpdatePlayerInfoEvent;
                GameManager.player.PlayerUpdated += UpdatePlayerInfoEvent;
                GameManager.player._Questmanager.QuestUpdated += UpdateQuestManagerEvent;
                Conversation.PointerPressed += EndConversation;

                UpdateBag();
                UpdateSkillTree();
                UpdatePlayerInfo();
                UpdateSkillBar();
                UpdateShopInfo();

                SetEventForSkillBar();
                SetEventForSkillTree();
                SetEventForShopItem();
                SetEventForEquip();

                ShopWindow.Visibility = Visibility.Collapsed;
                _InventarioCanvas.Visibility = Visibility.Collapsed;
                Atributos.Visibility = Visibility.Collapsed;
                WindowEquipamento.Visibility = Visibility.Collapsed;
                WindowTreeSkill.Visibility = Visibility.Collapsed;


                GameManager.player._Questmanager.ReceiveNewQuest(QuestList.allquests[1]);
                GameManager.player._Questmanager.ReceiveNewQuest(QuestList.allquests[2]);
                GameManager.player._Questmanager.actualQuest = GameManager.player._Questmanager.allQuests[1];
                

                //CreateChest(100, 600, new Bau(Category.Legendary, 15));
                //CreateChest(350, 600, new Bau(Category.Normal, 10));
            });

        }

        private void WindowControl(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case Windows.System.VirtualKey.B:
                    if (_InventarioCanvas.Visibility == Visibility.Collapsed)
                    {
                        UpdateBag();
                        _InventarioCanvas.Visibility = Visibility.Visible;
                    }
                    else _InventarioCanvas.Visibility = Visibility.Collapsed;
                    break;
                case Windows.System.VirtualKey.P:
                    if (Atributos.Visibility == Visibility.Collapsed)
                    {
                        UpdatePlayerInfo();
                        Atributos.Visibility = Visibility.Visible;
                    }
                    else Atributos.Visibility = Visibility.Collapsed;
                    break;
                case Windows.System.VirtualKey.K:
                    if (WindowTreeSkill.Visibility == Visibility.Collapsed)
                    {
                        UpdateSkillTree();
                        WindowTreeSkill.Visibility = Visibility.Visible;
                    }
                    else WindowTreeSkill.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public static void SetImageSource(Image img, string path)
        {
            img.Source = new BitmapImage(new Uri(instance.BaseUri, path));
        }

        #region Interface Update and Events
        
        
        #region Inventario
        
        #endregion
        
        #region Conversation
        private Grid ButtonsGrid;
        //ObjectPooling<Button> ButtonPool = new ObjectPooling<Button>();
        private Button QuesterBtn;
        private Button TraderBtn;
        private Button ExitBtn;
        private Dictionary<string, Button> ListButtons = new Dictionary<string, Button>();
        private NPC lastNPC = null;
        private int Buttons = 3;

        public void CreateConversationLayout()
        {
            ButtonsGrid = new Grid();
            ButtonsGrid.Width = Conversation.Width;
            ButtonsGrid.Height = Conversation.Height / 2;
            Conversation.Children.Add(ButtonsGrid);
            ButtonsGrid.SetValue(Canvas.LeftProperty, ButtonsGrid.Height / 2);
            ColumnDefinition column = new ColumnDefinition()
            {
                Width = new GridLength(ButtonsGrid.Width)
            };
            ButtonsGrid.ColumnDefinitions.Add(column);

            for (int i = 0; i < Buttons; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(ButtonsGrid.Height / Buttons)
                };
                ButtonsGrid.RowDefinitions.Add(row);
            }

            QuesterBtn = new Button()
            {
                Height = (ButtonsGrid.Height / Buttons) - 10,
                Width = ButtonsGrid.Height,
                Content = "Quest"
            };
            ButtonsGrid.Children.Add(QuesterBtn);

            TraderBtn = new Button()
            {
                Height = (ButtonsGrid.Height / Buttons) - 10,
                Width = ButtonsGrid.Height,
                Content = "Trader"
            };
            ButtonsGrid.Children.Add(TraderBtn);

            ExitBtn = new Button()
            {
                Height = (ButtonsGrid.Height / Buttons) - 10,
                Width = ButtonsGrid.Height,
                Content = "Exit"
            };
            ExitBtn.Click += HasToCloseConv;
            ButtonsGrid.Children.Add(ExitBtn);

            QuesterBtn.SetValue(Grid.RowProperty, 0);
            TraderBtn.SetValue(Grid.RowProperty, 1);
            ExitBtn.SetValue(Grid.RowProperty, 2);
            
            ListButtons.Add("Quester", QuesterBtn);
            ListButtons.Add("Trader", TraderBtn);
            ListButtons.Add("Exit", ExitBtn);
        }

        public void CallConversationBox(NPC npc)
        {
            if (GameManager.interfaceManager.Conversation) return;
            GameManager.npcTarget = npc;
            Conversation.Visibility = Visibility.Visible;
            List<string> funcString = npc.GetFunctionsString();
            
            for (int i = 0; i < Buttons; i++)
            {
                string f = ListButtons.Keys.ElementAt(i);
                if (f == "Exit")
                {
                    ListButtons[f].Visibility = Visibility.Visible;
                    break;
                }
                if (lastNPC != null && lastNPC.GetFunction(f) != null)
                {
                    ListButtons[f].Click -= lastNPC.GetFunction(f).MyFunction;
                }
                if(npc.GetFunction(f) != null)
                {
                    ListButtons[f].Click += npc.GetFunction(f).MyFunction;
                    ListButtons[f].Visibility = Visibility.Visible;
                }
            }
            ConvText.Text = npc.Introduction;
            ConvLevel.Text = npc.MyLevel.actuallevel.ToString();
            string convfunc = "";
            foreach (string s in npc.GetFunctionsString())
            {
                convfunc += s + "/";
            }
            ConvFuncs.Text = convfunc;
            ConvName.Text = npc.Name;
            lastNPC = npc;
        }
        public void HasToCloseConv(object sender, RoutedEventArgs e)
        {
            if (GameManager.interfaceManager.ConvHasToClose != false) return;
            
            foreach (Button b in ListButtons.Values)
            {
                b.Visibility = Visibility.Collapsed;
            }
            GameManager.npcTarget = null;
            GameManager.interfaceManager.ConvHasToClose = true;
        }
        public void EndConversation(object sender, RoutedEventArgs e)
        {
            if (GameManager.interfaceManager.ConvHasToClose)
            {
                GameManager.interfaceManager.Conversation = false;
                Conversation.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
        #region General
        
        private void Skill_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            int indicadorzao = -1;
            if (e.VirtualKey == Windows.System.VirtualKey.Number1)
            {
                if (GameManager.player._SkillManager.SkillList.Count >= 1)
                {
                    indicadorzao = 0;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number2)
            {
                if (GameManager.player._SkillManager.SkillList.Count >= 2)
                {
                    indicadorzao = 1;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number3)
            {
                if (GameManager.player._SkillManager.SkillList.Count >= 3)
                {
                    indicadorzao = 2;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number4)
            {
                if (GameManager.player._SkillManager.SkillList.Count >= 4)
                {
                    indicadorzao = 3;
                }
            }
            if(indicadorzao != -1) GameManager.player._SkillManager.BeAbleSkill(indicadorzao);

        }
        private void MenuSemiOpenEnter(object sender, PointerRoutedEventArgs e)
        {
            MenuFBolaAtras.Visibility = Visibility.Visible;
        }
        private void MenuSemiOpenExit(object sender, PointerRoutedEventArgs e)
        {
            MenuFBolaAtras.Visibility = Visibility.Collapsed;
        }
        private void MenuOpen(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(this).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    MenuAberto.Visibility = Visibility.Visible;
                    MenuFechado.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void MenuClose(object sender, PointerRoutedEventArgs e)
        {
            MenuAberto.Visibility = Visibility.Collapsed;
            MenuFechado.Visibility = Visibility.Visible;
        }
        
        #endregion

        #region ButtonEvents
        private void ItemBuyingQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
            {
                uint MaxValue;
                if (!Switch)
                {
                    MaxValue = GameManager.player._Inventory.GetSlot(GameManager.traderTarget.shop.SlotInOffer.ItemID).ItemAmount;
                }
                else MaxValue = GameManager.traderTarget.shop.TradingItems.GetSlot(GameManager.traderTarget.shop.SlotInOffer.ItemID).ItemAmount;

                if (val <= 0)
                {
                    val = 1;
                }
                else if (val >= MaxValue)
                {
                    val = MaxValue;
                }
                ItemBuyingQuantity.Text = val.ToString();
            }
        }

        #region ButtonEvents
        private void ClickAcceptQuestButton(object sender, RoutedEventArgs e)
        {
            Quester npcF = GameManager.npcTarget.GetFunction("Quester") as Quester;
            Quest generic = npcF.myQuest;
            QuestBox1.Text = generic.name;
            QuestBox2.Text = generic.Description;
            QuestWindow.Visibility = Visibility.Collapsed;
        }
        private void ClickDenyQuestButton(object sender, RoutedEventArgs e)
        {
            ActiveQuestsWindows.Visibility = Visibility.Collapsed;
            QuestWindow.Visibility = Visibility.Collapsed;
        }
        

    
    private void ClickNewMob(object sender, RoutedEventArgs e)//recria o mob aleatoriamente (temporario)
    {
        //int level;
        //int.TryParse(xLevelBox.Text, out level);
        //GameManager.mobTarget.Mob = new Mob(level);
        //GameManager.mobTarget.Mob.Status(xMobStatus);
    }

    private void OfferItemButton(object sender, RoutedEventArgs e)
    {
        if (GameManager.traderTarget == null) return;
        if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
        {
            if (Switch == false)
            {

                if (val <= Bag.MaxStack)
                {
                    if (GameManager.player._Inventory.RemoveFromBag(GameManager.traderTarget.shop.SlotInOffer.ItemID, val))
                    {
                        Slot newSlot = new Slot(GameManager.traderTarget.shop.SlotInOffer.ItemID, val);
                        GameManager.traderTarget.shop.AddToBuyingItems(newSlot);
                        GameManager.traderTarget.shop.SlotInOffer = null;
                        UpdateShopInfo();
                        CloseOfferItem();
                    }
                }
            }
            else
            {
                if (val <= Bag.MaxStack)
                {
                    Slot newSlot = new Slot(GameManager.traderTarget.shop.SlotInOffer.ItemID, val);
                    GameManager.traderTarget.shop.SellItem(newSlot, GameManager.player._Inventory);
                    CloseOfferItem();
                }
            }

        }
    }

    private void IncrementOfferAmount(object sender, RoutedEventArgs e)
    {
        if (GameManager.traderTarget.shop.SlotInOffer == null) return;
        if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
        {
            uint MaxValue = GameManager.player._Inventory.GetSlot(GameManager.traderTarget.shop.SlotInOffer.ItemID).ItemAmount;
            val++;
            if (val >= MaxValue)
            {
                val = MaxValue;
            }
            ItemBuyingQuantity.Text = val.ToString();
        }
    }

    private void DecrementOfferAmount(object sender, RoutedEventArgs e)
    {
        if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
        {
            val--;
            if (val <= 0)
            {
                val = 1;
            }
            ItemBuyingQuantity.Text = val.ToString();
        }
    }

    private void SellButton(object sender, RoutedEventArgs e)
    {
        if (Switch == false)
        {
            GameManager.traderTarget.shop.BuyItem(GameManager.player._Inventory);
            UpdateShopInfo();
            UpdatePlayerInfo();
        }
    }

    private void CancelSellingButton(object sender, RoutedEventArgs e)
    {
        CloseOfferItem();
    }

    private void GeralSumStat()
    {
        GameManager.player._Class.StatsPoints--;
        UpdatePlayerInfo();
    }

    private void GeralSubStat()
    {
        GameManager.player._Class.StatsPoints++;
        UpdatePlayerInfo();
    }

    private void XPPlus(object sender, RoutedEventArgs e)
    {
        //GameManager.player.;
        GameManager.player.LevelUpdate(0, 0, 0, 0, 0, 100);
    }

    private void MPPlus(object sender, RoutedEventArgs e)
    {
        GameManager.player.AddMP(20);
    }

    private void HPPlus(object sender, RoutedEventArgs e)
    {
        GameManager.player.AddHP(20);
    }

    private void PSTR(object sender, RoutedEventArgs e)
    {
        if (GameManager.player._Class.StatsPoints > 0)
        {
            _str++;
            GeralSumStat();
        }
    }

    private void PMND(object sender, RoutedEventArgs e)
    {
        if (GameManager.player._Class.StatsPoints > 0)
        {
            _mnd++;
            GeralSumStat();
        }
    }

    private void PSPD(object sender, RoutedEventArgs e)
    {
        if (GameManager.player._Class.StatsPoints > 0)
        {
            _spd++;
            GeralSumStat();
        }
    }

    private void PDEX(object sender, RoutedEventArgs e)
    {
        if (GameManager.player._Class.StatsPoints > 0)
        {
            _dex++;
            GeralSumStat();
        }
    }

    private void PCON(object sender, RoutedEventArgs e)
    {
        if (GameManager.player._Class.StatsPoints > 0)
        {
            _con++;
            GeralSumStat();
        }
    }

    private void MSTR(object sender, RoutedEventArgs e)
    {
        if (_str > 0)
        {
            _str--;
            GeralSubStat();
        }
    }

    private void MDEX(object sender, RoutedEventArgs e)
    {
        if (_dex > 0)
        {
            _dex--;
            GeralSubStat();
        }
    }

    private void MSPD(object sender, RoutedEventArgs e)
    {
        if (_spd > 0)
        {
            _spd--;
            GeralSubStat();
        }
    }

    private void MCON(object sender, RoutedEventArgs e)
    {
        if (_con > 0)
        {
            _con--;
            GeralSubStat();
        }
    }

    private void MMND(object sender, RoutedEventArgs e)
    {
        if (_mnd > 0)
        {
            _mnd--;
            GeralSubStat();
        }
    }

    private void TrocaButton(object sender, RoutedEventArgs e)
    {
        Switch = !Switch;
        Buy.Content = Switch == true ? "Buy" : "Sell";
        UpdateShopInfo();
    }

    private void ApplyStats(object sender, RoutedEventArgs e)
    {
        GameManager.player.LevelUpdate(_str, _spd, _dex, _con, _mnd, 50);
        _str = _spd = _dex = _con = _mnd = 0;
    }
    #endregion

    #endregion

    #endregion
}
}