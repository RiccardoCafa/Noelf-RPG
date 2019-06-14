using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents
{
    class EquipImage : Canvas
    {
        public Item MyEquip;
        private Image image;

        public ImageSource Source {
            set { image.Source = value; }
        }

        public EquipImage(int width, int height)
        {
            image = new Image()
            {
                Width = width,
                Height = height
            };
            Children.Add(image);
            SetEvents();
        }

        public void SetEvents()
        {
            PointerPressed += InterfaceManager.instance.DesequiparEvent;
            PointerEntered += InterfaceManager.instance.ShowEquipWindow;
            PointerExited += InterfaceManager.instance.CloseItemWindow;
        }

        public void UpdateImage()
        {
            if (MyEquip == null)
            {
                Source = null;
                return;
            }
            Source = new BitmapImage(new Uri("ms-appx://" + MyEquip.PathImage));
        }
    }
}
