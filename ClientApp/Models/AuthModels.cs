using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public static class Login
    {
        public class LoginModel
        {
            [Required(ErrorMessage = "O e-mail é obrigatório")]
            [EmailAddress(ErrorMessage = "E-mail inválido")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A senha é obrigatória")]
            [StringLength(100, ErrorMessage = "A senha deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string Password { get; set; } = string.Empty;

            public bool RememberMe { get; set; } = false;
        }
    }

    public static class Register
    {
        public class RegisterModel
        {
            [Required(ErrorMessage = "O nome é obrigatório")]
            [StringLength(100, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "O e-mail é obrigatório")]
            [EmailAddress(ErrorMessage = "E-mail inválido")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A senha é obrigatória")]
            [StringLength(100, ErrorMessage = "A senha deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
            [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não correspondem")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string? RefreshToken { get; set; }
    }
}