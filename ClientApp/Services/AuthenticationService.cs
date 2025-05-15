using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace FinanceManager.ClientApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            HttpClient httpClient,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage,
            ILogger<AuthenticationService> logger)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _logger = logger;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);

        public string Username { get; private set; }

        public string UserId { get; private set; }

        public string Email { get; private set; }

        public string Token { get; private set; }

        public async Task<AuthResultModel> LoginAsync(LoginModel loginModel)
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);
                // response.EnsureSuccessStatusCode();
                // var result = await response.Content.ReadFromJsonAsync<AuthResultModel>();

                // Simulando uma autenticação para desenvolvimento
                var result = new AuthResultModel
                {
                    Success = true,
                    UserId = "user123",
                    UserName = "Usuário Teste",
                    Email = loginModel.Email,
                    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMTIzIiwibmFtZSI6IlVzdcOhcmlvIFRlc3RlIiwiZW1haWwiOiJ0ZXN0ZUBleGFtcGxlLmNvbSIsImV4cCI6MTcxODk5MzYwMH0.jM9Gd0nQ-5IfF-N8rLIxBz43O8EJmXi2hVHOFqB4Gl0",
                    RefreshToken = "refresh-token-123",
                    Message = "Login realizado com sucesso"
                };

                if (result.Success)
                {
                    await _localStorage.SetItemAsync("authToken", result.Token);
                    await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                    await _localStorage.SetItemAsync("userId", result.UserId);
                    await _localStorage.SetItemAsync("userName", result.UserName);
                    await _localStorage.SetItemAsync("userEmail", result.Email);

                    UserId = result.UserId;
                    Username = result.UserName;
                    Email = result.Email;
                    Token = result.Token;

                    ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer login");
                return new AuthResultModel
                {
                    Success = false,
                    Message = $"Erro ao fazer login: {ex.Message}"
                };
            }
        }

        public async Task<AuthResultModel> RegisterAsync(RegisterModel registerModel)
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);
                // response.EnsureSuccessStatusCode();
                // var result = await response.Content.ReadFromJsonAsync<AuthResultModel>();

                // Simulando um registro para desenvolvimento
                var result = new AuthResultModel
                {
                    Success = true,
                    Message = "Registro realizado com sucesso"
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar usuário");
                return new AuthResultModel
                {
                    Success = false,
                    Message = $"Erro ao registrar: {ex.Message}"
                };
            }
        }

        public async Task<AuthResultModel> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // var response = await _httpClient.PostAsJsonAsync("api/auth/forgot-password", forgotPasswordModel);
                // response.EnsureSuccessStatusCode();
                // var result = await response.Content.ReadFromJsonAsync<AuthResultModel>();

                // Simulando para desenvolvimento
                var result = new AuthResultModel
                {
                    Success = true,
                    Message = "Instruções de recuperação de senha enviadas para o email"
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao solicitar recuperação de senha");
                return new AuthResultModel
                {
                    Success = false,
                    Message = $"Erro ao solicitar recuperação de senha: {ex.Message}"
                };
            }
        }

        public async Task<AuthResultModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // var response = await _httpClient.PostAsJsonAsync("api/auth/reset-password", resetPasswordModel);
                // response.EnsureSuccessStatusCode();
                // var result = await response.Content.ReadFromJsonAsync<AuthResultModel>();

                // Simulando para desenvolvimento
                var result = new AuthResultModel
                {
                    Success = true,
                    Message = "Senha redefinida com sucesso"
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao redefinir senha");
                return new AuthResultModel
                {
                    Success = false,
                    Message = $"Erro ao redefinir senha: {ex.Message}"
                };
            }
        }

        public async Task<AuthResultModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                // var response = await _httpClient.PostAsJsonAsync("api/auth/change-password", changePasswordModel);
                // response.EnsureSuccessStatusCode();
                // var result = await response.Content.ReadFromJsonAsync<AuthResultModel>();

                // Simulando para desenvolvimento
                var result = new AuthResultModel
                {
                    Success = true,
                    Message = "Senha alterada com sucesso"
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao alterar senha");
                return new AuthResultModel
                {
                    Success = false,
                    Message = $"Erro ao alterar senha: {ex.Message}"
                };
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("userId");
            await _localStorage.RemoveItemAsync("userName");
            await _localStorage.RemoveItemAsync("userEmail");

            UserId = null;
            Username = null;
            Email = null;
            Token = null;

            ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
        }

        public async Task<UserProfileModel> GetUserProfileAsync()
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                // return await _httpClient.GetFromJsonAsync<UserProfileModel>("api/auth/profile");

                // Simulando para desenvolvimento
                return new UserProfileModel
                {
                    Name = Username ?? "Usuário Teste",
                    Email = Email ?? "teste@example.com",
                    PhoneNumber = "(11) 98765-4321",
                    ProfilePictureUrl = null,
                    Currency = "BRL",
                    TimeZone = "America/Sao_Paulo",
                    Language = "pt-BR"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter perfil do usuário");
                throw;
            }
        }

        public async Task<AuthResultModel> UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                // var response = await _httpClient.PutAsJsonAsync("api/auth/profile", userProfileModel);
                // response.EnsureSuccessStatusCode();
                // var result = await response.Content.ReadFromJsonAsync<AuthResultModel>();

                // Simulando para desenvolvimento
                var result = new AuthResultModel
                {
                    Success = true,
                    Message = "Perfil atualizado com sucesso"
                };

                Username = userProfileModel.Name;
                Email = userProfileModel.Email;

                await _localStorage.SetItemAsync("userName", Username);
                await _localStorage.SetItemAsync("userEmail", Email);

                ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar perfil do usuário");
                return new AuthResultModel
                {
                    Success = false,
                    Message = $"Erro ao atualizar perfil: {ex.Message}"
                };
            }
        }

        public async Task<bool> RefreshTokenAsync()
        {
            try
            {
                // Em uma implementação real, a chamada seria para a API
                // var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
                // var response = await _httpClient.PostAsJsonAsync("api/auth/refresh-token", new { RefreshToken = refreshToken });
                // if (response.IsSuccessStatusCode)
                // {
                //     var result = await response.Content.ReadFromJsonAsync<AuthResultModel>();
                //     if (result.Success)
                //     {
                //         await _localStorage.SetItemAsync("authToken", result.Token);
                //         await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                //         Token = result.Token;
                //         ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
                //         return true;
                //     }
                // }

                // Simulando para desenvolvimento
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao renovar token");
                return false;
            }
        }
    }

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<CustomAuthStateProvider> _logger;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ILocalStorageService localStorage, ILogger<CustomAuthStateProvider> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrWhiteSpace(token))
                    return new AuthenticationState(_anonymous);

                var userId = await _localStorage.GetItemAsync<string>("userId");
                var userName = await _localStorage.GetItemAsync<string>("userName");
                var userEmail = await _localStorage.GetItemAsync<string>("userEmail");

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim("token", token)
                };

                var identity = new ClaimsIdentity(claims, "BlazorAuth");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter estado de autenticação");
                return new AuthenticationState(_anonymous);
            }
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}