using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<FinanceManager.ClientApp.Models.AuthResult> Login(string email, string password);
        Task<FinanceManager.ClientApp.Models.AuthResult> Register(string name, string email, string password, string confirmPassword);
        Task Logout();
        Task<FinanceManager.ClientApp.Models.AuthResult> RefreshToken();
        Task<bool> CheckIsAuthenticated();
    }
}