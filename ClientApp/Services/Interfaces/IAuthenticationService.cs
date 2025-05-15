using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResult> Login(string email, string password);
        Task<AuthResult> Register(string name, string email, string password, string confirmPassword);
        Task<AuthResult> RefreshToken();
        Task Logout();
        Task<UserViewModel> GetCurrentUser();
        Task<bool> UpdateProfile(UpdateProfileModel model);
        Task<bool> ChangePassword(ChangePasswordModel model);
        Task<bool> IsAuthenticated();
    }
}