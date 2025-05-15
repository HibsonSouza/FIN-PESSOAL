using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class GoalService : IGoalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/goals";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public GoalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GoalViewModel>> GetGoals()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiEndpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<GoalViewModel>>(_jsonOptions) 
                        ?? new List<GoalViewModel>();
                }
                
                return new List<GoalViewModel>();
            }
            catch
            {
                return new List<GoalViewModel>();
            }
        }

        public async Task<GoalViewModel> GetGoalById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<GoalViewModel>(_jsonOptions) 
                        ?? new GoalViewModel();
                }
                
                return new GoalViewModel();
            }
            catch
            {
                return new GoalViewModel();
            }
        }

        public async Task<bool> CreateGoal(GoalCreateModel goal)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiEndpoint, goal);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateGoal(string id, GoalUpdateModel goal)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiEndpoint}/{id}", goal);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteGoal(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiEndpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddContribution(string goalId, GoalContributionModel contribution)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}/{goalId}/contributions", contribution);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}