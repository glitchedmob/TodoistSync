using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TodoistSync.Repositories;
using Clickup = TodoistSync.Models.Clickup;

namespace TodoistSync.Services
{
    public class TodoistService
    {
        public long ClickupLabelId => _todoistRepository.ClickupLabelId;

        private readonly TodoistRepository _todoistRepository;

        public TodoistService(TodoistRepository todoistRepository)
        {
            _todoistRepository = todoistRepository;
        }
    }
}
