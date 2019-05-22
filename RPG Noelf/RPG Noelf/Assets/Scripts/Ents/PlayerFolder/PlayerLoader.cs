using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Ents.PlayerFolder
{
    class PlayerLoader
    {

        public Solid box;
        public string Id;

        public Dictionary<string, Image> playerImages = new Dictionary<string, Image>()
        {
            {"armse0", new Image() { Width = 51, Height = 57 } },
            {"armse1", new Image() { Width = 53, Height = 41 } },
            {"legse1", new Image() { Width = 54, Height = 57 } },
            {"legse0", new Image() { Width = 45, Height = 45 } },
            {"body", new Image() { Width = 69, Height = 84 } },
            {"head", new Image() { Width = 78, Height = 78 } },
            {"eye", new Image() { Width = 78, Height = 78 } },
            {"hair", new Image() { Width = 78, Height = 78 } },
            {"legsd1", new Image() { Width = 51, Height = 51 } },
            {"legsd0", new Image() { Width = 42, Height = 48 } },
            {"armsd0", new Image() { Width = 60, Height = 54 } },
            {"armsd1", new Image() { Width = 45, Height = 48 } }
        };
        public Dictionary<string, Image> clothesImages = new Dictionary<string, Image>()
        {
            {"armse0", new Image() { Width = 51, Height = 57 } },
            {"armse1", new Image() { Width = 53, Height = 41 } },
            {"legse1", new Image() { Width = 54, Height = 57 } },
            {"legse0", new Image() { Width = 45, Height = 45 } },
            {"body", new Image() { Width = 69, Height = 84 } },
            {"legsd1", new Image() { Width = 51, Height = 51 } },
            {"legsd0", new Image() { Width = 42, Height = 48 } },
            {"armsd0", new Image() { Width = 60, Height = 54 } },
            {"armsd1", new Image() { Width = 45, Height = 48 } }
        };


        public PlayerLoader(Solid box, string id)
        {
            this.box = box;
            this.Id = id;
        }

        public void Load(string[] parts, string[] sides)
        {
            SetPlayer(playerImages, parts, sides);
            SetClothes(clothesImages, parts, sides);
            foreach (string key in playerImages.Keys)
            {
                box.Children.Add(playerImages[key]);
                if (key != "head" && key != "eye" && key != "hair") box.Children.Add(clothesImages[key]);
            }
            Canvas.SetLeft(playerImages["armse0"], 16); Canvas.SetTop(playerImages["armse0"], 24);
            Canvas.SetLeft(playerImages["armse1"], 26); Canvas.SetTop(playerImages["armse1"], 42);
            Canvas.SetLeft(playerImages["legse1"], 17); Canvas.SetTop(playerImages["legse1"], 73);
            Canvas.SetLeft(playerImages["legse0"], 15); Canvas.SetTop(playerImages["legse0"], 58);
            Canvas.SetLeft(playerImages["body"], -9); Canvas.SetTop(playerImages["body"], 9);
            Canvas.SetLeft(playerImages["head"], -5); Canvas.SetTop(playerImages["head"], -27);
            Canvas.SetLeft(playerImages["eye"], -5); Canvas.SetTop(playerImages["eye"], -27);
            Canvas.SetLeft(playerImages["hair"], -5); Canvas.SetTop(playerImages["hair"], -27);
            Canvas.SetLeft(playerImages["legsd1"], -17); Canvas.SetTop(playerImages["legsd1"], 78);
            Canvas.SetLeft(playerImages["legsd0"], -5); Canvas.SetTop(playerImages["legsd0"], 59);
            Canvas.SetLeft(playerImages["armsd0"], -24); Canvas.SetTop(playerImages["armsd0"], 18);
            Canvas.SetLeft(playerImages["armsd1"], -17); Canvas.SetTop(playerImages["armsd1"], 39);
            Canvas.SetLeft(clothesImages["armse0"], 16); Canvas.SetTop(clothesImages["armse0"], 24);
            Canvas.SetLeft(clothesImages["armse1"], 26); Canvas.SetTop(clothesImages["armse1"], 42);
            Canvas.SetLeft(clothesImages["legse1"], 17); Canvas.SetTop(clothesImages["legse1"], 73);
            Canvas.SetLeft(clothesImages["legse0"], 15); Canvas.SetTop(clothesImages["legse0"], 58);
            Canvas.SetLeft(clothesImages["body"], -9); Canvas.SetTop(clothesImages["body"], 9);
            Canvas.SetLeft(clothesImages["legsd1"], -17); Canvas.SetTop(clothesImages["legsd1"], 78);
            Canvas.SetLeft(clothesImages["legsd0"], -5); Canvas.SetTop(clothesImages["legsd0"], 59);
            Canvas.SetLeft(clothesImages["armsd0"], -24); Canvas.SetTop(clothesImages["armsd0"], 18);
            Canvas.SetLeft(clothesImages["armsd1"], -17); Canvas.SetTop(clothesImages["armsd1"], 39);
        }//monta as imagens na box do Player

        public void SetPlayer(Dictionary<string, Image> playerImages, string[] parts, string[] sides)//aplica as imagens das caracteristicas fisicas do player
        {
            for (int i = 0; i < 6; i++)
            {
                string path1 = "/Assets/Images/player/player/" + parts[i];
                string path2;
                switch (parts[i])
                {
                    case "arms":
                    case "legs":
                        foreach (string side in sides)
                        {
                            path2 = "/" + side;
                            for (int bit = 0; bit < 2; bit++)
                            {
                                string path3 = "/" + bit + "/" + Id[0] + Id.Substring(2, 2) + "___.png";
                                playerImages[parts[i] + side + bit].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2 + path3));
                            }
                        }
                        break;
                    case "hair":
                        if (Id[5] == '3') path2 = "/" + Id[0] + Id[2] + "__" + Id[5] + "_.png";
                        else path2 = "/" + Id[0] + Id[2] + "__" + Id.Substring(5, 2) + ".png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                        break;
                    case "eye":
                        path2 = "/" + Id[0] + Id[2] + "_" + Id[4] + "__.png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                        break;
                    default:
                        path2 = "/" + Id[0] + Id.Substring(2, 2) + "___.png";
                        playerImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                        break;
                }
            }
        }

        public void SetClothes(Dictionary<string, Image> clothesImages, string[] parts, string[] sides)//aplica as imagens das roupas do player (classe)
        {
            for (int i = 3; i < 6; i++)
            {
                string path1 = "/Assets/Images/player/clothes/" + parts[i];
                if (parts[i] == "arms" || parts[i] == "legs")
                {
                    foreach (string side in sides)
                    {
                        string path2 = "/" + side;
                        for (int bit = 0; bit < 2; bit++)
                        {
                            string path3 = "/" + bit + "/" + Id[2] + Id[1] + ".png";
                            clothesImages[parts[i] + side + bit].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2 + path3));
                        }
                    }
                }
                else
                {
                    string path2 = "/" + Id[2] + Id[1] + ".png";
                    clothesImages[parts[i]].Source = new BitmapImage(new Uri("ms-appx://" + path1 + path2));
                }
            }
        }


    }
}
