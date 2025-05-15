using System;

namespace FinanceManager.ClientApp.Models
{
    public class DateTimeRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Retorna um DateTimeRange para o mês atual
        /// </summary>
        public static DateTimeRange ThisMonth()
        {
            var today = DateTime.Today;
            var firstDay = new DateTime(today.Year, today.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            
            return new DateTimeRange(firstDay, lastDay);
        }

        /// <summary>
        /// Retorna um DateTimeRange para o mês anterior
        /// </summary>
        public static DateTimeRange LastMonth()
        {
            var today = DateTime.Today;
            var firstDayLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var lastDayLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
            
            return new DateTimeRange(firstDayLastMonth, lastDayLastMonth);
        }

        /// <summary>
        /// Retorna um DateTimeRange para o mês especificado
        /// </summary>
        public static DateTimeRange ForMonth(int year, int month)
        {
            var firstDay = new DateTime(year, month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            
            return new DateTimeRange(firstDay, lastDay);
        }

        /// <summary>
        /// Retorna um DateTimeRange para o ano atual
        /// </summary>
        public static DateTimeRange ThisYear()
        {
            var today = DateTime.Today;
            var firstDay = new DateTime(today.Year, 1, 1);
            var lastDay = new DateTime(today.Year, 12, 31);
            
            return new DateTimeRange(firstDay, lastDay);
        }

        /// <summary>
        /// Retorna um DateTimeRange para o ano anterior
        /// </summary>
        public static DateTimeRange LastYear()
        {
            var today = DateTime.Today;
            var firstDay = new DateTime(today.Year - 1, 1, 1);
            var lastDay = new DateTime(today.Year - 1, 12, 31);
            
            return new DateTimeRange(firstDay, lastDay);
        }

        /// <summary>
        /// Retorna um DateTimeRange para os últimos N dias
        /// </summary>
        public static DateTimeRange LastNDays(int days)
        {
            var today = DateTime.Today;
            var start = today.AddDays(-days);
            
            return new DateTimeRange(start, today);
        }
    }
}