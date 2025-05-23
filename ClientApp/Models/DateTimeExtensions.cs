using System.Globalization;

namespace FinanceManager.ClientApp.Models
{
    public static class DateTimeExtensions
    {
        public static string ToDisplayString(this DateTime date, string format = "dd/MM/yyyy")
        {
            return date.ToString(format, CultureInfo.GetCultureInfo("pt-BR"));
        }
        
        public static string ToDisplayString(this DateTime? date, string format = "dd/MM/yyyy")
        {
            return date?.ToString(format, CultureInfo.GetCultureInfo("pt-BR")) ?? "-";
        }
        
        public static string ToRelativeDateString(this DateTime date)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);
            var tomorrow = today.AddDays(1);
            
            if (date.Date == today)
                return "Hoje";
            
            if (date.Date == yesterday)
                return "Ontem";
            
            if (date.Date == tomorrow)
                return "Amanhã";
            
            var diff = (date.Date - today).TotalDays;
            
            if (diff > 0 && diff < 7)
                return $"Em {diff} dias";
            
            if (diff < 0 && diff > -7)
                return $"Há {Math.Abs(diff)} dias";
            
            return date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
        }
        
        public static string GetMonthName(this DateTime date)
        {
            return CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(date.Month);
        }
        
        public static string GetShortMonthName(this DateTime date)
        {
            return CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetAbbreviatedMonthName(date.Month);
        }
    }
}
