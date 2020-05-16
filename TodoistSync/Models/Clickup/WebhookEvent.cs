using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class WebhookEvent
    {
        public WebhookEventType Event { get; set; }
        [JsonProperty("task_id")]
        public string TaskId { get; set; }
        [JsonProperty("webhook_id")]
        public string WebhookId { get; set; }
    }
}
