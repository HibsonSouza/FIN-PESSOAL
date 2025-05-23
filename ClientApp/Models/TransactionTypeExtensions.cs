using MudBlazor;

namespace FinanceManager.ClientApp.Models
{
    public static class TransactionTypeExtensions
    {
        public static string ToDisplayString(this TransactionType type)
        {
            return type switch
            {
                TransactionType.Income => "Receita",
                TransactionType.Expense => "Despesa",
                TransactionType.Transfer => "Transferência",
                _ => "Desconhecido"
            };
        }
        
        public static string GetIcon(this TransactionType type)
        {
            return type switch
            {
                TransactionType.Income => Icons.Material.Filled.TrendingUp,
                TransactionType.Expense => Icons.Material.Filled.TrendingDown,
                TransactionType.Transfer => Icons.Material.Filled.SwapHoriz,
                _ => Icons.Material.Filled.Help
            };
        }
        
        public static Color GetColor(this TransactionType type)
        {
            return type switch
            {
                TransactionType.Income => Color.Success,
                TransactionType.Expense => Color.Error,
                TransactionType.Transfer => Color.Info,
                _ => Color.Default
            };
        }
        
        public static string GetColorHex(this TransactionType type)
        {
            return type switch
            {
                TransactionType.Income => "#4caf50",  // Verde
                TransactionType.Expense => "#f44336", // Vermelho
                TransactionType.Transfer => "#2196f3", // Azul
                _ => "#9e9e9e" // Cinza
            };
        }
        
        // Método para compatibilidade com constantes em maiúsculas
        public static TransactionType FromConstant(string constant)
        {
            return constant.ToUpperInvariant() switch
            {
                "INCOME" => TransactionType.Income,
                "EXPENSE" => TransactionType.Expense,
                "TRANSFER" => TransactionType.Transfer,
                "INVESTMENT" => TransactionType.Income, // Como não temos um tipo específico para investimento
                _ => TransactionType.Expense // Valor padrão
            };
        }
    }
}
