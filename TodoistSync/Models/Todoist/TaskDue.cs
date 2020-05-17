using System;

namespace TodoistSync.Models.Todoist
{
    public class TaskDue
    {
        public bool Recurring { get; set; }
        public string String { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
