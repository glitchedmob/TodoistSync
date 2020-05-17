using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoistSync.Models.Todoist
{
    public class Task
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("project_id")]
        public long ProjectId { get; set; }

        [JsonProperty("section_id")]
        public long SectionId { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("label_ids")]
        public List<long> LabelIds { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("comment_count")]
        public long CommentCount { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("due")]
        public TaskDue Due { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
