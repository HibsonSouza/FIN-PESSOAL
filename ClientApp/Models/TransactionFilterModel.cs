using System;
using System.Collections.Generic;
using System.Linq;

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
        public bool? IsReconciled { get; set; }
        
        public DateTimeRange? DateRange { get; set; }
        public List<string>? AccountIds { get; set; }
        public List<string>? CategoryIds { get; set; }
        public List<TransactionType>? Types { get; set; }
        public List<string>? TagIds { get; set; }

        public bool IsEmpty =>
            string.IsNullOrEmpty(AccountId) &&
            string.IsNullOrEmpty(CategoryId) &&
            !Type.HasValue &&
            !MinAmount.HasValue &&
            !MaxAmount.HasValue &&
            !IsRecurring.HasValue &&
            !IsReconciled.HasValue &&
            string.IsNullOrWhiteSpace(SearchTerm) &&
            !TagIds?.Any() == true;
            
        public void Clear()
        {
            Type = null;
            CategoryId = null;
            AccountId = null;
            CreditCardId = null;
            StartDate = null;
            EndDate = null;
            MinAmount = null;
            MaxAmount = null;
            SearchTerm = null;
            IsRecurring = null;
            IncludePending = true;
            IsReconciled = null;
            DateRange = DateTimeRange.CurrentMonth();
            AccountIds = null;
            CategoryIds = null;
            Types = null;
            TagIds = null;
        }
    }
}