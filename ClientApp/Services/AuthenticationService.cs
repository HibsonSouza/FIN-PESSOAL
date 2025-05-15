using Blazored.LocalStorage;
using FinanceManager.ClientApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinanceManager.ClientApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly string _apiUrl = "api/auth";

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

                var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>();

                    if (result.Success)
                    {
                        await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(result.Token, result.User);
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
                    }

                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    
                    try
                    {
                        var errorResult = JsonSerializer.Deserialize<AuthResult>(errorContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        return errorResult ?? AuthResult.Failed("Falha na autenticação. Por favor, tente novamente.");
                    }
                    catch
                    {
                        return AuthResult.Failed(response.StatusCode == System.Net.HttpStatusCode.Unauthorized 
                            ? "Email ou senha inválidos." 
                            : $"Erro de autenticação: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante login: {ex.Message}");
                return AuthResult.Failed("Erro na comunicação com o servidor. Por favor, tente novamente mais tarde.");
            }
        }

        public async Task<AuthResult> Register(string email, string password, string confirmPassword, string name)
        {
            try
            {
                var registerModel = new
                {
                    Email = email,
                    Password = password,
                    ConfirmPassword = confirmPassword,
                    Name = name
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/register", registerModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                    
                    if (result.Success)
                    {
                        await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(result.Token, result.User);
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
                    }
                    
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    
                    try
                    {
                        var errorResult = JsonSerializer.Deserialize<AuthResult>(errorContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        
                        return errorResult ?? AuthResult.Failed("Falha no registro. Por favor, tente novamente.");
                    }
                    catch
                    {
                        return AuthResult.Failed($"Erro no registro: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante registro: {ex.Message}");
                return AuthResult.Failed("Erro na comunicação com o servidor. Por favor, tente novamente mais tarde.");
            }
        }

        public async Task<bool> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            try
            {
                var token = await _localStorage.GetItemAsStringAsync("authToken");
                
                if (string.IsNullOrEmpty(token))
                    return false;
                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var changePasswordModel = new
                {
                    CurrentPassword = currentPassword,
                    NewPassword = newPassword,
                    ConfirmPassword = confirmPassword
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/change-password", changePasswordModel);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante alteração de senha: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                var model = new { Email = email };
                var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/forgot-password", model);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante recuperação de senha: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ResetPassword(string email, string token, string newPassword, string confirmPassword)
        {
            try
            {
                var resetPasswordModel = new
                {
                    Email = email,
                    Token = token,
                    NewPassword = newPassword,
                    ConfirmPassword = confirmPassword
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/reset-password", resetPasswordModel);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante redefinição de senha: {ex.Message}");
                return false;
            }
        }

        public async Task Logout()
        {
            await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<UserInfo> GetUserInfo()
        {
            try
            {
                var token = await _localStorage.GetItemAsStringAsync("authToken");
                
                if (string.IsNullOrEmpty(token))
                    return null;
                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await _httpClient.GetAsync($"{_apiUrl}/me");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserInfo>();
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter informações do usuário: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateUserProfile(UserProfileUpdateModel model)
        {
            try
            {
                var token = await _localStorage.GetItemAsStringAsync("authToken");
                
                if (string.IsNullOrEmpty(token))
                    return false;
                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await _httpClient.PutAsJsonAsync($"{_apiUrl}/profile", model);
                
                if (response.IsSuccessStatusCode)
                {
                    var userInfo = await GetUserInfo();
                    
                    if (userInfo != null)
                    {
                        var authResult = new AuthResult
                        {
                            Success = true,
                            Token = token,
                            User = userInfo
                        };
                        
                        await _localStorage.SetItemAsync("authUser", authResult.User);
                    }
                    
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar perfil do usuário: {ex.Message}");
                return false;
            }
        }
    }
}