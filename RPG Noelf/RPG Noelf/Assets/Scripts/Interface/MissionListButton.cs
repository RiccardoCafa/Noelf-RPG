using RPG_Noelf.Assets.Scripts.Ents.NPCs;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Interface
{
    class MissionListButton : Canvas
    {
        private Quest quest;
        private Image image;
        private TextBlock titulo;

        public string QuestTitle {
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
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/UI Elements/UIAtivo 33-0.png")),
                Width = 200,
                Height = 15,
                Stretch = Windows.UI.Xaml.Media.Stretch.Fill
            };

            titulo = new TextBlock()
            {
                Width = 100,
                Height = 15,
                FontSize = 7,
                Text = quest.name,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Left
            };

            Children.Add(image);
            Children.Add(titulo);

            this.quest = quest;
        }
    }
}
