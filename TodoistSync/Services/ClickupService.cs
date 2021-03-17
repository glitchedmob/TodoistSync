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
            // Fire off tasks then await later so they're executed in parallel
            var clickupRequestTask = _clickupRepository.GetTaskById(taskId);
            var todoistRequestTask = _todoistRepository.GetTasksByLabelId(_todoistRepository.ClickupLabelId);

            var clickupTask = await clickupRequestTask;
            var allTodoistClickupTasks = await todoistRequestTask;

            if (clickupTask == null)
            {
                return;
            }

            var existingTodoistTask = allTodoistClickupTasks
                .FirstOrDefault(t => GetClickupIdFromTodoistTask(t) == clickupTask.Id);

            var clickupUser = clickupTask.Assignees.FirstOrDefault(u => u.Id == _clickupRepository.ClickupUserId);

            var hasBeenAssignedToOurClickupUser = clickupUser != null;

            // If the task is not assigned to the user we're syncing for and it's not in Todoist that
            // means clickup is notifying us about a task we don't care about and we don't do anything
            if (!hasBeenAssignedToOurClickupUser && existingTodoistTask == null)
            {
                return;
            }

            // If the task is not assigned but it is in Todoist then it must have been
            // unassigned in Clickup and needs removed from Todoist
            if (!hasBeenAssignedToOurClickupUser)
            {
                await _todoistRepository.DeleteTask(existingTodoistTask);
                return;
            }

            // If the task is assigned and in Todoist then we need to make sure all the
            // to update any new details
            if (existingTodoistTask != null)
            {
                await UpdateClickupTaskInTodoist(clickupTask, existingTodoistTask);
                return;
            }

            // At this point we know the task is assigned to our clickup user, but it's not in Todoist and
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
                await CreateClickupTaskInTodoist(clickupTask);
                return;
            }

            var parentTodoistTask = allTodoistClickupTasks
                .FirstOrDefault(t => GetClickupIdFromTodoistTask(t) == clickupTask.Parent);

            // If we can't find the parent task in Todoist for some reason (e.g. the parent task
            // wasn't assigned to our Clickup user) we just create it normally
            if (parentTodoistTask == null)
            {
                await CreateClickupTaskInTodoist(clickupTask);
                return;
            }

            // Otherwise if we do have a parent task we create the todoist task under that task
            await CreateClickupTaskInTodoist(clickupTask, parentTodoistTask.Id);
        }

        private async Task CreateClickupTaskInTodoist(Clickup.Task clickupTask, long? parent)
        {
            var content = FormatTodoistContent(clickupTask);

            await _todoistRepository.CreateTask(
                content,
                new List<long> { _todoistRepository.ClickupLabelId },
                parent,
                dueDatetime: clickupTask.DueDate
            );
        }

        private Task CreateClickupTaskInTodoist(Clickup.Task clickupTask)
        {
            return CreateClickupTaskInTodoist(clickupTask, null);
        }

        private async Task UpdateClickupTaskInTodoist(Clickup.Task clickupTask, Todoist.Task todoistTask)
        {
            if (clickupTask.Status.Type == "closed")
            {
                await _todoistRepository.CompleteTask(todoistTask);
                return;
            }

            var updatedContent = FormatTodoistContent(clickupTask);

            if (
                updatedContent == todoistTask.Content &&
                clickupTask.DueDate == todoistTask.Due?.Date.UpdateTimeZone(_todoistRepository.TodoistTimeZone)
            )
            {
                return;
            }

            await _todoistRepository.UpdateTask(
                todoistTask,
                updatedContent,
                dueDatetime: null
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

        public async Task CompleteTask(string taskId)
        {
            await _clickupRepository.CompleteTask(taskId);
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
