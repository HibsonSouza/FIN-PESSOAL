using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        string Username { get; }
        string UserId { get; }
        string Email { get; }
        string Token { get; }

        Task<AuthResultModel> LoginAsync(LoginModel loginModel);
        Task<AuthResultModel> RegisterAsync(RegisterModel registerModel);
        Task<AuthResultModel> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel);
        Task<AuthResultModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
        Task<AuthResultModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
        Task<UserProfileModel> GetUserProfileAsync();
        Task<AuthResultModel> UpdateUserProfileAsync(UserProfileModel userProfileModel);
        Task LogoutAsync();
        Task<bool> RefreshTokenAsync();
    }
}