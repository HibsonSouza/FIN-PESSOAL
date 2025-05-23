using FinanceManager.Models;
using FluentValidation;

namespace FinanceManager.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Tipo de categoria inválido");

            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("A descrição não pode ter mais de 255 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Icon)
                .MaximumLength(50).WithMessage("O ícone não pode ter mais de 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Icon));

            RuleFor(x => x.Color)
                .MaximumLength(20).WithMessage("A cor não pode ter mais de 20 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Color));
        }
    }
}
