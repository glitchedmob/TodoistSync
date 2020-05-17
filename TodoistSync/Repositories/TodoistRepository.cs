using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Todoist = TodoistSync.Models.Todoist;

namespace TodoistSync.Repositories
{
    public class TodoistRepository
    {
        public List<Todoist.Task> ClickupTasks { get; set; }
        private readonly string _clickupLabelId;
        private readonly HttpClient _client;

        public TodoistRepository(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _clickupLabelId = configuration["TODOIST_CLICKUP_LABEL_ID"];
            _client.BaseAddress = new Uri("https://api.todoist.com/rest/v1/");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", configuration["TODOIST_API_KEY"]);
        }

        public async Task LoadClickupTasks()
        {
            if (ClickupTasks != null)
            {
                return;
            }

            var content = await _client.GetStringAsync($"tasks?label_id={_clickupLabelId}");

            ClickupTasks = JsonConvert.DeserializeObject<List<Todoist.Task>>(content);
        }

        public async Task DeleteTask(Todoist.Task task)
        {
            await _client.DeleteAsync($"tasks/{task.Id}");
        }
    }
}
