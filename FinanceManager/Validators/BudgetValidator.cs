using FinanceManager.Models;
using FluentValidation;

namespace FinanceManager.Validators
{
    public class BudgetValidator : AbstractValidator<Budget>
    {
        public BudgetValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("A categoria é obrigatória");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("O valor é obrigatório")
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero");

            RuleFor(x => x.Month)
                .NotEmpty().WithMessage("O mês é obrigatório")
                .InclusiveBetween(1, 12).WithMessage("O mês deve estar entre 1 e 12");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("O ano é obrigatório")
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano deve ser igual ou maior que o ano atual");
        }
    }
}
