using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using FinanceManager.ClientApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace FinanceManager.ClientApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private const string AuthTokenName = "authToken";
        private const string RefreshTokenName = "refreshToken";
        private const string UserInfoName = "userInfo";

        public AuthenticationService(
            HttpClient httpClient,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthResult> Login(string email, string password)
        {
            try
            {
                var loginModel = new
                {
                    Email = email,
                    Password = password
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();

                    if (authResult.Success)
                    {
                        await _localStorage.SetItemAsync(AuthTokenName, authResult.Token);
                        await _localStorage.SetItemAsync(RefreshTokenName, authResult.RefreshToken);
                        await _localStorage.SetItemAsync(UserInfoName, authResult.User);

                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Token);

                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new AuthenticationHeaderValue("Bearer", authResult.Token);

                        return authResult;
                    }

                    return AuthResult.FailedResult("Falha ao autenticar usuário");
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return AuthResult.FailedResult($"Falha ao autenticar: {response.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                return AuthResult.FailedResult($"Erro na autenticação: {ex.Message}");
            }
        }

        public async Task<AuthResult> Register(string name, string email, string password, string confirmPassword)
        {
            try
            {
                var registerModel = new
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    ConfirmPassword = confirmPassword
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);

                if (response.IsSuccessStatusCode)
                {
                    var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();

                    if (authResult.Success)
                    {
                        await _localStorage.SetItemAsync(AuthTokenName, authResult.Token);
                        await _localStorage.SetItemAsync(RefreshTokenName, authResult.RefreshToken);
                        await _localStorage.SetItemAsync(UserInfoName, authResult.User);

                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Token);

                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new AuthenticationHeaderValue("Bearer", authResult.Token);

                        return authResult;
                    }

                    return AuthResult.FailedResult("Falha ao registrar usuário");
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return AuthResult.FailedResult($"Falha ao registrar: {response.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                return AuthResult.FailedResult($"Erro no registro: {ex.Message}");
            }
        }

        public async Task<AuthResult> RefreshToken()
        {
            try
            {
                var savedToken = await _localStorage.GetItemAsync<string>(AuthTokenName);
                var refreshToken = await _localStorage.GetItemAsync<string>(RefreshTokenName);

                if (string.IsNullOrEmpty(savedToken) || string.IsNullOrEmpty(refreshToken))
                {
                    return AuthResult.FailedResult("Token de acesso não encontrado");
                }

                var refreshModel = new
                {
                    Token = savedToken,
                    RefreshToken = refreshToken
                };

                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", savedToken);

                var response = await _httpClient.PostAsJsonAsync("api/auth/refresh", refreshModel);

                if (response.IsSuccessStatusCode)
                {
                    var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();

                    if (authResult.Success)
                    {
                        await _localStorage.SetItemAsync(AuthTokenName, authResult.Token);
                        await _localStorage.SetItemAsync(RefreshTokenName, authResult.RefreshToken);
                        
                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Token);
                        
                        return authResult;
                    }
                }

                // Se não conseguiu atualizar, desloga o usuário
                await Logout();
                return AuthResult.FailedResult("Não foi possível atualizar o token");
            }
            catch (Exception ex)
            {
                await Logout();
                return AuthResult.FailedResult($"Erro ao atualizar token: {ex.Message}");
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(AuthTokenName);
            await _localStorage.RemoveItemAsync(RefreshTokenName);
            await _localStorage.RemoveItemAsync(UserInfoName);
            
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
            
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> IsAuthenticated()
        {
            var savedToken = await _localStorage.GetItemAsync<string>(AuthTokenName);
            return !string.IsNullOrEmpty(savedToken);
        }

        public async Task<string> GetToken()
        {
            return await _localStorage.GetItemAsync<string>(AuthTokenName);
        }

        public async Task<UserModel> GetUserInfo()
        {
            var userInfo = await _localStorage.GetItemAsync<UserModel>(UserInfoName);
            
            if (userInfo != null)
            {
                return userInfo;
            }

            var token = await _localStorage.GetItemAsync<string>(AuthTokenName);
            
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
                
            var response = await _httpClient.GetAsync("api/auth/userinfo");
            
            if (response.IsSuccessStatusCode)
            {
                userInfo = await response.Content.ReadFromJsonAsync<UserModel>();
                await _localStorage.SetItemAsync(UserInfoName, userInfo);
                return userInfo;
            }
            
            return null;
        }
    }
}