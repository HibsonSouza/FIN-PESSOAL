using System;

namespace FinanceManager.ClientApp.Models
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateRange()
        {
            // Por padrão, define o intervalo para o mês atual
            Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            End = Start.AddMonths(1).AddDays(-1);
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        // Métodos auxiliares para criar períodos comuns
        public static DateRange CurrentMonth()
        {
            var now = DateTime.Now;
            return new DateRange(
                new DateTime(now.Year, now.Month, 1),
                new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month))
            );
        }

        public static DateRange PreviousMonth()
        {
            var now = DateTime.Now.AddMonths(-1);
            return new DateRange(
                new DateTime(now.Year, now.Month, 1),
                new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month))
            );
        }

        public static DateRange CurrentYear()
        {
            var now = DateTime.Now;
            return new DateRange(
                new DateTime(now.Year, 1, 1),
                new DateTime(now.Year, 12, 31)
            );
        }

        public static DateRange Last30Days()
        {
            return new DateRange(
                DateTime.Now.AddDays(-30),
                DateTime.Now
            );
        }

        public static DateRange Last90Days()
        {
            return new DateRange(
                DateTime.Now.AddDays(-90),
                DateTime.Now
            );
        }

        public static DateRange Custom(DateTime start, DateTime end)
        {
            return new DateRange(start, end);
        }
    }
}