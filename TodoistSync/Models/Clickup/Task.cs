using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("text_content")]
        public string TextContent { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public string Orderindex { get; set; }
        [JsonProperty("date_created")]
        public string DateCreated { get; set; }
        [JsonProperty("date_updated")]
        public string DateUpdated { get; set; }
        [JsonProperty("date_closed")]
        public string DateClosed { get; set; }
        public bool Archived { get; set; }
        public User Creator { get; set; }
        public List<User> Assignees { get; set; }
        public List<User> Watchers { get; set; }
        public List<Checklist> Checklists { get; set; }
        public List<Tag> Tags { get; set; }
        public string Parent { get; set; }
        public TaskPriority Priority { get; set; }
        [JsonProperty("due_date")]
        public string DueDate { get; set; }
        [JsonProperty("start_date")]
        public string StartDate { get; set; }
        [JsonProperty("time_estimate")]
        public string TimeEstimate { get; set; }
        [JsonProperty("time_spent")]
        public long TimeSpent { get; set; }
        [JsonProperty("custom_fields")]
        public List<object> CustomFields { get; set; }
        public List<Dependency> Dependencies { get; set; }
        [JsonProperty("linked_tasks")]
        public List<object> LinkedTasks { get; set; }
        [JsonProperty("team_id")]
        public long TeamId { get; set; }
        public Uri Url { get; set; }
        [JsonProperty("permission_level")]
        public string PermissionLevel { get; set; }
        public TaskList List { get; set; }
        public Project Project { get; set; }
        public Folder Folder { get; set; }
        public Space Space { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
