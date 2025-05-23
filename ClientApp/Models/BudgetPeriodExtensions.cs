using System.Globalization;

namespace FinanceManager.ClientApp.Models
{
    public static class BudgetPeriodExtensions
    {
        public static string ToDisplayString(this BudgetPeriod period)
        {
            return period switch
            {
                BudgetPeriod.Daily => "Diário",
                BudgetPeriod.Weekly => "Semanal",
                BudgetPeriod.BiWeekly => "Quinzenal",
                BudgetPeriod.Monthly => "Mensal",
                BudgetPeriod.Quarterly => "Trimestral",
                BudgetPeriod.SemiAnnual => "Semestral",
                BudgetPeriod.Annual => "Anual",
                BudgetPeriod.Custom => "Personalizado",
                _ => "Desconhecido"
            };
        }
        
        public static DateTime GetStartDate(this BudgetPeriod period, DateTime? customStart = null)
        {
            if (period == BudgetPeriod.Custom && customStart.HasValue)
                return customStart.Value;
                
            var today = DateTime.Today;
            
            return period switch
            {
                BudgetPeriod.Daily => today,
                BudgetPeriod.Weekly => today.AddDays(-(int)today.DayOfWeek),
                BudgetPeriod.BiWeekly => today.AddDays(-(int)today.DayOfWeek - 7),
                BudgetPeriod.Monthly => new DateTime(today.Year, today.Month, 1),
                BudgetPeriod.Quarterly => new DateTime(today.Year, (today.Month - 1) / 3 * 3 + 1, 1),
                BudgetPeriod.SemiAnnual => new DateTime(today.Year, (today.Month <= 6) ? 1 : 7, 1),
                BudgetPeriod.Annual => new DateTime(today.Year, 1, 1),
                _ => today
            };
        }
        
        public static DateTime GetEndDate(this BudgetPeriod period, DateTime? customEnd = null)
        {
            if (period == BudgetPeriod.Custom && customEnd.HasValue)
                return customEnd.Value;
                
            var startDate = period.GetStartDate();
            
            return period switch
            {
                BudgetPeriod.Daily => startDate.AddDays(1).AddSeconds(-1),
                BudgetPeriod.Weekly => startDate.AddDays(7).AddSeconds(-1),
                BudgetPeriod.BiWeekly => startDate.AddDays(14).AddSeconds(-1),
                BudgetPeriod.Monthly => startDate.AddMonths(1).AddSeconds(-1),
                BudgetPeriod.Quarterly => startDate.AddMonths(3).AddSeconds(-1),
                BudgetPeriod.SemiAnnual => startDate.AddMonths(6).AddSeconds(-1),
                BudgetPeriod.Annual => startDate.AddYears(1).AddSeconds(-1),
                _ => DateTime.Today.AddDays(30)
            };
        }
        
        public static string GetPeriodDisplayString(this BudgetPeriod period, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (period == BudgetPeriod.Custom && startDate.HasValue && endDate.HasValue)
            {
                return $"{startDate.Value.ToString("dd/MM/yyyy")} - {endDate.Value.ToString("dd/MM/yyyy")}";
            }
            
            var start = period.GetStartDate(startDate);
            var end = period.GetEndDate(endDate);
            
            return period switch
            {
                BudgetPeriod.Daily => start.ToString("dd 'de' MMMM", new CultureInfo("pt-BR")),
                BudgetPeriod.Weekly => $"{start.ToString("dd/MM")} - {end.ToString("dd/MM")}",
                BudgetPeriod.BiWeekly => $"{start.ToString("dd/MM")} - {end.ToString("dd/MM")}",
                BudgetPeriod.Monthly => start.ToString("MMMM 'de' yyyy", new CultureInfo("pt-BR")),
                BudgetPeriod.Quarterly => $"{GetQuarterName(start.Month)} {start.Year}",
                BudgetPeriod.SemiAnnual => $"{(start.Month <= 6 ? "1º" : "2º")} Semestre de {start.Year}",
                BudgetPeriod.Annual => start.Year.ToString(),
                _ => $"{start.ToString("dd/MM/yyyy")} - {end.ToString("dd/MM/yyyy")}"
            };
        }
        
        private static string GetQuarterName(int month)
        {
            return (month - 1) / 3 switch
            {
                0 => "1º Trimestre",
                1 => "2º Trimestre",
                2 => "3º Trimestre",
                3 => "4º Trimestre",
                _ => string.Empty
            };
        }
    }
}
