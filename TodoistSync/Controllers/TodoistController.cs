using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoistSync.Repositories;
using TodoistSync.Services;
using Todoist = TodoistSync.Models.Todoist;

namespace TodoistSync.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoistController : ControllerBase
    {
        private readonly ClickupService _clickupService;
        private readonly ClickupRepository _clickupRepository;
        private readonly TodoistRepository _todoistRepository;

        public TodoistController(
            ClickupService clickupService,
            ClickupRepository clickupRepository,
            TodoistRepository todoistRepository)
        {
            _clickupService = clickupService;
            _clickupRepository = clickupRepository;
            _todoistRepository = todoistRepository;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook(Todoist.WebhookEvent webhookEvent)
        {
            if (!webhookEvent.EventData.Labels.Contains(_todoistRepository.ClickupLabelId))
            {
                return Ok();
            }

            var clickupTaskId = _clickupService.GetClickupIdFromTodoistContent(webhookEvent.EventData.Content);

            if (clickupTaskId == null)
            {
                return Ok();
            }

            switch (webhookEvent.EventName)
            {
                case "item:completed":
                    await _clickupRepository.CompleteTask(clickupTaskId);
                    break;
                case "item:updated":
                    await _clickupService.UpdateClickupTask(clickupTaskId, webhookEvent.EventData.Due?.Date);
                    break;
            }

            return Ok();
        }
    }
}
