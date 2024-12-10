using TodoAPI.Models;

namespace TodoAPI
{
    public interface IRepository
    {
        Todo? GetById(int id);
        IEnumerable<Todo> GetAll();
        void Add(Todo entity);
        void Update(Todo entity);
        void Delete(Todo entity);
    }
}
