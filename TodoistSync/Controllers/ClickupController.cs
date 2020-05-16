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

        public ClickupController(ClickupService clickupService)
        {
            _clickupService = clickupService;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook(Clickup.WebhookEvent webhookEvent)
        {
            var task = await _clickupService.GetTaskById(webhookEvent.TaskId);
            Console.WriteLine(task.Name);
            return Ok("clickup webhook");
        }
    }
}
