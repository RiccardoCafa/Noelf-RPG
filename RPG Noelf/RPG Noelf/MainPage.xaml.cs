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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPG_Noelf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainPage : Page
    {
        Thread Start;
        List<CharacterPlayer> players = new List<CharacterPlayer>();
        CharacterPlayer player;
        CharacterMob mob;
        MainCamera mainCamera;
        Shop shopper = new Shop();
        InterfaceManager interfaceManager = new InterfaceManager();
        Player p1, p2;
        public TextBlock mobStatus;
        public static MainPage instance;
        public string MobText;

        public Dictionary<string, Image> MobImages;
        public Dictionary<string, Image> PlayerImages;
        public Dictionary<string, Image> ClothesImages;

        public static Canvas Telona;
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
            // Settando Janelas de Interface
            interfaceManager.Inventario = InventarioWindow;

            Encyclopedia.LoadItens();

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                mobStatus = xMobStatus;
                Windows.UI.Xaml.Window.Current.CoreWindow.KeyDown += Skill_KeyDown;
                Scene elel = new Scene(xScene);//criaçao do cenario
                // Settando o player
                player = new CharacterPlayer(PlayerCanvas, new Player("0000000", PlayerImages, ClothesImages));//criaçao do player
                player.Player.Status(xPlayerStatus);//fornecimento das informaçoes do player (temporario)
                player.UpdateBlocks(xScene);
                mainCamera = new MainCamera(player, Camera, Chunck01);
                players.Add(player);
                mob = new CharacterMob(MobCanvas, players, new Mob(MobImages, level: 100));//criaçao do mob
                mob.Mob.Status(xMobStatus);//fornecimento das informaçoes do mob (temporario)
                mob.UpdateBlocks(xScene);
            });

            p1 = player.Player;

            uint banana = 1;
            uint jorro = 2;
            uint espadona = 3;
            uint potion = 4;

            shopper.TradingItems.AddToBag(1, Bag.MaxStack);
            shopper.TradingItems.AddToBag(2, Bag.MaxStack);
            shopper.TradingItems.AddToBag(3, Bag.MaxStack);

            #region InvTest

            p1._Inventory.AddGold(50);

            p1._Inventory.AddToBag(banana, 1);
            p1._Inventory.AddToBag(jorro, 1);
            p1._Inventory.AddToBag(banana, 1);
            p1._Inventory.AddToBag(jorro, 1);
            p1._Inventory.AddToBag(banana, 1);
            p1._Inventory.AddToBag(jorro, 1);
            p1._Inventory.AddToBag(banana, 1);
            p1._Inventory.AddToBag(jorro, 1);
            /*
            p1._Inventory.RemoveFromBag(jorro, 1);
            p1._Inventory.RemoveFromBag(jorro, 1);
            p1._Inventory.RemoveFromBag(jorro, 1);
            p1._Inventory.RemoveFromBag(jorro, 1);
            */
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);

            p1._Inventory.AddToBag(potion, 1);
            p1._Inventory.AddToBag(potion, 1);
            p1._Inventory.AddToBag(potion, 1);

            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);
            p1._Inventory.AddToBag(espadona, 1);

            p1._Inventory.RemoveFromBag(espadona, 1);
            p1._Inventory.RemoveFromBag(espadona, 1);

            p1._Inventory.RemoveFromBag(potion, 1);

            p1._Inventory.RemoveFromBag(banana, 1);

            p1._Inventory.AddToBag(27, 1);
            p1._Inventory.AddToBag(35, 1);

            #endregion


            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
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
                case Windows.System.VirtualKey.E:
                    if (WindowEquipamento.Visibility == Visibility.Collapsed)
                    {
                        if (shopOpen)
                        {
                            shopOpen = false;
                            ShopWindow.Visibility = Visibility.Collapsed;
                        }
                        WindowEquipamento.Visibility = Visibility.Visible;
                        equipOpen = true;
                    }
                    else
                    {
                        equipOpen = false;
                        WindowEquipamento.Visibility = Visibility.Collapsed;
                    }
                    break;
                case Windows.System.VirtualKey.O:
                    if (ShopWindow.Visibility == Visibility.Collapsed)
                    {
                        if (equipOpen)
                        {
                            equipOpen = false;
                            WindowEquipamento.Visibility = Visibility.Collapsed;
                        }
                        UpdateShopInfo();
                        ShopWindow.Visibility = Visibility.Visible;
                        shopOpen = true;
                    }
                    else
                    {
                        shopOpen = false;
                        ShopWindow.Visibility = Visibility.Collapsed;
                    }
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

        #region Player Updates
        public void UpdatePlayerInfo()
        {
            PlayerInfo.Text = p1.Race.NameRace + " " + p1._Class.ClassName + "\n";
            PlayerInfo.Text += "Atributos: ( " + p1._Class.StatsPoints + " pontos)\n" +
                                "Força: " + p1.Str + " + " + _str + "\n" +
                                "Mente: " + p1.Mnd + " + " + _mnd + "\n" +
                                "Velocidade: " + p1.Spd + " + " + _spd + "\n" +
                                "Destreza: " + p1.Dex + " + " + _dex + "\n" +
                                "Constituição: " + p1.Con + " + " + _con + "\n\n" +
                                "HP: " + p1.Hp + "/" + p1.HpMax + "\n" +
                                "MP: " + p1.Mp + "/" + p1.MpMax + "\n" +
                                "Damage: " + p1.Damage + "\n" +
                                "Atack Speed: " + p1.AtkSpd + "\n" +
                                "Armor: " + p1.Armor + "\n\n" +
                                "Level: " + p1.Level + "\n" +
                                "Experience: " + p1.Xp + "/" + p1.XpLim + "\n" +
                                "Pontos de skill disponivel: " + p1._SkillManager.SkillPoints + "\n" +
                                "Gold: " + p1._Inventory.Gold;
        }
        public void UpdateSkillTree()
        {
            int cont = 0;
            foreach (UIElement element in SkillsTree.Children)
            {
                Image img = element as Image;
                if (cont < p1._SkillManager.SkillList.Count)
                    img.Source = new BitmapImage(new Uri(this.BaseUri, p1._SkillManager.SkillList.ElementAt(cont).pathImage));
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
                    (element as Image).Source = new BitmapImage(new Uri(this.BaseUri, p1._SkillManager.Passive.pathImage));
                }
                else
                {
                    if (p1._SkillManager.SkillBar[cont - 1] != null)
                        (element as Image).Source = new BitmapImage(new Uri(this.BaseUri, p1._SkillManager.SkillBar[cont - 1].pathImage));
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
                    Slot s = p1._Inventory.GetSlot(index);
                    if (s == null) return;
                    if (shopOpen)
                    {
                        shopper.SlotInOffer = s;
                        ShowOfferItem(s);
                        UpdateShopInfo();
                    }
                    if (equipOpen)
                    {
                        Item i = Encyclopedia.encyclopedia[s.ItemID];
                        if (i is Armor || i is Weapon)
                        {
                            p1.Equipamento.UseEquip(s.ItemID);
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
                        s = new Slot(p1.Equipamento.armor[row], 1);
                    }
                    else
                    {
                        s = new Slot(p1.Equipamento.weapon, 1);
                    }
                    if (s == null || s.ItemID == 0) return;
                    p1.Equipamento.DesEquip(s.ItemID);
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
                    p1._SkillManager.SkillBar[position - 1] = null;
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
                    skillClicked = p1._SkillManager.SkillList[position];
                    if (p1._SkillManager.UpSkill(skillClicked))
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

                    skillClicked = p1._SkillManager.SkillList[position];
                    if (skillClicked.Unlocked == false) return;
                    p1._SkillManager.ChangeSkill(skillClicked);
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
                skillInfo = p1._SkillManager.Passive;
            }
            else
            {
                skillInfo = p1._SkillManager.SkillBar[position - 1];
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
                itemInfo = new Slot(p1.Equipamento.armor[rowPosition], 1);
            }
            else
            {
                itemInfo = new Slot(p1.Equipamento.weapon, 1);
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

            for (int i = 0; i < p1._Inventory.Slots.Count; i++)
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
                    slot.Source = new BitmapImage(new Uri(this.BaseUri, Encyclopedia.encyclopedia[p1._Inventory.Slots[i].ItemID].PathImage));
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

            if (position < p1._Inventory.Slots.Count)
            {
                itemInfo = p1._Inventory.Slots[position];
            }
            if (itemInfo == null) return;

            RealocateWindow(WindowBag, mousePosition);

            UpdateItemWindowText(itemInfo);

        }
        #endregion
        #region Shop
        public void UpdateShopInfo()
        {
            int count = 0;
            foreach (Image img in ShopGrid.Children)
            {
                if (Switch == false)
                {
                    if (count >= shopper.BuyingItems.Slots.Count) img.Source = new BitmapImage();
                    else
                    {
                        Slot s = shopper.BuyingItems.GetSlot(count);
                        img.Source = new BitmapImage(new Uri(this.BaseUri, Encyclopedia.SearchFor(s.ItemID).PathImage));
                    }
                }
                else
                {
                    if (count >= shopper.TradingItems.Slots.Count) img.Source = new BitmapImage();
                    else
                    {
                        Slot s = shopper.TradingItems.GetSlot(count);
                        img.Source = new BitmapImage(new Uri(this.BaseUri, Encyclopedia.SearchFor(s.ItemID).PathImage));
                    }
                }

                count++;
            }
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
                if (position < shopper.BuyingItems.Slots.Count)
                {
                    itemInfo = shopper.BuyingItems.Slots[position];
                }
                if (itemInfo == null) return;
            }
            else
            {
                if (position < shopper.TradingItems.Slots.Count)
                {
                    itemInfo = shopper.TradingItems.Slots[position];
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
            shopper.SlotInOffer = null;
            ItemToSellBuy.Visibility = Visibility.Collapsed;
        }

        private void CloseItemWindow(object sender, PointerRoutedEventArgs e)
        {
            WindowBag.Visibility = Visibility.Collapsed;
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
                        Slot s = shopper.TradingItems.GetSlot(index);
                        shopper.SlotInOffer = s;
                        if (s == null) return;
                        ShowOfferItem(s);
                    }
                }
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
                if (p1._SkillManager.SkillList.Count >= 1)
                {
                    indicadorzao = 0;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number2)
            {
                if (p1._SkillManager.SkillList.Count >= 2)
                {
                    indicadorzao = 1;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number3)
            {
                if (p1._SkillManager.SkillList.Count >= 3)
                {
                    indicadorzao = 2;
                }
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Number4)
            {
                if (p1._SkillManager.SkillList.Count >= 4)
                {
                    indicadorzao = 3;
                }
            }
            string s;
            if (p1._SkillManager.SkillBar[indicadorzao] != null)
            {
                s = (p1._SkillManager.SkillBar[indicadorzao]).UseSkill(p1, p2).ToString();
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
                    MaxValue = p1._Inventory.GetSlot(shopper.SlotInOffer.ItemID).ItemAmount;
                }
                else MaxValue = shopper.TradingItems.GetSlot(shopper.SlotInOffer.ItemID).ItemAmount;

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
        private void ClickNewMob(object sender, RoutedEventArgs e)
        {
            int level;
            int.TryParse(xLevelBox.Text, out level);
            mob.Mob = new Mob(MobImages, level);
            mob.Mob.Status(xMobStatus);
        }

        private void ClickCustom(object sender, RoutedEventArgs e)
        {
            string id = player.Player.Id;

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
            player.Player = new Player(id, PlayerImages, ClothesImages);
            player.Player.Status(xPlayerStatus);
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
            if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
            {
                if (Switch == false)
                {

                    if (val <= Bag.MaxStack)
                    {
                        if (p1._Inventory.RemoveFromBag(shopper.SlotInOffer.ItemID, val))
                        {
                            Slot newSlot = new Slot(shopper.SlotInOffer.ItemID, val);
                            shopper.AddToBuyingItems(newSlot);
                            shopper.SlotInOffer = null;
                            UpdateShopInfo();
                            CloseOfferItem();
                        }
                    }
                }
                else
                {
                    if (val <= Bag.MaxStack)
                    {
                        Slot newSlot = new Slot(shopper.SlotInOffer.ItemID, val);
                        shopper.SellItem(newSlot, p1._Inventory);
                        CloseOfferItem();
                    }
                }

            }
        }

        private void IncrementOfferAmount(object sender, RoutedEventArgs e)
        {
            if (shopper.SlotInOffer == null) return;
            if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
            {
                uint MaxValue = p1._Inventory.GetSlot(shopper.SlotInOffer.ItemID).ItemAmount;
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
                shopper.BuyItem(p1._Inventory);
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
            p1._Class.StatsPoints--;
            UpdatePlayerInfo();
        }

        private void GeralSubStat()
        {
            p1._Class.StatsPoints++;
            UpdatePlayerInfo();
        }

        private void XPPlus(object sender, RoutedEventArgs e)
        {
            p1.XpLevel(50);
            UpdatePlayerInfo();
        }

        private void MPPlus(object sender, RoutedEventArgs e)
        {
            p1.AddMP(20);
            UpdatePlayerInfo();
        }

        private void HPPlus(object sender, RoutedEventArgs e)
        {
            p1.AddHP(20);
            UpdatePlayerInfo();
        }

        private void PSTR(object sender, RoutedEventArgs e)
        {
            if (p1._Class.StatsPoints > 0)
            {
                _str++;
                GeralSumStat();
            }
        }

        private void PMND(object sender, RoutedEventArgs e)
        {
            if (p1._Class.StatsPoints > 0)
            {
                _mnd++;
                GeralSumStat();
            }
        }

        private void PSPD(object sender, RoutedEventArgs e)
        {
            if (p1._Class.StatsPoints > 0)
            {
                _spd++;
                GeralSumStat();
            }
        }

        private void PDEX(object sender, RoutedEventArgs e)
        {
            if (p1._Class.StatsPoints > 0)
            {
                _dex++;
                GeralSumStat();
            }
        }

        private void PCON(object sender, RoutedEventArgs e)
        {
            if (p1._Class.StatsPoints > 0)
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
            p1.LevelUpdate(_str, _spd, _dex, _con, _mnd);
            _str = _spd = _dex = _con = _mnd = 0;
            UpdatePlayerInfo();
        }
        #endregion
        #endregion

        #endregion

    }
}
