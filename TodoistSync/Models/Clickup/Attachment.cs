using System;

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
        public Uri ThumbnailSmall { get; set; }
        public Uri ThumbnailLarge { get; set; }
        public bool IsFolder { get; set; }
        public string Mimetype { get; set; }
        public bool Hidden { get; set; }
        public string ParentId { get; set; }
        public long Size { get; set; }
        public long TotalComments { get; set; }
        public long ResolvedComments { get; set; }
        public User User { get; set; }
        public bool Deleted { get; set; }
        public Uri Url { get; set; }
    }
}
