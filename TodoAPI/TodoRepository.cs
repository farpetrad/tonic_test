using TodoAPI.Models;

namespace TodoAPI
{
    public class TodoRepository : IRepository
    {
        private readonly TodoContext _todoContext;
        public TodoRepository(TodoContext context)
        {
            _todoContext = context;
        }
        public void Add(Todo entity)
        {
            if (_todoContext == null) return;
            var addedEntity = _todoContext?.Add(entity);
            _todoContext?.SaveChanges();
        }

        public void Delete(Todo entity)
        {
            if (_todoContext == null) return;
            var todo = _todoContext?.Todos?.Where(todo => todo.Id == entity.Id).FirstOrDefault();
            if (todo is null) return;
            var removed = _todoContext?.Remove(todo);
            _todoContext?.SaveChanges();
        }

        public IEnumerable<Todo> GetAll()
        {
            if (_todoContext == null) return [];
            return _todoContext?.Todos?.ToArray() ?? [];
        }

        public Todo? GetById(int id)
        {
            if (_todoContext == null) return null;
            return _todoContext?.Todos?.Where(todo => todo.Id == id)
                                       .FirstOrDefault();
        }

        public void Update(Todo entity)
        {
            if (_todoContext == null) return;
            var todo = _todoContext?.Todos?.Where(t => t.Id == entity.Id).FirstOrDefault();
            if (todo is null) return;
            todo.Content = entity.Content;
            _todoContext?.Update(todo);
            _todoContext?.SaveChanges();
        }
    }
}
