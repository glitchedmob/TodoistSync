using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class TaskPriority
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("orderindex")]
        public long Orderindex { get; set; }
    }
}
