
using System;


namespace Domain.Util
{
    public static class GerarCupomHelper
    {
        public static string Gerar()
        {
            Random rand = new Random();

            string numero = "";

            string ano = String.Format("{0:yy}", DateTime.Now);
            string mes = String.Format("{0:MM}", DateTime.Now);
            string dia = String.Format("{0:dd}", DateTime.Now);
            string hora = String.Format("{0:hh}", DateTime.Now);
            string mili = String.Format("{0:ff}", DateTime.Now);
            string random = rand.Next(1, 99999).ToString();

            numero = ano + mes + dia + hora + mili + random;
          

            return numero;
        }

      }
}
