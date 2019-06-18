using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using RPG_Noelf.Assets.Scripts.Enviroment;
using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using RPG_Noelf.Assets.Scripts.Scenes;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    public class InterfaceManager
    {
        public static InterfaceManager instance;
        
        // Geral

        public Canvas Tela;
        public Canvas CanvasChunck01 { get; set; }
        public Canvas MenuAberto { get; set; }
        public Canvas MenuFechado { get; set; }
        // Chest
        public Canvas CanvasChest { get; set; }
        // Inventario
        public Canvas CanvasInventario { get; set; }
        public Canvas CanvasWindowBag { get; set; }
        // Skill
        public Canvas CanvasSkillWindow { get; set; }
        public Canvas CanvasSkillTree { get; set; }
        public Canvas CanvasSkillBar { get; set; }
        // Shop
        public Canvas CanvasShop { get; set; }
        public Canvas CanvasOfferShop { get; set; }
        // Player
        public Canvas CanvasAtributos { get; set; }
        // Equipamento
        public Canvas CanvasEquipamento { get; set; }
        // Crafting
        public Canvas CanvasCrafting { get; set; }
        // Quest
        public Canvas CanvasQuestList { get; set; }
        public Canvas CanvasActiveQuests { get; set; }
        public Canvas CanvasQuestManager { get; set; }
        public Canvas CanvasQuest { get; set; }
        // Conversation
        public Canvas CanvasConversation { get; set; }

        public StackPanel StackCraft { get; set; }
        public StackPanel StackQuest { get; set; }
        
        public Grid GridChest { get; set; }
        public Grid GridEquip { get; set; }
        public Grid GridShop { get; set; }
        public Grid GridInventario { get; set; }
        public Grid GridBarraSkill { get; set; }
        public Grid GridSkill { get; set; }

        public Grid ButtonsGrid;

        // Player
        public TextBlock TextPlayerInfo { get; set; }
        // Skill
        public TextBlock TextW_SkillName { get; set; }
        public TextBlock TextW_SkillType { get; set; }
        public TextBlock TextW_SkillDescr { get; set; }
        public TextBlock TextW_SkillLevel { get; set; }
        public TextBlock TextW_SkillCD { get; set; }
        // Chest
        public TextBlock TextChestName { get; set; }
        // Shop
        public TextBlock TextItemBuyingName { get; set; }
        public TextBlock TextItemBuyingValue { get; set; }
        public TextBlock TextTotalValue { get; set; }
        public TextBox TextItemBuyingQuantity { get; set; }
        // Quest
        public TextBlock TextMQuestTitle { get; set; }
        public TextBlock TextMQuestDescr { get; set; }
        public TextBlock TextMQuestGold { get; set; }
        public TextBlock TextMQuestXP { get; set; }
        public TextBlock TextQuestTitulo { get; set; }
        public TextBlock TextQuestDescription { get; set; }
        public TextBlock TextQuestRewards { get; set; }
        public TextBlock TextQuestWindow { get; set; }
        public TextBlock TextMQuestItemQntd { get; set; }
        public TextBlock TextQuestDescr { get; set; }
        public TextBlock TextQuestName { get; set; }
        // Item
        public TextBlock TextW_ItemName { get; set; }
        public TextBlock TextW_ItemQntd { get; set; }
        public TextBlock TextW_ItemRarity { get; set; }
        public TextBlock TextW_ItemType { get; set; }
        public TextBlock TextW_ItemDescr { get; set; }
        public TextBlock TextW_ItemValue { get; set; }
        // Conversation
        public TextBlock TextConv { get; set; }
        public TextBlock TextConvLevel { get; set; }
        public TextBlock TextConvFuncs { get; set; }
        public TextBlock TextConvName { get; set; }
        // Barra de EXP
        public TextBlock ExpIndicator { get; set; }

        public Image MenuFBolaAtras { get; set; }
        public Image ImageW_Skill { get; set; }
        public Image ImageItemBuying { get; set; }
        public Image ImageArrowQuest { get; set; }
        public Image ImageMQuestItem { get; set; }
        public Image ImageW_Item { get; set; }

        public Button ButtonBuy { get; set; }

        public Button ButtonQuester;

        public Button ButtonTrader;

        public Button ButtonExit;

        public bool InventarioOpen { get; set; }
        public bool ShopOpen { get; set; } = false;
        public bool ConvHasToClose { get; set; }
        public bool Conversation { get; set; } = false;

        public bool Switch = false;

        public bool shopOpen = false;

        public bool equipOpen = false;

        public const int LootWidth = 50;

        public const int LootHeight = 50;

        private int Buttons = 3;

        private int _str, _spd, _dex, _con, _mnd;

        private NPC lastNPC = null;

        private Dictionary<string, Button> ListButtons = new Dictionary<string, Button>();

        private ObjectPooling<MissionListButton> MsnBtnPool = new ObjectPooling<MissionListButton>();

        public string BaseUri = "ms-appx://";

        public InterfaceManager(Canvas Tela)
        {
            this.Tela = Tela;
            instance = this;
            CreateChunck(1366, 768);
        }

        public void GenerateHUD()
        {
            
            CreateCraftingCenter();

            CreateChestWindow(300, 300);

            CreateMenu(0, 0);

            CreateShopWindow();

            CreateActiveQuest();

            CreateQuestWindow();

            CreateOfferItem();

            CreateEquipamento();

            CreateAtributos();

            CreateSkillBar();

            CreateSkillTree();

            CreateInventory();

            CreateQuestManager();

            CreateWindowBag();

            CreateConversation();

            CreateSkillWindow();
            
            CreateCraftingWindow();

            CreateConversationLayout();

            GenerateExpBar();

            CanvasShop.Visibility = Visibility.Collapsed;
            CanvasInventario.Visibility = Visibility.Collapsed;
            CanvasAtributos.Visibility = Visibility.Collapsed;
            CanvasEquipamento.Visibility = Visibility.Collapsed;
            CanvasSkillTree.Visibility = Visibility.Collapsed;

            CanvasConversation.PointerPressed += EndConversation;
            Window.Current.CoreWindow.KeyUp += ManageKey;
        }

        private void ManageKey(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            
            switch (e.VirtualKey)
            {
                case Windows.System.VirtualKey.B:
                    if (InventarioOpen)
                    {
                        CanvasInventario.Visibility = Visibility.Visible;
                        InventarioOpen = false;
                    }
                    else
                    {
                        CanvasInventario.Visibility = Visibility.Collapsed;
                        InventarioOpen = true;
                    }
                    break;
                case Windows.System.VirtualKey.P:
                    if (CanvasAtributos.Visibility == Visibility.Collapsed)
                    {
                        UpdatePlayerInfo();
                        CanvasAtributos.Visibility = Visibility.Visible;
                    }
                    else CanvasAtributos.Visibility = Visibility.Collapsed;
                    break;
                case Windows.System.VirtualKey.K:
                    if (CanvasSkillTree.Visibility == Visibility.Collapsed)
                    {
                        //UpdateSkillTree();
                        CanvasSkillTree.Visibility = Visibility.Visible;
                    }
                    else CanvasSkillTree.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public void Skill_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            int indicadorzao = -1;
            if (e.VirtualKey == Windows.System.VirtualKey.Number1)
            {
                if (GameManager.instance.player._SkillManager.SkillList.Count >= 1)
                {
                    indicadorzao = 0;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number2)
            {
                if (GameManager.instance.player._SkillManager.SkillList.Count >= 2)
                {
                    indicadorzao = 1;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number3)
            {
                if (GameManager.instance.player._SkillManager.SkillList.Count >= 3)
                {
                    indicadorzao = 2;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number4)
            {
                if (GameManager.instance.player._SkillManager.SkillList.Count >= 4)
                {
                    indicadorzao = 3;
                }
            }
            if (indicadorzao != -1) GameManager.instance.player._SkillManager.BeAbleSkill(indicadorzao);

        }

        /* ############################################################################################ */
        /* ####################################### INSTANTIATES ####################################### */
        /* ############################################################################################ */

        public void GenerateExpBar()
        {
            var child = new Canvas()
            {
                Width = 300,
                Height = 300
            };
            Tela.Children.Add(child);
            ExpIndicator = new TextBlock()
            {
                Text = GameManager.instance.player.level.actualEXP + "/" + GameManager.instance.player.level.EXPlim,
                TextAlignment = TextAlignment.Center

            };
            child.Children.Add(ExpIndicator);
            Canvas.SetTop(child, 10);
        } 

        public void GenerateLifeBar()
        {
            var child = new Canvas()
            {
                Width = 200,
                Height = 400
            };
            Tela.Children.Add(child);
            var text = new TextBlock()
            {
                Text = "HP: " + GameManager.instance.player.Hp + "/" + GameManager.instance.player.HpMax + "\n",
                TextAlignment = TextAlignment.Center
            };
            child.Children.Add(text);
            Canvas.SetTop(child, -30);



        }

        public Solid CreateChest(double x, double y, Bau bau, double w, double h)
        {
            ChestBody chest = new ChestBody(x, y, bau)
            {
                Width = w,
                Height = h
            };
            CanvasChunck01.Children.Add(chest);
            Image chestImage = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/Interactable/chest-rpg-normal.png")),
                Width = w,
                Height = h
            };
            chest.Children.Add(chestImage);
            chest.ChestOpen += ChestOpen;
            chest.PointerPressed += OnChestClicked;
            //bau.itens.BagUpdated += UpdateChestEvent;
            return chest;
        }

        public void CreateChunck(double width, double height)
        {
            CanvasChunck01 = new Canvas()
            {
                HorizontalAlignment= HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = height,
                Width = width
            };

            Tela.Children.Add(CanvasChunck01);
        }

        public void CreateCraftingCenter()
        {
            CanvasCrafting = new Canvas()
            {
                Visibility = Visibility.Collapsed,
                Height = 400,
                Width = 500,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(190, 182, 182, 182))
            };
            Canvas.SetLeft(CanvasCrafting, 30);
            Canvas.SetTop(CanvasCrafting, 200);
            Tela.Children.Add(CanvasCrafting);
            /*
            Image bg = new Image()
            {
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/UIAtivo 33-0.png")),
                Stretch = Stretch.Fill,
                Width = 500,
                Height = 300
            };
            CanvasCrafting.Children.Add(bg);*/

            ScrollViewer ListadeCraftings = new ScrollViewer()
            {
                Height = 300,
                Width = 500
            };
            CanvasCrafting.Children.Add(ListadeCraftings);

            StackCraft = new StackPanel()
            {
                Width = 500,
                Height = 750,
                Orientation = Orientation.Vertical,
                Spacing = 5
            };
            ListadeCraftings.Content = StackCraft;

            ScrollBar scroll = new ScrollBar();
            Canvas.SetLeft(scroll, 138);
            Canvas.SetTop(scroll, 144);

            CanvasCrafting.Children.Add(scroll);
        }

        public void CreateChestWindow(double x, double y)
        {
            if (CanvasChest != null) return;
            //Configurando o ChestWindow
            CanvasChest = new Canvas();
            CanvasChest.Width = 250;
            CanvasChest.Height = 150;
            Canvas.SetTop(CanvasChest, x);
            Canvas.SetLeft(CanvasChest, y);
            CanvasChest.Visibility = Visibility.Collapsed;
            Tela.Children.Add(CanvasChest);

            // Criando o texto e a imagem de fundo
            TextChestName = new TextBlock()
            {
                Text = "Normal Chest",
                Width = 250,
                Height = 20,
                TextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(TextChestName, -20);
            CanvasChest.Children.Add(TextChestName);

            Image background = new Image()
            {
                Width = 250,
                Height = 150,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/UIAtivo 23-0.png"))
            };
            CanvasChest.Children.Add(background);

            //Criando o grid com os ItemImage
            GridChest = new Grid()
            {
                Width = 250,
                Height = 150,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Padding = new Thickness(5, 5, 5, 5)
            };
            CanvasChest.Children.Add(GridChest);

            DefinitionsGrid(GridChest, 5, 3, 50, 50);
            FillGridItemImage(GridChest, null, 5, 3, 40, 40);

            Button GetAllBtn = new Button()
            {
                Width = 90,
                Height = 60,
                Content = "All"
            };
            CanvasChest.Children.Add(GetAllBtn);
            Canvas.SetTop(GetAllBtn, 0);
            Canvas.SetLeft(GetAllBtn, CanvasChest.Width);
            GetAllBtn.Click += (object sender, RoutedEventArgs e) =>
            {
                Bag bag = ((ItemImage)GridChest.Children[0]).myBagRef;
                List<Slot> ItemSlots = new List<Slot>();
                foreach (ItemImage item in GridChest.Children)
                {
                    ItemSlots.Add(item.Slot);
                }
                foreach (Slot s in ItemSlots)
                {
                    if (GameManager.instance.player._Inventory.AddToBag(s))
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
            CanvasChest.Children.Add(Close);
            Canvas.SetTop(Close, GetAllBtn.Height + 10);
            Canvas.SetLeft(Close, CanvasChest.Width);
            Close.Click += (object sender, RoutedEventArgs e) =>
            {
                CanvasChest.Visibility = Visibility.Collapsed;
            };

        }

        public void CreateMenu(double width, double height)
        {
            if(width == 0 || height == 0)
            {
                width = 202; height = 199;
            }
            Canvas Menu = new Canvas()
            {
                Width = width,
                Height = height
            };
            Canvas.SetLeft(Menu, 1020);
            Canvas.SetTop(Menu, 450);
            Tela.Children.Add(Menu);

            MenuAberto = new Canvas()
            {
                Width = width,
                Height = height
            };
            MenuAberto.PointerExited += MenuClose;
            Menu.Children.Add(MenuAberto);

            Image aberto = new Image()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = height,
                Width = width,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/aberto.png"))
            };
            MenuAberto.Children.Add(aberto);

            Image mochila = new Image()
            {
                Height = 35,
                Width = 35,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/mochila.png"))
            };
            mochila.PointerPressed += ShowBag;
            Canvas.SetLeft(mochila, 51);
            Canvas.SetTop(mochila, 23);
            MenuAberto.Children.Add(mochila);

            Image equipamento = new Image()
            {
                Height = 35,
                Width = 43,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/equipamento.png"))
            };
            equipamento.PointerPressed += ShowEquip;
            Canvas.SetLeft(equipamento, 112);
            Canvas.SetTop(equipamento, 24);
            MenuAberto.Children.Add(equipamento);

            Image skills = new Image()
            {
                Height = 45,
                Width = 36,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/skills.png"))
            };
            skills.PointerPressed += ShowSkillTree;
            Canvas.SetLeft(skills, 150);
            Canvas.SetTop(skills, 79);
            MenuAberto.Children.Add(skills);
            
            Image status = new Image()
            {
                Height = 35,
                Width = 38,
                Stretch = Stretch.Fill,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/status.png"))
            };
            status.PointerPressed += ShowAtributes;
            Canvas.SetLeft(status, 13);
            Canvas.SetTop(status, 81);
            MenuAberto.Children.Add(status);


            Image craft = new Image()
            {
                Height = 32,
                Width = 54,
                Stretch = Stretch.Fill,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/craft.png"))
            };
            craft.PointerPressed += ShowCrafting;
            Canvas.SetLeft(craft, 40);
            Canvas.SetTop(craft, 144);
            MenuAberto.Children.Add(craft);

            Image missoes = new Image()
            {
                Height = 38,
                Width = 50,
                Stretch = Stretch.Fill,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/missoes.png"))
            };
            missoes.PointerPressed += ShowQuestEvent;
            Canvas.SetLeft(missoes, 111);
            Canvas.SetTop(missoes, 139);
            MenuAberto.Children.Add(missoes);

            MenuFechado = new Canvas()
            {
                Width = 20,
                Height = 20
            };
            Menu.Children.Add(MenuFechado);

            MenuFBolaAtras = new Image()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/semiaberto.png")),
                Height = 90,
                Width = 90,
                Visibility = Visibility.Visible
            };
            Canvas.SetLeft(MenuFBolaAtras, 55);
            Canvas.SetTop(MenuFBolaAtras, 54);
            
            MenuFechado.Children.Add(MenuFBolaAtras);

            Image bola = new Image()
            {
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Menu/fechado.png")),
                Height = 78,
                Width = 78,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            Canvas.SetLeft(bola, 61);
            Canvas.SetTop(bola, 61);
            MenuFechado.Children.Add(bola);

            bola.PointerExited += MenuSemiOpenExit;
            bola.PointerEntered += MenuSemiOpenEnter;
            bola.PointerPressed += MenuOpen;

            MenuAberto.Visibility = Visibility.Collapsed;
            MenuFechado.Visibility = Visibility.Visible;

        }

        public void CreateShopWindow()
        {
            CanvasShop = new Canvas()
            {
                Width = 120,
                Height = 120,
                Visibility = Visibility.Collapsed
            };
            Tela.Children.Add(CanvasShop);
            Canvas.SetTop(CanvasShop, 40);
            Canvas.SetLeft(CanvasShop, 1010);

            TextBlock title = new TextBlock()
            {
                Width = 120,
                Height = 20,
                Text = "Shop",
                TextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(title, -20);
            CanvasShop.Children.Add(title);

            GridShop = new Grid()
            {
                Width = 120,
                Height = 120
            };
            CanvasShop.Children.Add(GridShop);

            DefinitionsGrid(GridShop, 4, 4, 30, 30);
            FillGridItemImage(GridShop, null, 4, 4, 25, 25, EItemOwner.shop);

            ButtonBuy = new Button()
            {
                Content = "Sell",
                Height = 32,
                Width = 40,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetTop(ButtonBuy, 125);

            ButtonBuy.Click += SellButton;
            CanvasShop.Children.Add(ButtonBuy);

            TextTotalValue = new TextBlock()
            {
                Width = 75,
                Height = 15,
                Text = "Total = 0",
                TextAlignment = TextAlignment.Left,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 7
            };
            Canvas.SetTop(TextTotalValue, 135);
            Canvas.SetLeft(TextTotalValue, 45);

            CanvasShop.Children.Add(TextTotalValue);

            Button change = new Button()
            {
                Height = 32,
                Width = 120,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Change"
            };
            Canvas.SetTop(change, 160);
            Canvas.SetLeft(change, 0);

            CanvasShop.Children.Add(change);

        }

        public void CreateActiveQuest()
        {
            CanvasActiveQuests = new Canvas()
            {
                Width = 200,
                Height = 100,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(90, 182, 182, 182)),
                Visibility = Visibility.Collapsed
            };
            Tela.Children.Add(CanvasActiveQuests);
            TextQuestName = new TextBlock()
            {
                Text = "Quest Title",
                Width = 200,
                Height = 50,
                TextAlignment = TextAlignment.Left
            };
            CanvasActiveQuests.Children.Add(TextQuestName);

            TextQuestDescr = new TextBlock()
            {
                Text = "Objetivo",
                Width = 200,
                Height = 50
            };
            Canvas.SetTop(TextQuestDescr, 50);
            CanvasActiveQuests.Children.Add(TextQuestDescr);
            
        }

        public void CreateQuestWindow()
        {
            CanvasQuest = new Canvas()
            {
                Width = 200,
                Height = 300,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(100, 182, 182, 182)),
                Visibility = Visibility.Collapsed
            };
            Tela.Children.Add(CanvasQuest);

            TextQuestTitulo = new TextBlock()
            {
                Width = 200,
                Height = 40,
                Text = "Titulo",
                HorizontalAlignment = HorizontalAlignment.Left
            };
            CanvasQuest.Children.Add(TextQuestTitulo);

            TextQuestDescription = new TextBlock()
            {
                Width = 200,
                Height = 150,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = "Descr",
                TextWrapping = TextWrapping.Wrap
            };
            CanvasQuest.Children.Add(TextQuestDescription);
            Canvas.SetTop(TextQuestDescription, 40);

            TextQuestRewards = new TextBlock()
            {
                Width = 200,
                Height = 50,
                Text = "Rewards",
                HorizontalAlignment = HorizontalAlignment.Left
            };
            CanvasQuest.Children.Add(TextQuestRewards);
            Canvas.SetTop(TextQuestRewards, 190);

            Button ButtonAcceptQuest = new Button()
            {
                Width = 80,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = "Accept"
            };
            CanvasQuest.Children.Add(ButtonAcceptQuest);
            Canvas.SetTop(ButtonAcceptQuest, 245);
            Canvas.SetLeft(ButtonAcceptQuest, 10);
            ButtonAcceptQuest.Click += ClickAcceptQuestButton;

            Button ButtonDenyQuest = new Button()
            {
                Width = 80,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = "Deny"
            };
            CanvasQuest.Children.Add(ButtonDenyQuest);
            Canvas.SetTop(ButtonAcceptQuest, 245);
            Canvas.SetLeft(ButtonAcceptQuest, 110);
            ButtonAcceptQuest.Click += ClickDenyQuestButton;

        }
        
        public void CreateOfferItem()
        {
            CanvasOfferShop = new Canvas()
            {
                Width = 110,
                Height = 155,
                Visibility = Visibility.Collapsed
            };
            Tela.Children.Add(CanvasOfferShop);
            Canvas.SetLeft(CanvasOfferShop, 950);
            Canvas.SetTop(CanvasOfferShop, 80);

            Image bg = new Image()
            {
                Width = 110,
                Height = 155,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/UIAtivo 33-0.png")),
                Stretch = Stretch.Fill
            };
            CanvasOfferShop.Children.Add(bg);

            ImageItemBuying = new Image()
            {
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/Chao.jpg")),
                Width = 25,
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Canvas.SetTop(ImageItemBuying, 0);
            Canvas.SetLeft(ImageItemBuying, 42.5);
            CanvasOfferShop.Children.Add(ImageItemBuying);

            TextItemBuyingName = new TextBlock()
            {
                Text = "Item name",
                TextWrapping = TextWrapping.Wrap,
                Height = 20,
                Width = 50,
                FontSize = 8,
                TextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(TextItemBuyingName, 25);
            CanvasOfferShop.Children.Add(TextItemBuyingName);

            TextItemBuyingValue = new TextBlock()
            {
                Text = "Item value",
                TextWrapping = TextWrapping.Wrap,
                Height = 20,
                Width = 50,
                FontSize = 8,
                TextAlignment = TextAlignment.Right
            };
            Canvas.SetTop(TextItemBuyingValue, 25);
            Canvas.SetLeft(TextItemBuyingValue, 54);
            CanvasOfferShop.Children.Add(TextItemBuyingValue);

            Button ButtonDecrement = new Button()
            {
                Width = 20,
                Height = 20,
                FontSize = 9,
                Content = "-"
            };
            Canvas.SetLeft(ButtonDecrement, 6);
            Canvas.SetTop(ButtonDecrement, 55);
            ButtonDecrement.Click += DecrementOfferAmount;
            CanvasOfferShop.Children.Add(ButtonDecrement);

            Button ButtonIncrement = new Button()
            {
                Width = 20,
                Height = 20,
                FontSize = 9,
                Content = "+"
            };
            Canvas.SetLeft(ButtonIncrement, 84);
            Canvas.SetTop(ButtonIncrement, 55);
            ButtonIncrement.Click += IncrementOfferAmount;
            CanvasOfferShop.Children.Add(ButtonIncrement);

            TextItemBuyingQuantity = new TextBox()
            {
                Width = 56,
                Height = 30,
                Text = "0",
                FontSize = 9,
                TextAlignment = TextAlignment.Center,
                IsDoubleTapEnabled = false
            };
            InputScope scope = new InputScope();
            InputScopeName nameScope = new InputScopeName();
            nameScope.NameValue = InputScopeNameValue.Number;
            scope.Names.Add(nameScope);
            TextItemBuyingQuantity.InputScope = scope;
            Canvas.SetLeft(TextItemBuyingQuantity, 27);
            Canvas.SetTop(TextItemBuyingQuantity, 50);
            CanvasOfferShop.Children.Add(TextItemBuyingQuantity);
            TextItemBuyingQuantity.TextChanged += ItemBuyingQuantity_TextChanged;

            Button buttonOffer = new Button()
            {
                Width = 80,
                Height = 30,
                Content = "Offer",
                FontSize = 8
            };
            CanvasOfferShop.Children.Add(buttonOffer);
            Canvas.SetLeft(buttonOffer, 15);
            Canvas.SetTop(buttonOffer, 85);
            buttonOffer.Click += OfferItemButton;

            Button buttonCancel = new Button()
            {
                Width = 80,
                Height = 30,
                Content = "Cancel",
                FontSize = 8
            };
            CanvasOfferShop.Children.Add(buttonCancel);
            Canvas.SetLeft(buttonCancel, 15);
            Canvas.SetTop(buttonCancel, 120);
            buttonCancel.Click += CancelSellingButton;


        }

        public void CreateEquipamento()
        {
            CanvasEquipamento = new Canvas()
            {
                Width = 100,
                Height = 200
            };
            Tela.Children.Add(CanvasEquipamento);
            Canvas.SetTop(CanvasEquipamento, 40);
            Canvas.SetLeft(CanvasEquipamento, 1200);

            TextBlock EquipTitle = new TextBlock()
            {
                Width = 100,
                Height = 25,
                Text = "Equipamento"
            };
            Canvas.SetTop(EquipTitle, -25);
            CanvasEquipamento.Children.Add(EquipTitle);

            GridEquip = new Grid()
            {
                Width = 100,
                Height = 200
            };
            CanvasEquipamento.Children.Add(GridEquip);
            DefinitionsGrid(GridEquip, 2, 4, 50, 50);
            EquipImage[] equips = new EquipImage[4];
            for(int i = 0; i < 4; i++)
            {
                equips[i] = new EquipImage(40, 40);
                Grid.SetColumn(equips[i], 0);
                Grid.SetRow(equips[i], i);
                GridEquip.Children.Add(equips[i]);
            }

            EquipImage weap = new EquipImage(40, 40);
            Grid.SetColumn(weap, 1);
            Grid.SetRow(weap, 1);
            GridEquip.Children.Add(weap);
        }
        
        public void CreateAtributos()
        {
            CanvasAtributos = new Canvas()
            {
                Width = 130,
                Height = 180
                //Background = new SolidColorBrush(Windows.UI.Color.FromArgb(100, 182, 182, 182))
            };
            Tela.Children.Add(CanvasAtributos);
            Canvas.SetLeft(CanvasAtributos, 630);
            Canvas.SetTop(CanvasAtributos, 40);

            ScrollViewer scroll = new ScrollViewer()
            {
                Width = 150,
                Height = 232,
                IsHorizontalRailEnabled = false,
                IsHorizontalScrollChainingEnabled = false,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(250, 182, 182, 182))
            };
            Canvas.SetLeft(scroll, -250);
            CanvasAtributos.Children.Add(scroll);

            TextPlayerInfo = new TextBlock()
            {
                Width = 150,
                Height = 300,
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                FontSize = 9
            };
            scroll.Content = TextPlayerInfo;

            StackPanel AtributosStack = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 120,
                Width = 70,
                Spacing = 10,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(200, 182, 182, 182))
            };
            AtributosStack.Padding = new Thickness(5, 5, 5, 5);
            Canvas.SetTop(AtributosStack, 20);
            Canvas.SetLeft(AtributosStack, -90);
            CanvasAtributos.Children.Add(AtributosStack);

            Button ButtonXP = new Button()
            {
                Content = "+XP",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 60,
                Height = 30,
                FontSize = 10
            };
            ButtonXP.Click += XPPlus;

            Button ButtonHP = new Button()
            {
                Content = "+HP",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 60,
                Height = 30,
                FontSize = 10
            };
            ButtonHP.Click += HPPlus;

            Button ButtonMP = new Button()
            {
                Content = "+MP",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 60,
                Height = 30,
                FontSize = 10
            };
            ButtonMP.Click += MPPlus;

            AtributosStack.Children.Add(ButtonXP);
            AtributosStack.Children.Add(ButtonHP);
            AtributosStack.Children.Add(ButtonMP);

            Grid grid = new Grid()
            {
                Height = 180,
                Width = 130,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(200, 182, 182, 182))
            };
            CanvasAtributos.Children.Add(grid);
            DefinitionsGrid(grid, 3, 6, new int[] { 30, 70, 30 }, new int[] { 30, 30, 30, 30, 30, 30 });
            /*
            Image bg = new Image()
            {
                Width = 180,
                Height = 130,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/UIAtivo 33-0.png")),
            };

            grid.Children.Add(bg);*/

            TextBlock strenght = new TextBlock()
            {
                Padding = new Thickness(0, 7, 0, 0),
                Width = 70,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Text = "STRENGHT",
                FontSize = 10,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Top
            };
            grid.Children.Add(strenght);
            Grid.SetColumn(strenght, 1);
            Grid.SetRow(strenght, 0);

            TextBlock constitution = new TextBlock()
            {
                Padding = new Thickness(0, 7, 0, 0),
                Width = 70,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Text = "CONSTITUTION",
                FontSize = 10,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Top
            };

            grid.Children.Add(constitution);
            Grid.SetColumn(constitution, 1);
            Grid.SetRow(constitution, 2);

            TextBlock speed = new TextBlock()
            {
                Padding = new Thickness(0, 7, 0, 0),
                Width = 70,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Text = "SPEED",
                FontSize = 10,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Top
            };
            grid.Children.Add(speed);
            Grid.SetColumn(speed, 1);
            Grid.SetRow(speed, 1);

            TextBlock mind = new TextBlock()
            {
                Padding = new Thickness(0, 7, 0, 0),
                Width = 70,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Text = "MIND",
                FontSize = 10,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Top
            };
            grid.Children.Add(mind);
            Grid.SetColumn(mind, 1);
            Grid.SetRow(mind, 4);

            TextBlock dexterity = new TextBlock()
            {
                Padding = new Thickness(0, 7, 0, 0),
                Width = 70,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Text = "DEXTERITY",
                FontSize = 10,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Top
            };
            grid.Children.Add(dexterity);
            Grid.SetColumn(dexterity, 1);
            Grid.SetRow(dexterity, 3);

            Button minusStr = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "-"
            };
            grid.Children.Add(minusStr);
            Grid.SetColumn(minusStr, 0);
            Grid.SetRow(minusStr, 0);
            minusStr.Click += MSTR;

            Button minusSpd = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "-"
            };
            grid.Children.Add(minusSpd);
            Grid.SetColumn(minusSpd, 0);
            Grid.SetRow(minusSpd, 1);
            minusSpd.Click += MSPD;

            Button minusCon = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "-"
            };
            grid.Children.Add(minusCon);
            Grid.SetColumn(minusCon, 0);
            Grid.SetRow(minusCon, 2);
            minusCon.Click += MCON;

            Button minusDex = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "-"
            };
            grid.Children.Add(minusDex);
            Grid.SetColumn(minusDex, 0);
            Grid.SetRow(minusDex, 3);
            minusDex.Click += MDEX;

            Button minusMnd = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "-"
            };
            grid.Children.Add(minusMnd);
            Grid.SetColumn(minusMnd, 0);
            Grid.SetRow(minusMnd, 4);
            minusMnd.Click += MMND;

            Button plusStr = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "+"
            };
            grid.Children.Add(plusStr);
            Grid.SetColumn(plusStr, 2);
            Grid.SetRow(plusStr, 0);
            plusStr.Click += PSTR;

            Button plusSpd = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "+"
            };
            grid.Children.Add(plusSpd);
            Grid.SetColumn(plusSpd, 2);
            Grid.SetRow(plusSpd, 1);
            plusSpd.Click += PSPD;

            Button plusCon = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "+"
            };
            grid.Children.Add(plusCon);
            Grid.SetColumn(plusCon, 2);
            Grid.SetRow(plusCon, 2);
            plusCon.Click += PCON;

            Button plusDex = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "+"
            };
            grid.Children.Add(plusDex);
            Grid.SetColumn(plusDex, 2);
            Grid.SetRow(plusDex, 3);
            plusDex.Click += PDEX;

            Button plusMnd = new Button()
            {
                Margin = new Thickness(2.5, 1, 0, 0),
                Width = 25,
                Height = 25,
                Padding = new Thickness(0, -9, 0, 0),
                FontSize = 24,
                Content = "+"
            };
            grid.Children.Add(plusMnd);
            Grid.SetColumn(plusMnd, 2);
            Grid.SetRow(plusMnd, 4);
            plusMnd.Click += PMND;

            Button Apply = new Button()
            {
                FontSize = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 25,
                Width = 90,
                Content = "Apply"
            };
            grid.Children.Add(Apply);
            Apply.Click += ApplyStats;
            Grid.SetRow(Apply, 6);
            Grid.SetColumnSpan(Apply, 3);
            //CanvasAtributos.Children.Add(Apply);
        }

        public void CreateSkillBar()
        {
            CanvasSkillBar = new Canvas()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Height = 50,
                Width = 300,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(100, 182, 182, 182))
            };
            Tela.Children.Add(CanvasSkillBar);
            Canvas.SetTop(CanvasSkillBar, 600);
            Canvas.SetLeft(CanvasSkillBar, 544);

            TextBlock title = new TextBlock()
            {
                Text = "Barra de Habilidades",
                Width = 300,
                Height = 20,
                TextAlignment = TextAlignment.Center
            };
            CanvasSkillBar.Children.Add(title);
            Canvas.SetTop(title, -20);

            GridBarraSkill = new Grid()
            {
                Height = 50,
                Width = 300,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            CanvasSkillBar.Children.Add(GridBarraSkill);
            GridBarraSkill.SetValue(Grid.PaddingProperty, new Thickness(5, 5, 5, 5));

            DefinitionsGrid(GridBarraSkill, 5, 1, 60, 50);
            FillGridSkillImage(GridBarraSkill, false, GameManager.instance.player, 5, 1, 40, 40);

            UpdateSkillBar();
        }

        public void CreateSkillTree()
        {
            CanvasSkillTree = new Canvas();
            CanvasSkillTree.Width = 250;
            CanvasSkillTree.Height = 150;
            CanvasSkillTree.HorizontalAlignment = HorizontalAlignment.Stretch;
            CanvasSkillTree.VerticalAlignment = VerticalAlignment.Stretch;
            Canvas.SetTop(CanvasSkillTree, 40);
            Canvas.SetLeft(CanvasSkillTree, 120);
            Tela.Children.Add(CanvasSkillTree);
            TextBlock text = new TextBlock()
            {
                Width = 250,
                Height = 20,
                Text = "Árvore de Habilidades",
                HorizontalTextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(text, -20);
            CanvasSkillTree.Children.Add(text);
            Image bg = new Image()
            {
                Width = 250,
                Height = 150,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/UIAtivo 23-0.png")),
                Stretch = Stretch.Fill
            };
            CanvasSkillTree.Children.Add(bg);
            GridSkill = new Grid()
            {
                Width = 250,
                Height = 150
            };
            CanvasSkillTree.Children.Add(GridSkill);
            GridSkill.SetValue(Grid.PaddingProperty, new Thickness(5, 5, 5, 5));
            DefinitionsGrid(GridSkill, 5, 3, 50, 50);
            FillGridSkillImage(GridSkill, true, GameManager.instance.player, 5, 3, 40, 40);
        }

        public void CreateInventory()
        {
            if (CanvasInventario != null) return;
            CanvasInventario = new Canvas();
            CanvasInventario.Width = 180;
            CanvasInventario.Height = 150;
            Canvas.SetLeft(CanvasInventario, 800);
            Canvas.SetTop(CanvasInventario, 40);
            Tela.Children.Add(CanvasInventario);
            TextBlock text = new TextBlock()
            {
                Width = 180,
                Height = 20,
                Text = "Bag",
                HorizontalTextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(text, -20);
            CanvasInventario.Children.Add(text);
            Image bg = new Image()
            {
                //Source="/Assets/Images/UI Elements/FundoInventario.png" Width="180" Height="150"
                Width = 180,
                Height = 150,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/FundoInventario.png"))
            };
            CanvasInventario.Children.Add(bg);
            GridInventario = new Grid()
            {
                Width = 180,
                Height = 150
            };
            GridInventario.SetValue(Grid.PaddingProperty, new Thickness(2.5, 2.5, 2.5, 2.5));
            CanvasInventario.Children.Add(GridInventario);

            DefinitionsGrid(GridInventario, 6, 5, 30, 30);
            FillGridItemImage(GridInventario, GameManager.instance.player._Inventory, 6, 5, 25, 25);


        }

        public void CreateQuestManager()
        {
            CanvasQuestManager = new Canvas()
            {
                Width = 400,
                Height = 300,
                Visibility = Visibility.Collapsed
            };
            Tela.Children.Add(CanvasQuestManager);
            Canvas.SetTop(CanvasQuestManager, 230);
            Canvas.SetLeft(CanvasQuestManager, 480);

            Image bg = new Image()
            {
                Width = 400,
                Height = 300,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/FundoInventario.png"))
            };
            CanvasQuestManager.Children.Add(bg);

            TextMQuestTitle = new TextBlock()
            {
                Width = 300,
                Height = 20,
                TextAlignment = TextAlignment.Center,
                Text = "Nome quest ativa"
            };
            Canvas.SetTop(TextMQuestTitle, 10);
            Canvas.SetLeft(TextMQuestTitle, 50);
            CanvasQuestManager.Children.Add(TextMQuestTitle);

            TextMQuestDescr = new TextBlock()
            {
                Width = 480,
                Height = 170,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                Text = "Descrição acerca do que deverá ser feito na quest ativa. Isoum prum rep lit ch et pra set lerium acth katzen in der figen."
            };
            CanvasQuestManager.Children.Add(TextMQuestDescr);
            Canvas.SetTop(TextMQuestDescr, 40);
            Canvas.SetLeft(TextMQuestDescr, 10);

            Canvas questC = new Canvas()
            {
                Width = 360,
                Height = 30
            };
            CanvasQuestManager.Children.Add(questC);
            Canvas.SetTop(questC, 220);
            Canvas.SetLeft(questC, 30);

            Image goldImg = new Image()
            {
                Width = 30,
                Height = 30,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/gold coin.png"))
            };
            questC.Children.Add(goldImg);
            Canvas.SetTop(goldImg, 0);
            Canvas.SetLeft(goldImg, 10);

            TextMQuestGold = new TextBlock()
            {
                Width = 70,
                Height = 20,
                Text = "x500",
                FontSize = 10,
                TextAlignment = TextAlignment.Left
            };
            questC.Children.Add(TextMQuestGold);
            Canvas.SetTop(TextMQuestGold, 0);
            Canvas.SetLeft(TextMQuestGold, 50);

            Image xpImg = new Image()
            {
                Width = 30,
                Height = 30,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/xp.png"))
            };
            questC.Children.Add(xpImg);
            Canvas.SetTop(xpImg, 0);
            Canvas.SetLeft(xpImg, 125);

            TextMQuestXP = new TextBlock()
            {
                Width = 70,
                Height = 20,
                Text = "x500",
                FontSize = 10,
                TextAlignment = TextAlignment.Left
            };
            questC.Children.Add(TextMQuestXP);
            Canvas.SetTop(TextMQuestXP, 5);
            Canvas.SetLeft(TextMQuestXP, 165);

            ImageMQuestItem = new Image()
            {
                Width = 30,
                Height = 30,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/itens/barraFerro.png"))
            };
            questC.Children.Add(ImageMQuestItem);
            Canvas.SetTop(ImageMQuestItem, 0);
            Canvas.SetLeft(ImageMQuestItem, 240);
            
            TextMQuestItemQntd = new TextBlock()
            {
                Width = 70,
                Height = 20,
                Text = "x500",
                FontSize = 10,
                TextAlignment = TextAlignment.Left
            };
            questC.Children.Add(TextMQuestItemQntd);
            Canvas.SetTop(TextMQuestItemQntd, 5);
            Canvas.SetLeft(TextMQuestItemQntd, 280);

            Button btnDesistir = new Button()
            {
                Width = 100,
                Height = 30,
                Content = "Desistir",
                FontSize = 12
            };
            CanvasQuestManager.Children.Add(btnDesistir);
            Canvas.SetTop(btnDesistir, 260);
            Canvas.SetLeft(btnDesistir, 150);
            btnDesistir.Click += GiveUpButton;

            Image exit = new Image()
            {
                Width = 20,
                Height = 20,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/circle-red.png"))
            };
            CanvasQuestManager.Children.Add(exit);
            Canvas.SetTop(exit, 10);
            Canvas.SetLeft(exit, 365);
            exit.PointerPressed += CloseQuestManagerWindow;

            ImageArrowQuest = new Image()
            {
                Width = 20,
                Height = 20,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/Expand.png")),
                RenderTransformOrigin = new Point(0.5, 0.5)
            };
            CanvasQuestManager.Children.Add(ImageArrowQuest);
            Canvas.SetTop(ImageArrowQuest, 10);
            Canvas.SetLeft(ImageArrowQuest, 20);
            ImageArrowQuest.PointerPressed += ShowQuestList;
            //((RotateTransform)ImageArrowQuest.RenderTransform.).Angle = 90;

            CanvasQuestList = new Canvas()
            {
                Width = 120,
                Height = 300,
                Visibility = Visibility.Collapsed
            };
            CanvasQuestManager.Children.Add(CanvasQuestList);
            Canvas.SetTop(CanvasQuestList, 0);
            Canvas.SetLeft(CanvasQuestList, -120);

            Image fundo = new Image()
            {
                Width = 120,
                Height = 300,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/FundoInventario.png"))
            };
            CanvasQuestList.Children.Add(fundo);

            ScrollViewer scroll = new ScrollViewer()
            {
                Width = 100,
                Height = 265
            };
            CanvasQuestList.Children.Add(scroll);
            Canvas.SetTop(scroll, 15);
            Canvas.SetLeft(scroll, 10);

            StackQuest = new StackPanel()
            {
                Width = 100,
                Height = 265,
                Spacing = 10
            };
            scroll.Content = StackQuest;
        }
        
        public void CreateWindowBag()
        {
            CanvasWindowBag = new Canvas()
            {
                Height = 250,
                Width = 200,
                Visibility = Visibility.Collapsed
            };
            Tela.Children.Add(CanvasWindowBag);
            Canvas.SetLeft(CanvasWindowBag, 150);

            Image bg = new Image()
            {
                Margin = new Thickness(-10, 0, 0, 0),
                Width = 210,
                Height = 250,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/UIAtivo 33-0.png"))
            };
            CanvasWindowBag.Children.Add(bg);

            ImageW_Item = new Image()
            {
                Width = 30,
                Height = 30,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/Chao.jpg"))
            };
            CanvasWindowBag.Children.Add(ImageW_Item);
            Canvas.SetLeft(ImageW_Item, 75);
            Canvas.SetTop(ImageW_Item, 5);

            TextW_ItemName = new TextBlock()
            {
                Height = 15,
                Text = "Item name",
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            CanvasWindowBag.Children.Add(TextW_ItemName);
            Canvas.SetTop(TextW_ItemName, 35);

            TextW_ItemType = new TextBlock()
            {
                Height = 15,
                Width = 60,
                Text = "Item type",
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            CanvasWindowBag.Children.Add(TextW_ItemType);
            Canvas.SetTop(TextW_ItemType, 35);
            Canvas.SetLeft(TextW_ItemType, 125);
            
            TextW_ItemQntd = new TextBlock()
            {
                Height = 15,
                Width = 60,
                Text = "999",
                FontSize = 10,
                TextAlignment = TextAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            CanvasWindowBag.Children.Add(TextW_ItemQntd);
            Canvas.SetTop(TextW_ItemQntd, 21);
            Canvas.SetLeft(TextW_ItemQntd, 108);

            TextW_ItemDescr = new TextBlock()
            {
                Height = 100,
                Width = 180,
                Text = "Descrição do item",
                FontSize = 10,
                FontStyle = Windows.UI.Text.FontStyle.Italic,
                TextAlignment = TextAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            CanvasWindowBag.Children.Add(TextW_ItemDescr);
            Canvas.SetTop(TextW_ItemDescr, 80);
            Canvas.SetLeft(TextW_ItemDescr, 5);

            TextW_ItemValue = new TextBlock()
            {
                Text = "Valor",
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            CanvasWindowBag.Children.Add(TextW_ItemValue);
            Canvas.SetTop(TextW_ItemValue, 200);
            Canvas.SetLeft(TextW_ItemValue, 15);

            TextW_ItemRarity = new TextBlock()
            {
                Text = "Rarity",
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            CanvasWindowBag.Children.Add(TextW_ItemRarity);
            Canvas.SetTop(TextW_ItemRarity, 200);
            Canvas.SetLeft(TextW_ItemRarity, 120);

        }
        
        public void CreateConversation()
        {
            CanvasConversation = new Canvas()
            {
                Width = 500,
                Height = 500,
                Visibility = Visibility.Collapsed
            };
            Canvas.SetLeft(CanvasConversation, 850);
            Canvas.SetTop(CanvasConversation, 250);
            Tela.Children.Add(CanvasConversation);

            Image bg = new Image()
            {
                Width = 500,
                Height = 250,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/UIAtivo 33-0.png")),
                Stretch = Stretch.Fill
            };
            CanvasConversation.Children.Add(bg);

            TextConv = new TextBlock()
            {
                Padding = new Thickness(15, 5, 15, 5),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12,
                Width = 500,
                Height = 200,
                Text = "Introduction"
            };
            Canvas.SetTop(TextConv, 300);
            CanvasConversation.Children.Add(TextConv);

            TextConvName = new TextBlock()
            {
                Padding = new Thickness(15, 5, 15, 5),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12,
                Width = 250,
                Height = 25,
                Text = "Level"
            };
            Canvas.SetTop(TextConvName, 250);
            CanvasConversation.Children.Add(TextConvName);

            TextConvLevel = new TextBlock()
            {
                Padding = new Thickness(15, 5, 15, 5),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12,
                Width = 250,
                Height = 25,
                Text = "Level"
            };
            Canvas.SetTop(TextConvLevel, 250);
            Canvas.SetLeft(TextConvLevel, 250);
            CanvasConversation.Children.Add(TextConvLevel);

            TextConvFuncs = new TextBlock()
            {
                Padding = new Thickness(15, 5, 15, 5),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12,
                Width = 250,
                Height = 25,
                Text = "Tipo 1/Tipo 2/Tipo 3"
            };
            Canvas.SetTop(TextConvFuncs, 250);
            Canvas.SetLeft(TextConvFuncs, 275);
            CanvasConversation.Children.Add(TextConvFuncs);


        }
        
        public void CreateSkillWindow()
        {
            CanvasSkillWindow = new Canvas()
            {
                Width = 200,
                Height = 250,
                Visibility = Visibility.Collapsed
            };
            Tela.Children.Add(CanvasSkillWindow);
            Canvas.SetTop(CanvasSkillWindow, 0);
            Canvas.SetLeft(CanvasSkillWindow, 150);

            Image bg = new Image()
            {
                Margin = new Thickness(-5, 0, 0, 0),
                Width = 205,
                Height = 250,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/UI Elements/UIAtivo 33-0.png")),
                Stretch = Stretch.Fill
            };
            CanvasSkillWindow.Children.Add(bg);
            
            ImageW_Skill = new Image()
            {
                Margin = new Thickness(-5, 0, 0, 0),
                Width = 30,
                Height = 30,
                Source = new BitmapImage(new Uri(BaseUri + "/Assets/Images/Chao.jpg")),
                Stretch = Stretch.Fill
            };
            CanvasSkillWindow.Children.Add(ImageW_Skill);
            Canvas.SetTop(ImageW_Skill, 2);
            Canvas.SetLeft(ImageW_Skill, 75);

            TextW_SkillName = new TextBlock()
            {
                Text = "Skill name",
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            CanvasSkillWindow.Children.Add(TextW_SkillName);
            Canvas.SetTop(TextW_SkillName, 40);
            Canvas.SetLeft(TextW_SkillName, 0);

            TextW_SkillType = new TextBlock()
            {
                Text = "Skill type",
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            CanvasSkillWindow.Children.Add(TextW_SkillType);
            Canvas.SetTop(TextW_SkillType, 40);
            Canvas.SetLeft(TextW_SkillType, 120);

            TextW_SkillDescr = new TextBlock()
            {
                Text = "Skill descr",
                Width = 190,
                Height = 120,
                TextWrapping = TextWrapping.Wrap,
                FontStyle = Windows.UI.Text.FontStyle.Italic,
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            CanvasSkillWindow.Children.Add(TextW_SkillDescr);
            Canvas.SetTop(TextW_SkillDescr, 60);
            Canvas.SetLeft(TextW_SkillDescr, 3);

            TextW_SkillLevel = new TextBlock()
            {
                Text = "Skill level",
                FontSize = 10,
                TextAlignment = TextAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            CanvasSkillWindow.Children.Add(TextW_SkillLevel);
            Canvas.SetTop(TextW_SkillLevel, 210);
            Canvas.SetLeft(TextW_SkillLevel, 20);

            TextW_SkillCD = new TextBlock()
            {
                Text = "Skill CD",
                FontSize = 10,
                TextAlignment = TextAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center
            };
            CanvasSkillWindow.Children.Add(TextW_SkillCD);
            Canvas.SetTop(TextW_SkillCD, 210);
            Canvas.SetLeft(TextW_SkillCD, 110);
        }

        public void CreateConversationLayout()
        {
            ButtonsGrid = new Grid();
            ButtonsGrid.Width = CanvasConversation.Width;
            ButtonsGrid.Height = CanvasConversation.Height / 2;
            CanvasConversation.Children.Add(ButtonsGrid);
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

            ButtonQuester = new Button()
            {
                Height = (ButtonsGrid.Height / Buttons) - 10,
                Width = ButtonsGrid.Height,
                Content = "Quest"
            };
            ButtonsGrid.Children.Add(ButtonQuester);

            ButtonTrader = new Button()
            {
                Height = (ButtonsGrid.Height / Buttons) - 10,
                Width = ButtonsGrid.Height,
                Content = "Trader"
            };
            ButtonsGrid.Children.Add(ButtonTrader);

            ButtonExit = new Button()
            {
                Height = (ButtonsGrid.Height / Buttons) - 10,
                Width = ButtonsGrid.Height,
                Content = "Exit"
            };
            ButtonExit.Click += HasToCloseConv;
            ButtonsGrid.Children.Add(ButtonExit);

            ButtonQuester.SetValue(Grid.RowProperty, 0);
            ButtonTrader.SetValue(Grid.RowProperty, 1);
            ButtonExit.SetValue(Grid.RowProperty, 2);

            ListButtons.Add("Quester", ButtonQuester);
            ListButtons.Add("Trader", ButtonTrader);
            ListButtons.Add("Exit", ButtonExit);
        }

        public void CreateCraftingWindow()
        {
            CraftBox IronBoots = new CraftBox(24);
            CraftBox IronIngot = new CraftBox(42);
            CraftBox IronChestplate = new CraftBox(21);

            StackCraft.Children.Add(IronBoots);
            StackCraft.Children.Add(IronIngot);
            StackCraft.Children.Add(IronChestplate);
        }

        public void CreateDrop(double x, double y, Slot dropSlot)
        {
            string pathImage = Encyclopedia.SearchFor(dropSlot.ItemID).PathImage;
            LootBody drop = new LootBody(dropSlot)
            {
                Width = LootWidth,
                Height = LootHeight
            };
            CanvasChunck01.Children.Add(drop);
            Canvas.SetTop(drop, y);
            Canvas.SetLeft(drop, x);
            Image dropImage = new Image();
            dropImage.Source = Encyclopedia.encycloImages[dropSlot.ItemID];//new BitmapImage(new Uri(BaseUri + pathImage));
            drop.Children.Add(dropImage);
            //LootBody loot = new LootBody(drop);
            //loot.UpdateBlocks(TheScene);
            //Trigger dropTrigger = new Trigger(loot);
        }

        /* ##################################################################################################*/
        /* ########################################## UPDATES ###############################################*/
        /* ##################################################################################################*/

        public void UpdateChest(Bau chest)
        {
            UpdateGridBagItemImages(GridChest, chest.itens, EItemOwner.drop);
            int count = 0;
            foreach (ItemImage img in GridChest.Children)
            {
                if (count >= chest.itens.Slots.Count) img.Source = null;
                else
                {
                    img.OnItemImageUpdate();
                    //img.Source = new BitmapImage(
                    //    new Uri("ms-appx://" + Encyclopedia.SearchFor(chest.itens.GetSlot(count).ItemID).PathImage));
                }
                count++;
            }
        }
        public void UpdateChestEvent(object sender, BagEventArgs e)
        {
            Bag chest = e.Bag;
            UpdateGridBagItemImages(GridChest, chest, EItemOwner.drop);
            int count = 0;
            foreach (ItemImage img in GridChest.Children)
            {
                if (count >= chest.Slots.Count) img.Source = null;
                else
                {
                    img.OnItemImageUpdate();
                    //img.Source = new BitmapImage(
                    //    new Uri("ms-appx://" + Encyclopedia.SearchFor(chest.GetSlot(count).ItemID).PathImage));
                }
                count++;
            }
        }
        
        public void UpdateGridBagItemImages(Grid grid, Bag bag, EItemOwner own)
        {
            foreach (ItemImage item in grid.Children)
            {
                item.myBagRef = bag;
                item.itemOwner = own;
                item.SetEvents();
            }
        }
        public void UpdatePlayerInfo()
        {
            TextPlayerInfo.Text = GameManager.instance.player.Race.NameRace + " " + GameManager.instance.player._Class.ClassName + "\n";
            TextPlayerInfo.Text += "CanvasAtributos: ( " + GameManager.instance.player._Class.StatsPoints + " pontos)\n" +
                                "Força: " + GameManager.instance.player.Str + " + " + _str + "\n" +
                                "Mente: " + GameManager.instance.player.Mnd + " + " + _mnd + "\n" +
                                "Velocidade: " + GameManager.instance.player.Spd + " + " + _spd + "\n" +
                                "Destreza: " + GameManager.instance.player.Dex + " + " + _dex + "\n" +
                                "Constituição: " + GameManager.instance.player.Con + " + " + _con + "\n\n" +
                                "HP: " + GameManager.instance.player.Hp + "/" + GameManager.instance.player.HpMax + "\n" +
                                "MP: " + GameManager.instance.player.Mp + "/" + GameManager.instance.player.MpMax + "\n" +
                                "Damage: " + GameManager.instance.player.Damage + "\n" +
                                "Atack Speed: " + GameManager.instance.player.AtkSpd + "\n" +
                                "Armor: " + GameManager.instance.player.Armor + "\n\n" +
                                "Level: " + GameManager.instance.player.level.actuallevel + "\n" +
                                "Experience: " + GameManager.instance.player.level.actualEXP + "/" + GameManager.instance.player.level.EXPlim + "\n" +
                                "Pontos de skill disponivel: " + GameManager.instance.player._SkillManager.SkillPoints + "\n" +
                                "Gold: " + GameManager.instance.player._Inventory.Gold;
            ExpIndicator.Text = GameManager.instance.player.level.actualEXP + "/" + GameManager.instance.player.level.EXPlim;

        }
        public void UpdateSkillBar()
        {
            foreach (SkillImage element in GridBarraSkill.Children)
            {
                element.UpdateImage();
            }
        }
        public void UpdateSkillWindowText(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                CanvasSkillWindow.Visibility = Visibility.Visible;
                SkillImage skillImage = (sender as SkillImage);
                SkillGenerics skillInfo = skillImage.skill;
                ImageW_Skill.Source = Encyclopedia.skillsImages[(sender as SkillImage).position];//new BitmapImage(new Uri(BaseUri + skillInfo.pathImage));
                TextW_SkillName.Text = skillInfo.name;
                TextW_SkillType.Text = skillInfo.GetTypeString();
                TextW_SkillDescr.Text = skillInfo.description;
                if (skillInfo.Unlocked == false)
                {
                    TextW_SkillLevel.Text = "Unlock Lv. " + skillInfo.block;
                }
                else
                {
                    TextW_SkillLevel.Text = "Lv. " + skillInfo.Lvl.ToString();
                }
                RealocateWindow(CanvasSkillWindow, e.GetCurrentPoint(Tela).Position);
            }
            catch (NullReferenceException ex)
            {
                CanvasSkillWindow.Visibility = Visibility.Collapsed;
                Debug.WriteLine(ex.Message);
                return;
            }

        }
        public void UpdateItemWindowText(int slotPosition, Bag bag)
        {
            Slot slot = bag.GetSlot(slotPosition);
            if (slot == null) return;
            Item item = Encyclopedia.encyclopedia[slot.ItemID];
            ImageW_Item.Source = Encyclopedia.encycloImages[slot.ItemID];//new BitmapImage(new Uri(BaseUri + item.PathImage));
            TextW_ItemName.Text = item.Name;
            TextW_ItemQntd.Text = slot.ItemAmount + "x";
            TextW_ItemRarity.Text = item.GetTypeString();
            //W_ItemType.Text = item.itemType;
            if (item.description != null) TextW_ItemDescr.Text = item.description;
            TextW_ItemValue.Text = item.GoldValue + " gold";
        }
        public void UpdateItemWindowText(Item item)
        {
            //Slot slot = bag.GetSlot(slotPosition);
            //if (slot == null) return;
            //Item item = Encyclopedia.encyclopedia[slot.ItemID];
            ImageW_Item.Source = Encyclopedia.encycloImages[Encyclopedia.SearchFor(item)]; //new BitmapImage(new Uri(BaseUri + item.PathImage));
            TextW_ItemName.Text = item.Name;
            TextW_ItemQntd.Text = "1x";
            TextW_ItemRarity.Text = item.GetTypeString();
            //W_ItemType.Text = item.itemType;
            if (item.description != null) TextW_ItemDescr.Text = item.description;
            TextW_ItemValue.Text = item.GoldValue + " gold";
        }
        public void UpdateBag()
        {
            foreach (ItemImage itemImg in GridInventario.Children)
            {
                itemImg.OnItemImageUpdate();
            }
        }
        public void UpdateEquip()
        {
            foreach (EquipImage element in GridEquip.Children)
            {
                EquipImage img = element as EquipImage;
                img.UpdateImage();
            }
        }
        public void UpdateQuest()
        {
            if (GameManager.instance.questerTarget != null)
            {
                GameManager.instance.questerTarget.myQuest = QuestList.allquests[GameManager.instance.questerTarget.GetQuestID()];
            }
            else
            {
                GameManager.instance.questerTarget = new Quester(1);
            }
            TextQuestTitulo.Text = GameManager.instance.questerTarget.myQuest.name;
            TextQuestDescription.Text = GameManager.instance.questerTarget.myQuest.Description;
            TextQuestRewards.Text = GameManager.instance.questerTarget.myQuest.RewardDescription;
            CanvasQuest.Visibility = Visibility.Visible;//TextQuestWindow
        }
        public void UpdateShopInfo()
        {
            if (GameManager.instance.traderTarget == null) return;
            //int count = 0;
            foreach (ItemImage img in GridShop.Children)
            {
                img.OnItemImageUpdate();
                //if (Switch == false)
                //{
                //    if (count >= GameManager.instance.traderTarget.shop.BuyingItems.Slots.Count) img.Source = null;
                //    else
                //    {
                //        //Slot s = GameManager.instance.traderTarget.shop.BuyingItems.GetSlot(count);
                //        //img.Source = new BitmapImage(new Uri(BaseUri + Encyclopedia.SearchFor(s.ItemID).PathImage));
                //    }
                //}
                //else
                //{
                //    if (count >= GameManager.instance.traderTarget.shop.TradingItems.Slots.Count) img.Source = null;
                //    else
                //    {
                //        img.OnItemImageUpdate();
                //        //Slot s = GameManager.instance.traderTarget.shop.TradingItems.GetSlot(count);
                //        //img.Source = new BitmapImage(new Uri(BaseUri + Encyclopedia.SearchFor(s.ItemID).PathImage));
                //    }
                //}
                //count++;
            }
        }
        public void UpdateActualQuestManager()
        {
            Quest q = GameManager.instance.player._Questmanager.actualQuest;
            if (q == null)
            {
                CanvasQuestManager.Visibility = Visibility.Collapsed;
                return;
            }
            TextMQuestTitle.Text = q.name;
            TextMQuestDescr.Text = q.Description;
            TextMQuestDescr.Text += "\n" + q.RewardDescription;
            TextMQuestGold.Text = "x" + q.GainedGold;
            TextMQuestXP.Text = "x" + q.GainedXP;
            if (q.GainedItem != null)
            {
                ImageMQuestItem.Source = Encyclopedia.encycloImages[q.GainedItem.ItemID]; //new BitmapImage(new Uri("ms-appx://" + Encyclopedia.SearchFor(q.GainedItem.ItemID).PathImage));
                TextMQuestItemQntd.Text = q.GainedItem.ItemAmount.ToString();
            }
            else
            {
                ImageMQuestItem.Source = null;
                TextMQuestItemQntd.Text = "";
            }
        }
        public void UpdateQuestList()
        {
            ResetQuestList();

            List<Quest> quests = GameManager.instance.player._Questmanager.allQuests;
            int count = 0;
            foreach (Quest q in quests)
            {
                MissionListButton msnB;
                if (MsnBtnPool.PoolSize > 0)
                {
                    MsnBtnPool.GetFromPool(out msnB);
                    msnB.Quest = q;
                    msnB.Titulo = q.name;
                    msnB.Visibility = Visibility.Visible;
                    StackQuest.Children.Add(msnB);
                }
                else
                {
                    msnB = new MissionListButton(q);
                    MsnBtnPool.Pooled.Add(msnB);
                    StackQuest.Children.Add(msnB);
                }
                count++;
            }
        }
        public void ResetQuestList()
        {
            List<MissionListButton> pooled = MsnBtnPool.Pooled;
            foreach (MissionListButton msnB in pooled)
            {
                msnB.Visibility = Visibility.Collapsed;
            }
            StackQuest.Children.Clear();
            MsnBtnPool.ReturnToPool();
        }

        /* ##################################################################################################*/
        /* ########################################### EVENTS ###############################################*/
        /* ##################################################################################################*/

        //chest
        public void OnChestClicked(object sender, PointerRoutedEventArgs e)
        {
            ChestBody chest = (ChestBody)sender;
            chest.OnChestOpened();
        }
            // player
        public void PlayerInfoEvent(object sender, EventArgs e)
        {
            UpdatePlayerInfo();
        }
            // inventory
        public void InventorySlotEvent(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(Tela).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    Slot s = GameManager.instance.player._Inventory.GetSlot((sender as ItemImage).myItemPosition);
                    if (s == null) return;
                    if (shopOpen)
                    {
                        GameManager.instance.traderTarget.shop.SlotInOffer = s;
                        ShowOfferItem(s);
                        UpdateShopInfo();
                    }
                    else if (equipOpen)
                    {
                        Item i = Encyclopedia.encyclopedia[s.ItemID];
                        if (i is Armor || i is Weapon)
                        {
                            if(i is Armor)
                            {
                                (GridEquip.Children[(i as Armor).GetPosition()] as EquipImage).MyEquip = i;
                            }
                            else
                            {
                                (GridEquip.Children[1] as EquipImage).MyEquip = i;
                            }
                            GameManager.instance.player.Equipamento.UseEquip(s.ItemID);
                            CanvasWindowBag.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        GameManager.instance.player._Inventory.RemoveFromBag(s.ItemID, s.ItemAmount);
                        CreateDrop(GameManager.instance.player.box.Xi + (GameManager.instance.player.box.Width / 2),
                                    GameManager.instance.player.box.Yi + (GameManager.instance.player.box.Height / 2), s);
                    }
                }
            }
        }
        public void ItemSlotEventDrop(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(Tela).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    Slot s = (sender as ItemImage).Slot;
                    if (GameManager.instance.player._Inventory.AddToBag(s))
                    {
                        bool c = (sender as ItemImage).myBagRef.RemoveFromBag(s.ItemID, s.ItemAmount);
                    }
                }
            }
        }
            // Conversation
        public void HasToCloseConv(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.interfaceManager.ConvHasToClose != false) return;

            foreach (Button b in ListButtons.Values)
            {
                b.Visibility = Visibility.Collapsed;
            }
            GameManager.instance.npcTarget = null;
            ConvHasToClose = true;
        }
        public void EndConversation(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.interfaceManager.ConvHasToClose)
            {
                GameManager.instance.interfaceManager.Conversation = false;
                CanvasConversation.Visibility = Visibility.Collapsed;
            }
        }
            // Equip
        public void UpdateEquipEvent(object sender, EventArgs e)
        {
            UpdateEquip();
            UpdatePlayerInfo();
        }
        public void DesequiparEvent(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(Tela).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    int s = -1;
                    s = (int)Encyclopedia.SearchFor((sender as EquipImage).MyEquip);
                    if (s <= -1) return;
                    GameManager.instance.player.Equipamento.DesEquip((uint) s);
                }
            }
        }
            // Skills
        public void SkillTreePointerEvent(object sender, PointerRoutedEventArgs e)
        {
            if ((sender as SkillImage).OnBar) return;
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(Tela).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    /*Image skillEnter = sender as Image;
                    SkillGenerics skillClicked;

                    int columnPosition = (int)skillEnter.GetValue(Grid.ColumnProperty);
                    int rowPosition = (int)skillEnter.GetValue(Grid.RowProperty);
                    int position = GridInventario.ColumnDefinitions.Count * rowPosition + columnPosition;
                    skillClicked = GameManager.instance.player._SkillManager.SkillList[position];
                    GameManager.instance.player._SkillManager.UpSkill(skillClicked);*/
                    (sender as SkillImage).UpSkill();
                }
                else if (prop.IsRightButtonPressed)
                {
                    /*Image skillEnter = sender as Image;
                    SkillGenerics skillClicked;

                    int columnPosition = (int)skillEnter.GetValue(Grid.ColumnProperty);
                    int rowPosition = (int)skillEnter.GetValue(Grid.RowProperty);
                    int position = GridInventario.ColumnDefinitions.Count * rowPosition + columnPosition;

                    skillClicked = GameManager.instance.player._SkillManager.SkillList[position];
                    if (skillClicked.Unlocked == false) return;
                    GameManager.instance.player._SkillManager.ChangeSkill(skillClicked);*/
                    (sender as SkillImage).ChangeSkill();
                    //UpdateSkillBar();
                }
            }
        }
        public void RemoveSkillFromBar(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(Tela).Properties;
                if (prop.IsRightButtonPressed)
                {
                    Image skillEnter = sender as Image;

                    int columnPosition = (int)skillEnter.GetValue(Grid.ColumnProperty);
                    int rowPosition = (int)skillEnter.GetValue(Grid.RowProperty);
                    int position = GridInventario.ColumnDefinitions.Count * rowPosition + columnPosition;
                    GameManager.instance.player._SkillManager.SkillBar[position - 1] = null;
                    //UpdateSkillWindowText(null);
                    UpdateSkillBar();
                }
            }
        }
            // Shop
        public void ItemBuyingQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uint.TryParse(TextItemBuyingQuantity.Text, out uint val))
            {
                uint MaxValue;
                if (!Switch)
                {
                    MaxValue = GameManager.instance.player._Inventory.GetSlot(GameManager.instance.traderTarget.shop.SlotInOffer.ItemID).ItemAmount;
                }
                else MaxValue = GameManager.instance.traderTarget.shop.TradingItems.GetSlot(GameManager.instance.traderTarget.shop.SlotInOffer.ItemID).ItemAmount;

                if (val <= 0)
                {
                    val = 1;
                }
                else if (val >= MaxValue)
                {
                    val = MaxValue;
                }
                TextItemBuyingQuantity.Text = val.ToString();
            }
        }
        public void IncrementOfferAmount(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.traderTarget.shop.SlotInOffer == null) return;
            if (uint.TryParse(TextItemBuyingQuantity.Text, out uint val))
            {
                uint MaxValue = GameManager.instance.player._Inventory.GetSlot(GameManager.instance.traderTarget.shop.SlotInOffer.ItemID).ItemAmount;
                val++;
                if (val >= MaxValue)
                {
                    val = MaxValue;
                }
                TextItemBuyingQuantity.Text = val.ToString();
            }
        }
        public void DecrementOfferAmount(object sender, RoutedEventArgs e)
        {
            if (uint.TryParse(TextItemBuyingQuantity.Text, out uint val))
            {
                val--;
                if (val <= 0)
                {
                    val = 1;
                }
                TextItemBuyingQuantity.Text = val.ToString();
            }
        }
        public void OfferItemButton(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.traderTarget == null) return;
            if (uint.TryParse(TextItemBuyingQuantity.Text, out uint val))
            {
                if (Switch == false)
                {

                    if (val <= Bag.MaxStack)
                    {
                        if (GameManager.instance.player._Inventory.RemoveFromBag(GameManager.instance.traderTarget.shop.SlotInOffer.ItemID, val))
                        {
                            Slot newSlot = new Slot(GameManager.instance.traderTarget.shop.SlotInOffer.ItemID, val);
                            GameManager.instance.traderTarget.shop.AddToBuyingItems(newSlot);
                            GameManager.instance.traderTarget.shop.SlotInOffer = null;
                            UpdateShopInfo();
                            CloseOfferItem();
                        }
                    }
                }
                else
                {
                    if (val <= Bag.MaxStack)
                    {
                        Slot newSlot = new Slot(GameManager.instance.traderTarget.shop.SlotInOffer.ItemID, val);
                        GameManager.instance.traderTarget.shop.SellItem(newSlot, GameManager.instance.player._Inventory);
                        CloseOfferItem();
                    }
                }

            }
        }
        public void SellButton(object sender, RoutedEventArgs e)
        {
            if (Switch == false)
            {
                GameManager.instance.traderTarget.shop.BuyItem(GameManager.instance.player._Inventory);
                UpdateShopInfo();
                UpdatePlayerInfo();
            }
        }
        public void ShopItemBuy(object sender, PointerRoutedEventArgs e)
        {
            if (Switch == true)
            {
                if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
                {
                    var prop = e.GetCurrentPoint(Tela).Properties;
                    if (prop.IsLeftButtonPressed)
                    {
                        /*int index;
                        int column, row;
                        column = (int)(sender as Image).GetValue(Grid.ColumnProperty);
                        row = (int)(sender as Image).GetValue(Grid.RowProperty);
                        index = column * row + column;*/
                        uint index = Encyclopedia.SearchFor((sender as EquipImage).MyEquip);
                        Slot s = GameManager.instance.traderTarget.shop.TradingItems.GetSlot(index);
                        GameManager.instance.traderTarget.shop.SlotInOffer = s;
                        if (s == null) return;
                        ShowOfferItem(s);
                    }
                }
            }
        }
        public void CancelSellingButton(object sender, RoutedEventArgs e)
        {
            CloseOfferItem();
        }
            // Menu
        public void MenuSemiOpenEnter(object sender, PointerRoutedEventArgs e)
        {
            MenuFBolaAtras.Visibility = Visibility.Visible;
        }
        public void MenuSemiOpenExit(object sender, PointerRoutedEventArgs e)
        {
            MenuFBolaAtras.Visibility = Visibility.Collapsed;
        }
        public void MenuOpen(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(Tela).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    MenuAberto.Visibility = Visibility.Visible;
                    MenuFechado.Visibility = Visibility.Collapsed;
                }
            }
        }
        public void MenuClose(object sender, PointerRoutedEventArgs e)
        {
            MenuAberto.Visibility = Visibility.Collapsed;
            MenuFechado.Visibility = Visibility.Visible;
        }
            // Quest
        public void UpdateQuestManagerEvent(object sender, QuestEventArgs e)
        {
            UpdateActualQuestManager();
            UpdateQuestList();
        }
        public void GiveUpButton(object sender, RoutedEventArgs e)
        {
            GameManager.instance.player._Questmanager.GiveUpActualQuest();
        }
        public void ClickAcceptQuestButton(object sender, RoutedEventArgs e)
        {
            Quester npcF = GameManager.instance.npcTarget.GetFunction("Quester") as Quester;
            Quest generic = npcF.myQuest;
            TextQuestName.Text = generic.name;
            TextQuestDescr.Text = generic.Description;
            CanvasQuest.Visibility = Visibility.Collapsed;
        }
        public void ClickDenyQuestButton(object sender, RoutedEventArgs e)
        {
            CanvasActiveQuests.Visibility = Visibility.Collapsed;
            CanvasQuest.Visibility = Visibility.Collapsed;
        }
        
            // Status
        public void XPPlus(object sender, RoutedEventArgs e)
        {
            //GameManager.instance.player.;
            GameManager.instance.player.level.GainEXP(50);
            //GameManager.instance.player.LevelUpdate(0, 0, 0, 0, 0);
        }
        public void MPPlus(object sender, RoutedEventArgs e)
        {
            GameManager.instance.player.AddMP(20);
        }
        public void HPPlus(object sender, RoutedEventArgs e)
        {
            GameManager.instance.player.AddHP(20);
        }
        public void PSTR(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.player._Class.StatsPoints > 0)
            {
                _str++;
                GeralSumStat();
            }
        }
        public void PMND(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.player._Class.StatsPoints > 0)
            {
                _mnd++;
                GeralSumStat();
            }
        }
        public void PSPD(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.player._Class.StatsPoints > 0)
            {
                _spd++;
                GeralSumStat();
            }
        }
        public void PDEX(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.player._Class.StatsPoints > 0)
            {
                _dex++;
                GeralSumStat();
            }
        }
        public void PCON(object sender, RoutedEventArgs e)
        {
            if (GameManager.instance.player._Class.StatsPoints > 0)
            {
                _con++;
                GeralSumStat();
            }
        }
        public void MSTR(object sender, RoutedEventArgs e)
        {
            if (_str > 0)
            {
                _str--;
                GeralSubStat();
            }
        }
        public void MDEX(object sender, RoutedEventArgs e)
        {
            if (_dex > 0)
            {
                _dex--;
                GeralSubStat();
            }
        }
        public void MSPD(object sender, RoutedEventArgs e)
        {
            if (_spd > 0)
            {
                _spd--;
                GeralSubStat();
            }
        }
        public void MCON(object sender, RoutedEventArgs e)
        {
            if (_con > 0)
            {
                _con--;
                GeralSubStat();
            }
        }
        public void MMND(object sender, RoutedEventArgs e)
        {
            if (_mnd > 0)
            {
                _mnd--;
                GeralSubStat();
            }
        }
        public void GeralSumStat()
        {
            GameManager.instance.player._Class.StatsPoints--;
            UpdatePlayerInfo();
        }
        public void GeralSubStat()
        {
            GameManager.instance.player._Class.StatsPoints++;
            UpdatePlayerInfo();
        }

        public void TrocaButton(object sender, RoutedEventArgs e)
        {
            Switch = !Switch;
            ButtonBuy.Content = Switch == true ? "Buy" : "Sell";
            UpdateShopInfo();
        }

        public void ApplyStats(object sender, RoutedEventArgs e)
        {
            GameManager.instance.player.LevelUpdate(_str, _spd, _dex, _con, _mnd);
            _str = _spd = _dex = _con = _mnd = 0;
        }
        
        /* ### SETTING EVENTS ### */

        public void SetEventForSkillBar()
        {
            foreach (UIElement element in GridBarraSkill.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowSkillBarWindow;
                    element.PointerExited += CloseSkillWindow;
                    element.PointerPressed += RemoveSkillFromBar;
                }
            }
        }

        /* ##################################################################################################*/
        /* ######################################## OPEN/CLOSE WINDOW #######################################*/
        /* ##################################################################################################*/

        public void ChestOpen(object sender, ChestEventArgs e)
        {
            CanvasChest.Visibility = Visibility.Visible;
            UpdateChest(e.chest);
        }

        public void ShowSkillTree(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasSkillTree.Visibility == Visibility.Collapsed)
            {
                CanvasSkillTree.Visibility = Visibility.Visible;
            }
            else
            {
                CanvasSkillTree.Visibility = Visibility.Collapsed;
            }
        }
        public void ShowSkillBarWindow(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasSkillWindow.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;

            SkillImage skillEnter = null;
            try
            {
                skillEnter = sender as SkillImage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }

            if (skillEnter == null) return;
            int position = (int)skillEnter.GetValue(Grid.ColumnProperty);

            RealocateWindow(CanvasSkillWindow, mousePosition);
        }
        public void ShowSkillTreeWindow(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasSkillWindow.Visibility == Visibility.Visible)
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
        public void CloseSkillWindow(object sender, PointerRoutedEventArgs e)
        {
            CanvasSkillWindow.Visibility = Visibility.Collapsed;
        }
        public void CloseSkillWindowText(object sender, PointerRoutedEventArgs args)
        {
            CanvasSkillWindow.Visibility = Visibility.Collapsed;
        }
        
        public void ShowAtributes(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasAtributos.Visibility == Visibility.Visible)
                CanvasAtributos.Visibility = Visibility.Collapsed;
            else CanvasAtributos.Visibility = Visibility.Visible;
        }

        public void ShowCrafting(object sender, PointerRoutedEventArgs e)
        {
            CanvasCrafting.Visibility = CanvasCrafting.Visibility == Visibility.Collapsed ?
                                        Visibility.Visible : Visibility.Collapsed;
        }
        
        public void UpdateBagEvent(object sender, BagEventArgs e)
        {
            UpdateBag();
        }
        public void ShowBag(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasInventario.Visibility == Visibility.Collapsed)
                CanvasInventario.Visibility = Visibility.Visible;
            else
                CanvasInventario.Visibility = Visibility.Collapsed;
        }
        public void ShowItemWindow(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasWindowBag.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;
            ItemImage itemImg = (ItemImage)sender;
            RealocateWindow(CanvasWindowBag, mousePosition);
            itemImg.OnItemImageUpdate();
            UpdateItemWindowText(itemImg.myItemPosition, itemImg.myBagRef);

        }
        public void CloseItemWindow(object sender, PointerRoutedEventArgs e)
        {
            CanvasWindowBag.Visibility = Visibility.Collapsed;
        }

        public void ShowEquip(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasEquipamento.Visibility == Visibility.Collapsed)
            {
                CanvasEquipamento.Visibility = Visibility.Visible;
                equipOpen = true;
            }
            else
            {
                CanvasEquipamento.Visibility = Visibility.Collapsed;
                equipOpen = false;
            }
        }
        public void ShowEquipWindow(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasWindowBag.Visibility == Visibility.Visible)
            {
                return;
            }

            Point mousePosition = e.GetCurrentPoint(Tela).Position;
            EquipImage itemImg = (EquipImage)sender;
            RealocateWindow(CanvasWindowBag, mousePosition);
            UpdateItemWindowText(itemImg.MyEquip);
        }

        public void CallConversationBox(NPC npc)
        {
            if (GameManager.instance.interfaceManager.Conversation) return;
            GameManager.instance.npcTarget = npc;
            CanvasConversation.Visibility = Visibility.Visible;
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
                if (npc.GetFunction(f) != null)
                {
                    ListButtons[f].Click += npc.GetFunction(f).MyFunction;
                    ListButtons[f].Visibility = Visibility.Visible;
                }
            }
            TextConv.Text = npc.Introduction;
            TextConvLevel.Text = npc.MyLevel.actuallevel.ToString();
            string convfunc = "";
            foreach (string s in npc.GetFunctionsString())
            {
                convfunc += s + "/";
            }
            TextConvFuncs.Text = convfunc;
            TextConvName.Text = npc.Name;
            lastNPC = npc;
        }
        
        public void ShowQuestEvent(object sender, PointerRoutedEventArgs e)
        {
            CanvasQuestManager.Visibility = CanvasQuestManager.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            UpdateActualQuestManager();
        }
        public void ShowQuestList(object sender, PointerRoutedEventArgs e)
        {
            //RotateTransform ta = (RotateTransform)ImageArrowQuest.RenderTransform;
            if (CanvasQuestList.Visibility == Visibility.Collapsed)
            {
                CanvasQuestList.Visibility = Visibility.Visible;
              //  ta.Angle = 90;
            }
            else
            {
                CanvasQuestList.Visibility = Visibility.Collapsed;
                //ta.Angle = -90;
            }
        }
        public void OpenQuest()
        {
            CanvasQuestManager.Visibility = CanvasQuestManager.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            UpdateActualQuestManager();
        }
        public void CloseQuest()
        {
            TextQuestWindow.Visibility = Visibility.Collapsed;
        }
        public void CloseQuestManagerWindow(object sender, PointerRoutedEventArgs e)
        {
            CanvasQuestManager.Visibility = Visibility.Collapsed;
        }

        public void ShowOfferItem(Slot offerSlot)
        {
            if (offerSlot == null) return;
            CanvasOfferShop.Visibility = Visibility.Visible;
            Item item = Encyclopedia.SearchFor(offerSlot.ItemID);
            ImageItemBuying.Source = Encyclopedia.encycloImages[offerSlot.ItemID];//new BitmapImage(new Uri(BaseUri + item.PathImage));
            TextItemBuyingName.Text = item.Name;
            TextItemBuyingQuantity.Text = offerSlot.ItemAmount.ToString();
            TextItemBuyingValue.Text = (offerSlot.ItemAmount * item.GoldValue).ToString();
        }
        public void OpenShop()
        {
            CanvasAtributos.Visibility = Visibility.Collapsed;
            CanvasShop.Visibility = Visibility.Visible;
            UpdateShopInfo();
        }
        
        public void CloseOfferItem()
        {
            GameManager.instance.traderTarget.shop.SlotInOffer = null;
            CanvasOfferShop.Visibility = Visibility.Collapsed;
        }
        public void CloseShop()
        {
            CanvasShop.Visibility = Visibility.Collapsed;
        }

        /* ###################################################################################################*/
        /* ######################################## TRANSFORM & MISC #########################################*/
        /* ###################################################################################################*/

        public void RealocateWindow(Canvas window, Point mousePosition)
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

        public void DefinitionsGrid(Grid grid, int columnNumber, int rowNumber, int widthCell, int heightCell)
        {
            int r = 0;
            for (int i = 0; i < columnNumber || r < rowNumber; i++, r++)
            {
                if (i < columnNumber)
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
        public void DefinitionsGrid(Grid grid, int columnNumber, int rowNumber, int[] widthCell, int[] heightCell)
        {
            int r = 0;
            for (int i = 0; i < columnNumber || r < rowNumber; i++, r++)
            {
                if (i < columnNumber)
                {
                    ColumnDefinition coldef = new ColumnDefinition() { Width = new GridLength(widthCell[i]) };
                    grid.ColumnDefinitions.Add(coldef);
                }
                if (r < rowNumber)
                {
                    RowDefinition rowdef = new RowDefinition() { Height = new GridLength(heightCell[r]) };
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
                if (column == columnSize - 1)
                {
                    row++;
                    column = -1;
                }
            }
        }
        public void FillGridItemImage(Grid grid, Bag gridBag, int columnSize, int rowSize, int widthImage, int heightImage, EItemOwner owner)
        {
            int column = -1, row = 0;
            for (int i = 0; i < columnSize * rowSize; i++)
            {
                ItemImage item;
                column++;
                item = gridBag != null ? new ItemImage(i, widthImage, heightImage, gridBag, owner) : new ItemImage(i, widthImage, heightImage);
                grid.Children.Add(item);
                Grid.SetColumn(item, column);
                Grid.SetRow(item, row);
                if (column == columnSize - 1)
                {
                    row++;
                    column = -1;
                }
            }
        }
        public void FillGridSkillImage(Grid grid, bool tree, Player player, int columnSize, int rowSize, int widthImage, int heightImage)
        {
            List<SkillGenerics> skills;
            int c = 0;
            if (player == null) return;
            if (tree) {
                skills = player._SkillManager.SkillList;
            } else
            {
                skills = player._SkillManager.SkillBar.ToList();
                c = -1;
            }
            int column = -1, row = 0;
            for (int i = 0; i < columnSize * rowSize; i++)
            {
                SkillImage item = null;
                column++;
                if(c == -1 && i == 0)
                {
                    item = new SkillImage(widthImage, heightImage, player._SkillManager.Passive, true);
                } else
                {
                    item = new SkillImage(widthImage, heightImage, skills[i + c], !tree);
                }
                grid.Children.Add(item);
                Grid.SetColumn(item, column);
                Grid.SetRow(item, row);
                if (column == columnSize - 1)
                {
                    row++;
                    column = -1;
                }
            }
        }
        /* ###################################################################################################*/


        //public void CreateMob()
        //{
        //    GameManager.instance.mob = new Mob(level: 2);
        //    GameManager.instance.mob.Spawn(250, 20);
        //    MobSlot smb;
        //    smb = new MobSlot(2, 10, 0.99);
        //    if(smb.Drop()) GameManager.instance.mob.MobBag.AddToBag(smb);
        //    smb = new MobSlot(23, 10, 0.3);
        //    if(smb.Drop()) GameManager.instance.mob.MobBag.AddToBag(smb);
        //    smb = new MobSlot(24, 10, 0.3);
        //    if(smb.Drop()) GameManager.instance.mob.MobBag.AddToBag(smb);
        //    smb = new MobSlot(25, 10, 0.1);
        //    if(smb.Drop()) GameManager.instance.mob.MobBag.AddToBag(smb);
        //    TheScene.Children.Add(GameManager.instance.mob.box);
        //}
        /*public void CreatePlayer()
        {
            if (PlayerCreated != null)
            {
                GameManager.instance.player = new Player("2220000");//PlayerCreated.Id
            }
            else
            {
                GameManager.instance.player = new Player("2222000");
            }
            GameManager.instance.player.Spawn(300, -100);
            GameManager.instance.player.Damage = 0.1;
            TheScene.Children.Add(GameManager.instance.player.box);
            //GameManager.instance.player.box.Background = new SolidColorBrush(Color.FromArgb(155, 255, 0, 127));
        }*/

    }
}
