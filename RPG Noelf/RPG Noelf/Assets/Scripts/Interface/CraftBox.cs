using RPG_Noelf.Assets.Scripts.Crafting_Scripts;
using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    class CraftBox : Button
    {
        public uint ItemToCraft { get; set; }

        public CraftBox(uint ItemToCraft)
        {
            this.ItemToCraft = ItemToCraft;
            this.Content = Encyclopedia.SearchFor(ItemToCraft).Name;
            Width = 500;
            Height = 50;
            Click += CraftBoxClick;
        }

        private void CraftBoxClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GameManager.CraftingStation.CraftItem(ItemToCraft);
        }
    }
}
