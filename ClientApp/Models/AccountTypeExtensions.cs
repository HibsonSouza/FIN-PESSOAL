using System;
using System.Collections.Generic;
using MudBlazor;

namespace FinanceManager.ClientApp.Models
{
    public static class AccountTypeExtensions
    {
        public static string ToDisplayString(this AccountType type)
        {
            return type switch
            {
                AccountType.Checking => "Conta Corrente",
                AccountType.Savings => "Poupança",
                AccountType.CreditCard => "Cartão de Crédito",
                AccountType.Investment => "Investimento",
                AccountType.Loan => "Empréstimo",
                AccountType.Cash => "Dinheiro",
                AccountType.Digital => "Carteira Digital",
                AccountType.Other => "Outros",
                _ => "Desconhecido"
            };
        }

        public static string GetDefaultIcon(this AccountType type)
        {
            return type switch
            {
                AccountType.Checking => Icons.Material.Filled.AccountBalance,
                AccountType.Savings => Icons.Material.Filled.Savings,
                AccountType.CreditCard => Icons.Material.Filled.CreditCard,
                AccountType.Investment => Icons.Material.Filled.TrendingUp,
                AccountType.Loan => Icons.Material.Filled.RequestQuote,
                AccountType.Cash => Icons.Material.Filled.LocalAtm,
                AccountType.Digital => Icons.Material.Filled.AccountBalanceWallet,
                AccountType.Other => Icons.Material.Filled.Dialpad,
                _ => Icons.Material.Filled.Help
            };
        }
    }
}
