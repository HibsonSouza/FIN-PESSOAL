using System;
using MudBlazor;

namespace FinanceManager.ClientApp.Models
{
    public static class DateRangeExtensions
    {
        /// <summary>
        /// Converte um MudBlazor.DateRange para um DateTimeRange
        /// </summary>
        public static DateTimeRange ToDateTimeRange(this MudBlazor.DateRange dateRange, string displayName = "Período Personalizado")
        {
            return new DateTimeRange
            {
                Start = dateRange.Start ?? DateTime.Today.AddMonths(-1),
                End = dateRange.End ?? DateTime.Today,
                DisplayName = displayName
            };
        }

        /// <summary>
        /// Converte um DateTimeRange para um MudBlazor.DateRange
        /// </summary>
        public static MudBlazor.DateRange ToMudDateRange(this DateTimeRange dateTimeRange)
        {
            return new MudBlazor.DateRange(dateTimeRange.Start, dateTimeRange.End);
        }
    }
}
