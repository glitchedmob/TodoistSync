using System;
using System.Collections.Generic;

namespace TodoistSync.Models.Clickup
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TextContent { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public string Orderindex { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string DateClosed { get; set; }
        public bool Archived { get; set; }
        public User Creator { get; set; }
        public List<User> Assignees { get; set; }
        public List<User> Watchers { get; set; }
        public List<Checklist> Checklists { get; set; }
        public List<Tag> Tags { get; set; }
        public string Parent { get; set; }
        public Priority Priority { get; set; }
        public string DueDate { get; set; }
        public string StartDate { get; set; }
        public string TimeEstimate { get; set; }
        public long TimeSpent { get; set; }
        public List<object> CustomFields { get; set; }
        public List<object> Dependencies { get; set; }
        public List<object> LinkedTasks { get; set; }
        public long TeamId { get; set; }
        public Uri Url { get; set; }
        public string PermissionLevel { get; set; }
        public TaskList List { get; set; }
        public Project Project { get; set; }
        public Folder Folder { get; set; }
        public Space Space { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
