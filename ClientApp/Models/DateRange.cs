using System;

namespace FinanceManager.ClientApp.Models
{
    // Classe renomeada para evitar conflito com MudBlazor.DateRange
    public class CustomDateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public CustomDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("Data final não pode ser anterior à data inicial");
            }
            
            StartDate = startDate;
            EndDate = endDate;
        }
        
        public static CustomDateRange CurrentMonth()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            return new CustomDateRange(firstDayOfMonth, lastDayOfMonth);
        }
        
        public static CustomDateRange CurrentYear()
        {
            var today = DateTime.Today;
            var firstDayOfYear = new DateTime(today.Year, 1, 1);
            var lastDayOfYear = new DateTime(today.Year, 12, 31);
            
            return new CustomDateRange(firstDayOfYear, lastDayOfYear);
        }
        
        public static CustomDateRange LastMonth()
        {
            var today = DateTime.Today;
            var firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
            
            return new CustomDateRange(firstDayOfLastMonth, lastDayOfLastMonth);
        }
        
        public static CustomDateRange Last30Days()
        {
            var today = DateTime.Today;
            var thirtyDaysAgo = today.AddDays(-30);
            
            return new CustomDateRange(thirtyDaysAgo, today);
        }
        
        public static CustomDateRange Last90Days()
        {
            var today = DateTime.Today;
            var ninetyDaysAgo = today.AddDays(-90);
            
            return new CustomDateRange(ninetyDaysAgo, today);
        }
        
        public static CustomDateRange Custom(DateTime startDate, DateTime endDate)
        {
            return new CustomDateRange(startDate, endDate);
        }
        
        public override string ToString()
        {
            return $"{StartDate:dd/MM/yyyy} - {EndDate:dd/MM/yyyy}";
        }
        
        // Converter para/de DateTimeRange
        public DateTimeRange ToDateTimeRange()
        {
            return new DateTimeRange(StartDate, EndDate);
        }
        
        public static CustomDateRange FromDateTimeRange(DateTimeRange range)
        {
            return new CustomDateRange(range.Start, range.End);
        }
    }
}