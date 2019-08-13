

using QRCoder;
using System.DrawingCore;

namespace Shared.Util
{
    public static class Qrcode
    {
        public static Bitmap GerarQRCodeImagem(string text)
        {
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);

                return qrCodeImage;
            }
            catch
            {
                throw;
            }
        }

        public static string GerarQRCodeBase64(string text)
        {
            try
            {
               
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20);

                return qrCodeImageAsBase64;
            }
            catch
            {
                throw;
            }
        }
    }
}
