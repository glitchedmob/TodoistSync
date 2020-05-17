using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoistSync.Repositories;
using Clickup = TodoistSync.Models.Clickup;
using Todoist = TodoistSync.Models.Todoist;

namespace TodoistSync.Services
{
    public class ClickupService
    {
        private readonly TodoistRepository _todoistRepository;
        private readonly ClickupRepository _clickupRepository;

        public ClickupService(TodoistRepository todoistRepository, ClickupRepository clickupRepository)
        {
            _todoistRepository = todoistRepository;
            _clickupRepository = clickupRepository;
        }

        public async Task DeleteTodoistTaskIfExists(string clickupTaskId)
        {
            var todoistClickupTasks = await _todoistRepository.GetTasksByLabelId(_todoistRepository.ClickupLabelId);

            var existingTask = todoistClickupTasks
                .FirstOrDefault(t => GetClickupIdFromTodoistTask(t) == clickupTaskId);

            if (existingTask == null)
            {
                return;
            }

            await _todoistRepository.DeleteTask(existingTask);
        }

        public async Task CreateOrUpdateTodoistTask(Clickup.Task clickupTask)
        {
            var todoistClickupTasks = await _todoistRepository.GetTasksByLabelId(_todoistRepository.ClickupLabelId);

            var existingTodoistTask = todoistClickupTasks
                .FirstOrDefault(t => GetClickupIdFromTodoistTask(t) == clickupTask.Id);

            var isAssigned = clickupTask.Assignees.FirstOrDefault(u => u.Id == _clickupRepository.ClickupUserId) !=
                             null;

            if (!isAssigned && existingTodoistTask == null)
            {
                return;
            }

            if (!isAssigned)
            {
                await _todoistRepository.DeleteTask(existingTodoistTask);
                return;
            }

            if (existingTodoistTask != null)
            {
                await UpdateTodoistTask(clickupTask, existingTodoistTask);
                return;
            }

            await CreateTodoistTask(clickupTask);
        }

        private async Task CreateTodoistTask(Clickup.Task clickupTask)
        {
            var content = FormatTodoistContent(clickupTask);
            await _todoistRepository.CreateTask(content, new List<long> { _todoistRepository.ClickupLabelId });
        }

        private async Task UpdateTodoistTask(Clickup.Task clickupTask, Todoist.Task existingTask)
        {
            await _todoistRepository.UpdateTask(existingTask, FormatTodoistContent(clickupTask));
        }

        private string GetClickupIdFromTodoistTask(Todoist.Task task)
        {
            var clickupLink = task.Content
                .Split('(', ')')
                .FirstOrDefault(s => s.StartsWith("https://app.clickup.com/"));

            return clickupLink == null
                ? null
                : clickupLink
                    .Replace("https://app.clickup.com/t/", "")
                    .Replace("/", "");
        }

        private string FormatTodoistContent(Clickup.Task clickupTask)
        {
            return $"{clickupTask.Name} - [(Clickup Task)]({clickupTask.Url})";
        }
    }
}
