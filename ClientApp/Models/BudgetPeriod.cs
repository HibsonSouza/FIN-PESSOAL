namespace FinanceManager.ClientApp.Models
{
    public enum BudgetPeriod
    {
        Daily,
        Weekly,
        BiWeekly,
        Monthly,
        Quarterly,
        SemiAnnual,
        Annual,
        Custom
    }
    
    public static class BudgetPeriodConstants
    {
        public static readonly BudgetPeriod MONTHLY = BudgetPeriod.Monthly;
        public static readonly BudgetPeriod ANNUAL = BudgetPeriod.Annual;
        public static readonly BudgetPeriod WEEKLY = BudgetPeriod.Weekly;
        public static readonly BudgetPeriod DAILY = BudgetPeriod.Daily;
        public static readonly BudgetPeriod QUARTERLY = BudgetPeriod.Quarterly;
        public static readonly BudgetPeriod CUSTOM = BudgetPeriod.Custom;
        public static readonly BudgetPeriod BIWEEKLY = BudgetPeriod.BiWeekly;
        public static readonly BudgetPeriod SEMIANNUAL = BudgetPeriod.SemiAnnual;
    }
}