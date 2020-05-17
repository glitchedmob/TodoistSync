using Newtonsoft.Json;

namespace TodoistSync.Models.Todoist
{
    public class WebhookEvent
    {
        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("initiator")]
        public User Initiator { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("event_data")]
        public EventData EventData { get; set; }
    }
}
