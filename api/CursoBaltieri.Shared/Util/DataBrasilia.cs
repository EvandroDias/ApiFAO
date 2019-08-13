using System;

namespace Shared.Util
{
    public static class DataBrasilia
    {
        public static DateTime HorarioBrasilia()
        {
            DateTime timeUtc = DateTime.UtcNow;
            var brasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, brasilia);
        }
    }
}
