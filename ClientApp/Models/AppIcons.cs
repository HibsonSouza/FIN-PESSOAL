using MudBlazor;

namespace FinanceManager.ClientApp.Models
{
    public static class AppIcons
    {
        public static string Dashboard = Icons.Material.Filled.Dashboard;
        public static string Accounts = Icons.Material.Filled.AccountBalance;
        public static string Transactions = Icons.Material.Filled.Receipt;
        public static string Categories = Icons.Material.Filled.Category;
        public static string Budget = Icons.Material.Filled.BarChart;
        public static string Reports = Icons.Material.Filled.InsertChart;
        public static string CreditCards = Icons.Material.Filled.CreditCard;
        public static string Settings = Icons.Material.Filled.Settings;
        public static string Users = Icons.Material.Filled.People;
        public static string Logout = Icons.Material.Filled.Logout;
        public static string Login = Icons.Material.Filled.Login;
        public static string Add = Icons.Material.Filled.Add;
        public static string Edit = Icons.Material.Filled.Edit;
        public static string Delete = Icons.Material.Filled.Delete;
        public static string Save = Icons.Material.Filled.Save;
        public static string Cancel = Icons.Material.Filled.Cancel;
        public static string Search = Icons.Material.Filled.Search;
        public static string Filter = Icons.Material.Filled.FilterList;
        public static string Sort = Icons.Material.Filled.Sort;
        public static string Refresh = Icons.Material.Filled.Refresh;
        public static string Calendar = Icons.Material.Filled.CalendarToday;
        public static string Money = Icons.Material.Filled.AttachMoney;
        public static string Income = Icons.Material.Filled.TrendingUp;
        public static string Expense = Icons.Material.Filled.TrendingDown;
        public static string Transfer = Icons.Material.Filled.SwapHoriz;
        public static string CashFlow = Icons.Material.Filled.ShowChart;
        public static string Goals = Icons.Material.Filled.Flag;
        public static string Investments = Icons.Material.Filled.AccountBalance;
        
        public static string GetTransactionTypeIcon(TransactionType type)
        {
            return type switch
            {
                TransactionType.Income => Income,
                TransactionType.Expense => Expense,
                TransactionType.Transfer => Transfer,
                _ => Money
            };
        }
    }
}
