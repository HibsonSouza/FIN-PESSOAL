using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CultureCode { get; set; } = "pt-BR";
        public string Currency { get; set; } = "BRL";
        public bool UseDarkTheme { get; set; } = false;
    }
    
    public class LoginModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;
        
        public bool RememberMe { get; set; } = false;
    }
    
    public class RegisterModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
        [Compare(nameof(Password), ErrorMessage = "As senhas não coincidem")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
    
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória")]
        public string CurrentPassword { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string NewPassword { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
        [Compare(nameof(NewPassword), ErrorMessage = "As senhas não coincidem")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
    
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O token é obrigatório")]
        public string Token { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string NewPassword { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
        [Compare(nameof(NewPassword), ErrorMessage = "As senhas não coincidem")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
    
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;
    }
    
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public UserViewModel? User { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        
        public static AuthResult SuccessResult(string token, string refreshToken, UserViewModel user)
        {
            return new AuthResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken,
                User = user
            };
        }
        
        public static AuthResult FailedResult(string error)
        {
            return new AuthResult
            {
                Success = false,
                Errors = new List<string> { error }
            };
        }
        
        public static AuthResult FailedResult(List<string> errors)
        {
            return new AuthResult
            {
                Success = false,
                Errors = errors
            };
        }
    }
}