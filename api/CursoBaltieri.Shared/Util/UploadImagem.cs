using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;

namespace Shared.Util
{
    public static class UploadImagem
    {
        public static bool Salvar(string base64,string nomeArquivo)
        {
            try
            {

                //var imagem = Decodificar(base64);
                //var imagemRedimensionada = ResizeImage(imagem, 700, 380);

                //Local onde vamos salvar a imagem
                //string diretorio = Directory.GetCurrentDirectory();

               // string path = diretorio + @"\wwwroot\imagens\sorteios\" + nomeArquivo;
                //Salvar a imagem no formato JPG na raiz do site
              //  imagemRedimensionada.Save(path, ImageFormat.Jpeg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Excluir(string caminho)
        {
            // Delete a file by using File class static method...
            if (File.Exists(caminho))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    File.Delete(caminho);

                    return true;
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }

            }

            return false;
        }
        public static Image Decodificar(string base64, string nomeArquivo,string dir,int altura,int largura)
        {
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                try
                {

                    //Criar um novo Bitmap baseado na MemoryStream
                    using (Bitmap bmp = new Bitmap(ms))
                    {
                        //Local onde vamos salvar a imagem
                        //string diretorio = Directory.GetCurrentDirectory();

                        //string path = diretorio + @"\wwwroot\imagens\sorteios\"+nomeArquivo;
                        //string path = Server.MapPath("~/cbsa.jpg");

                        //Salvar a imagem no formato JPG na raiz do site
                        //bmp.Save(path, ImageFormat.Jpeg);
                        var imagemRedimensionada = ResizeImage(bmp, altura, largura, true);
                        //Local onde vamos salvar a imagem
                        string diretorio = Directory.GetCurrentDirectory();

                        string path = diretorio + dir + nomeArquivo;
                        //Salvar a imagem no formato JPG na raiz do site
                        imagemRedimensionada.Save(path, ImageFormat.Jpeg);
                        return imagemRedimensionada;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static Image ResizeImage(Image originalFile, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            Image fullsizeImage = originalFile;

            // Prevent using images internal thumbnail
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider)
            {
                if (fullsizeImage.Width <= newWidth)
                {
                    newWidth = fullsizeImage.Width;
                }
            }

            int newHeight = fullsizeImage.Height * newWidth / fullsizeImage.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead
                newWidth = fullsizeImage.Width * maxHeight / fullsizeImage.Height;
                newHeight = maxHeight;
            }

            Image newImage = fullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            fullsizeImage.Dispose();

            // Save resized picture
            //newImage.Save(newFile);

            return newImage;
        }

        //public static Bitmap ResizeImage(Image image, int width, int height)
        //{
        //    var destRect = new Rectangle(0, 0, width, height);
        //    var destImage = new Bitmap(width, height);

        //    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        //    using (var graphics = Graphics.FromImage(destImage))
        //    {
        //        graphics.CompositingMode = CompositingMode.SourceCopy;
        //        graphics.CompositingQuality = CompositingQuality.HighQuality;
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = SmoothingMode.HighQuality;
        //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //        using (var wrapMode = new ImageAttributes())
        //        {
        //            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        //            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        //        }
        //    }

        //    return destImage;
        //}
    }
}
