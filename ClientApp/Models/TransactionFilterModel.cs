using System;

namespace FinanceManager.ClientApp.Models
{
    public class TransactionFilterModel
    {
        public TransactionType? Type { get; set; }
        public string? CategoryId { get; set; }
        public string? AccountId { get; set; }
        public string? CreditCardId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string? SearchTerm { get; set; }
        public bool? IsRecurring { get; set; }
        public bool IncludePending { get; set; } = true;
    }
}