using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class BudgetFormModel
    {
        [Required(ErrorMessage = "Digite um nome")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Name { get; set; } = string.Empty; // Inicializado
        
        public string Description { get; set; } = string.Empty; // Inicializado
        
        [Required(ErrorMessage = "Digite um valor")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Selecione um período")]
        public BudgetPeriod Period { get; set; }
        
        [Required(ErrorMessage = "Selecione uma data de início")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        
        public DateTime? EndDate { get; set; }
        
        public int? CategoryId { get; set; }
        
        public bool IsRecurring { get; set; }
    }
    
    public class InvestmentFormModel
    {
        [Required(ErrorMessage = "Digite um nome")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Selecione um tipo")]
        public InvestmentType Type { get; set; }
        
        [Required(ErrorMessage = "Digite um valor inicial")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal InitialAmount { get; set; }
        
        [Required(ErrorMessage = "Digite um valor atual")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal CurrentValue { get; set; }
        
        [Required(ErrorMessage = "Selecione uma data de início")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        
        public DateTime? EndDate { get; set; }
        
        public decimal ReturnRate { get; set; }
        
        public string Notes { get; set; } = string.Empty;
        
        // Propriedades adicionais
        public bool IsActive { get; set; } = true;
        public DateTime AcquisitionDate { get; set; } = DateTime.Today;
        public int? LiquidityDays { get; set; }
        public DateTime? MaturityDate { get; set; }
        
        // Propriedades necessárias para o formulário
        public decimal Profitability { get => ReturnRate; set => ReturnRate = value; }
        public InvestmentRiskLevel Risk { get; set; } = InvestmentRiskLevel.Medium;
        public string Institution { get; set; } = string.Empty;
        
        // Compatibilidade para campos correspondentes
        public decimal InitialValue { get => InitialAmount; set => InitialAmount = value; }
        
        // Métodos para conversão
        public InvestmentCreateModel ToCreateModel()
        {
            return new InvestmentCreateModel
            {
                Name = this.Name,
                Description = this.Notes,
                Type = this.Type,
                RiskLevel = InvestmentRiskLevel.Medium,
                InitialValue = this.InitialAmount,
                CurrentValue = this.CurrentValue,
                Profitability = this.ReturnRate,
                Institution = string.Empty,
                StartDate = this.AcquisitionDate,
                MaturityDate = this.MaturityDate,
                Icon = string.Empty,
                Color = string.Empty
            };
        }
        
        public InvestmentUpdateModel ToUpdateModel()
        {
            return new InvestmentUpdateModel
            {
                Name = this.Name,
                Description = this.Notes,
                CurrentValue = this.CurrentValue,
                Profitability = this.ReturnRate,
                IsActive = this.IsActive
            };
        }
    }

    public class InvestmentTransactionFormModel
    {
        [Required(ErrorMessage = "O investimento é obrigatório")]
        public string InvestmentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um tipo")]
        public InvestmentTransactionType Type { get; set; }
        
        [Required(ErrorMessage = "Digite um valor")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Digite a quantidade de ações/cotas")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public decimal Quantity { get; set; }
        
        [Required(ErrorMessage = "Digite o preço por ação/cota")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }
        
        public decimal Shares { get => Quantity; set => Quantity = value; }
        public decimal PricePerShare { get => Price; set => Price = value; }
        public decimal Taxes { get; set; }
        
        [Required(ErrorMessage = "Selecione uma data")]
        public DateTime Date { get; set; } = DateTime.Today;
        
        public string Notes { get; set; } = string.Empty;
    }
}