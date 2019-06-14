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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    public class InterfaceManager
    {
        public Canvas Tela;
        // Geral
        public Canvas CanvasChunck01 { get; set; }
        public Canvas MenuAberto { get; set; }
        public Canvas MenuFechado { get; set; }
        public Canvas MenuFBolaAtras { get; set; }
        // Chest
        public Canvas CanvasChest { get; set; }
        // Inventario
        public Canvas CanvasInventario { get; set; }
        public Canvas CanvasWindowBag { get; set; }
        // Skill
        public Canvas CanvasSkillWindow { get; set; }
        public Canvas CanvasSkillTree { get; set; }
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
        public Canvas CanvasQuestManager { get; set; }
        public Canvas CanvasActiveQuests { get; set; }
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
        // Chest
        public TextBlock TextChestName { get; set; }
        // Shop
        public TextBlock TextItemBuyingName { get; set; }
        public TextBlock TextItemBuyingQuantity { get; set; }
        public TextBlock TextItemBuyingValue { get; set; }
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

        public string BaseUri = "ms-appx:\\";

        public InterfaceManager(Canvas Tela)
        {
            Window.Current.CoreWindow.KeyUp += ManageKey;
            this.Tela = Tela;
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
                        UpdateSkillTree();
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
            if (indicadorzao != -1) GameManager.player._SkillManager.BeAbleSkill(indicadorzao);

        }

        /* ####################################### INSTANTIATES #######################################*/

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
            bau.itens.BagUpdated += UpdateChestEvent;
            return chest;
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

        public void CreateSkill(Canvas SkillCanvas)
        {
            SkillCanvas.Width = 250;
            SkillCanvas.Height = 150;
            SkillCanvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            SkillCanvas.VerticalAlignment = VerticalAlignment.Stretch;
            Canvas.SetTop(CanvasSkillTree, 40);
            Canvas.SetLeft(CanvasSkillTree, 120);
            TextBlock text = new TextBlock()
            {
                Width = 250,
                Height = 20,
                Text = "Árvore de Habilidades",
                HorizontalTextAlignment = TextAlignment.Center
            };
            Canvas.SetTop(text, -20);
            SkillCanvas.Children.Add(text);
            Image bg = new Image()
            {
                Width = 250,
                Height = 150,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/UIAtivo 23-0.png")),
                Stretch = Stretch.Fill
            };
            SkillCanvas.Children.Add(bg);
            GridSkill = new Grid()
            {
                Width = 250,
                Height = 150
            };
            SkillCanvas.Children.Add(GridSkill);
            GridSkill.SetValue(Grid.PaddingProperty, new Thickness(5, 5, 5, 5));
            DefinitionsGrid(GridSkill, 5, 3, 50, 50);
            FillGridSkillImage(GridSkill, GameManager.player, 5, 3, 40, 40);
        }
        public void CreateInventory(Canvas BagCanvas)
        {
            BagCanvas.Width = 180;
            BagCanvas.Height = 150;
            Canvas.SetLeft(BagCanvas, 800);
            Canvas.SetTop(BagCanvas, 40);
            CanvasInventario = BagCanvas;
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
            FillGridItemImage(GridInventario, GameManager.player._Inventory, 6, 5, 25, 25);

        }
        public void CreateChestWindow(double x, double y)
        {
            //Configurando o ChestWindow
            CanvasChest.Width = 250;
            CanvasChest.Height = 150;
            Canvas.SetTop(CanvasChest, x);
            Canvas.SetLeft(CanvasChest, y);
            CanvasChest.Visibility = Visibility.Collapsed;

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
            CanvasChest.Children.Add(Close);
            Canvas.SetTop(Close, GetAllBtn.Height + 10);
            Canvas.SetLeft(Close, CanvasChest.Width);
            Close.Click += (object sender, RoutedEventArgs e) =>
            {
                CanvasChest.Visibility = Visibility.Collapsed;
            };

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
            dropImage.Source = new BitmapImage(new Uri(BaseUri + pathImage));
            drop.Children.Add(dropImage);
            //LootBody loot = new LootBody(drop);
            //loot.UpdateBlocks(TheScene);
            //Trigger dropTrigger = new Trigger(loot);
        }

        /* ############################################################################################*/

        /* ########################################## UPDATES #########################################*/

        public void UpdateChest(Bau chest)
        {
            UpdateGridBagItemImages(GridChest, chest.itens);
            int count = 0;
            foreach (ItemImage img in GridChest.Children)
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
        public void UpdateChestEvent(object sender, BagEventArgs e)
        {
            Bag chest = e.Bag;
            UpdateGridBagItemImages(GridChest, chest);
            int count = 0;
            foreach (ItemImage img in GridChest.Children)
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
        
        public void UpdateGridBagItemImages(Grid grid, Bag bag)
        {
            foreach (ItemImage item in grid.Children)
            {
                item.myBagRef = bag;
                item.itemOwner = EItemOwner.drop;
                item.SetEvents();
            }
        }
        public void UpdatePlayerInfo()
        {
            TextPlayerInfo.Text = GameManager.player.Race.NameRace + " " + GameManager.player._Class.ClassName + "\n";
            TextPlayerInfo.Text += "CanvasAtributos: ( " + GameManager.player._Class.StatsPoints + " pontos)\n" +
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
                                "Experience: " + GameManager.player.level.actualEXP + "/" + GameManager.player.level.EXPlim + "\n" +
                                "Pontos de skill disponivel: " + GameManager.player._SkillManager.SkillPoints + "\n" +
                                "Gold: " + GameManager.player._Inventory.Gold;
        }
        public void UpdateSkillBar()
        {
            int cont = 0;
            foreach (UIElement element in GridBarraSkill.Children)
            {
                if (cont == 0)
                {
                    (element as Image).Source = new BitmapImage(new Uri(BaseUri + GameManager.player._SkillManager.Passive.pathImage));
                }
                else
                {
                    if (GameManager.player._SkillManager.SkillBar[cont - 1] != null)
                        (element as Image).Source = new BitmapImage(new Uri(BaseUri + GameManager.player._SkillManager.SkillBar[cont - 1].pathImage));
                    else
                        (element as Image).Source = new BitmapImage();
                }
                cont++;
            }
        }
        public void UpdateSkillWindowText(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                CanvasSkillWindow.Visibility = Visibility.Visible;
                SkillImage skillImage = (sender as SkillImage);
                SkillGenerics skillInfo = skillImage.skill;
                ImageW_Skill.Source = new BitmapImage(new Uri(BaseUri + skillInfo.pathImage));
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
                return;
            }

        }
        public void UpdateItemWindowText(int slotPosition, Bag bag)
        {
            Slot slot = bag.GetSlot(slotPosition);
            if (slot == null) return;
            Item item = Encyclopedia.encyclopedia[slot.ItemID];
            ImageW_Item.Source = new BitmapImage(new Uri(BaseUri + item.PathImage));
            TextW_ItemName.Text = item.Name;
            TextW_ItemQntd.Text = slot.ItemAmount + "x";
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
            int count = 0;
            string pathImage;
            foreach (UIElement element in GridEquip.Children)
            {
                Image img = element as Image;
                if ((int)img.GetValue(Grid.ColumnProperty) == 0)
                {
                    if (GameManager.player.Equipamento.armor[count] == null)
                    {
                        img.Source = new BitmapImage(new Uri(BaseUri + "/Assets/Imagens/Chao.jpg"));
                    }
                    else
                    {
                        pathImage = GameManager.player.Equipamento.armor[count].PathImage;
                        img.Source = new BitmapImage(new Uri(BaseUri + pathImage));
                    }
                }
                else
                {
                    if (GameManager.player.Equipamento.weapon == null)
                    {
                        img.Source = new BitmapImage(new Uri(BaseUri + "/Assets/Imagens/Chao.jpg"));
                    }
                    else
                    {
                        pathImage = GameManager.player.Equipamento.weapon.PathImage;
                        img.Source = new BitmapImage(new Uri(BaseUri + pathImage));
                    }
                }
                count++;
            }
        }
        public void UpdateQuest()
        {
            if (GameManager.questerTarget != null)
            {
                GameManager.questerTarget.myQuest = QuestList.allquests[GameManager.questerTarget.GetQuestID()];
            }
            else
            {
                GameManager.questerTarget = new Quester(1);
            }
            TextQuestTitulo.Text = GameManager.questerTarget.myQuest.name;
            TextQuestDescription.Text = GameManager.questerTarget.myQuest.Description;
            TextQuestRewards.Text = GameManager.questerTarget.myQuest.RewardDescription;
            TextQuestWindow.Visibility = Visibility.Visible;
        }
        public void UpdateShopInfo()
        {
            if (GameManager.traderTarget == null) return;
            int count = 0;
            foreach (Image img in GridShop.Children)
            {
                if (Switch == false)
                {
                    if (count >= GameManager.traderTarget.shop.BuyingItems.Slots.Count) img.Source = new BitmapImage();
                    else
                    {
                        Slot s = GameManager.traderTarget.shop.BuyingItems.GetSlot(count);
                        img.Source = new BitmapImage(new Uri(BaseUri + Encyclopedia.SearchFor(s.ItemID).PathImage));
                    }
                }
                else
                {
                    if (count >= GameManager.traderTarget.shop.TradingItems.Slots.Count) img.Source = new BitmapImage();
                    else
                    {
                        Slot s = GameManager.traderTarget.shop.TradingItems.GetSlot(count);
                        img.Source = new BitmapImage(new Uri(BaseUri + Encyclopedia.SearchFor(s.ItemID).PathImage));
                    }
                }
                count++;
            }
        }
        public void UpdateActualQuestManager()
        {
            Quest q = GameManager.player._Questmanager.actualQuest;
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
                ImageMQuestItem.Source = new BitmapImage(new Uri("ms-appx://" + Encyclopedia.SearchFor(q.GainedItem.ItemID).PathImage));
                TextMQuestItemQntd.Text = q.GainedItem.ItemAmount.ToString();
            }
            else
            {
                ImageMQuestItem.Source = new BitmapImage();
                TextMQuestItemQntd.Text = "";
            }
        }
        public void UpdateQuestList()
        {
            ResetQuestList();

            List<Quest> quests = GameManager.player._Questmanager.allQuests;
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
        
        /* ############################################################################################*/

        /* ########################################### EVENTS #########################################*/

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
                            CanvasWindowBag.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        GameManager.player._Inventory.RemoveFromBag(s.ItemID, s.ItemAmount);
                        CreateDrop(GameManager.player.box.Xi + (GameManager.player.box.Width / 2),
                                    GameManager.player.box.Yi + (GameManager.player.box.Height / 2),
                                    s);
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
                    if (GameManager.player._Inventory.AddToBag(s))
                    {
                        bool c = (sender as ItemImage).myBagRef.RemoveFromBag(s.ItemID, s.ItemAmount);
                    }
                }
            }
        }
            // Conversation
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
            // Skills
        public void SkillTreePointerEvent(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var prop = e.GetCurrentPoint(Tela).Properties;
                if (prop.IsLeftButtonPressed)
                {
                    Image skillEnter = sender as Image;
                    SkillGenerics skillClicked;

                    int columnPosition = (int)skillEnter.GetValue(Grid.ColumnProperty);
                    int rowPosition = (int)skillEnter.GetValue(Grid.RowProperty);
                    int position = GridInventario.ColumnDefinitions.Count * rowPosition + columnPosition;
                    skillClicked = GameManager.player._SkillManager.SkillList[position];
                    if (GameManager.player._SkillManager.UpSkill(skillClicked))
                    {
                        //UpdateSkillWindowText(skillClicked);
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
                    int position = GridInventario.ColumnDefinitions.Count * rowPosition + columnPosition;

                    skillClicked = GameManager.player._SkillManager.SkillList[position];
                    if (skillClicked.Unlocked == false) return;
                    GameManager.player._SkillManager.ChangeSkill(skillClicked);
                    UpdateSkillBar();
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
                    GameManager.player._SkillManager.SkillBar[position - 1] = null;
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
                TextItemBuyingQuantity.Text = val.ToString();
            }
        }
        public void IncrementOfferAmount(object sender, RoutedEventArgs e)
        {
            if (GameManager.traderTarget.shop.SlotInOffer == null) return;
            if (uint.TryParse(TextItemBuyingQuantity.Text, out uint val))
            {
                uint MaxValue = GameManager.player._Inventory.GetSlot(GameManager.traderTarget.shop.SlotInOffer.ItemID).ItemAmount;
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
            if (GameManager.traderTarget == null) return;
            if (uint.TryParse(TextItemBuyingQuantity.Text, out uint val))
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
        public void SellButton(object sender, RoutedEventArgs e)
        {
            if (Switch == false)
            {
                GameManager.traderTarget.shop.BuyItem(GameManager.player._Inventory);
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
            GameManager.player._Questmanager.GiveUpActualQuest();
        }
        public void ClickAcceptQuestButton(object sender, RoutedEventArgs e)
        {
            Quester npcF = GameManager.npcTarget.GetFunction("Quester") as Quester;
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
            //GameManager.player.;
            GameManager.player.LevelUpdate(0, 0, 0, 0, 0, 100);
        }
        public void MPPlus(object sender, RoutedEventArgs e)
        {
            GameManager.player.AddMP(20);
        }
        public void HPPlus(object sender, RoutedEventArgs e)
        {
            GameManager.player.AddHP(20);
        }
        public void PSTR(object sender, RoutedEventArgs e)
        {
            if (GameManager.player._Class.StatsPoints > 0)
            {
                _str++;
                GeralSumStat();
            }
        }
        public void PMND(object sender, RoutedEventArgs e)
        {
            if (GameManager.player._Class.StatsPoints > 0)
            {
                _mnd++;
                GeralSumStat();
            }
        }
        public void PSPD(object sender, RoutedEventArgs e)
        {
            if (GameManager.player._Class.StatsPoints > 0)
            {
                _spd++;
                GeralSumStat();
            }
        }
        public void PDEX(object sender, RoutedEventArgs e)
        {
            if (GameManager.player._Class.StatsPoints > 0)
            {
                _dex++;
                GeralSumStat();
            }
        }
        public void PCON(object sender, RoutedEventArgs e)
        {
            if (GameManager.player._Class.StatsPoints > 0)
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
            GameManager.player._Class.StatsPoints--;
            UpdatePlayerInfo();
        }
        public void GeralSubStat()
        {
            GameManager.player._Class.StatsPoints++;
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
            GameManager.player.LevelUpdate(_str, _spd, _dex, _con, _mnd, 50);
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
        public void SetEventForEquip()
        {
            foreach (UIElement element in GridEquip.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowEquipWindow;
                    element.PointerExited += CloseItemWindow;
                    element.PointerPressed += DesequiparEvent;
                }
            }
        }
        public void SetEventForShopItem()
        {
            foreach (UIElement element in GridShop.Children)
            {
                if (element is Image)
                {
                    element.PointerEntered += ShowItemBuying;
                    element.PointerExited += CloseItemWindow;
                    element.PointerPressed += ShopItemBuy;
                }
            }
        }

        /* ############################################################################################*/

        /* ######################################## OPEN/CLOSE WINDOW #######################################*/

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

            Image itemEnter = null;
            try
            {
                itemEnter = sender as Windows.UI.Xaml.Controls.Image;
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

            RealocateWindow(CanvasWindowBag, mousePosition);

            //UpdateItemWindowText(itemInfo);

        }
        
        public void CallConversationBox(NPC npc)
        {
            if (GameManager.interfaceManager.Conversation) return;
            GameManager.npcTarget = npc;
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
            RotateTransform ta = (RotateTransform)ImageArrowQuest.RenderTransform;
            if (CanvasQuestList.Visibility == Visibility.Collapsed)
            {
                CanvasQuestList.Visibility = Visibility.Visible;
                ta.Angle = 90;
            }
            else
            {
                CanvasQuestList.Visibility = Visibility.Collapsed;
                ta.Angle = -90;
            }
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
            ImageItemBuying.Source = new BitmapImage(new Uri(BaseUri + item.PathImage));
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
        public void ShowItemBuying(object sender, PointerRoutedEventArgs e)
        {
            if (CanvasWindowBag.Visibility == Visibility.Visible)
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
            int position = GridShop.ColumnDefinitions.Count * rowPosition + columnPosition;

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

            RealocateWindow(CanvasWindowBag, mousePosition);

            // TODO UpdateItemWindowText(itemInfo);

        }
        public void CloseOfferItem()
        {
            GameManager.traderTarget.shop.SlotInOffer = null;
            CanvasOfferShop.Visibility = Visibility.Collapsed;
        }
        public void CloseShop()
        {
            CanvasShop.Visibility = Visibility.Collapsed;
        }
        
        /* ############################################################################################*/

        /* ######################################## TRANSFORM & MISC #########################################*/

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
        public void FillGridSkillImage(Grid grid, Player player, int columnSize, int rowSize, int widthImage, int heightImage)
        {
            List<SkillGenerics> skills = player._SkillManager.SkillList;
            int column = -1, row = 0;
            for (int i = 0; i < columnSize * rowSize; i++)
            {
                SkillImage item;
                column++;
                item = new SkillImage(widthImage, heightImage, skills[i]); //skills != null ?  : new SkillImage(widthImage, heightImage);
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
        
        /* ############################################################################################*/


        //public void CreateMob()
        //{
        //    GameManager.mob = new Mob(level: 2);
        //    GameManager.mob.Spawn(250, 20);
        //    MobSlot smb;
        //    smb = new MobSlot(2, 10, 0.99);
        //    if(smb.Drop()) GameManager.mob.MobBag.AddToBag(smb);
        //    smb = new MobSlot(23, 10, 0.3);
        //    if(smb.Drop()) GameManager.mob.MobBag.AddToBag(smb);
        //    smb = new MobSlot(24, 10, 0.3);
        //    if(smb.Drop()) GameManager.mob.MobBag.AddToBag(smb);
        //    smb = new MobSlot(25, 10, 0.1);
        //    if(smb.Drop()) GameManager.mob.MobBag.AddToBag(smb);
        //    TheScene.Children.Add(GameManager.mob.box);
        //}
        /*public void CreatePlayer()
        {
            if (PlayerCreated != null)
            {
                GameManager.player = new Player("2220000");//PlayerCreated.Id
            }
            else
            {
                GameManager.player = new Player("2222000");
            }
            GameManager.player.Spawn(300, -100);
            GameManager.player.Damage = 0.1;
            TheScene.Children.Add(GameManager.player.box);
            //GameManager.player.box.Background = new SolidColorBrush(Color.FromArgb(155, 255, 0, 127));
        }*/

    }
}
