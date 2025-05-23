using FinanceManager.Models;
using FluentValidation;

namespace FinanceManager.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da conta é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");

            RuleFor(x => x.Balance)
                .NotNull().WithMessage("O saldo é obrigatório");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Tipo de conta inválido");

            RuleFor(x => x.BankName)
                .MaximumLength(50).WithMessage("O nome do banco não pode ter mais de 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.BankName));

            RuleFor(x => x.AccountNumber)
                .MaximumLength(50).WithMessage("O número da conta não pode ter mais de 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.AccountNumber));

            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("A descrição não pode ter mais de 255 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
