using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using FinanceManager.ClientApp.Services.Interfaces;

namespace FinanceManager.ClientApp.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private const string AUTH_TOKEN_KEY = "authToken";
        private const string AUTH_USER_KEY = "authUser";

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync(AUTH_TOKEN_KEY);

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var userJson = await _localStorage.GetItemAsStringAsync(AUTH_USER_KEY);
            UserInfo user = null;

            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonSerializer.Deserialize<UserInfo>(userJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user?.Name ?? ""),
                new Claim(ClaimTypes.Email, user?.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user?.Id ?? Guid.NewGuid().ToString()),
                new Claim("token", token)
            };

            if (user?.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return new AuthenticationState(claimsPrincipal);
        }

        public async Task MarkUserAsAuthenticated(string token, UserInfo user)
        {
            await _localStorage.SetItemAsStringAsync(AUTH_TOKEN_KEY, token);
            await _localStorage.SetItemAsync(AUTH_USER_KEY, user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("token", token)
            };

            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync(AUTH_TOKEN_KEY);
            await _localStorage.RemoveItemAsync(AUTH_USER_KEY);

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}