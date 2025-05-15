using System;
using System.Collections.Generic;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public AccountType Type { get; set; }
        public string AccountNumber { get; set; }
        public string Agency { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IncludeInTotal { get; set; } = true;
        public List<TransactionViewModel> RecentTransactions { get; set; }
    }

    public enum AccountType
    {
        Checking,
        Savings,
        Investment,
        CreditCard,
        Cash,
        Other
    }

    public class AccountCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal InitialBalance { get; set; }
        public string BankName { get; set; }
        public AccountType Type { get; set; }
        public string AccountNumber { get; set; }
        public string Agency { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IncludeInTotal { get; set; } = true;
    }

    public class AccountUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BankName { get; set; }
        public AccountType Type { get; set; }
        public string AccountNumber { get; set; }
        public string Agency { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeInTotal { get; set; }
    }
}