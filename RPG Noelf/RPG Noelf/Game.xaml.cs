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
        public static Grid ChestGrid;
        public LevelScene scene1;

        public static TextBlock texticulus;
        public static int i;

        public string test;

        public bool Switch = false;
        public bool shopOpen = false;
        public bool equipOpen = false;

        private int _str, _spd, _dex, _con, _mnd;
        private const int LootWidth = 50;
        private const int LootHeight = 50;

        public Game()
        {
            instance = this;
            this.InitializeComponent();

            Telona = Tela;
            dayText = DayText;
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

        public async void start()
        {
            _str = _spd = _dex = _con = _mnd = 0;


            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //mobStatus = xMobStatus;
                Window.Current.CoreWindow.KeyDown += Skill_KeyDown;
                scene1 = new LevelScene(xScene);//criaçao do cenario

                CreatePlayer();
                GameManager.InitializeGame();
                CreateInventory(BagWindow);
                CreateChestWindow(350, 250);
                CreateMob();
                CreateCraftingWindow();

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
                SetEventForBagItem();
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

                Slot slotDrop = new Slot(40, 1);
                CreateDrop(100, 500, slotDrop);

                Bau bauchicabaubau = new Bau(Category.Legendary, 15);
                CreateChest(200, 220, bauchicabaubau);

                Bau bauchicabaubau2 = new Bau(Category.Normal, 10);
                CreateChest(300, 220, bauchicabaubau2);
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
        #region Interface Elements Creation
        public void CreateMob()
        {
            GameManager.mob = new Mob(level: 2);
            GameManager.mob.Spawn(30, 30);
            TheScene.Children.Add(GameManager.mob.box);
        }
        public void CreatePlayer()
        {
            if (PlayerCreated != null)
            {
                GameManager.player = new Player(PlayerCreated.Id);
            }
            else
            {
                GameManager.player = new Player("0000000");
            }
            GameManager.player.Spawn(300, 30);
            TheScene.Children.Add(GameManager.player.box);
            //GameManager.player.box.Background = new SolidColorBrush(Color.FromArgb(155, 255, 0, 127));
        }
        public void CreateCraftingWindow()
        {
            CraftBox craftb = new CraftBox(24);
            CraftBox crafta = new CraftBox(42);
            
            CraftPanel.Children.Add(craftb);
            CraftPanel.Children.Add(crafta);
        }
        public void CreateChest(double x, double y, Bau bau) 
        {
            ChestBody chest = new ChestBody(x, y, bau)
            {
                Width = 85,
                Height = 85
            };
            Chunck01.Children.Add(chest);
            Image chestImage = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/Interactable/chest-rpg-normal.png")),
                Width = 85,
                Height = 85
            };
            chest.Children.Add(chestImage);
            chest.ChestOpen += ChestOpen;
            chest.PointerPressed += ShowChest;
            bau.itens.BagUpdated += UpdateChestEvent;
        }
        public Canvas CreateCharacterNPC()
        {
            return NPCCanvas;
        }
        public void CreateDrop(double x, double y, Slot dropSlot)
        {
            string pathImage = Encyclopedia.SearchFor(dropSlot.ItemID).PathImage;
            LootBody drop = new LootBody(dropSlot)
            {
                Width = LootWidth,
                Height = LootHeight
            };
            Chunck01.Children.Add(drop);
            Canvas.SetTop(drop, y);
            Canvas.SetLeft(drop, x);
            Image dropImage = new Image();
            dropImage.Source = new BitmapImage(new Uri(this.BaseUri, pathImage));
            drop.Children.Add(dropImage);
            //LootBody loot = new LootBody(drop);
            //loot.UpdateBlocks(TheScene);
            //Trigger dropTrigger = new Trigger(loot);
        }
        public void CreateSkill(SkillGenerics skill)
        {
            WindowTreeSkill.Width = 250;
            WindowTreeSkill.Height = 150;
            WindowTreeSkill.HorizontalAlignment = HorizontalAlignment.Stretch;
            WindowTreeSkill.VerticalAlignment = VerticalAlignment.Stretch;
            Canvas.SetTop(WindowTreeSkill, 40);
            Canvas.SetLeft(WindowTreeSkill, 120);
            TextBlock txt = new TextBlock()
            {

            }
            Image bg = new Image()
            {
                Width = 40,
                Height = 30
            };
            WindowTreeSkill.Children.Add(bg);

        }
        public void CreateInventory(Canvas BagCanvas)
        {
            // Width="180" Height="20" Text="Bag" Canvas.Top="-20" TextAlignment="Center"
            // Height="150" Canvas.Left="800" Canvas.Top="40" Width="180"
            BagCanvas.Width = 180;
            BagCanvas.Height = 150;
            Canvas.SetLeft(BagCanvas, 800);
            Canvas.SetTop(BagCanvas, 40);
            _InventarioCanvas = BagCanvas;
            TextBlock text = new TextBlock()
            {
                Width = 180,
                Height = 20,
                Text = "Bag",
                HorizontalTextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(text, -20);
            _InventarioCanvas.Children.Add(text);
            Image bg = new Image()
            {
                //Source="/Assets/Images/UI Elements/FundoInventario.png" Width="180" Height="150"
                Width = 180,
                Height = 150,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/FundoInventario.png"))
            };
            _InventarioCanvas.Children.Add(bg);
            _InventarioGrid = new Grid()
            {
                Width = 180,
                Height = 150
            };
            _InventarioGrid.SetValue(Grid.PaddingProperty, new Thickness(2.5, 2.5, 2.5, 2.5));
            _InventarioCanvas.Children.Add(_InventarioGrid);

            DefinitionsGrid(_InventarioGrid, 6, 5, 30, 30);
            FillGridItemImage(_InventarioGrid, GameManager.player._Inventory, 6, 5, 25, 25);

        }
        public void CreateChestWindow(double x, double y)
        {
            //Configurando o ChestWindow
            ChestWindow.Width = 250;
            ChestWindow.Height = 150;
            Canvas.SetTop(ChestWindow, x);
            Canvas.SetLeft(ChestWindow, y);
            ChestWindow.Visibility = Visibility.Collapsed;

            // Criando o texto e a imagem de fundo
            ChestName = new TextBlock()
            {
                Text = "Normal Chest",
                Width = 250,
                Height = 20,
                TextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(ChestName, -20);
            ChestWindow.Children.Add(ChestName);

            Image background = new Image()
            {
                Width = 250,
                Height = 150,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/UIAtivo 23-0.png"))
            };
            ChestWindow.Children.Add(background);

            //Criando o grid com os ItemImage
            ChestGrid = new Grid()
            {
                Width = 250,
                Height = 150,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Padding = new Thickness(5, 5, 5, 5)
            };
            ChestWindow.Children.Add(ChestGrid);

            DefinitionsGrid(ChestGrid, 5, 3, 50, 50);
            FillGridItemImage(ChestGrid, null, 5, 3, 40, 40);

            Button GetAllBtn = new Button()
            {
                Width = 90,
                Height = 60,
                Content = "All"
            };
            ChestWindow.Children.Add(GetAllBtn);
            Canvas.SetTop(GetAllBtn, 0);
            Canvas.SetLeft(GetAllBtn, ChestWindow.Width);
            GetAllBtn.Click += (object sender, RoutedEventArgs e) =>
            {
                Bag bag = ((ItemImage)ChestGrid.Children[0]).myBagRef;
                List<Slot> ItemSlots = new List<Slot>();
                foreach(ItemImage item in ChestGrid.Children)
                {
                    ItemSlots.Add(item.Slot);
                }
                foreach(Slot s in ItemSlots)
                {
                    if (GameManager.player._Inventory.AddToBag(s))
                    {
                        bool c = bag.RemoveFromBag(s.ItemID, s.ItemAmount);
                    }
                }
            };

            Button Close = new Button()
            {
                Width = 90,
                Height = 60,
                Content = "Close"
            };
            ChestWindow.Children.Add(Close);
            Canvas.SetTop(Close, GetAllBtn.Height + 10);
            Canvas.SetLeft(Close, ChestWindow.Width);
            Close.Click += (object sender, RoutedEventArgs e) =>
            {
                ChestWindow.Visibility = Visibility.Collapsed;
            };

        }
        public void UpdateGridBagItemImages(Grid grid, Bag bag)
        {
            foreach(ItemImage item in grid.Children)
            {
                item.myBagRef = bag;
                item.itemOwner = EItemOwner.drop;
                item.SetEvents();
            }
        }
        public void DefinitionsGrid(Grid grid, int columnNumber, int rowNumber, int widthCell, int heightCell)
        {
            int r = 0;
            for (int i = 0; i < columnNumber || r < rowNumber; i++, r++)
            {
                if(i < columnNumber)
                {
                    ColumnDefinition coldef = new ColumnDefinition() { Width = new GridLength(widthCell) };
                    grid.ColumnDefinitions.Add(coldef);
                }
                if (r < rowNumber)
                {
                    RowDefinition rowdef = new RowDefinition() { Height = new GridLength(heightCell) };
                    grid.RowDefinitions.Add(rowdef);
                }
            }
        }
        public void FillGridItemImage(Grid grid, Bag gridBag, int columnSize, int rowSize, int widthImage, int heightImage)
        {
            int column = -1, row = 0;
            for (int i = 0; i < columnSize * rowSize; i++)
            {
                ItemImage item;
                column++;
                item = gridBag != null ? new ItemImage(i, widthImage, heightImage, gridBag) : new ItemImage(i, widthImage, heightImage);
                grid.Children.Add(item);
                Grid.SetColumn(item, column);
                Grid.SetRow(item, row);
                if(item.myBagRef != null && item.myBagRef.GetSlot(i) != null)
                    Debug.WriteLine(Encyclopedia.SearchFor(item.myBagRef.GetSlot(i).ItemID).Name 
                                    + " col " + column + " row " + row + "\n");
                if (column == columnSize - 1)
                {
                    row++;
                    column = -1;
                }
            }
        }
        #endregion
        #region Player Updates
        public void UpdatePlayerInfo()
        {
            PlayerInfo.Text = GameManager.player.Race.NameRace + " " + GameManager.player._Class.ClassName + "\n";
            PlayerInfo.Text += "Atributos: ( " + GameManager.player._Class.StatsPoints + " pontos)\n" +
                                "Força: " + GameManager.player.Str + " + " + _str + "\n" +
                                "Mente: " + GameManager.player.Mnd + " + " + _mnd + "\n" +
                                "Velocidade: " + GameManager.player.Spd + " + " + _spd + "\n" +
                                "Destreza: " + GameManager.player.Dex + " + " + _dex + "\n" +
                                "Constituição: " + GameManager.player.Con + " + " + _con + "\n\n" +
                                "HP: " + GameManager.player.Hp + "/" + GameManager.player.HpMax + "\n" +
                                "MP: " + GameManager.player.Mp + "/" + GameManager.player.MpMax + "\n" +
                                "Damage: " + GameManager.player.Damage + "\n" +
                                "Atack Speed: " + GameManager.player.AtkSpd + "\n" +
                                "Armor: " + GameManager.player.Armor + "\n\n" +
                                "Level: " + GameManager.player.level.actuallevel + "\n" +
                                "Experience: " + GameManager.player.Xp + "/" + GameManager.player.XpLim + "\n" +
                                "Pontos de skill disponivel: " + GameManager.player._SkillManager.SkillPoints + "\n" +
                                "Gold: " + GameManager.player._Inventory.Gold;
        }
        public void UpdateSkillTree()
        {
            int cont = 0;
            foreach (UIElement element in SkillsTree.Children)
            {
                Image img = element as Image;
                if (cont < GameManager.player._SkillManager.SkillList.Count)
                    img.Source = new BitmapImage(new Uri(this.BaseUri, GameManager.player._SkillManager.SkillList.ElementAt(cont).pathImage));
                else break;
                cont++;
            }
        }
        public void UpdateSkillBar()
        {
            int cont = 0;
            foreach (UIElement element in BarraSkill.Children)
            {
                if (cont == 0)
                {
                    (element as Image).Source = new BitmapImage(new Uri(this.BaseUri, GameManager.player._SkillManager.Passive.pathImage));
                }
                else
                {
                    if (GameManager.player._SkillManager.SkillBar[cont - 1] != null)
                        (element as Image).Source = new BitmapImage(new Uri(this.BaseUri, GameManager.player._SkillManager.SkillBar[cont - 1].pathImage));
                    else
                        (element as Image).Source = new BitmapImage();
                }
                cont++;
            }
        }
        private void UpdatePlayerInfoEvent(object sender, EventArgs e)
        {
            UpdatePlayerInfo();
        }
        private void UpdateSkillWindowText(SkillGenerics skillInfo)
        {
            try
            {
                W_SkillImage.Source = new BitmapImage(new Uri(this.BaseUri, skillInfo.pathImage));
                W_SkillName.Text = skillInfo.name;
                W_SkillType.Text = skillInfo.GetTypeString();
                W_SkillDescr.Text = skillInfo.description;
                if (skillInfo.Unlocked == false)
                {
                    W_SkillLevel.Text = "Unlock Lv. " + skillInfo.block;
                }
                else
                {
                    W_SkillLevel.Text = "Lv. " + skillInfo.Lvl.ToString();
                }
            }
            catch (NullReferenceException e)
            {
                WindowSkill.Visibility = Visibility.Collapsed;
                return;
            }

        }
        private void UpdateEquip()
        {
            int count = 0;
            string pathImage;
            foreach (UIElement element in EquipWindow.Children)
            {
                Image img = element as Image;
                if ((int)img.GetValue(Grid.ColumnProperty) == 0)
                {
                    if (GameManager.player.Equipamento.armor[count] == null)
                    {
                        img.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Imagens/Chao.jpg"));
                    }
                    else
                    {
                        pathImage = GameManager.player.Equipamento.armor[count].PathImage;
                        img.Source = new BitmapImage(new Uri(this.BaseUri, pathImage));
                    }
                }
                else
                {
                    if (GameManager.player.Equipamento.weapon == null)
                    {
                        img.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Imagens/Chao.jpg"));
                    }
                    else
                    {
                        pathImage = GameManager.player.Equipamento.weapon.PathImage;
                        img.Source = new BitmapImage(new Uri(this.BaseUri, pathImage));
                    }
                }
                count++;
            }
        }
        private void UpdateEquipEvent(object sender, EventArgs e)
        {
            UpdateEquip();
            UpdatePlayerInfo();
        }

        private void SetEventForSkillBar()
        {
            foreach (UIElement element in BarraSkill.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowSkillBarWindow;
                    element.PointerExited += CloseSkillWindow;
                    element.PointerPressed += RemoveSkillFromBar;
                }
            }
        }
        private void SetEventForSkillTree()
        {
            foreach (UIElement element in SkillsTree.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowSkillTreeWindow;
                    element.PointerExited += CloseSkillWindow;
                    element.PointerPressed += SkillTreePointerEvent;
                }
            }
        }
        private void SetEventForEquip()
        {
            foreach (UIElement element in EquipWindow.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowEquipWindow;
                    element.PointerExited += CloseItemWindow;
                    element.PointerPressed += DesequiparEvent;
                }
            }
        }

        public void InventorySlotEvent(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(this).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    Slot s = GameManager.player._Inventory.GetSlot((sender as ItemImage).myItemPosition);
                    if (s == null) return;
                    if (shopOpen)
                    {
                        GameManager.traderTarget.shop.SlotInOffer = s;
                        ShowOfferItem(s);
                        UpdateShopInfo();
                    }
                    else if (equipOpen)
                    {
                        Item i = Encyclopedia.encyclopedia[s.ItemID];
                        if (i is Armor || i is Weapon)
                        {
                            GameManager.player.Equipamento.UseEquip(s.ItemID);
                            WindowBag.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        GameManager.player._Inventory.RemoveFromBag(s.ItemID, s.ItemAmount);
                        CreateDrop(GameManager.player.box.Xi + 10,
                                    GameManager.player.box.Yi + 60,
                                    s);
                    }
                }
            }
        }
        public void ItemSlotEventDrop(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(this).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    Slot s = (sender as ItemImage).Slot;
                    if (GameManager.player._Inventory.AddToBag(s))
                    {
                        bool c = (sender as ItemImage).myBagRef.RemoveFromBag(s.ItemID, s.ItemAmount);
                    }
                }
            }
        }
        private void DesequiparEvent(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(this).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    int index;
                    int column, row;
                    column = (int)(sender as Image).GetValue(Grid.ColumnProperty);
                    row = (int)(sender as Image).GetValue(Grid.RowProperty);
                    index = column * row + column;
                    uint s = 0;
                    if (column == 0)
                    {
                        s = Encyclopedia.SearchFor(GameManager.player.Equipamento.armor[row]);
                    }
                    else
                    {
                        s = Encyclopedia.SearchFor(GameManager.player.Equipamento.weapon);
                    }
                    if (s == 0) return;
                    GameManager.player.Equipamento.DesEquip(s);
                }
            }
        }
        private void RemoveSkillFromBar(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(this).Properties;
                if (prop.IsRightButtonPressed)
                {
                    Image skillEnter = sender as Image;

                    int columnPosition = (int)skillEnter.GetValue(Grid.ColumnProperty);
                    int rowPosition = (int)skillEnter.GetValue(Grid.RowProperty);
                    int position = _InventarioGrid.ColumnDefinitions.Count * rowPosition + columnPosition;
                    GameManager.player._SkillManager.SkillBar[position - 1] = null;
                    UpdateSkillWindowText(null);
                    UpdateSkillBar();
                }
            }
        }
        private void SkillTreePointerEvent(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(this).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    Image skillEnter = sender as Image;
                    SkillGenerics skillClicked;

                    int columnPosition = (int)skillEnter.GetValue(Grid.ColumnProperty);
                    int rowPosition = (int)skillEnter.GetValue(Grid.RowProperty);
                    int position = _InventarioGrid.ColumnDefinitions.Count * rowPosition + columnPosition;
                    skillClicked = GameManager.player._SkillManager.SkillList[position];
                    if (GameManager.player._SkillManager.UpSkill(skillClicked))
                    {
                        UpdateSkillWindowText(skillClicked);
                        UpdatePlayerInfo();
                        UpdateSkillBar();
                    }
                }
                else if (prop.IsRightButtonPressed)
                {
                    Image skillEnter = sender as Image;
                    SkillGenerics skillClicked;

                    int columnPosition = (int)skillEnter.GetValue(Grid.ColumnProperty);
                    int rowPosition = (int)skillEnter.GetValue(Grid.RowProperty);
                    int position = _InventarioGrid.ColumnDefinitions.Count * rowPosition + columnPosition;

                    skillClicked = GameManager.player._SkillManager.SkillList[position];
                    if (skillClicked.Unlocked == false) return;
                    GameManager.player._SkillManager.ChangeSkill(skillClicked);
                    UpdateSkillBar();
                }
            }
        }

        private void ShowSkillBarWindow(object sender, PointerRoutedEventArgs e)
        {
            if (WindowSkill.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;

            Image skillEnter = null;
            try
            {
                skillEnter = sender as Image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }

            if (skillEnter == null) return;
            int position = (int)skillEnter.GetValue(Grid.ColumnProperty);
            SkillGenerics skillInfo;

            if (position == 0)
            {
                skillInfo = GameManager.player._SkillManager.Passive;
            }
            else
            {
                skillInfo = GameManager.player._SkillManager.SkillBar[position - 1];
            }

            if (skillInfo == null) return;

            RealocateWindow(WindowSkill, mousePosition);

            UpdateSkillWindowText(skillInfo);
        }
        private void ShowSkillTreeWindow(object sender, PointerRoutedEventArgs e)
        {
            if (WindowSkill.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;

            Image skillEnter = null;
            try
            {
                skillEnter = sender as Image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }
        }
        private void ShowAtributes(object sender, PointerRoutedEventArgs e)
        {
            if (Atributos.Visibility == Visibility.Visible)
                Atributos.Visibility = Visibility.Collapsed;
            else Atributos.Visibility = Visibility.Visible;
        }
        private void ShowCrafting(object sender, PointerRoutedEventArgs e)
        {
            CraftingCenter.Visibility = CraftingCenter.Visibility == Visibility.Collapsed ? 
                                        Visibility.Visible : Visibility.Collapsed;
        }
        private void ShowSkillTree(object sender, PointerRoutedEventArgs e)
        {
            if (WindowTreeSkill.Visibility == Visibility.Collapsed)
            {
                WindowTreeSkill.Visibility = Visibility.Visible;
            }
            else
            {
                WindowTreeSkill.Visibility = Visibility.Collapsed;
            }
        }
        private void ShowEquipWindow(object sender, PointerRoutedEventArgs e)
        {
            if (WindowBag.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;

            Image itemEnter = null;
            try
            {
                itemEnter = sender as Image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }

            if (itemEnter == null) return;

            int columnPosition = (int)itemEnter.GetValue(Grid.ColumnProperty);
            int rowPosition = (int)itemEnter.GetValue(Grid.RowProperty);

            Slot itemInfo;

            if (columnPosition == 0)
            {
                if (GameManager.player.Equipamento.armor[rowPosition] == null) return;
                uint idinfo = Encyclopedia.SearchFor(GameManager.player.Equipamento.armor[rowPosition]);
                if (idinfo == 0) return;
                itemInfo = new Slot(idinfo, 1);
            }
            else
            {
                if (GameManager.player.Equipamento.weapon == null) return;
                uint idinfo = Encyclopedia.SearchFor(GameManager.player.Equipamento.weapon);
                itemInfo = new Slot(idinfo, 1);
            }
            if (itemInfo.ItemID == 0) return;

            RealocateWindow(WindowBag, mousePosition);

            //UpdateItemWindowText(itemInfo);

        }
        private void ShowEquip(object sender, PointerRoutedEventArgs e)
        {
            if (WindowEquipamento.Visibility == Visibility.Collapsed)
            {
                WindowEquipamento.Visibility = Visibility.Visible;
                equipOpen = true;
            }
            else
            {
                WindowEquipamento.Visibility = Visibility.Collapsed;
                equipOpen = false;
            }
        }
        private void CloseSkillWindow(object sender, PointerRoutedEventArgs e)
        {
            WindowSkill.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region Inventario
        public void UpdateBag()
        {
            foreach(ItemImage itemImg in _InventarioGrid.Children)
            {
                itemImg.OnItemImageUpdate();
            }
        }
        public void UpdateBagEvent(object sender, BagEventArgs e)
        {
            UpdateBag();
        }
        public void UpdateItemWindowText(int slotPosition, Bag bag)
        {
            Slot slot = bag.GetSlot(slotPosition);
            if (slot == null) return;
            Item item = Encyclopedia.encyclopedia[slot.ItemID];
            W_ItemImage.Source = new BitmapImage(new Uri(this.BaseUri, item.PathImage));
            W_ItemName.Text = item.Name;
            W_ItemQntd.Text = slot.ItemAmount + "x";
            W_ItemRarity.Text = item.GetTypeString();
            //W_ItemType.Text = item.itemType;
            if (item.description != null) W_ItemDescr.Text = item.description;
            W_ItemValue.Text = item.GoldValue + " gold";
        }

        private void SetEventForBagItem()
        {
            /*foreach (UIElement element in _InventarioGrid.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowItemWindow;
                    element.PointerExited += CloseItemWindow;
                    element.PointerPressed += InventorySlotEvent;
                }
            }*/
        }

        public void ShowItemWindow(object sender, PointerRoutedEventArgs e)
        {
            if (WindowBag.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;
            ItemImage itemImg = (ItemImage)sender;
            RealocateWindow(WindowBag, mousePosition);
            itemImg.OnItemImageUpdate();
            UpdateItemWindowText(itemImg.myItemPosition, itemImg.myBagRef);

        }
        private void ShowBag(object sender, PointerRoutedEventArgs e)
        {
            if (_InventarioCanvas.Visibility == Visibility.Collapsed)
                _InventarioCanvas.Visibility = Visibility.Visible;
            else
                _InventarioCanvas.Visibility = Visibility.Collapsed;
        }
        public void CloseItemWindow(object sender, PointerRoutedEventArgs e)
        {
            WindowBag.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region Shop
        public void UpdateShopInfo()
        {
            if (GameManager.traderTarget == null) return;
            int count = 0;
            foreach (Image img in ShopGrid.Children)
            {
                if (Switch == false)
                {
                    if (count >= GameManager.traderTarget.shop.BuyingItems.Slots.Count) img.Source = new BitmapImage();
                    else
                    {
                        Slot s = GameManager.traderTarget.shop.BuyingItems.GetSlot(count);
                        img.Source = new BitmapImage(new Uri(this.BaseUri, Encyclopedia.SearchFor(s.ItemID).PathImage));
                    }
                }
                else
                {
                    if (count >= GameManager.traderTarget.shop.TradingItems.Slots.Count) img.Source = new BitmapImage();
                    else
                    {
                        Slot s = GameManager.traderTarget.shop.TradingItems.GetSlot(count);
                        img.Source = new BitmapImage(new Uri(this.BaseUri, Encyclopedia.SearchFor(s.ItemID).PathImage));
                    }
                }
                count++;
            }
        }

        public void OpenShop()
        {
            Atributos.Visibility = Visibility.Collapsed;
            ShopWindow.Visibility = Visibility.Visible;
            UpdateShopInfo();
        }
        public void CloseShop()
        {
            ShopWindow.Visibility = Visibility.Collapsed;
        }
        
        #region Quest
        public void CloseQuest()
        {
            QuestWindow.Visibility = Visibility.Collapsed;
        }
        public void OpenQuest()
        {
            if (GameManager.questerTarget != null)
            {
                GameManager.questerTarget.myQuest = QuestList.allquests[GameManager.questerTarget.GetQuestID()];
            }
            else
            {
                GameManager.questerTarget = new Quester(1);
            }
            QuestTitulo.Text = GameManager.questerTarget.myQuest.name;
            QuestDescription.Text = GameManager.questerTarget.myQuest.Description;
            QuestRewards.Text = GameManager.questerTarget.myQuest.RewardDescription;
            QuestWindow.Visibility = Visibility.Visible;
        }

        private void ShowQuest(object sender, PointerRoutedEventArgs e)
        {
            QuestManagerWindow.Visibility = QuestManagerWindow.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            UpdateActualQuestManager();
        }
        private void ShowQuestList(object sender, PointerRoutedEventArgs e)
        {
            RotateTransform ta = (RotateTransform)ArrowQuestList.RenderTransform;
            if(MQuestList.Visibility == Visibility.Collapsed)
            {
                MQuestList.Visibility = Visibility.Visible;
                ta.Angle = 90;
            }
            else
            {
                MQuestList.Visibility = Visibility.Collapsed;
                ta.Angle = -90;
            }
        }
        private void CloseQuestManagerWindow(object sender, PointerRoutedEventArgs e)
        {
            QuestManagerWindow.Visibility = Visibility.Collapsed;
        }
        private void UpdateActualQuestManager()
        {
            Quest q = GameManager.player._Questmanager.actualQuest;
            if (q == null)
            {
                QuestManagerWindow.Visibility = Visibility.Collapsed;
                return;
            }
            MQuestTitle.Text = q.name;
            MQuestDescr.Text = q.Description;
            MQuestDescr.Text += "\n" + q.RewardDescription;
            MQuestGold.Text = "x" + q.GainedGold;
            MQuestXP.Text = "x" + q.GainedXP;
            if(q.GainedItem != null)
            {
                MQuestItem.Source = new BitmapImage(new Uri("ms-appx://" + Encyclopedia.SearchFor(q.GainedItem.ItemID).PathImage));
                MQuestItemQntd.Text = q.GainedItem.ItemAmount.ToString();
            } else
            {
                MQuestItem.Source = new BitmapImage();
                MQuestItemQntd.Text = "";
            }
        }
        ObjectPooling<MissionListButton> MsnBtnPool = new ObjectPooling<MissionListButton>();
        private void UpdateQuestList()
        {
            ResetQuestList();

            List<Quest> quests = GameManager.player._Questmanager.allQuests;
            int count = 0;
            foreach(Quest q in quests)
            {
                MissionListButton msnB;
                if (MsnBtnPool.PoolSize > 0)
                {
                    MsnBtnPool.GetFromPool(out msnB);
                    msnB.Quest = q;
                    msnB.Titulo = q.name;
                    msnB.Visibility = Visibility.Visible;
                    MissionList.Children.Add(msnB);
                } else
                {
                    msnB = new MissionListButton(q);
                    MsnBtnPool.Pooled.Add(msnB);
                    MissionList.Children.Add(msnB);
                }
                count++;
            }
        }
        private void UpdateQuestManagerEvent(object sender, QuestEventArgs e)
        {
            UpdateActualQuestManager();
            UpdateQuestList();
        }
        private void ResetQuestList()
        {
            List<MissionListButton> pooled = MsnBtnPool.Pooled;
            foreach(MissionListButton msnB in pooled)
            {
                msnB.Visibility = Visibility.Collapsed;
            }
            MissionList.Children.Clear();
            MsnBtnPool.ReturnToPool();
        }
        private void GiveUpButton(object sender, RoutedEventArgs e) 
        {
            GameManager.player._Questmanager.GiveUpActualQuest();
        }
        #endregion

        private void SetEventForShopItem()
        {
            foreach (UIElement element in ShopGrid.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowItemBuying;
                    element.PointerExited += CloseItemWindow;
                    element.PointerPressed += ShopItemBuy;
                }
            }
        }

        private void ShowItemBuying(object sender, PointerRoutedEventArgs e)
        {
            if (WindowBag.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;

            Image itemEnter = null;
            try
            {
                itemEnter = sender as Image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }

            if (itemEnter == null) return;

            int columnPosition = (int)itemEnter.GetValue(Grid.ColumnProperty);
            int rowPosition = (int)itemEnter.GetValue(Grid.RowProperty);
            int position = ShopGrid.ColumnDefinitions.Count * rowPosition + columnPosition;

            Slot itemInfo = null;

            if (!Switch)
            {
                if (position < GameManager.traderTarget.shop.BuyingItems.Slots.Count)
                {
                    itemInfo = GameManager.traderTarget.shop.BuyingItems.Slots[position];
                }
                if (itemInfo == null) return;
            }
            else
            {
                if (position < GameManager.traderTarget.shop.TradingItems.Slots.Count)
                {
                    itemInfo = GameManager.traderTarget.shop.TradingItems.Slots[position];
                }
                if (itemInfo == null) return;
            }

            RealocateWindow(WindowBag, mousePosition);

            // TODO UpdateItemWindowText(itemInfo);

        }
        private void ShowOfferItem(Slot offerSlot)
        {
            if (offerSlot == null) return;
            ItemToSellBuy.Visibility = Visibility.Visible;
            Item item = Encyclopedia.SearchFor(offerSlot.ItemID);
            ItemBuyingImage.Source = new BitmapImage(new Uri(this.BaseUri, item.PathImage));
            ItemBuyingName.Text = item.Name;
            ItemBuyingQuantity.Text = offerSlot.ItemAmount.ToString();
            ItemBuyingValue.Text = (offerSlot.ItemAmount * item.GoldValue).ToString();
        }
        private void CloseOfferItem()
        {
            GameManager.traderTarget.shop.SlotInOffer = null;
            ItemToSellBuy.Visibility = Visibility.Collapsed;
        }
        private void ShopItemBuy(object sender, PointerRoutedEventArgs e)
        {
            if (Switch == true)
            {
                if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
                {
                    var prop = e.GetCurrentPoint(this).Properties;
                    if (prop.IsLeftButtonPressed)
                    {
                        int index;
                        int column, row;
                        column = (int)(sender as Image).GetValue(Grid.ColumnProperty);
                        row = (int)(sender as Image).GetValue(Grid.RowProperty);
                        index = column * row + column;
                        Slot s = GameManager.traderTarget.shop.TradingItems.GetSlot(index);
                        GameManager.traderTarget.shop.SlotInOffer = s;
                        if (s == null) return;
                        ShowOfferItem(s);
                    }
                }
            }
        }
        #endregion
        #region Conversation
        private Grid ButtonsGrid;
        ObjectPooling<Button> ButtonPool = new ObjectPooling<Button>();
        public void CallConversationBox(NPC npc)
        {
            if (GameManager.interfaceManager.Conversation) return;
            GameManager.npcTarget = npc;
            Conversation.Visibility = Visibility.Visible;
            int Buttons = npc.GetFunctionSize() + 1;
            List<string> funcString = npc.GetFunctionsString();
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
                Button b;
                if (ButtonPool.PoolSize > 0)
                {
                    ButtonPool.GetFromPool(out b);
                    b.Visibility = Visibility.Visible;
                }
                else
                {
                    b = new Button
                    {
                        Height = (ButtonsGrid.Height / Buttons) - 10,
                        Width = ButtonsGrid.Height
                    };
                    ButtonsGrid.Children.Add(b);
                }

                if (i < Buttons - 1)
                {
                    b.Content = funcString[i];
                    b.Click += npc.GetFunction(funcString[i]).MyFunction;
                }
                else
                {
                    b.Content = "Nothing";
                    b.Click += HasToCloseConv;
                }
                b.SetValue(Grid.RowProperty, i);
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
        }
        public void HasToCloseConv(object sender, RoutedEventArgs e)
        {
            if (GameManager.interfaceManager.ConvHasToClose != false) return;
            
            Button[] listaButton = new Button[ButtonPool.Pooled.Count]; 
            foreach (Button b in listaButton)
            {
                b.Visibility = Visibility.Collapsed;
            }
            ButtonPool.ReturnToPool();
            GameManager.npcTarget = null;
        }
        public void CloseConversationBox(object sender, RoutedEventArgs e)
        {
            Conversation.Visibility = Visibility.Collapsed;
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
        private void RealocateWindow(Canvas window, Point mousePosition)
        {
            window.Visibility = Visibility.Visible;

            if (mousePosition.X >= Tela.Width / 2)
            {
                window.SetValue(Canvas.LeftProperty, mousePosition.X - window.Width - 10);
            }
            else
            {
                window.SetValue(Canvas.LeftProperty, mousePosition.X + 10);
            }


            if (mousePosition.Y >= Tela.Height / 2)
            {
                window.SetValue(Canvas.TopProperty, mousePosition.Y - window.Height - 10);
            }
            else
            {
                window.SetValue(Canvas.TopProperty, mousePosition.Y + 10);
            }
        }
        private void Skill_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            int indicadorzao = 0;
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
            string s;
            if (GameManager.player._SkillManager.SkillBar[indicadorzao] != null)
            {
                if (GameManager.mobTarget.Mob != null)
                {
                    s = (GameManager.player._SkillManager.SkillBar[indicadorzao]).UseSkill(GameManager.player, GameManager.mobTarget.Mob).ToString();
                }
            }

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
        private void ShowChest(object sender, PointerRoutedEventArgs e)
        {
            ChestBody chest = (ChestBody)sender;
            chest.OnChestOpened();
        }
        private void ChestOpen(object sender, ChestEventArgs e)
        {
            ChestWindow.Visibility = Visibility.Visible;
            UpdateChest(e.chest);
        }
        private void UpdateChest(Bau chest)
        {
            UpdateGridBagItemImages(ChestGrid, chest.itens);
            int count = 0;
            foreach (ItemImage img in ChestGrid.Children)
            {
                if (count >= chest.itens.Slots.Count) img.Source = new BitmapImage();
                else
                {
                    img.Source = new BitmapImage(
                        new Uri("ms-appx://" + Encyclopedia.SearchFor(chest.itens.GetSlot(count).ItemID).PathImage));
                }
                count++;
            }
        }
        private void UpdateChestEvent(object sender, BagEventArgs e)
        {
            Bag chest = e.Bag;
            UpdateGridBagItemImages(ChestGrid, chest);
            int count = 0;
            foreach (ItemImage img in ChestGrid.Children)
            {
                if (count >= chest.Slots.Count) img.Source = new BitmapImage();
                else
                {
                    img.Source = new BitmapImage(
                        new Uri("ms-appx://" + Encyclopedia.SearchFor(chest.GetSlot(count).ItemID).PathImage));
                }
                count++;
            }
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