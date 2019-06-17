using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using RPG_Noelf.Assets.Scripts.General;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    class MissionListButton : Canvas
    {
        private Image image;
        private TextBlock titulo;

        public Quest Quest { get; set; }

        public string Titulo {
            get {
                return titulo.Text;
            }
            set {
                titulo.Text = value;
            }
        }

        public MissionListButton(Quest quest) 
        {
            Width = 200;
            Height = 15;
            image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/ComoFazerMagica.png")),
                Width = 100,
                Height = 15,
                Stretch = Windows.UI.Xaml.Media.Stretch.Fill
            };

            titulo = new TextBlock()
            {
                Width = 100,
                Height = 15,
                FontSize = 10,
                Text = quest.name,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center
            };

            Children.Add(image);
            Children.Add(titulo);
            PointerPressed += SelectQuest;
            this.Quest = quest;
        }

        public void SelectQuest(object sender, PointerRoutedEventArgs e)
        {
            GameManager.instance.player._Questmanager.SetActualQuest(Quest);
        }
    }
}
