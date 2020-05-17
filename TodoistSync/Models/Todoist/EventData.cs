using System;
using Newtonsoft.Json;

namespace TodoistSync.Models.Todoist
{
    public class EventData
    {
        [JsonProperty("is_deleted")]
        public long IsDeleted { get; set; }

        [JsonProperty("assigned_by_uid")]
        public object AssignedByUid { get; set; }

        [JsonProperty("labels")]
        public object[] Labels { get; set; }

        [JsonProperty("sync_id")]
        public object SyncId { get; set; }

        [JsonProperty("section_id")]
        public object SectionId { get; set; }

        [JsonProperty("in_history")]
        public long InHistory { get; set; }

        [JsonProperty("child_order")]
        public long ChildOrder { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset DateAdded { get; set; }

        [JsonProperty("checked")]
        public long Checked { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("date_completed")]
        public object DateCompleted { get; set; }

        [JsonProperty("added_by_uid")]
        public long AddedByUid { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("due")]
        public Task Due { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("parent_id")]
        public object ParentId { get; set; }

        [JsonProperty("responsible_uid")]
        public object ResponsibleUid { get; set; }

        [JsonProperty("project_id")]
        public long ProjectId { get; set; }

        [JsonProperty("collapsed")]
        public long Collapsed { get; set; }
        
        public class TaskDue
        {
            [JsonProperty("date")]
            public DateTimeOffset Date { get; set; }

            [JsonProperty("timezone")]
            public object Timezone { get; set; }

            [JsonProperty("is_recurring")]
            public bool IsRecurring { get; set; }

            [JsonProperty("string")]
            public string String { get; set; }

            [JsonProperty("lang")]
            public string Lang { get; set; }
        }
    }
}
