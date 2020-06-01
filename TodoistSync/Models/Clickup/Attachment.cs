using System;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Attachment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("source")]
        public long Source { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("thumbnail_small")]
        public Uri ThumbnailSmall { get; set; }

        [JsonProperty("thumbnail_large")]
        public Uri ThumbnailLarge { get; set; }

        [JsonProperty("is_folder")]
        public bool? IsFolder { get; set; }

        [JsonProperty("mimetype")]
        public string Mimetype { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("parentid")]
        public string ParentId { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("total_comments")]
        public long TotalComments { get; set; }

        [JsonProperty("resolved_comments")]
        public long ResolvedComments { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
