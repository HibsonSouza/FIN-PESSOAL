using Blazored.LocalStorage;
using FinanceManager.ClientApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace FinanceManager.ClientApp.Components.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var userData = await _localStorage.GetItemAsync<UserViewModel>("user");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userData?.Name ?? string.Empty),
                new Claim(ClaimTypes.Email, userData?.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, userData?.Id ?? string.Empty)
            };

            // Adicionar claims de cultura e tema
            if (userData != null)
            {
                claims.Add(new Claim("culture", userData.CultureCode));
                claims.Add(new Claim("currency", userData.Currency));
                claims.Add(new Claim("theme", userData.UseDarkTheme ? "dark" : "light"));
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token, UserViewModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user?.Name ?? string.Empty),
                new Claim(ClaimTypes.Email, user?.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user?.Id ?? string.Empty),
                new Claim("culture", user?.CultureCode ?? "pt-BR"),
                new Claim("currency", user?.Currency ?? "BRL"),
                new Claim("theme", user?.UseDarkTheme == true ? "dark" : "light")
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            var authenticatedUser = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}