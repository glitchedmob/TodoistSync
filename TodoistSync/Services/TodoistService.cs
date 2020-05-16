using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Clickup = TodoistSync.Models.Clickup;

namespace TodoistSync.Services
{
    public class TodoistService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public TodoistService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task DeleteClickupTaskIfExists(string clickupTaskId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateOrUpdateClickupTask(Clickup.Task task)
        {
            throw new System.NotImplementedException();
        }
    }
}
