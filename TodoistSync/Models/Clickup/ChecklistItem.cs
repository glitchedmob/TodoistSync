using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class ChecklistItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Orderindex { get; set; }
        public User Assignee { get; set; }
        public bool Resolved { get; set; }
        public string Parent { get; set; }
        [JsonProperty("date_created")]
        public string DateCreated { get; set; }
        public List<ChecklistItem> Children { get; set; }
    }
}
