using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class CreditCardFormModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do cartão é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O nome do banco é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do banco deve ter no máximo 50 caracteres")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "O número do cartão é obrigatório")]
        [StringLength(19, MinimumLength = 13, ErrorMessage = "O número do cartão deve ter entre 13 e 19 caracteres")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O limite do cartão é obrigatório")]
        [Range(0.01, 1000000, ErrorMessage = "O limite deve ser maior que zero")]
        public decimal Limit { get; set; }

        [Required(ErrorMessage = "O dia de vencimento é obrigatório")]
        [Range(1, 31, ErrorMessage = "O dia de vencimento deve estar entre 1 e 31")]
        public int DueDay { get; set; }

        [Required(ErrorMessage = "O dia de fechamento é obrigatório")]
        [Range(1, 31, ErrorMessage = "O dia de fechamento deve estar entre 1 e 31")]
        public int ClosingDay { get; set; }

        public string Color { get; set; } = "#1976D2";

        public string Icon { get; set; } = "fa-solid fa-credit-card";

        public bool IsActive { get; set; } = true;

        // Método para converter para CreditCardCreateModel
        public CreditCardCreateModel ToCreditCardCreateModel()
        {
            return new CreditCardCreateModel
            {
                Name = Name,
                BankName = BankName,
                Number = Number,
                Limit = Limit,
                DueDay = DueDay,
                ClosingDay = ClosingDay,
                Color = Color,
                Icon = Icon
            };
        }

        // Método para converter para CreditCardUpdateModel
        public CreditCardUpdateModel ToCreditCardUpdateModel()
        {
            return new CreditCardUpdateModel
            {
                Name = Name,
                BankName = BankName,
                Number = Number,
                Limit = Limit,
                DueDay = DueDay,
                ClosingDay = ClosingDay,
                Color = Color,
                Icon = Icon,
                IsActive = IsActive
            };
        }

        // Método para criar a partir de um CreditCardViewModel
        public static CreditCardFormModel FromCreditCardViewModel(CreditCardViewModel model)
        {
            return new CreditCardFormModel
            {
                Id = model.Id,
                Name = model.Name,
                BankName = model.BankName,
                Number = model.Number,
                Limit = model.Limit,
                DueDay = model.DueDay,
                ClosingDay = model.ClosingDay,
                Color = model.Color,
                Icon = model.Icon,
                IsActive = model.IsActive
            };
        }
    }
}