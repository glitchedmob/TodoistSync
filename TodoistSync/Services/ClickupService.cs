using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Clickup = TodoistSync.Models.Clickup;

namespace TodoistSync.Services
{
    public class ClickupService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public ClickupService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.BaseAddress = new Uri("https://api.clickup.com/api/v2/");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(_configuration["CLICKUP_API_KEY"]);
        }

        public async Task<Clickup.Task> GetTaskById(string taskId)
        {
            var content = await _client.GetStringAsync($"task/{taskId}");

            return JsonConvert.DeserializeObject<Clickup.Task>(content);
        }
    }
}
