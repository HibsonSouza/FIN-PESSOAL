using FinanceManager.Data;
using FinanceManager.Models;
using FinanceManager.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Services
{
    /// <summary>
    /// Serviço responsável por semear o banco de dados com dados iniciais
    /// </summary>
    public static class DataSeeder
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                await SeedUsers(context);
            }

            if (!context.Categories.Any())
            {
                await SeedCategories(context);
            }
        }

        private static async Task SeedUsers(ApplicationDbContext context)
        {
            var hasher = new PasswordHasher<User>();
            
            var adminUser = new User
            {
                Name = "Administrador",
                Email = "admin@financepersonal.com",
                CreatedAt = DateTime.UtcNow
            };
            
            // Define a senha como "Admin@123"
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");
            
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCategories(ApplicationDbContext context)
        {
            // Categorias de receita
            var incomeCategories = new List<Category>
            {
                new Category { Name = "Salário", Description = "Rendimentos do trabalho", Type = TransactionType.Income, Icon = "attach_money", Color = "#4CAF50" },
                new Category { Name = "Investimentos", Description = "Rendimentos de investimentos", Type = TransactionType.Income, Icon = "trending_up", Color = "#2196F3" },
                new Category { Name = "Freelance", Description = "Trabalhos temporários", Type = TransactionType.Income, Icon = "work", Color = "#673AB7" },
                new Category { Name = "Presente", Description = "Dinheiro recebido de presente", Type = TransactionType.Income, Icon = "card_giftcard", Color = "#E91E63" },
                new Category { Name = "Outros Rendimentos", Description = "Outras fontes de receita", Type = TransactionType.Income, Icon = "payments", Color = "#FF9800" }
            };

            // Categorias de despesa
            var expenseCategories = new List<Category>
            {
                new Category { Name = "Alimentação", Description = "Gastos com alimentação", Type = TransactionType.Expense, Icon = "restaurant", Color = "#F44336" },
                new Category { Name = "Moradia", Description = "Aluguel, condomínio, etc.", Type = TransactionType.Expense, Icon = "home", Color = "#795548" },
                new Category { Name = "Transporte", Description = "Gastos com transporte", Type = TransactionType.Expense, Icon = "directions_car", Color = "#607D8B" },
                new Category { Name = "Saúde", Description = "Médicos, remédios, etc.", Type = TransactionType.Expense, Icon = "local_hospital", Color = "#009688" },
                new Category { Name = "Educação", Description = "Cursos, materiais, etc.", Type = TransactionType.Expense, Icon = "school", Color = "#3F51B5" },
                new Category { Name = "Lazer", Description = "Entretenimento", Type = TransactionType.Expense, Icon = "sports_esports", Color = "#9C27B0" },
                new Category { Name = "Roupas", Description = "Vestuário", Type = TransactionType.Expense, Icon = "checkroom", Color = "#8BC34A" },
                new Category { Name = "Assinaturas", Description = "Serviços de assinatura", Type = TransactionType.Expense, Icon = "subscriptions", Color = "#FF5722" },
                new Category { Name = "Outras Despesas", Description = "Gastos diversos", Type = TransactionType.Expense, Icon = "miscellaneous_services", Color = "#9E9E9E" }
            };
            
            context.Categories.AddRange(incomeCategories);
            context.Categories.AddRange(expenseCategories);
            
            await context.SaveChangesAsync();
        }
    }
}
