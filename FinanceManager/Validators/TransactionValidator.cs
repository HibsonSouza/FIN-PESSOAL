using FinanceManager.Models;
using FluentValidation;

namespace FinanceManager.Validators
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("A conta é obrigatória");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("A categoria é obrigatória");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("A data é obrigatória");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("O valor é obrigatório")
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória")
                .MaximumLength(200).WithMessage("A descrição não pode ter mais de 200 caracteres");
        }
    }
}
