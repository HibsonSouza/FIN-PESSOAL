using System.Threading.Tasks;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResult> Login(string email, string password);
        Task<AuthResult> Register(string email, string password, string confirmPassword, string name);
        Task<bool> ChangePassword(string currentPassword, string newPassword, string confirmPassword);
        Task<bool> ForgotPassword(string email);
        Task<bool> ResetPassword(string email, string token, string newPassword, string confirmPassword);
        Task Logout();
        Task<UserInfo> GetUserInfo();
        Task<bool> UpdateUserProfile(UserProfileUpdateModel model);
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public UserInfo User { get; set; }

        public static AuthResult Failed(string message)
        {
            return new AuthResult
            {
                Success = false,
                Message = message
            };
        }

        public static AuthResult Successful(string token, UserInfo user)
        {
            return new AuthResult
            {
                Success = true,
                Token = token,
                User = user,
                Message = "Autenticação bem-sucedida"
            };
        }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string[] Roles { get; set; }
        public bool EmailVerified { get; set; }
        public string CreatedAt { get; set; }
        public UserPreferences Preferences { get; set; }
    }

    public class UserPreferences
    {
        public string DateFormat { get; set; } = "dd/MM/yyyy";
        public string CurrencyFormat { get; set; } = "pt-BR";
        public string DefaultView { get; set; } = "dashboard";
        public string Theme { get; set; } = "light";
        public bool ShowNotifications { get; set; } = true;
        public bool EmailNotifications { get; set; } = true;
        public int PageSize { get; set; } = 10;
    }

    public class UserProfileUpdateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public UserPreferences Preferences { get; set; }
    }
}