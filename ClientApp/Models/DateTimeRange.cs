using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class DateTimeRange
    {
        public DateTime Start { get; set; } = DateTime.Today.AddMonths(-1);
        
        public DateTime End { get; set; } = DateTime.Today;
        
        public string DisplayName { get; set; } = "Último Mês";
        
        // Construtores
        public DateTimeRange() { }
        
        public DateTimeRange(DateTime start, DateTime end, string displayName = "")
        {
            Start = start;
            End = end;
            DisplayName = displayName;
        }
        
        // Métodos para criar períodos comuns
        public static DateTimeRange CurrentMonth()
        {
            var now = DateTime.Today;
            var start = new DateTime(now.Year, now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            return new DateTimeRange(start, end, "Mês Atual");
        }
        
        public static DateTimeRange PreviousMonth()
        {
            var now = DateTime.Today;
            var start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            var end = new DateTime(now.Year, now.Month, 1).AddDays(-1);
            return new DateTimeRange(start, end, "Mês Anterior");
        }
        
        public static DateTimeRange Last30Days()
        {
            var now = DateTime.Today;
            var start = now.AddDays(-30);
            return new DateTimeRange(start, now, "Últimos 30 Dias");
        }
        
        public static DateTimeRange Last90Days()
        {
            var now = DateTime.Today;
            var start = now.AddDays(-90);
            return new DateTimeRange(start, now, "Últimos 90 Dias");
        }
        
        public static DateTimeRange CurrentYear()
        {
            var now = DateTime.Today;
            var start = new DateTime(now.Year, 1, 1);
            var end = new DateTime(now.Year, 12, 31);
            return new DateTimeRange(start, end, "Ano Atual");
        }
        
        public static DateTimeRange PreviousYear()
        {
            var now = DateTime.Today;
            var start = new DateTime(now.Year - 1, 1, 1);
            var end = new DateTime(now.Year - 1, 12, 31);
            return new DateTimeRange(start, end, "Ano Anterior");
        }
        
        public static DateTimeRange LastYear()
        {
            var now = DateTime.Today;
            var start = now.AddYears(-1);
            return new DateTimeRange(start, now, "Último Ano");
        }
        
        public static DateTimeRange ThisWeek()
        {
            var now = DateTime.Today;
            var start = now.AddDays(-(int)now.DayOfWeek);
            return new DateTimeRange(start, now, "Esta Semana");
        }
        
        public static DateTimeRange ThisMonth()
        {
            return CurrentMonth();
        }
        
        public static DateTimeRange LastWeek()
        {
            var now = DateTime.Today;
            var start = now.AddDays(-(int)now.DayOfWeek - 7);
            var end = start.AddDays(6);
            return new DateTimeRange(start, end, "Semana Passada");
        }
        
        public static List<DateTimeRange> GetCommonRanges()
        {
            return new List<DateTimeRange>
            {
                CurrentMonth(),
                PreviousMonth(),
                Last30Days(),
                Last90Days(),
                CurrentYear(),
                PreviousYear(),
                ThisWeek(),
                LastWeek()
            };
        }
    }
}