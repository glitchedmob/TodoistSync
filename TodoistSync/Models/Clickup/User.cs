using System;
using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("profilePicture")]
        public Uri ProfilePicture { get; set; }
    }
}
