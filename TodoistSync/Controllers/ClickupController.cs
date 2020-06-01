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

        public ClickupController(ClickupService clickupService)
        {
            _clickupService = clickupService;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook(Clickup.WebhookEvent webhookEvent)
        {
            if (webhookEvent.Event == Clickup.WebhookEventType.TaskDeleted)
            {
                await _clickupService.DeleteTodoistTaskIfExists(webhookEvent.TaskId);
                return Ok();
            }

            await _clickupService.CreateOrUpdateTodoistTask(webhookEvent.TaskId);

            return Ok();
        }
    }
}
