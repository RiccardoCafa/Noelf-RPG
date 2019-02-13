using System.IO;
using System;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Windows.Media.Imaging;
//using Windows.Graphics;
//using Windows.Graphics.DirectX;
//using Windows.Graphics.Imaging;

namespace RPG_Noelf.Assets
{
    class GeradorFoto
    {
        public static async void MergeImages(UIElement personagem, int width, int height, Image renderedImage)
        {

            RenderTargetBitmap rTb = new RenderTargetBitmap();
            await rTb.RenderAsync(personagem, width, height);
            renderedImage.Source = rTb;
            SaveFrame(rTb);
        }

        public static async void SaveFrame(RenderTargetBitmap _bitmap)
        {
            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileSavePicker.FileTypeChoices.Add("PNG files", new List<string>() { ".png" });
            fileSavePicker.SuggestedFileName = "image";
            StorageFile saveFile = await fileSavePicker.PickSaveFileAsync();
            if (saveFile == null)
            {
                return;
            }
            var buffer = await _bitmap.GetPixelsAsync();
            
            var _bitmapPixel = SoftwareBitmap.CreateCopyFromBuffer(buffer, BitmapPixelFormat.Bgra8, 197, 202);
            
            using (IRandomAccessStream stream = await saveFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder;
                encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

                encoder.SetSoftwareBitmap(_bitmapPixel);

                try
                {
                    await encoder.FlushAsync();

                } catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            
        }

    }


}


/*
//int offsetWidth = img1.Width > img2.Width ? img1.Width : img2.Width;
//int offsetHeight = img1.Height > img2.Height ? img1.Height : img2.Height;

//Bitmap output = new Bitmap(offsetWidth, offsetHeight, PixelFormat.Format32bppRgb);
Bitmap output = new Bitmap((int)img1.Width, (int)img1.Height, PixelFormat.Format32bppRgb);
//Windows.UI.Xaml.Controls.Image output = new Windows.UI.Xaml.Controls.Image();

using (Graphics g = Graphics.FromImage(output))
{
    g.Clear(System.Drawing.Color.Transparent);
    int _width = (int)img1.Width;
    int _height = (int)img1.Height;
    g.DrawImage(img1, new Rectangle(0, 0, _width, _height), new Rectangle(0, 0, _width, _height), GraphicsUnit.Pixel);
}*/
