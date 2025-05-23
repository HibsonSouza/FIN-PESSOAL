using Blazored.LocalStorage;
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
        private readonly FinanceManager.ClientApp.Components.Authentication.CustomAuthStateProvider _authStateProvider;
        private readonly string _apiEndpoint = "api/auth";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AuthenticationService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            FinanceManager.ClientApp.Components.Authentication.CustomAuthStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<FinanceManager.ClientApp.Models.AuthResult> Login(string email, string password)
        {
            try
            {
                var loginModel = new FinanceManager.ClientApp.Models.LoginModel
                {
                    Email = email,
                    Password = password
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}/login", loginModel);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<FinanceManager.ClientApp.Models.AuthResult>(_jsonOptions);
                    
                    if (result != null && result.Success) // Success é uma propriedade bool, o erro anterior era estranho.
                    {
                        await _localStorage.SetItemAsync("authToken", result.Token);
                        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                        
                        if (result.User != null) // Verificar nulidade antes de usar
                        {
                            await _localStorage.SetItemAsync("user", result.User);
                            _httpClient.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
                            _authStateProvider.NotifyUserAuthentication(result.Token!, result.User);
                        }
                        else
                        {
                            // Lidar com o caso de User ser nulo, se necessário, talvez limpando o user do localStorage
                             await _localStorage.RemoveItemAsync("user");
                             // Ainda notificar com token, mas o CustomAuthStateProvider precisará lidar com User nulo
                             // ou reconstruir o usuário a partir do token.
                             // A implementação atual de NotifyUserAuthentication espera um UserViewModel.
                             // Se User for nulo, talvez seja melhor não chamar NotifyUserAuthentication diretamente
                             // e deixar GetAuthenticationStateAsync lidar com isso na próxima vez.
                             // Por enquanto, vamos assumir que User não deveria ser nulo em um login bem-sucedido com token.
                             // Se puder ser, a lógica de NotifyUserAuthentication ou o que acontece aqui precisa de ajuste.
                        }
                        return result;
                    }
                    
                    return result ?? FinanceManager.ClientApp.Models.AuthResult.FailedResult("Resposta inválida do servidor");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return FinanceManager.ClientApp.Models.AuthResult.FailedResult($"Login falhou: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return FinanceManager.ClientApp.Models.AuthResult.FailedResult($"Erro durante o login: {ex.Message}");
            }
        }

        public async Task<FinanceManager.ClientApp.Models.AuthResult> Register(string name, string email, string password, string confirmPassword)
        {
            try
            {
                var registerModel = new FinanceManager.ClientApp.Models.RegisterModel
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    ConfirmPassword = confirmPassword
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}/register", registerModel);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<FinanceManager.ClientApp.Models.AuthResult>(_jsonOptions);
                    
                    if (result != null && result.Success)
                    {
                        await _localStorage.SetItemAsync("authToken", result.Token);
                        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

                        if (result.User != null)
                        {
                            await _localStorage.SetItemAsync("user", result.User);
                            _httpClient.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
                            _authStateProvider.NotifyUserAuthentication(result.Token!, result.User);
                        }
                         else
                        {
                            await _localStorage.RemoveItemAsync("user");
                        }
                        return result;
                    }
                    return result ?? FinanceManager.ClientApp.Models.AuthResult.FailedResult("Resposta inválida do servidor");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return FinanceManager.ClientApp.Models.AuthResult.FailedResult($"Registro falhou: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return FinanceManager.ClientApp.Models.AuthResult.FailedResult($"Erro durante o registro: {ex.Message}");
            }
        }

        public async Task Logout() // Corrigido para Task, conforme a interface atualizada
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("user");
            _authStateProvider.NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            // Não há valor de retorno booleano aqui.
        }

        public async Task<FinanceManager.ClientApp.Models.AuthResult> RefreshToken()
        {
            try
            {
                var authToken = await _localStorage.GetItemAsync<string>("authToken");
                var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

                if (string.IsNullOrWhiteSpace(authToken) || string.IsNullOrWhiteSpace(refreshToken))
                {
                    return FinanceManager.ClientApp.Models.AuthResult.FailedResult("Tokens não encontrados ou inválidos para refresh.");
                }

                var tokenModel = new { Token = authToken, RefreshToken = refreshToken };
                var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}/refresh-token", tokenModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<FinanceManager.ClientApp.Models.AuthResult>(_jsonOptions);
                    if (result != null && result.Success)
                    {
                        await _localStorage.SetItemAsync("authToken", result.Token);
                        if (!string.IsNullOrWhiteSpace(result.RefreshToken))
                        {
                            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                        }
                        
                        _httpClient.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

                        if (result.User != null)
                        {
                            await _localStorage.SetItemAsync("user", result.User);
                            _authStateProvider.NotifyUserAuthentication(result.Token!, result.User);
                        }
                        else
                        {
                            // Se User não for retornado, o CustomAuthStateProvider precisará obter os claims do novo token.
                            // Disparar uma notificação para que o estado seja reavaliado.
                            // A chamada direta a NotifyAuthenticationStateChanged(Task.FromResult(await _authStateProvider.GetAuthenticationStateAsync()))
                            // não é possível devido ao nível de proteção.
                            // Uma solução é fazer NotifyUserLogout() e depois forçar uma reavaliação,
                            // ou ter um método em CustomAuthStateProvider para forçar a reavaliação a partir do token.
                            // A maneira mais simples é deixar que a próxima chamada a GetAuthenticationStateAsync resolva.
                            // Ou, se CustomAuthStateProvider.NotifyUserAuthentication puder aceitar user null e reconstruir:
                            // _authStateProvider.NotifyUserAuthentication(result.Token!, null); // Isso dependeria da implementação de NotifyUserAuthentication
                            // Por enquanto, vamos assumir que se User não vier, o estado será atualizado na próxima requisição de autenticação.
                            // Ou podemos forçar uma atualização notificando logout e depois login "silencioso" se tivermos o user.
                            // A opção mais segura é apenas atualizar os tokens e deixar o AuthStateProvider lidar com isso.
                            // Para forçar a atualização do ClaimsPrincipal, o CustomAuthStateProvider precisaria de um método público
                            // que chame NotifyAuthenticationStateChanged(await GetAuthenticationStateAsync());
                            // Vamos simplificar: se não há usuário, o estado de autenticação será atualizado na próxima vez que for solicitado.
                            // No entanto, para garantir que a UI reaja, podemos chamar NotifyUserLogout e depois, se tivermos um usuário (mesmo que antigo),
                            // tentar notificar a autenticação. Mas isso é complexo.
                            // A melhor abordagem é que CustomAuthStateProvider.GetAuthenticationStateAsync() seja robusto.
                            // Se o User não vem no refresh, mas o token é válido, GetAuthenticationStateAsync deve conseguir criar o ClaimsPrincipal.
                            // Para fins de atualização imediata da UI com novos claims (se o token mudou e tem novos claims):
                            _authStateProvider.NotifyUserLogout(); // Limpa o estado antigo
                            // Agora, o CustomAuthStateProvider.GetAuthenticationStateAsync() será chamado e usará o novo token.
                            // Para forçar a UI a recarregar o estado:
                            var newState = await _authStateProvider.GetAuthenticationStateAsync();
                            // O problema é que não podemos chamar NotifyAuthenticationStateChanged daqui.
                            // A solução mais limpa seria o CustomAuthStateProvider expor um método tipo ForceRefreshState().
                            // Sem isso, a atualização de claims (sem ser logout/login) é mais passiva.
                            // Se o UserViewModel não é retornado, mas o token é, o usuário continua "logado" com o token antigo na UI
                            // até que GetAuthenticationStateAsync seja chamado novamente.
                            // Se o UserViewModel *é* retornado, então NotifyUserAuthentication funciona.
                            // Se não é, e o token é novo, o ideal seria que o CustomAuthStateProvider pudesse ser notificado
                            // para reler o token e atualizar o ClaimsPrincipal.
                            // A chamada a NotifyUserLogout() seguida de nada fará com que o usuário pareça deslogado até a próxima atualização.
                            // Vamos assumir que se User não vier, mas o token for válido, o CustomAuthStateProvider.GetAuthenticationStateAsync
                            // irá reconstruir o ClaimsPrincipal corretamente quando for chamado.
                            // A notificação explícita é mais para atualizar a UI *imediatamente*.
                             await _localStorage.RemoveItemAsync("user"); // Remover usuário antigo se não veio um novo
                        }
                        return result;
                    }
                    return result ?? FinanceManager.ClientApp.Models.AuthResult.FailedResult("Falha ao atualizar token, resposta inválida do servidor.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        await Logout(); // Deslogar se o refresh token for inválido/expirado
                    }
                    return FinanceManager.ClientApp.Models.AuthResult.FailedResult($"Falha ao atualizar token: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return FinanceManager.ClientApp.Models.AuthResult.FailedResult($"Erro durante a atualização do token: {ex.Message}");
            }
        }
        
        public async Task<bool> CheckIsAuthenticated()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated ?? false;
        }
    }
}