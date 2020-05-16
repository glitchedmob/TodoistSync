using System.Collections.Generic;

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
        public string DateCreated { get; set; }
        public List<ChecklistItem> Children { get; set; }
    }
}
