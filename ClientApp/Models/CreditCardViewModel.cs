using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class CreditCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string Number { get; set; }
        public decimal Limit { get; set; }
        public decimal AvailableLimit { get; set; }
        public int DueDay { get; set; }
        public int ClosingDay { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime LastStatementDate { get; set; }
        public DateTime NextStatementDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public decimal LastStatementAmount { get; set; }
    }

    public class CreditCardCreateModel
    {
        public string Name { get; set; }
        public string BankName { get; set; }
        public string Number { get; set; }
        public decimal Limit { get; set; }
        public int DueDay { get; set; }
        public int ClosingDay { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
    }

    public class CreditCardUpdateModel
    {
        public string Name { get; set; }
        public string BankName { get; set; }
        public string Number { get; set; }
        public decimal Limit { get; set; }
        public int DueDay { get; set; }
        public int ClosingDay { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreditCardStatementModel
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public string CreditCardName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();
    }

    public class CreditCardStatementViewModel
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public string CreditCardName { get; set; }
        public DateTime Month { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DueDate { get; set; }
    }
}