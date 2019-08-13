using Shared.Security;
using Shared.Util;
using System.Collections.Generic;

namespace Domain.Util
{
    public static class NumeroCupom
    {
        public static List<string> RetornoNumero()
        {
            var lista = new List<string>();

            var gerarCupom = GerarCupomHelper.Gerar();

            var qrcode = Qrcode.GerarQRCodeBase64(gerarCupom);

            var qrcodeBase64 = qrcode;
            var foto = Functions.GetRandomString() + ".jpg";
            UploadImagem.Decodificar(qrcode, foto, @"\wwwroot\imagens\cupom\", 400, 400);

            lista.Add(gerarCupom);
            lista.Add(foto);

            return lista;
        }
    }
}
