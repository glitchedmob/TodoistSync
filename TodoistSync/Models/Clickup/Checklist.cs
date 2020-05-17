using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Checklist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("task_id")]
        public string TaskId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("orderindex")]
        public long Orderindex { get; set; }

        [JsonProperty("creator")]
        public long Creator { get; set; }

        [JsonProperty("resolved")]
        public long Resolved { get; set; }

        [JsonProperty("unresolved")]
        public long Unresolved { get; set; }

        [JsonProperty("items")]
        public List<ChecklistItem> Items { get; set; }
    }
}
