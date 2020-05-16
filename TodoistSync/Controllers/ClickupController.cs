using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clickup = TodoistSync.Models.Clickup;
using TodoistSync.Services;

namespace TodoistSync.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClickupController : ControllerBase
    {
        private readonly ClickupService _clickupService;
        private readonly TodoistService _todoistService;

        public ClickupController(ClickupService clickupService, TodoistService todoistService)
        {
            _clickupService = clickupService;
            _todoistService = todoistService;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook(Clickup.WebhookEvent webhookEvent)
        {
            if (webhookEvent.Event == Clickup.WebhookEventType.TaskDeleted)
            {
                await _todoistService.DeleteClickupTaskIfExists(webhookEvent.TaskId);
                return Ok();
            }

            var task = await _clickupService.GetTaskById(webhookEvent.TaskId);

            await _todoistService.CreateOrUpdateClickupTask(task);

            return Ok("clickup webhook");
        }
    }
}
