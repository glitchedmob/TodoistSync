using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class ChecklistItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orderindex")]
        public long Orderindex { get; set; }

        [JsonProperty("assignee")]
        public User Assignee { get; set; }

        [JsonProperty("resolved")]
        public bool Resolved { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("children")]
        public List<ChecklistItem> Children { get; set; }
    }
}
