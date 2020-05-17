using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class TaskStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("orderindex")]
        public long Orderindex { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
