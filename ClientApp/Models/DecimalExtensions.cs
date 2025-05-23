using System.Globalization;

namespace FinanceManager.ClientApp.Models
{
    public static class DecimalExtensions
    {
        public static string ToDisplayString(this decimal value, string currency = "BRL", string cultureCode = "pt-BR")
        {
            var culture = CultureInfo.GetCultureInfo(cultureCode);
            return value.ToString("C", culture);
        }
        
        public static string ToDisplayString(this decimal? value, string currency = "BRL", string cultureCode = "pt-BR")
        {
            if (!value.HasValue)
                return "-";
                
            var culture = CultureInfo.GetCultureInfo(cultureCode);
            return value.Value.ToString("C", culture);
        }
        
        public static string ToPercentageString(this decimal value, int decimals = 1)
        {
            return $"{Math.Round(value * 100, decimals)}%";
        }
        
        public static string ToPercentageString(this double value, int decimals = 1)
        {
            return $"{Math.Round(value, decimals)}%";
        }
    }
}
