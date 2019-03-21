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
        public static MainPage instance;
        public Dictionary<string, Image> images = new Dictionary<string, Image>();
        public string MobText;

        public static Canvas Telona;

        public static TextBlock texticulus;
        public static int i;

        public bool shopOpen = true;

        int _str, _spd, _dex, _con, _mnd;

        public MainPage()
        {
            instance = this;
            this.InitializeComponent();

            Telona = Tela;

            Application.Current.DebugSettings.EnableFrameRateCounter = true;
            Start = new Thread(start);
            Start.Start();
        }
        
        public async void start()
        {
            _str = _spd = _dex = _con = _mnd = 0;
            // Settando Janelas de Interface
            interfaceManager.Inventario = InventarioWindow;

            Encyclopedia.LoadItens();

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Windows.UI.Xaml.Window.Current.CoreWindow.KeyDown += Skill_KeyDown;
                // Settando o player
                player = new CharacterPlayer(PlayerCanvas);
                player.UpdateBlocks(PlatChunck);
                player.ResetPosition(320, 40);
                mainCamera = new MainCamera(player, Camera, Chunck01);
                players.Add(player);
                {
                    images[face.Name] = face;
                    images[body.Name] = body;
                    images[arm_d0.Name] = arm_d0;
                    images[arm_d1.Name] = arm_d1;
                    images[arm_e0.Name] = arm_e0;
                    images[arm_e1.Name] = arm_e1;
                    images[leg_d0.Name] = leg_d0;
                    images[leg_d1.Name] = leg_d1;
                    images[leg_e0.Name] = leg_e0;
                    images[leg_e1.Name] = leg_e1;
                }
                mob = new CharacterMob(MobCanvas, players, Spawn.CreateMob());
                mob.UpdateBlocks(PlatChunck);
                mob.ResetPosition(920, 40);
            });

            p1 = new Player("1", IRaces.Orc, IClasses.Warrior)
            {
                Armor = 5
            };
            p2 = new Player("2", IRaces.Human, IClasses.Wizard)
            {
                Armor = 2
            };

            uint banana = 1;
            uint jorro = 2;
            uint espadona = 3;
            uint potion = 4;


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

            #endregion
            

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                UpdateBag();
                LoadSkillTree();
                UpdatePlayerInfo();
                UpdateSkillBar();
                UpdateShopInfo();
                SetEventForSkillBar();
                SetEventForSkillTree();
                SetEventForBagItem();
                SetEventForShopItem();
            });
        }

        public static async void UpdateTexti()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                texticulus.Text = (i / 2).ToString();
            });
        }

        public static void SetImageSource(Image img, string path)
        {
            img.Source = new BitmapImage(new Uri(instance.BaseUri, path));
        }

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

        private void UpdateShopInfo()
        {
            int count = 0;
            foreach (Image img in ShopGrid.Children)
            {
                if (count >= shopper.BuyingItems.Slots.Count) img.Source = new BitmapImage();
                else
                {
                    Slot s = shopper.BuyingItems.GetSlot(count);
                    img.Source = new BitmapImage(new Uri(this.BaseUri, Encyclopedia.SearchFor(s.ItemID).PathImage));
                }
                count++;
            }
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


        #region Events
        public void InventorySlotEvent(object sender, PointerRoutedEventArgs e)
        {
            if (shopOpen)
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
                        shopper.SlotInOffer = s;
                        ShowOfferItem(s);
                        UpdateShopInfo();
                    }
                }
            }
        }

        public void RemoveSkillFromBar(object sender, PointerRoutedEventArgs e)
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

        public void SkillTreePointerEvent(object sender, PointerRoutedEventArgs e)
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

                    if (p1._SkillManager.SkillPoints > 0)
                    {
                        if (skillClicked.Unlocked)
                        {
                            p1._SkillManager.UpSkill(skillClicked); // returns true if sucessfully up
                        }
                        else if (skillClicked.block <= p1.Level)
                        {
                            p1._SkillManager.UnlockSkill(position);
                            for (int i = 0; i < 3; i++)
                            {
                                if (p1._SkillManager.SkillBar[i] == null)
                                {
                                    p1._SkillManager.AddSkillToBar(skillClicked, i);
                                    break;
                                }
                            }
                        }
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
                    if (skillClicked.tipo == SkillType.habilite)
                    {
                        foreach (SkillGenerics s in p1._SkillManager.SkillBar)
                        {
                            if (s != null)
                            {
                                if (s.Equals(skillClicked))
                                {
                                    return;
                                }
                            }
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            if (p1._SkillManager.SkillBar[i] == null)
                            {
                                p1._SkillManager.SkillBar[i] = skillClicked;
                                UpdateSkillBar();
                                break;
                            }
                        }
                    }
                    else if (skillClicked.tipo == SkillType.ultimate)
                    {
                        if (p1._SkillManager.SkillBar[3] == null)
                        {
                            p1._SkillManager.SkillBar[3] = skillClicked;
                            UpdateSkillBar();
                        }
                    }
                }
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

        public void LoadSkillTree()
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

        private void SetEventForShopItem()
        {
            foreach (UIElement element in ShopGrid.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowItemBuying;
                    element.PointerExited += CloseItemBuying;
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

        private void CloseItemBuying(object sender, PointerRoutedEventArgs e)
        {
            WindowBag.Visibility = Visibility.Collapsed;
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

            if (position < shopper.BuyingItems.Slots.Count)
            {
                itemInfo = shopper.BuyingItems.Slots[position];
            }
            if (itemInfo == null) return;

            RealocateWindow(WindowBag, mousePosition);

            UpdateItemWindowText(itemInfo);

        }


        private void CloseItemWindow(object sender, PointerRoutedEventArgs e)
        {
            WindowBag.Visibility = Visibility.Collapsed;
        }

        private void UpdateItemWindowText(Slot slot)
        {
            Item item = Encyclopedia.encyclopedia[slot.ItemID];
            W_ItemImage.Source = new BitmapImage(new Uri(this.BaseUri, item.PathImage));
            W_ItemName.Text = item.Name;
            W_ItemQntd.Text = slot.ItemAmount + "x";
            W_ItemRarity.Text = item.GetTypeString();
            //W_ItemType.Text = item.itemType;
            if (item.description != null) W_ItemDescr.Text = item.description;
            W_ItemValue.Text = item.GoldValue + " gold";
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

        private void RealocateWindow(Canvas window, Point mousePosition)
        {
            window.Visibility = Visibility.Visible;

            window.SetValue(Canvas.LeftProperty, mousePosition.X);

            if (mousePosition.Y >= Tela.Height / 2)
            {
                window.SetValue(Canvas.TopProperty, mousePosition.Y);
            }
            else
            {
                window.SetValue(Canvas.TopProperty, mousePosition.Y - window.Height - 10);
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

            if (skillEnter == null) return;
            int positionColumn = (int)skillEnter.GetValue(Grid.ColumnProperty);
            int positionRow = (int)skillEnter.GetValue(Grid.RowProperty);
            SkillGenerics skillInfo;

            int index = positionRow * 5 + positionColumn;

            skillInfo = p1._SkillManager.SkillList.ElementAt(index);

            if (skillInfo == null) return;

            RealocateWindow(WindowSkill, mousePosition);

            UpdateSkillWindowText(skillInfo);
        }

        private void CloseSkillWindow(object sender, PointerRoutedEventArgs e)
        {
            WindowSkill.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region ButtonEvents
        private void OfferItemButton(object sender, RoutedEventArgs e)
        {
            if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
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


        private void ItemBuyingQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uint.TryParse(ItemBuyingQuantity.Text, out uint val))
            {
                uint MaxValue = p1._Inventory.GetSlot(shopper.SlotInOffer.ItemID).ItemAmount;
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

        private void SellButton(object sender, RoutedEventArgs e)
        {
            shopper.BuyItem(p1._Inventory);
            UpdateShopInfo();
            UpdatePlayerInfo();
        }

        private void CancelSellingButton(object sender, RoutedEventArgs e)
        {
            CloseOfferItem();
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

        private void BuyButton(object sender, RoutedEventArgs e)
        {
            //fazer
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

        private void ApplyStats(object sender, RoutedEventArgs e)
        {
            p1.LevelUpdate(_str, _spd, _dex, _con, _mnd);
            _str = _spd = _dex = _con = _mnd = 0;
            UpdatePlayerInfo();
        }
        #endregion

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
    }
}
