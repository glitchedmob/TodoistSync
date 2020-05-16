using System.Text.Json.Serialization;

namespace TodoistSync.Models.Clickup
{
    public class WebhookEvent
    {
        public string Event { get; set; }

        [JsonPropertyName("task_id")]
        public string TaskId { get; set; }

        [JsonPropertyName("webhook_id")]
        public string WebhookId { get; set; }
    }
}
