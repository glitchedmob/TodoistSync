using System;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Attachment
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public long Type { get; set; }
        public long Source { get; set; }
        public long Version { get; set; }
        public string Extension { get; set; }
        [JsonProperty("thumbnail_small")]
        public Uri ThumbnailSmall { get; set; }
        [JsonProperty("thumbnail_large")]
        public Uri ThumbnailLarge { get; set; }
        [JsonProperty("is_folder")]
        public bool IsFolder { get; set; }
        public string Mimetype { get; set; }
        public bool Hidden { get; set; }
        public string ParentId { get; set; }
        public long Size { get; set; }
        [JsonProperty("total_comments")]
        public long TotalComments { get; set; }
        [JsonProperty("resolved_comments")]
        public long ResolvedComments { get; set; }
        public User User { get; set; }
        public bool Deleted { get; set; }
        public Uri Url { get; set; }
    }
}
