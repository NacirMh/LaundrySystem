using LaundrySystem.Domain.Dtos.Cycle;
using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.Domain.Dtos.Owner;
using System.Text;
using System.Text.Json;


namespace LaundrySystem.Simulator.Services
{
    public class ConfigurationService
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        public ConfigurationService(string ApiUrl)
        {
            _apiUrl = ApiUrl;
            _httpClient = new HttpClient();
        }

        public async Task<OwnerDTO?> GetOwnerConfig(string id)
        {
            var url = $"{_apiUrl}/{id}";
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            OwnerDTO laverie = JsonSerializer.Deserialize<OwnerDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (laverie != null)
            {
                return laverie;
            }
            return null;
        }

      
    }
}
