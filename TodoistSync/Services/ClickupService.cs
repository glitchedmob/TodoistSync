using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;
using TodoistSync.Repositories;
using TodoistSync.Utilities;
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

        public async Task CreateOrUpdateTodoistTask(string taskId)
        {
            // Storing the System.Threading.Tasks.Task instances without await to run
            // each request in parallel
            var clickupRequestTask = _clickupRepository.GetTaskById(taskId);
            var todoistRequestTask = _todoistRepository.GetTasksByLabelId(_todoistRepository.ClickupLabelId);

            var clickupTask = await clickupRequestTask;
            var todoistClickupTasks = await todoistRequestTask;

            if (clickupTask == null)
            {
                return;
            }

            var existingTodoistTask = todoistClickupTasks
                .FirstOrDefault(t => GetClickupIdFromTodoistTask(t) == clickupTask.Id);

            var isAssigned = clickupTask.Assignees.FirstOrDefault(u => u.Id == _clickupRepository.ClickupUserId) !=
                             null;

            // If the task is not assigned to the user we care about and it's not in Todoist
            // It's someone else's task and we don't need to do anything
            if (!isAssigned && existingTodoistTask == null)
            {
                return;
            }

            // If the task is not assigned but it is in Todoist then it must have been
            // unassigned in Clickup and needs deleted
            if (!isAssigned)
            {
                await _todoistRepository.DeleteTask(existingTodoistTask);
                return;
            }

            // If the task is assigned and in Todoist then we need to make sure all the
            // details are up to date
            if (existingTodoistTask != null)
            {
                await UpdateTodoistTask(clickupTask, existingTodoistTask);
                return;
            }

            // At this point we know the task is assigned to the user we care about, but it's not in Todoist and
            // it probably needs created. However, we need to make sure the task isn't marked as closed in Clickup.
            // This check allows us to clear a task in Todoist and have it clear in Clickup without being recreated.
            if (clickupTask.Status.Type == "closed")
            {
                return;
            }


            // After the above checks we know we need to create the task in Todoist.
            // We do that right away if the current task is not a child task
            if (string.IsNullOrEmpty(clickupTask.Parent))
            {
                await CreateTodoistTask(clickupTask);
                return;
            }

            var parentTodoistTask = todoistClickupTasks
                .FirstOrDefault(t => GetClickupIdFromTodoistTask(t) == clickupTask.Parent);

            // If we can't find the parent task in Todoist for some reason (an example being the parent task
            // wasn't assigned to the user we care about) we just create it normally
            if (parentTodoistTask == null)
            {
                await CreateTodoistTask(clickupTask);
                return;
            }

            await CreateTodoistTask(clickupTask, parentTodoistTask.Id);
        }

        private async Task CreateTodoistTask(Clickup.Task clickupTask, long? parent)
        {
            var content = FormatTodoistContent(clickupTask);

            await _todoistRepository.CreateTask(
                content,
                new List<long> { _todoistRepository.ClickupLabelId },
                parent,
                dueDatetime: clickupTask.DueDate
            );
        }

        private Task CreateTodoistTask(Clickup.Task clickupTask)
        {
            return CreateTodoistTask(clickupTask, null);
        }

        private async Task UpdateTodoistTask(Clickup.Task clickupTask, Todoist.Task existingTask)
        {
            if (clickupTask.Status.Type == "closed")
            {
                await _todoistRepository.CompleteTask(existingTask);
                return;
            }

            var updatedContent = FormatTodoistContent(clickupTask);

            if (
                updatedContent == existingTask.Content &&
                clickupTask.DueDate == existingTask.Due?.Date.UpdateTimeZone(_todoistRepository.TodoistTimeZone)
            )
            {
                return;
            }

            await _todoistRepository.UpdateTask(
                existingTask,
                updatedContent,
                dueDatetime: clickupTask.DueDate
            );
        }

        public async Task UpdateClickupTask(string taskId, DateTimeOffset? dueDate)
        {
            var clickupTask = await _clickupRepository.GetTaskById(taskId);

            if (clickupTask == null)
            {
                return;
            }

            dueDate = dueDate.UpdateTimeZone(_todoistRepository.TodoistTimeZone);

            if (clickupTask.DueDate == dueDate)
            {
                return;
            }

            await _clickupRepository.UpdateTask(taskId, dueDate?.ToUnixTimeMilliseconds());
        }

        public string GetClickupIdFromTodoistContent(string content)
        {
            var clickupLink = content
                .Split('(', ')')
                .FirstOrDefault(s => s.StartsWith("https://app.clickup.com/"));

            return clickupLink == null
                ? null
                : clickupLink
                    .Replace("https://app.clickup.com/t/", "")
                    .Replace("/", "");
        }

        public string GetClickupIdFromTodoistTask(Todoist.Task task)
        {
            return GetClickupIdFromTodoistContent(task.Content);
        }

        private string FormatTodoistContent(Clickup.Task clickupTask)
        {
            return $"{clickupTask.Name} - [(Clickup Task)]({clickupTask.Url})";
        }
    }
}
