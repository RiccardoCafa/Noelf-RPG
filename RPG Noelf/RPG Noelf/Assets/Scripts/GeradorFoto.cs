using System;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets
{
    class GeradorFoto
    {
        public static async Task MergeImages(UIElement personagem, RenderTargetBitmap render, int width, int height)
        {

            RenderTargetBitmap rTb = new RenderTargetBitmap();
            await rTb.RenderAsync(personagem, width, height);
            render = rTb;
            //SaveFrame(rTb);
        }

        public static async void MergeImages(Canvas canvas, string path)
        {

            RenderTargetBitmap rTb = new RenderTargetBitmap();
            await rTb.RenderAsync(canvas, (int)canvas.Width, (int)canvas.Height);
            //renderedImage.Source = rTb;
            //SaveFrame(rTb);
        }

        public static async void SaveFrame(RenderTargetBitmap _bitmap)
        {
            /* TODO Save Frame
             * Mudar essa parte de salvamento para ser interno em alguma pasta e tentar fazer algum jeito de criptografia.
             * Descriptografar depois para conseguir obter a imagem.
             */ 
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
