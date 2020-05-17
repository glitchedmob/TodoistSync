using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Dependency
    {
        [JsonProperty("task_id")]
        public string TaskId { get; set; }

        [JsonProperty("depends_on")]
        public string DependsOn { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
