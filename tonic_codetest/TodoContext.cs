using Microsoft.EntityFrameworkCore;
using tonic_codetest.Models;

namespace tonic_codetest
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo>? Todos { get; set; }

        public string DbPath { get; }

        public TodoContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "todo.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
