using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.ClientApp.Models
{
    public class TransactionFormModel : IValidatableObject
    {
        public string? Id { get; set; }
        
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; } = DateTime.Today;
        
        [Required(ErrorMessage = "O tipo de transação é obrigatório")]
        public TransactionType Type { get; set; } = TransactionType.Expense;
        
        [Required(ErrorMessage = "A conta é obrigatória")]
        public string? AccountId { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? ToAccountId { get; set; }
        
        public bool IsRecurring { get; set; } = false;
        
        public string? RecurrencePattern { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsPending { get; set; } = false;
        
        public string? Notes { get; set; }
        
        public List<string>? TagIds { get; set; } = new();

        // Alias para compatibilidade com Páginas que usam `Tags`
        public IEnumerable<string> Tags
        {
            get => TagIds ?? new List<string>();
            set => TagIds = value.ToList();
        }
        
        // Propriedades adicionais
        public string? Location { get; set; }
        
        public bool IsReconciled { get; set; } = false;
        
        public bool IsInstallment { get; set; } = false;
        
        public int? TotalInstallments { get; set; }
        
        public int? CurrentInstallment { get; set; }

        public string? ReceiptUrl { get; set; }

        public string? CreditCardId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type == TransactionType.Transfer && string.IsNullOrWhiteSpace(ToAccountId))
            {
                yield return new ValidationResult("A conta de destino é obrigatória para transferências.", new[] { nameof(ToAccountId) });
            }

            if (IsRecurring && string.IsNullOrWhiteSpace(RecurrencePattern))
            {
                yield return new ValidationResult("O padrão de recorrência é obrigatório para transações recorrentes.", new[] { nameof(RecurrencePattern) });
            }

            if (IsRecurring && EndDate.HasValue && EndDate.Value < Date)
            {
                yield return new ValidationResult("A data final da recorrência não pode ser anterior à data da transação.", new[] { nameof(EndDate) });
            }

            if(IsInstallment)
            {
                if(!TotalInstallments.HasValue || TotalInstallments <= 0)
                {
                    yield return new ValidationResult("O número total de parcelas deve ser maior que zero.", new[] { nameof(TotalInstallments) });
                }
                if (!CurrentInstallment.HasValue || CurrentInstallment <= 0)
                {
                    yield return new ValidationResult("O número da parcela atual deve ser maior que zero.", new[] { nameof(CurrentInstallment) });
                }
                if(CurrentInstallment > TotalInstallments)
                {
                    yield return new ValidationResult("A parcela atual não pode ser maior que o total de parcelas.", new[] { nameof(CurrentInstallment), nameof(TotalInstallments) });
                }
            }
        }
    }
}
