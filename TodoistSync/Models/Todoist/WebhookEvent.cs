using Newtonsoft.Json;

namespace TodoistSync.Models.Todoist
{
    public class WebhookEvent
    {
        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("initiator")]
        public EventInitiator Initiator { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("event_data")]
        public EventData EventData { get; set; }

        public class EventInitiator
        {
            [JsonProperty("is_premium")]
            public bool IsPremium { get; set; }

            [JsonProperty("image_id")]
            public string ImageId { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("full_name")]
            public string FullName { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }
        }
    }
}
