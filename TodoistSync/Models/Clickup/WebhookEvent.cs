using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class WebhookEvent
    {
        public string Event { get; set; }
        [JsonProperty("task_id")]
        public string TaskId { get; set; }
        [JsonProperty("webhook_id")]
        public string WebhookId { get; set; }
    }
}
