using DinkToPdf;

namespace Api.Util
{
    public static class RetornoPdf
    {
        public static HtmlToPdfDocument Retorno(string html,string diretorio,string arquivo, Orientation orientacao)
        {
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = ConfiguracaoPdf._GlobalSettings(orientacao),
                Objects = { ConfiguracaoPdf._ObjectSettings(html, diretorio,arquivo) }
            };

            return pdf;
        }
    }
}
