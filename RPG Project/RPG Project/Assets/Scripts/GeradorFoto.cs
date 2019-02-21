using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Windows.Graphics;
using Windows.Graphics.DirectX;
using Windows.Graphics.Imaging;

namespace RPG_Noelf.Assets
{
    class GeradorFoto 
    {
        public static Bitmap MergeImages(Windows.UI.Xaml.Controls.Image img1, Image img2)
        {
            if(img1 == null || img2 == null)
            {
                return null;
            }
            //int offsetWidth = img1.Width > img2.Width ? img1.Width : img2.Width;
            //int offsetHeight = img1.Height > img2.Height ? img1.Height : img2.Height;

            //Bitmap output = new Bitmap(offsetWidth, offsetHeight, PixelFormat.Format32bppRgb);
            Bitmap output = new Bitmap((int)img1.Width, (int)img1.Height, PixelFormat.Format32bppRgb);
            //Windows.UI.Xaml.Controls.Image output = new Windows.UI.Xaml.Controls.Image();

            using (Graphics g = Graphics.FromImage(output))
            {
                g.Clear(System.Drawing.Color.Transparent);
                int _width = (int) img1.Width;
                int _height = (int)img1.Height;
                g.DrawImage(img1, new Rectangle(0, 0, _width, _height), new Rectangle(0, 0, _width, _height), GraphicsUnit.Pixel);
            }
            return output;
        }

        public static Bitmap BitmapFromSource(Windows.UI.Xaml.Media.Imaging.BitmapSource source)
        {
            using(MemoryStream outStream = new MemoryStream() )
            {
                BitmapEncoder enc = new PngBitmapEncoder();
            }
        }

    }

    
}
