using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Data
{
    /// <summary>
    /// Contexto de banco de dados para a aplicação Finance Manager
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Transaction> Transactions { get; set; }
        public DbSet<Models.CreditCard> CreditCards { get; set; }
        public DbSet<Models.Budget> Budgets { get; set; }
        public DbSet<Models.Goal> Goals { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuração de relacionamentos e índices
            
            // User
            modelBuilder.Entity<Models.User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
            // Account
            modelBuilder.Entity<Models.Account>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Category
            modelBuilder.Entity<Models.Category>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Transaction
            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.DestinationAccount)
                .WithMany()
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
                
            // CreditCard
            modelBuilder.Entity<Models.CreditCard>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Models.CreditCard>()
                .HasOne(c => c.Account)
                .WithMany()
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
                
            // Budget
            modelBuilder.Entity<Models.Budget>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Models.Budget>()
                .HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
                
            // Goal
            modelBuilder.Entity<Models.Goal>()
                .HasOne(g => g.User)
                .WithMany()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Models.Goal>()
                .HasOne(g => g.Account)
                .WithMany()
                .HasForeignKey(g => g.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
                
            // Dados iniciais (seed)
            SeedData(modelBuilder);
        }
        
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Categorias padrão
            modelBuilder.Entity<Models.Category>().HasData(
                new Models.Category
                {
                    Id = 1,
                    Name = "Alimentação",
                    Type = Models.Enums.TransactionType.Expense,
                    Color = "#FF5722",
                    Icon = "Restaurant",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 2,
                    Name = "Transporte",
                    Type = Models.Enums.TransactionType.Expense,
                    Color = "#2196F3",
                    Icon = "DirectionsCar",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 3,
                    Name = "Moradia",
                    Type = Models.Enums.TransactionType.Expense,
                    Color = "#4CAF50",
                    Icon = "Home",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 4,
                    Name = "Saúde",
                    Type = Models.Enums.TransactionType.Expense,
                    Color = "#F44336",
                    Icon = "LocalHospital",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 5,
                    Name = "Lazer",
                    Type = Models.Enums.TransactionType.Expense,
                    Color = "#9C27B0",
                    Icon = "SportsEsports",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 6,
                    Name = "Educação",
                    Type = Models.Enums.TransactionType.Expense,
                    Color = "#009688",
                    Icon = "School",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 7,
                    Name = "Salário",
                    Type = Models.Enums.TransactionType.Income,
                    Color = "#4CAF50",
                    Icon = "Work",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 8,
                    Name = "Investimentos",
                    Type = Models.Enums.TransactionType.Income,
                    Color = "#FFC107",
                    Icon = "TrendingUp",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Models.Category
                {
                    Id = 9,
                    Name = "Transferência",
                    Type = Models.Enums.TransactionType.Transfer,
                    Color = "#607D8B",
                    Icon = "SwapHoriz",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
