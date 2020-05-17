using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoistSync.Models.Todoist
{
    public class Task
    {
        public long Id { get; set; }
        [JsonProperty("project_id")]
        public long ProjectId { get; set; }
        [JsonProperty("section_id")]
        public long SectionId { get; set; }
        public long Order { get; set; }
        public string Content { get; set; }
        public bool Completed { get; set; }
        [JsonProperty("label_ids")]
        public List<string> LabelIds { get; set; }
        public long Priority { get; set; }
        [JsonProperty("comment_count")]
        public long CommentCount { get; set; }
        public DateTimeOffset Created { get; set; }
        public TaskDue Due { get; set; }
        public Uri Url { get; set; }
    }
}
