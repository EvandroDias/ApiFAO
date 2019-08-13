using DinkToPdf;
using System.IO;

namespace Api.Util
{
    public static class ConfiguracaoPdf
    {
        public static GlobalSettings _GlobalSettings(Orientation orientation)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 2 },
                DocumentTitle = "PDF Report",
                
                //Out = @"C:\Employee_Report.pdf" // USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
            };

            return globalSettings;
        }

        public static ObjectSettings _ObjectSettings(dynamic obj,string diretorio,string arquivo)
        {
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = obj,
                //Page = "https://code-maze.com/", USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), diretorio, arquivo),LoadImages=true },
                
                //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Pagina [page] de [toPage]", Line = true },
                // FooterSettings = { FontName = "Arial", FontSize = 9, Line = true}
            };

            return objectSettings;
        }
        
      
    }
}
