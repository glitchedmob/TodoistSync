using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoistSync.Repositories;
using Clickup = TodoistSync.Models.Clickup;
using TodoistSync.Services;

namespace TodoistSync.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClickupController : ControllerBase
    {
        private readonly ClickupService _clickupService;
        private readonly ClickupRepository _clickupRepository;

        public ClickupController(ClickupService clickupService, ClickupRepository clickupRepository)
        {
            _clickupService = clickupService;
            _clickupRepository = clickupRepository;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook(Clickup.WebhookEvent webhookEvent)
        {
            if (webhookEvent.Event == Clickup.WebhookEventType.TaskDeleted)
            {
                await _clickupService.DeleteTodoistTaskIfExists(webhookEvent.TaskId);
                return Ok();
            }

            var task = await _clickupRepository.GetTaskById(webhookEvent.TaskId);

            await _clickupService.CreateOrUpdateTodoistTask(task);

            return Ok();
        }
    }
}
