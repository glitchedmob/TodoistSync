using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoistSync.Controllers
{
    [Route("[controller]")]
    public class TodoistController : ControllerBase
    {
        [HttpGet("webhook")]
        public async Task<IActionResult> Webhook()
        {
            return Ok("todoist webhook");
        }
    }
}
