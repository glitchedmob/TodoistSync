using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Todoist = TodoistSync.Models.Todoist;

namespace TodoistSync.Repositories
{
    public class TodoistRepository
    {
        public readonly string ClickupLabelId;
        private readonly HttpClient _client;

        public TodoistRepository(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            ClickupLabelId = configuration["TODOIST_CLICKUP_LABEL_ID"];
            _client.BaseAddress = new Uri("https://api.todoist.com/rest/v1/");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", configuration["TODOIST_API_KEY"]);
        }

        public async Task<List<Todoist.Task>> GetTasksByLabelId(string labelId)
        {
            var content = await _client.GetStringAsync($"tasks?label_id={ClickupLabelId}");

            return JsonConvert.DeserializeObject<List<Todoist.Task>>(content);
        }

        public async Task DeleteTask(Todoist.Task task)
        {
            await _client.DeleteAsync($"tasks/{task.Id}");
        }

        public async Task CreateTask(
            string content,
            List<string> labelIds,
            string dueDate = null,
            DateTimeOffset? dueDatetime = null)
        {
            var json = JsonConvert.SerializeObject(new TaskPostBody
            {
                Content = content,
                LabelIds = labelIds,
                DueDate = dueDate,
                DueDatetime = dueDatetime,
            });
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("tasks", postContent);
        }

        public async Task UpdateTask(
            Todoist.Task task,
            string content = null,
            List<string> labelIds = null,
            string dueDate = null,
            DateTimeOffset? dueDatetime = null)
        {
            var json = JsonConvert.SerializeObject(new TaskPostBody
            {
                Content = content,
                LabelIds = labelIds,
                DueDate = dueDate,
                DueDatetime = dueDatetime,
            });
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync($"tasks/{task.Id}", postContent);
        }

        public async Task CompleteTask(Todoist.Task task)
        {
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        private class TaskPostBody
        {
            public string Content { get; set; }
            [JsonProperty("label_ids")] public List<string> LabelIds { get; set; }
            [JsonProperty("due_date")] public string DueDate { get; set; }
            [JsonProperty("due_datetime")] public DateTimeOffset? DueDatetime { get; set; }
        }
    }
}
