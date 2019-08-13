using Newtonsoft.Json;

namespace Api.Util
{
    public static class DeserializerObjeto<T>
    {
       
        public static dynamic RetornoObjetoTipado(dynamic body)
        {
            string output = JsonConvert.SerializeObject(body);
            return JsonConvert.DeserializeObject<T>(output);
        }
    }
}
