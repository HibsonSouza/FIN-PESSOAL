using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
        public TransactionType Type { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public int? AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountIcon { get; set; }
        public string AccountColor { get; set; }
        public bool IsReconciled { get; set; }
        public string Notes { get; set; }
        public List<string> Tags { get; set; }
    }

    public enum TransactionType
    {
        Income,
        Expense,
        Transfer
    }

    public class TransactionCreateModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "O tipo de transação é obrigatório")]
        public TransactionType Type { get; set; }

        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "A conta é obrigatória")]
        public int AccountId { get; set; }

        public bool IsRecurring { get; set; }

        public bool IsReconciled { get; set; }

        public string Notes { get; set; }

        public List<string> Tags { get; set; }
    }

    public class TransactionUpdateModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "O tipo de transação é obrigatório")]
        public TransactionType Type { get; set; }

        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "A conta é obrigatória")]
        public int AccountId { get; set; }

        public bool IsRecurring { get; set; }

        public bool IsReconciled { get; set; }

        public string Notes { get; set; }

        public List<string> Tags { get; set; }
    }

    public class TransactionBulkUpdateModel
    {
        public List<int> TransactionIds { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsReconciled { get; set; }
        public List<string> TagsToAdd { get; set; }
        public List<string> TagsToRemove { get; set; }
    }

    public class TransactionFilterModel
    {
        public DateRange DateRange { get; set; } = DateRange.CurrentMonth();
        public List<int> AccountIds { get; set; } = new List<int>();
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<TransactionType> Types { get; set; } = new List<TransactionType>();
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public bool? IsReconciled { get; set; }
        public string SearchText { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class ImportTransactionsModel
    {
        public int AccountId { get; set; }
        public string FileContent { get; set; }
        public string FileFormat { get; set; } // CSV, OFX, etc.
        public Dictionary<string, string> MappingConfiguration { get; set; }
    }

    public class TransactionSummaryViewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
        public List<CategorySummary> TopExpenseCategories { get; set; } = new List<CategorySummary>();
        public List<CategorySummary> TopIncomeCategories { get; set; } = new List<CategorySummary>();
    }

    public class CategorySummary
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
    }

    public class MonthlyFinancialSummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }

    public class FinancialFlowViewModel
    {
        public List<MonthlyFinancialSummary> MonthlySummaries { get; set; } = new List<MonthlyFinancialSummary>();
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }
}