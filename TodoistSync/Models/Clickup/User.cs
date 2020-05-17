using System;

namespace TodoistSync.Models.Clickup
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Color { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public Uri ProfilePicture { get; set; }
    }
}
