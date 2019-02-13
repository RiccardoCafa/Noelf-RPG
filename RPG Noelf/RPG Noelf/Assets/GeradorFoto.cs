using System.Drawing;
using System.Drawing.Imaging;
using Windows.Graphics;
using Windows.Graphics.DirectX;

namespace RPG_Noelf.Assets
{
    class GeradorFoto 
    {
        public static Bitmap MergeImages(Image img1, Image img2)
        {
            if(img1 == null || img2 == null)
            {
                return null;
            }
            int offsetWidth = img1.Width > img2.Width ? img1.Width : img2.Width;
            int offsetHeight = img1.Height > img2.Height ? img1.Height : img2.Height;

            Bitmap output = new Bitmap(offsetWidth, offsetHeight, PixelFormat.Format32bppRgb);

            using (Graphics g = Graphics.FromImage(output))
            {
                g.DrawImage(img1, new Rectangle(new Point(), img1.Size), new Rectangle(new Point(), img1.Size), GraphicsUnit.Pixel);
                g.DrawImage(img2, new Rectangle(new Point(0, img1.Height + 1), img2.Size), new Rectangle(new Point(), img1.Size), GraphicsUnit.Pixel);
            }
            return output;
        }
    }

    
}
