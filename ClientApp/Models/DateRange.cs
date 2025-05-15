using System;

namespace FinanceManager.ClientApp.Models
{
    public class DateTimeRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public DateTimeRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("Data final não pode ser anterior à data inicial");
            }
            
            StartDate = startDate;
            EndDate = endDate;
        }
        
        public static DateTimeRange CurrentMonth()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            return new DateTimeRange(firstDayOfMonth, lastDayOfMonth);
        }
        
        public static DateTimeRange CurrentYear()
        {
            var today = DateTime.Today;
            var firstDayOfYear = new DateTime(today.Year, 1, 1);
            var lastDayOfYear = new DateTime(today.Year, 12, 31);
            
            return new DateTimeRange(firstDayOfYear, lastDayOfYear);
        }
        
        public static DateTimeRange LastMonth()
        {
            var today = DateTime.Today;
            var firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
            
            return new DateTimeRange(firstDayOfLastMonth, lastDayOfLastMonth);
        }
        
        public static DateTimeRange Last30Days()
        {
            var today = DateTime.Today;
            var thirtyDaysAgo = today.AddDays(-30);
            
            return new DateTimeRange(thirtyDaysAgo, today);
        }
        
        public static DateTimeRange Last90Days()
        {
            var today = DateTime.Today;
            var ninetyDaysAgo = today.AddDays(-90);
            
            return new DateTimeRange(ninetyDaysAgo, today);
        }
        
        public static DateTimeRange Custom(DateTime startDate, DateTime endDate)
        {
            return new DateTimeRange(startDate, endDate);
        }
        
        public override string ToString()
        {
            return $"{StartDate:dd/MM/yyyy} - {EndDate:dd/MM/yyyy}";
        }
    }
}