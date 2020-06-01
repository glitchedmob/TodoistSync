using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TodoistSync.Utilities;

namespace TodoistSync.Models.Clickup
{
    public class Task
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text_content")]
        public string TextContent { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public TaskStatus Status { get; set; }

        [JsonProperty("orderindex")]
        public string Orderindex { get; set; }

        [JsonProperty("date_created")]
        [JsonConverter(typeof(EpochDateTimeOffsetConverter))]
        public DateTimeOffset? DateCreated { get; set; }

        [JsonProperty("date_updated")]
        [JsonConverter(typeof(EpochDateTimeOffsetConverter))]
        public DateTimeOffset? DateUpdated { get; set; }

        [JsonProperty("date_closed")]
        [JsonConverter(typeof(EpochDateTimeOffsetConverter))]
        public DateTimeOffset? DateClosed { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("creator")]
        public User Creator { get; set; }

        [JsonProperty("assignees")]
        public List<User> Assignees { get; set; }

        [JsonProperty("watchers")]
        public List<User> Watchers { get; set; }

        [JsonProperty("checklists")]
        public List<Checklist> Checklists { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("priority")]
        public TaskPriority Priority { get; set; }

        [JsonProperty("due_date")]
        [JsonConverter(typeof(EpochDateTimeOffsetConverter))]
        public DateTimeOffset? DueDate { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("time_estimate")]
        public string TimeEstimate { get; set; }

        [JsonProperty("time_spent")]
        public long TimeSpent { get; set; }

        [JsonProperty("custom_fields")]
        public List<object> CustomFields { get; set; }

        [JsonProperty("dependencies")]
        public List<Dependency> Dependencies { get; set; }

        [JsonProperty("linked_tasks")]
        public List<object> LinkedTasks { get; set; }

        [JsonProperty("team_id")]
        public long TeamId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("permission_level")]
        public string PermissionLevel { get; set; }

        [JsonProperty("list")]
        public TaskList List { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("folder")]
        public Folder Folder { get; set; }

        [JsonProperty("space")]
        public Space Space { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
    }
}
