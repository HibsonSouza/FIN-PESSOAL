using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace FinanceManager.ClientApp.Services
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, object value);
        Task<T> PutAsync<T>(string uri, object value);
        Task DeleteAsync(string uri);
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Configure base URL for API
            _httpClient.BaseAddress = new Uri("http://localhost:8000/");
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequestAsync<T>(request);
        }

        public async Task<T> PostAsync<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequestAsync<T>(request);
        }

        public async Task<T> PutAsync<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequestAsync<T>(request);
        }

        public async Task DeleteAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            await SendRequestAsync<object>(request);
        }

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
        {
            // Add JWT auth header if user is logged in
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Set accept header to JSON
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Send request
            using var response = await _httpClient.SendAsync(request);

            // Auto logout if 401 response returned from API
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _localStorage.RemoveItemAsync("authToken");
                _navigationManager.NavigateTo("/login");
                throw new UnauthorizedAccessException("Authentication failed");
            }

            // Throw exception if not successful
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>(_jsonOptions);
                string errorMessage = error != null && error.ContainsKey("message")
                    ? error["message"]
                    : response.ReasonPhrase;
                    
                throw new HttpRequestException($"API Error: {errorMessage}");
            }

            // Return data from response
            return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
        }
    }
}
