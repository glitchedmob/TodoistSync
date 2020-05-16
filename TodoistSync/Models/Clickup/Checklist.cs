using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Checklist
    {
        public string Id { get; set; }
        [JsonProperty("task_id")]
        public string TaskId { get; set; }
        public string Name { get; set; }
        [JsonProperty("date_created")]
        public string DateCreated { get; set; }
        public long Orderindex { get; set; }
        public long Creator { get; set; }
        public long Resolved { get; set; }
        public long Unresolved { get; set; }
        public List<ChecklistItem> Items { get; set; }
    }
}
