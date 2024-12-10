using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IRepository _todoRepository;
        public TodoController(ILogger<TodoController> logger, IRepository repository)
        {
            _logger = logger;
            _todoRepository = repository;
        }

        [HttpGet(Name = "GetTodo")]
        public Todo? GetTodo(int id)
        {
            if (_todoRepository is null) return null;
            return _todoRepository.GetById(id);
        }

        [HttpGet(Name = "GetTodos")]
        public Todo[] GetTodos()
        {
            if (_todoRepository is null) return [];
            return _todoRepository.GetAll().ToArray();
        }

        [HttpDelete(Name = "DeleteTodo")]
        public bool DeleteTodo(int id)
        {
            if (_todoRepository is null) return false;
            _todoRepository.Delete(new Todo { Id = id});
            return true;
        }

        [HttpPut(Name = "AddTodo")]
        public bool AddTodo(string todoItem)
        {
            if (_todoRepository is null) return false;
            var todo = new Todo()
            {
                Content = todoItem,
            };
            _todoRepository?.Add(todo);
            return true;
        }

        [HttpPatch(Name = "UpdateTodo")]
        public bool UpdateTodo(int id, string todoItem)
        {
            if (_todoRepository is null) return false;
            _todoRepository.Update(new Todo { Id =id, Content = todoItem });
            return true;
        }
    }
}
