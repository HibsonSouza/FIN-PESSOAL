using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResult> Login(string email, string password);
        Task<AuthResult> Register(string name, string email, string password, string confirmPassword);
        Task<bool> Logout();
        Task<AuthResult> RefreshToken();
        Task<bool> CheckIsAuthenticated();
    }
}