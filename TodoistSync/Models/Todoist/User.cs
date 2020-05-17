using Newtonsoft.Json;

namespace TodoistSync.Models.Todoist
{
    public class User
    {
        [JsonProperty("is_premium")]
        public bool IsPremium { get; set; }

        [JsonProperty("image_id")]
        public string ImageId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
