using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    public class ItemImageArgs : EventArgs
    {
        public int item { get; set; }
    }
    
    public enum EItemOwner
    {
        player,
        drop
    }

    class ItemImage : Canvas
    {
        public delegate void ItemImageHandler(object sender, ItemImageArgs e);
        public event ItemImageHandler ItemImageUpdate;

        public int myItemPosition;
        public Image image;
        public Bag myBagRef;
        public EItemOwner itemOwner;

        public ImageSource Source {
            set { image.Source = value; }
        }

        public Slot Slot {
            get { return myBagRef.GetSlot(myItemPosition); }
        }

        public ItemImage(int itemPosition, int widthSize, int heightSize, Bag bag)
        {
            itemOwner = EItemOwner.player;
            myBagRef = bag;
            this.myItemPosition = itemPosition;
            image = new Image()
            {
                Width = widthSize,
                Height = heightSize
            };
            Children.Add(image);
            SetEvents();
        }

        public ItemImage(int itemPosition, int widthSize, int heightSize)
        {
            itemOwner = EItemOwner.player;
            this.myItemPosition = itemPosition;
            image = new Image()
            {
                Width = widthSize,
                Height = heightSize
            };
            Children.Add(image);
            SetEvents();
        }

        public void SetEvents()
        {
            if (itemOwner == EItemOwner.player)
            {
                PointerPressed -= Game.instance.ItemSlotEventDrop;
                PointerPressed += Game.instance.InventorySlotEvent;
            }
            else if(itemOwner == EItemOwner.drop)
            {
                PointerPressed -= Game.instance.InventorySlotEvent;
                PointerPressed += Game.instance.ItemSlotEventDrop;
            }
            if(PointerExitedEvent != null) PointerExited += Game.instance.CloseItemWindow;
            if(PointerEnteredEvent != null) PointerEntered += Game.instance.ShowItemWindow;
        }

        public void OnItemImageUpdate()
        {
            ItemImageUpdate?.Invoke(this, new ItemImageArgs() { item = myItemPosition });

            if (myBagRef == null) return;
            uint itemID = myBagRef.GetSlot(myItemPosition) != null ?
                        myBagRef.GetSlot(myItemPosition).ItemID : 0;
            if (itemID != 0)
            {
                image.Source = new BitmapImage(new Uri("ms-appx://" + Encyclopedia.SearchFor(itemID).PathImage));
            } else
            {
                image.Source = new BitmapImage();
            }
        }
    }
}
