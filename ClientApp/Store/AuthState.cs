using System;
using System.Security.Claims;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace FinanceManager.ClientApp.Store
{
    // State
    public class AuthState
    {
        public bool IsAuthenticated { get; }
        public string UserName { get; }
        public string Email { get; }

        public AuthState(bool isAuthenticated, string userName, string email)
        {
            IsAuthenticated = isAuthenticated;
            UserName = userName;
            Email = email;
        }
    }

    // Actions
    public static class AuthActions
    {
        public class SetAuthenticated
        {
            public bool IsAuthenticated { get; }
            public string UserName { get; }
            public string Email { get; }

            public SetAuthenticated(bool isAuthenticated, string userName, string email)
            {
                IsAuthenticated = isAuthenticated;
                UserName = userName;
                Email = email;
            }
        }
    }

    // Feature
    public class AuthFeature : Feature<AuthState>
    {
        public override string GetName() => "Auth";

        protected override AuthState GetInitialState() => new AuthState(
            isAuthenticated: false,
            userName: null,
            email: null
        );
    }

    // Reducers
    public static class AuthReducers
    {
        [ReducerMethod]
        public static AuthState ReduceSetAuthenticated(AuthState state, AuthActions.SetAuthenticated action) =>
            new AuthState(
                isAuthenticated: action.IsAuthenticated,
                userName: action.UserName,
                email: action.Email
            );
    }

    // Custom Authentication State Provider
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IDispatcher _dispatcher;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public CustomAuthStateProvider(ILocalStorageService localStorage, IDispatcher dispatcher)
        {
            _localStorage = localStorage;
            _dispatcher = dispatcher;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                
                if (string.IsNullOrEmpty(token))
                {
                    return CreateAnonymousState();
                }

                // Validate and parse token
                var tokenData = _tokenHandler.ReadJwtToken(token);
                
                // Check if token is expired
                if (tokenData.ValidTo < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    await _localStorage.RemoveItemAsync("userName");
                    return CreateAnonymousState();
                }

                // Extract claims from token
                var claims = new List<Claim>();
                
                foreach (var claim in tokenData.Claims)
                {
                    claims.Add(new Claim(claim.Type, claim.Value));
                }

                // Ensure we have necessary claims
                var email = tokenData.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var name = tokenData.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                
                _dispatcher.Dispatch(new AuthActions.SetAuthenticated(
                    isAuthenticated: true,
                    userName: name,
                    email: email
                ));

                // Create authenticated user
                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);
                
                return new AuthenticationState(user);
            }
            catch (Exception)
            {
                return CreateAnonymousState();
            }
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private AuthenticationState CreateAnonymousState()
        {
            _dispatcher.Dispatch(new AuthActions.SetAuthenticated(
                isAuthenticated: false,
                userName: null,
                email: null
            ));
            
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
