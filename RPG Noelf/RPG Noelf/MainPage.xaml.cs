using RPG_Noelf.Assets.Scripts.PlayerFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml.Media;
using RPG_Noelf.Assets.Scripts.Interface;
using System.Threading;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using RPG_Noelf.Assets.Scripts.Skills;
using Windows.UI.Input;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.Shop_Scripts;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Mobs;
using RPG_Noelf.Assets.Scripts.Ents.Mobs;
using RPG_Noelf.Assets.Scripts.Scenes;
using RPG_Noelf.Assets.Scripts.Enviroment;
using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using RPG_Noelf.Assets.Scripts.General;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPG_Noelf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainPage : Page
    {
        public static MainPage instance;

        Thread Start;

        public TextBlock mobStatus;
        public TextBlock dayText;
        public string MobText;

        public Dictionary<string, Image> MobImages;
        public Dictionary<string, Image> PlayerImages;
        public Dictionary<string, Image> ClothesImages;

        public static Canvas Telona;
        public static Canvas ActualChunck;
        public static Canvas inventarioWindow;
        public static Canvas TheScene;
        public string test;

        public static TextBlock texticulus;
        public static int i;

        public bool Switch = false;
        public bool shopOpen = false;
        public bool equipOpen = false;

        int _str, _spd, _dex, _con, _mnd;

        public MainPage()
        {
            instance = this;
            this.InitializeComponent();

            Telona = Tela;
            dayText = DayText;
            inventarioWindow = InventarioWindow;
            TheScene = xScene;
            
            Application.Current.DebugSettings.EnableFrameRateCounter = true;
            Window.Current.CoreWindow.KeyUp += WindowControl;
            Start = new Thread(start);
            Start.Start();
            PlayerImages = new Dictionary<string, Image>()
            {
                {"armsd0", xPlayerArm_d0 },
                {"armsd1", xPlayerArm_d1 },
                {"armse0", xPlayerArm_e0 },
                {"armse1", xPlayerArm_e1 },
                {"body", xPlayerBody },
                {"head", xPlayerHead },
                {"eye", xPlayerEye },
                {"hair", xPlayerHair },
                {"legsd0", xPlayerLeg_d0 },
                {"legsd1", xPlayerLeg_d1 },
                {"legse0", xPlayerLeg_e0 },
                {"legse1", xPlayerLeg_e1 }
            };
            ClothesImages = new Dictionary<string, Image>()
            {
                {"armsd0", xClothArm_d0 },
                {"armsd1", xClothArm_d1 },
                {"armse0", xClothArm_e0 },
                {"armse1", xClothArm_e1 },
                {"body", xClothBody },
                {"legsd0", xClothLeg_d0 },
                {"legsd1", xClothLeg_d1 },
                {"legse0", xClothLeg_e0 },
                {"legse1", xClothLeg_e1 }
            };
            MobImages = new Dictionary<string, Image>()
            {
                { "head",xMobHead },
                { "body",xMobBody },
                { "armsd0", xMobArm_d0 },
                { "armsd1", xMobArm_d1 },
                { "armse0", xMobArm_e0 },
                { "armse1", xMobArm_e1 },
                { "legsd0", xMobLeg_d0 },
                { "legsd1", xMobLeg_d1 },
                { "legse0", xMobLeg_e0 },
                { "legse1", xMobLeg_e1 }
            };
        }

        public async void start()
        {
            _str = _spd = _dex = _con = _mnd = 0;


            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                mobStatus = xMobStatus;
                Window.Current.CoreWindow.KeyDown += Skill_KeyDown;
                Scene elel = new Scene(xScene);//criaçao do cenario
                CreatePlayer();
                CreateMob();
                GameManager.InitializeGame();
                GameManager.mainCamera = new MainCamera(GameManager.characterPlayer, Camera, Chunck01);

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
                InventarioWindow.Visibility = Visibility.Collapsed;
                Atributos.Visibility = Visibility.Collapsed;
                WindowEquipamento.Visibility = Visibility.Collapsed;
                WindowTreeSkill.Visibility = Visibility.Collapsed;
            });

        }

        private void WindowControl(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case Windows.System.VirtualKey.B:
                    if (InventarioWindow.Visibility == Visibility.Collapsed)
                    {
                        UpdateBag();
                        InventarioWindow.Visibility = Visibility.Visible;
                    }
                    else InventarioWindow.Visibility = Visibility.Collapsed;
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
        #region Character Creation
        public void CreateMob()
        {
            GameManager.mobTarget = new CharacterMob(MobCanvas, GameManager.players, new Mob(MobImages, level: 100));//criaçao do mob
            GameManager.mobTarget.Mob.Status(xMobStatus);//fornecimento das informaçoes do mob (temporario)
            GameManager.mobTarget.UpdateBlocks(xScene);
        }
        public void CreatePlayer()
        {
            GameManager.characterPlayer = new CharacterPlayer(PlayerCanvas, new Player("0000000", PlayerImages, ClothesImages));//criaçao do player
            GameManager.characterPlayer.Player.Status(xPlayerStatus);//fornecimento das informaçoes do player (temporario)
            GameManager.characterPlayer.UpdateBlocks(xScene);
            GameManager.player = GameManager.characterPlayer.Player;
            GameManager.players.Add(GameManager.characterPlayer);
        }
        public Canvas CreateCharacterNPC()
        {
            return NPCCanvas;
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
            foreach(UIElement element in EquipWindow.Children)
            {
                if(element is Image)
                {
                    element.PointerEntered += ShowEquipWindow;
                    element.PointerExited += CloseItemWindow;
                    element.PointerPressed += DesequiparEvent;
                }
            }
        }

        private void InventorySlotEvent(object sender, PointerRoutedEventArgs e)
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
                    Slot s = GameManager.player._Inventory.GetSlot(index);
                    if (s == null) return;
                    if (shopOpen)
                    {
                        GameManager.traderTarget.shop.SlotInOffer = s;
                        ShowOfferItem(s);
                        UpdateShopInfo();
                    }
                    if (equipOpen)
                    {
                        Item i = Encyclopedia.encyclopedia[s.ItemID];
                        if (i is Armor || i is Weapon)
                        {
                            GameManager.player.Equipamento.UseEquip(s.ItemID);
                            WindowBag.Visibility = Visibility.Collapsed;
                        }
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
                    Slot s = null;
                    if (column == 0)
                    {
                        s = new Slot(GameManager.player.Equipamento.armor[row], 1);
                    }
                    else
                    {
                        s = new Slot(GameManager.player.Equipamento.weapon, 1);
                    }
                    if (s == null || s.ItemID == 0) return;
                    GameManager.player.Equipamento.DesEquip(s.ItemID);
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
                    int position = InventarioGrid.ColumnDefinitions.Count * rowPosition + columnPosition;
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
                    int position = InventarioGrid.ColumnDefinitions.Count * rowPosition + columnPosition;
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
                    int position = InventarioGrid.ColumnDefinitions.Count * rowPosition + columnPosition;

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
                itemInfo = new Slot(GameManager.player.Equipamento.armor[rowPosition], 1);
            }
            else
            {
                itemInfo = new Slot(GameManager.player.Equipamento.weapon, 1);
            }
            if (itemInfo.ItemID == 0) return;

            RealocateWindow(WindowBag, mousePosition);

            UpdateItemWindowText(itemInfo);

        }
        private void CloseSkillWindow(object sender, PointerRoutedEventArgs e)
        {
            WindowSkill.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region Inventario
        public void UpdateBag()
        {

            for (int i = 0; i < GameManager.player._Inventory.Slots.Count; i++)
            {
                int column = i, row = i;
                row = i / 6;
                while (column > 5) column -= 6;

                var slotTemp = from element in InventarioGrid.Children
                               where (int)element.GetValue(Grid.ColumnProperty) == column && (int)element.GetValue(Grid.RowProperty) == row
                               select element;
                if (slotTemp != null)
                {
                    Image slot = (Image)slotTemp.ElementAt(0);
                    slot.Source = new BitmapImage(new Uri(this.BaseUri, Encyclopedia.encyclopedia[GameManager.player._Inventory.Slots[i].ItemID].PathImage));
                }

            }
        }
        private void UpdateItemWindowText(Slot slot)
        {
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
            foreach (UIElement element in InventarioGrid.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowItemWindow;
                    element.PointerExited += CloseItemWindow;
                    element.PointerPressed += InventorySlotEvent;
                }
            }
        }

        private void ShowItemWindow(object sender, PointerRoutedEventArgs e)
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
            int position = InventarioGrid.ColumnDefinitions.Count * rowPosition + columnPosition;

            Slot itemInfo = null;

            if (position < GameManager.player._Inventory.Slots.Count)
            {
                itemInfo = GameManager.player._Inventory.Slots[position];
            }
            if (itemInfo == null) return;

            RealocateWindow(WindowBag, mousePosition);

            UpdateItemWindowText(itemInfo);

        }
        private void CloseItemWindow(object sender, PointerRoutedEventArgs e)
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

            UpdateItemWindowText(itemInfo);

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
            if(Switch == true)
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
        private NPC npc;
        private Grid ButtonsGrid;
        private Queue<Button> QueueButtons = new Queue<Button>();
        private List<Button> PoolButtons = new List<Button>();
        public void CallConversationBox(NPC npc)
        {
            if (GameManager.interfaceManager.Conversation) return;
            this.npc = npc;
            Conversation.Visibility = Visibility.Visible;
            int Buttons = npc.GetFunctionSize() + 1;
            List<string> funcString = npc.GetFunctionsString();
            ButtonsGrid = new Grid();
            ButtonsGrid.Width = Conversation.Width;
            ButtonsGrid.Height = Conversation.Height / 2;
            Conversation.Children.Add(ButtonsGrid);
            ButtonsGrid.SetValue(Canvas.LeftProperty, ButtonsGrid.Height / 2);
            ColumnDefinition column = new ColumnDefinition() {
                Width = new GridLength(ButtonsGrid.Width)
            };
            ButtonsGrid.ColumnDefinitions.Add(column);
            for(int i = 0; i < Buttons; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(ButtonsGrid.Height / Buttons)
                };
                ButtonsGrid.RowDefinitions.Add(row);
                Button b;
                if (QueueButtons.Count > 0)
                {
                    b = QueueButtons.Dequeue();
                    b.Visibility = Visibility.Visible;
                } else
                {
                    b = new Button
                    {
                        Height = (ButtonsGrid.Height / Buttons) - 10,
                        Width = ButtonsGrid.Height
                    };
                    ButtonsGrid.Children.Add(b);
                    PoolButtons.Add(b);
                }
                
                if (i < Buttons - 1)
                {
                    b.Content = funcString[i];
                    b.Click += npc.GetFunction(funcString[i]).MyFunction;
                }
                else
                {
                    b.Content = "Exit";
                    b.Click += HasToCloseConv;
                }
                b.SetValue(Grid.RowProperty, i);
            }
            ConvText.Text = npc.Introduction;
            ConvLevel.Text = npc.MyLevel.actuallevel.ToString();
            string convfunc = "";
            foreach(string s in npc.GetFunctionsString())
            {
                convfunc += s + "/";
            }
            ConvFuncs.Text = convfunc;
            ConvName.Text = npc.Name;
        }

        public void HasToCloseConv(object sender, RoutedEventArgs e)
        {
            if (GameManager.interfaceManager.ConvHasToClose != false) return;
            ConvText.Text = npc.Conclusion;
            npc.EndConversation();
            foreach(Button b in PoolButtons)
            {
                QueueButtons.Enqueue(b);
                b.Visibility = Visibility.Collapsed;
            }
        }

        public void CloseConversationBox(object sender, RoutedEventArgs e)
        {
            Conversation.Visibility = Visibility.Collapsed;
        }

        public void EndConversation(object sender, RoutedEventArgs e)
        {
            if (GameManager.interfaceManager.ConvHasToClose)
            {
                Conversation.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
        #region General
        private void RealocateWindow(Canvas window, Point mousePosition)
        {
            window.Visibility = Visibility.Visible;

            window.SetValue(Canvas.LeftProperty, mousePosition.X);

            if (mousePosition.Y >= Tela.Height / 2)
            {
                window.SetValue(Canvas.TopProperty, mousePosition.Y - window.Height - 10);
            }
            else
            {
                window.SetValue(Canvas.TopProperty, mousePosition.Y);
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
                if(GameManager.mobTarget.Mob != null)
                {
                    s = (GameManager.player._SkillManager.SkillBar[indicadorzao]).UseSkill(GameManager.player, GameManager.mobTarget.Mob).ToString();
                }
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

        }
        private void ClickDenyQuestButton(object sender, RoutedEventArgs e)
        {

        } 
        private void ClickNewMob(object sender, RoutedEventArgs e)
        {
            int level;
            int.TryParse(xLevelBox.Text, out level);
            GameManager.mobTarget.Mob = new Mob(MobImages, level);
            GameManager.mobTarget.Mob.Status(xMobStatus);
        }

        private void ClickCustom(object sender, RoutedEventArgs e)
        {
            string id = GameManager.characterPlayer.Player.Id;

            if (sender == xEsqRace || sender == xDirRace)
            {
                id = ChangeCustom(id[0], 3, sender == xDirRace) + id.Substring(1, 6);
            }
            else if (sender == xEsqClass || sender == xDirClass)
            {
                id = id.Substring(0, 1) + ChangeCustom(id[1], 3, sender == xDirClass) + id.Substring(2, 5);
            }
            else if (sender == xEsqSex || sender == xDirSex)
            {
                id = id.Substring(0, 2) + ChangeCustom(id[2], 2, sender == xDirSex) + id.Substring(3, 4);
            }
            else if (sender == xEsqSkinTone || sender == xDirSkinTone)
            {
                id = id.Substring(0, 3) + ChangeCustom(id[3], 3, sender == xDirSkinTone) + id.Substring(4, 3);
            }
            else if (sender == xEsqEyeColor || sender == xDirEyeColor)
            {
                id = id.Substring(0, 4) + ChangeCustom(id[4], 3, sender == xDirEyeColor) + id.Substring(5, 2);
            }
            else if (sender == xEsqHairStyle || sender == xDirHairStyle)
            {
                id = id.Substring(0, 5) + ChangeCustom(id[5], 4, sender == xDirHairStyle) + id.Substring(6, 1);
            }
            else if (sender == xEsqHairColor || sender == xDirHairColor)
            {
                id = id.Substring(0, 6) + ChangeCustom(id[6], 3, sender == xDirHairColor);
            }
            GameManager.characterPlayer.Player = new Player(id, PlayerImages, ClothesImages);
            GameManager.characterPlayer.Player.Status(xPlayerStatus);
        }

        private string ChangeCustom(char current, int range, bool isNext)
        {
            int.TryParse(current.ToString(), out int x);
            if (isNext)
            {
                if (x == range - 1) x = 0;
                else x++;
            }
            else
            {
                if (x == 0) x = range - 1;
                else x--;
            }
            return x.ToString();
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
            UpdatePlayerInfo();
        }

        private void MPPlus(object sender, RoutedEventArgs e)
        {
            GameManager.player.AddMP(20);
            UpdatePlayerInfo();
        }

        private void HPPlus(object sender, RoutedEventArgs e)
        {
            GameManager.player.AddHP(20);
            UpdatePlayerInfo();
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
            UpdatePlayerInfo();
        }
        #endregion
        #endregion

        #endregion

    }
}
