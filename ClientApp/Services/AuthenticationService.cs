using Blazored.LocalStorage;
using FinanceManager.ClientApp.Components.Authentication;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly string _apiEndpoint = "api/auth";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AuthenticationService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthResult> Login(string email, string password)
        {
            try
            {
                var loginModel = new LoginModel
                {
                    Email = email,
                    Password = password
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}/login", loginModel);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>(_jsonOptions);
                    
                    if (result != null && result.Success)
                    {
                        await _localStorage.SetItemAsync("authToken", result.Token);
                        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                        await _localStorage.SetItemAsync("user", result.User);
                        
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new AuthenticationHeaderValue("Bearer", result.Token);
                        
                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(
                            result.Token!, result.User!);
                        
                        return result;
                    }
                    
                    return result ?? AuthResult.FailedResult("Resposta inválida do servidor");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return AuthResult.FailedResult($"Login falhou: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return AuthResult.FailedResult($"Erro durante o login: {ex.Message}");
            }
        }

        public async Task<AuthResult> Register(string name, string email, string password, string confirmPassword)
        {
            try
            {
                var registerModel = new RegisterModel
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    ConfirmPassword = confirmPassword
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}/register", registerModel);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>(_jsonOptions);
                    
                    if (result != null && result.Success)
                    {
                        await _localStorage.SetItemAsync("authToken", result.Token);
                        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                        await _localStorage.SetItemAsync("user", result.User);
                        
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new AuthenticationHeaderValue("Bearer", result.Token);
                        
                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(
                            result.Token!, result.User!);
                        
                        return result;
                    }
                    
                    return result ?? AuthResult.FailedResult("Resposta inválida do servidor");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return AuthResult.FailedResult($"Registro falhou: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return AuthResult.FailedResult($"Erro durante o registro: {ex.Message}");
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _httpClient.PostAsync($"{_apiEndpoint}/logout", null);
                
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("refreshToken");
                await _localStorage.RemoveItemAsync("user");
                
                _httpClient.DefaultRequestHeaders.Authorization = null;
                
                ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AuthResult> RefreshToken()
        {
            try
            {
                var savedToken = await _localStorage.GetItemAsync<string>("authToken");
                var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
                
                if (string.IsNullOrEmpty(savedToken) || string.IsNullOrEmpty(refreshToken))
                {
                    return AuthResult.FailedResult("Tokens não encontrados");
                }
                
                var content = new StringContent(
                    JsonSerializer.Serialize(new { Token = savedToken, RefreshToken = refreshToken }),
                    Encoding.UTF8,
                    "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiEndpoint}/refresh-token", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>(_jsonOptions);
                    
                    if (result != null && result.Success)
                    {
                        await _localStorage.SetItemAsync("authToken", result.Token);
                        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                        
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new AuthenticationHeaderValue("Bearer", result.Token);
                        
                        return result;
                    }
                    
                    return result ?? AuthResult.FailedResult("Resposta inválida do servidor");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return AuthResult.FailedResult($"Refresh token falhou: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return AuthResult.FailedResult($"Erro durante refresh token: {ex.Message}");
            }
        }

        public async Task<bool> CheckIsAuthenticated()
        {
            var authToken = await _localStorage.GetItemAsync<string>("authToken");
            return !string.IsNullOrEmpty(authToken);
        }
    }
}