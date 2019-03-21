using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Scenes
{
    class Scene
    {
        public string[] Blueprint { get; private set; } = new string[15];

        public Scene(Canvas canvas)
        {
            Blueprint[00] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[01] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[02] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[03] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[04] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[05] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGGGGGGGGGGGGGGGGGGaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[06] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[07] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Blueprint[08] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGGGGGaaaaaaaaaaaaaaaaaGGGGG";
            Blueprint[09] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGGGGGaaaaaaaGGGggggg";
            Blueprint[10] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGGgggggggg";
            Blueprint[11] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGgggggggggg";
            Blueprint[12] = "aaaaaaaaaaaaaaaGGGGGGGGGGGGGGGGGGGGaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaGGggggggggggg";
            Blueprint[13] = "GGGGGGGGGGGGGGGggggggggggggggggggggaaaaaaaaaaaaaaaaaaaaaaaaaaaaGGGGGGGGGGGGGGGGGGGGGGGGggggggggggggg";
            Blueprint[14] = "gggggggggggggggggggggggggggggggggggGGGGGGGGGGGGGGGGGGGGGGGGGGGGggggggggggggggggggggggggggggggggggggg";
            //Blueprint = Blueprint.Reverse().ToString();
            for(int i = 0; i < 15; i++)
            {
                int j = 0;
                foreach (char block in Blueprint[i])
                {
                    if(block != 'a')
                    {
                        Tile tile = new Tile(Tile.TileCode[block], i, j);
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri(MainPage.instance.BaseUri, tile.Path));
                        image.Width = tile.VirtualSize[0];
                        image.Height = tile.VirtualSize[1];
                        //image.SetValue.
                        canvas.Children.Add(image);
                    }
                    j++;
                }
            }
        }
    }
}
