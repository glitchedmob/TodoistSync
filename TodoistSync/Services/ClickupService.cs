using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TodoistSync.Repositories;
using Clickup = TodoistSync.Models.Clickup;
using Todoist = TodoistSync.Models.Todoist;

namespace TodoistSync.Services
{
    public class ClickupService
    {
        private readonly TodoistRepository _todoistRepository;

        public ClickupService(TodoistRepository todoistRepository)
        {
            _todoistRepository = todoistRepository;
        }

        public async Task DeleteTodoistTaskIfExists(string clickupTaskId)
        {
            await _todoistRepository.LoadClickupTasks();

            var todoistTask = _todoistRepository.ClickupTasks
                .FirstOrDefault(t => GetClickupIdFromTodoistTask(t) == clickupTaskId);

            if (todoistTask == null)
            {
                return;
            }

            await _todoistRepository.DeleteTask(todoistTask);
        }

        public async Task CreateOrUpdateTodoistTask(Clickup.Task task)
        {
            await _todoistRepository.LoadClickupTasks();
            throw new System.NotImplementedException();
        }

        private string GetClickupIdFromTodoistTask(Todoist.Task task)
        {
            var clickupLink = task.Content
                .Split('(', ')')
                .FirstOrDefault(s => s.StartsWith("https://app.clickup.com/"));

            if (clickupLink == null)
            {
                return null;
            }

            return clickupLink.Replace("https://app.clickup.com/t/", "").Replace("/", "");
        }
    }
}
