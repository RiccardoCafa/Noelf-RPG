using RPG_Noelf.Assets;
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
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using RPG_Noelf.Assets.Scripts.InventoryScripts;
using RPG_Noelf.Assets.Scripts.Skills;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPG_Noelf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Thread Start;
        Character player;
        InterfaceManager interfaceManager = new InterfaceManager();
        Bag bag = new Bag();
        Player p1, p2;

        int _str, _spd, _dex, _con, _mnd;

        public MainPage()
        {
            this.InitializeComponent();
            
            Start = new Thread(start);
            Start.Start();
        }

        public async void start()
        {
            _str = _spd = _dex = _con = _mnd = 0;
            // Settando Janelas de Interface
            interfaceManager.Inventario = InventarioWindow;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Window.Current.CoreWindow.KeyDown += Skill_KeyDown;
                // Settando o player
                player = new Character(Player, PlayerCanvas);
                player.UpdateBlocks(Chunck01);
                player.ResetPosition(320, 40);
                //player.textBlock = Texticulu;
                player.rotation = Rotation;
            });

            p1 = new Player("1", IRaces.Orc, IClasses.Warrior);
            //p1.MpMax = 5000;
            //p1.Mp += 500;
            p2 = new Player("2", IRaces.Human, IClasses.Wizard);

            p2.Armor = 0;

            p1._SkillManager.MakeSkill(10, 2, 1, 0.5f, 'P', 'F', "/Assets/Images/Item1.jpg", "jorrada");
            p1._SkillManager.MakeSkill(15, 1, 1, 0.2f, 'H', 'F', "/Assets/Images/Item2.jpg", "Trovao do Comunismo");

            Item banana = new Item(5, 1, "Banana", true, Category.Legendary, "Comunista", "/Assets/Images/Item1.jpg");
            Item jorro = new Item(500000, 1, "Jorro", true, Category.Magical, "ComunistaJorrando", "/Assets/Images/Item2.jpg");
            Item espadona = new Item(2500000, 1, "Espadona", false, Category.Normal, "ComunistaComunismo", "/Assets/Images/Item1.jpg");
            Consumable potion = new Consumable(50, 1, "HP pot", true, Category.Normal, "PotHP", "/Assets/Images/Item2.jpg");

            #region InvTest

            bag.AddGold(50);

            bag.AddToBag(banana);
            bag.AddToBag(jorro);
            bag.AddToBag(banana);
            bag.AddToBag(jorro);
            bag.AddToBag(banana);
            bag.AddToBag(jorro);
            bag.AddToBag(banana);
            bag.AddToBag(jorro);

            bag.RemoveFromBag(jorro);
            bag.RemoveFromBag(jorro);
            bag.RemoveFromBag(jorro);
            bag.RemoveFromBag(jorro);

            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);

            bag.AddToBag(potion);
            bag.AddToBag(potion);
            bag.AddToBag(potion);

            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);
            bag.AddToBag(espadona);

            bag.RemoveFromBag(espadona);
            bag.RemoveFromBag(espadona);

            bag.RemoveFromBag(potion);

            bag.RemoveFromBag(banana);
            #endregion

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                UpdateBag();
                LoadSkillTree();
                UpdatePlayerInfo();
            });
        }

        private void Skill_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            int indicadorzao = 0;
            if (e.VirtualKey == Windows.System.VirtualKey.Number1)
            {
                if(p1._SkillManager.SkillList.Count >= 1)
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
            if (e.VirtualKey == Windows.System.VirtualKey.Number5)
            {
                if (p1._SkillManager.SkillList.Count >= 5)
                {
                    indicadorzao = 4;
                }
            }

            string s = p1._SkillManager.SkillList[indicadorzao].UseSkill(p1, p2).ToString();
            Texticulu.Text = p1._SkillManager.SkillList[indicadorzao].name + " tirou " + s + " de dano";

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
                                "Pontos de skill disponivel: " + p1._SkillManager.SkillPoints;
        }

        public void LoadSkillTree()
        {
            for (int i = 0; i < p1._SkillManager.SkillList.Count; i++)
            {
                var slotTemp = from element in BarraSkill.Children
                               where (int)element.GetValue(Grid.ColumnProperty) == i
                               select element;
                if (slotTemp != null)
                {
                    Image slot = (Image)slotTemp.ElementAt(0);
                    slot.Source = new BitmapImage(new Uri(this.BaseUri, p1._SkillManager.SkillList[i].pathImage));
                }

            }
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
            if(p1._Class.StatsPoints > 0)
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

        private void ApplyStats(object sender, RoutedEventArgs e)
        {
            p1.LevelUpdate(_str, _spd, _dex, _con, _mnd);
            _str = _spd = _dex = _con = _mnd = 0;
            UpdatePlayerInfo();
        }

        public void UpdateBag()
        {
            for (int i = 0; i < bag.slots.Count; i++)
            {
                int column = i, row = i;
                row = i / 6;
                while (column > 5) column -= 6;

                var slotTemp = from element in InventarioGrid.Children
                             where (int)element.GetValue(Grid.ColumnProperty) == column && (int)element.GetValue(Grid.RowProperty) == row
                             select element;
                if(slotTemp != null)
                {
                    Image slot = (Image)slotTemp.ElementAt(0);
                    slot.Source = new BitmapImage(new Uri(this.BaseUri, bag.slots[i].pathImage));
                }
                
            }
        }
    }
}
