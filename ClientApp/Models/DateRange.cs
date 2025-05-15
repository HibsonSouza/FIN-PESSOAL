using System;

namespace FinanceManager.ClientApp.Models
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateRange()
        {
            // Padrão: Mês atual
            var today = DateTime.Today;
            StartDate = new DateTime(today.Year, today.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);
        }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        // Filtros pré-definidos
        public static DateRange CurrentMonth()
        {
            var today = DateTime.Today;
            return new DateRange(
                new DateTime(today.Year, today.Month, 1),
                new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month))
            );
        }

        public static DateRange PreviousMonth()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var firstDayOfPreviousMonth = firstDayOfMonth.AddMonths(-1);
            return new DateRange(
                firstDayOfPreviousMonth,
                firstDayOfPreviousMonth.AddDays(DateTime.DaysInMonth(firstDayOfPreviousMonth.Year, firstDayOfPreviousMonth.Month) - 1)
            );
        }

        public static DateRange CurrentYear()
        {
            var today = DateTime.Today;
            return new DateRange(
                new DateTime(today.Year, 1, 1),
                new DateTime(today.Year, 12, 31)
            );
        }

        public static DateRange Last30Days()
        {
            var today = DateTime.Today;
            return new DateRange(
                today.AddDays(-30),
                today
            );
        }

        public static DateRange Last90Days()
        {
            var today = DateTime.Today;
            return new DateRange(
                today.AddDays(-90),
                today
            );
        }

        public static DateRange Last6Months()
        {
            var today = DateTime.Today;
            return new DateRange(
                today.AddMonths(-6),
                today
            );
        }

        public static DateRange Last12Months()
        {
            var today = DateTime.Today;
            return new DateRange(
                today.AddMonths(-12),
                today
            );
        }

        public override string ToString()
        {
            return $"{StartDate:dd/MM/yyyy} - {EndDate:dd/MM/yyyy}";
        }
    }
}