using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de autenticação
    /// </summary>
    public interface IAuthService
    {
        Task<(bool Success, string Token, string RefreshToken, User User)> Login(string email, string password);
        Task<(bool Success, string Message)> Register(User user, string password);
        Task<(bool Success, string Token, string RefreshToken)> RefreshToken(string token, string refreshToken);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Serviço responsável pela autenticação e registro de usuários
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<(bool Success, string Token, string RefreshToken, User User)> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            
            if (user == null)
            {
                return (false, null, null, null);
            }
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            
            if (result == PasswordVerificationResult.Failed)
            {
                return (false, null, null, null);
            }
            
            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
            await _userRepository.UpdateAsync(user);
            
            return (true, token, refreshToken, user);
        }

        public async Task<(bool Success, string Message)> Register(User user, string password)
        {
            // Verificar se já existe um usuário com o mesmo email
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return (false, "Email já está em uso");
            }
            
            // Criar a hash da senha
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            user.CreatedAt = DateTime.UtcNow;
            
            // Salvar o usuário
            await _userRepository.AddAsync(user);
            
            return (true, "Usuário registrado com sucesso");
        }

        public async Task<(bool Success, string Token, string RefreshToken)> RefreshToken(string token, string refreshToken)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return (false, null, null);
            }
            
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return (false, null, null);
            }
            
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return (false, null, null);
            }
            
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return (false, null, null);
            }
            
            var newToken = _jwtService.GenerateToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();
            
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
            await _userRepository.UpdateAsync(user);
            
            return (true, newToken, newRefreshToken);
        }
    }
}
