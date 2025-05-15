using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Store;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;

namespace FinanceManager.ClientApp.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginModel model);
        Task<AuthResult> RegisterAsync(RegisterModel model);
        Task<AuthResult> LogoutAsync();
        Task<AuthResult> ForgotPasswordAsync(string email);
        bool IsAuthenticated();
    }

    public class AuthService : IAuthService
    {
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IDispatcher _dispatcher;

        public AuthService(
            IHttpService httpService,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider,
            IDispatcher dispatcher)
        {
            _httpService = httpService;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
            _dispatcher = dispatcher;
        }

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            try
            {
                var response = await _httpService.PostAsync<AuthResponseDto>("api/auth/login", model);
                
                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    await _localStorage.SetItemAsync("authToken", response.Token);
                    await _localStorage.SetItemAsync("userName", response.Name);
                    
                    (_authStateProvider as CustomAuthStateProvider)?.NotifyAuthenticationStateChanged();
                    
                    _dispatcher.Dispatch(new AuthActions.SetAuthenticated(
                        isAuthenticated: true,
                        userName: response.Name,
                        email: response.Email
                    ));
                    
                    return AuthResult.Success();
                }
                
                return AuthResult.Failure("Login failed. Please check your credentials and try again.");
            }
            catch (Exception ex)
            {
                return AuthResult.Failure(ex.Message);
            }
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel model)
        {
            try
            {
                var response = await _httpService.PostAsync<AuthResponseDto>("api/auth/register", model);
                
                if (response != null && response.Success)
                {
                    return AuthResult.Success();
                }
                
                return AuthResult.Failure(response?.Message ?? "Registration failed. Please try again.");
            }
            catch (Exception ex)
            {
                return AuthResult.Failure(ex.Message);
            }
        }

        public async Task<AuthResult> LogoutAsync()
        {
            try
            {
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("userName");
                
                (_authStateProvider as CustomAuthStateProvider)?.NotifyAuthenticationStateChanged();
                
                _dispatcher.Dispatch(new AuthActions.SetAuthenticated(
                    isAuthenticated: false,
                    userName: null,
                    email: null
                ));
                
                return AuthResult.Success();
            }
            catch (Exception ex)
            {
                return AuthResult.Failure(ex.Message);
            }
        }

        public async Task<AuthResult> ForgotPasswordAsync(string email)
        {
            try
            {
                await _httpService.PostAsync<AuthResponseDto>("api/auth/forgot-password", new { Email = email });
                return AuthResult.Success();
            }
            catch (Exception ex)
            {
                // Always return success for security reasons - don't reveal if email exists
                return AuthResult.Success();
            }
        }

        public bool IsAuthenticated()
        {
            var authenticationState = (_authStateProvider as CustomAuthStateProvider)?.GetAuthenticationStateAsync().Result;
            return authenticationState?.User?.Identity?.IsAuthenticated ?? false;
        }
    }

    public class AuthResult
    {
        public bool Succeeded { get; private set; }
        public string ErrorMessage { get; private set; }

        private AuthResult(bool succeeded, string errorMessage = null)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }

        public static AuthResult Success() => new AuthResult(true);
        public static AuthResult Failure(string errorMessage) => new AuthResult(false, errorMessage);
    }

    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
