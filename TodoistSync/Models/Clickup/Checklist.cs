using System.Collections.Generic;

namespace TodoistSync.Models.Clickup
{
    public class Checklist
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public long Orderindex { get; set; }
        public long Creator { get; set; }
        public long Resolved { get; set; }
        public long Unresolved { get; set; }
        public List<ChecklistItem> Items { get; set; }
    }
}
