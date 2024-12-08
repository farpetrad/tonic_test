using tonic_codetest.Models;

namespace tonic_codetest
{
    public class Application
    {
        private readonly TodoContext DbContext;
        public Application(TodoContext context)
        {
            DbContext = context;
        }
        public void Run()
        {
            bool done = false;
            while(!done)
            {
                Console.WriteLine("Select an option.");
                Console.WriteLine("I - insert new todo");
                Console.WriteLine("D - delete todo");
                Console.WriteLine("U - update todo");
                Console.WriteLine("V - view todos");
                Console.WriteLine("X - exit");
                var choice = Console.ReadKey();
                Console.WriteLine();

                if (choice.KeyChar == 'X' || choice.KeyChar == 'x')
                    done = true;
                else if (choice.KeyChar == 'I' || choice.KeyChar == 'i')
                    AddTodo();
                else if (choice.KeyChar == 'V' || choice.KeyChar == 'v')
                    GetTodos();
                else if (choice.KeyChar == 'U' || choice.KeyChar == 'u')
                    UpdateTodo();
                else if (choice.KeyChar == 'D' || choice.KeyChar == 'd')
                    DeleteTodo();

            }
        }

        private void AddTodo()
        {
            Console.WriteLine("Enter a todo:");
            var todo = new Todo() { Content = Console.ReadLine() ?? "" };
            DbContext.Add(todo);
            DbContext.SaveChanges();
        }

        private void GetTodos()
        {
            var todos = DbContext.Todos;
            foreach(var todo in todos)
            {
                Console.WriteLine($"{todo.Id} - {todo.Content}");
            }
            Console.WriteLine();
        }

        private void UpdateTodo()
        {
            if (DbContext.Todos?.ToList().Count == 0)
            {
                Console.WriteLine("There are no Todo's to update");
                return;
            }
            Console.WriteLine("Enter the id number of the todo to update:");
            var todo = ValidateTodoIdInput();
                        
            Console.WriteLine("Enter the todo content:");
            var content = Console.ReadLine() ?? "";
            todo.Content = content;
            DbContext.Todos?.Update(todo);
            DbContext.SaveChanges();
        }

        private Todo? ValidateTodoIdInput()
        {
            var choice = Console.ReadKey();
            Console.WriteLine();
            while ((int)choice.KeyChar < 48 || (int)choice.KeyChar > 57)
            {
                Console.WriteLine("Invalid entry, enter a valid number");
                choice = Console.ReadKey();
                Console.WriteLine();
            }

            var todo = DbContext.Todos?.Where(t => t.Id == (int)choice.KeyChar)
                                       .FirstOrDefault();
            while (todo is null)
            {
                Console.WriteLine("Invalid todo id, enter another value");
                choice = Console.ReadKey();
                Console.WriteLine();
                todo = DbContext.Todos?.Where(t => t.Id == (int)choice.KeyChar)
                                       .FirstOrDefault();
            }
            return todo;
        }

        private void DeleteTodo()
        {
            if (DbContext.Todos?.ToList().Count == 0)
            {
                Console.WriteLine("There are no Todo's to delete");
                return;
            }
            Console.WriteLine("Enter the id number of the todo to delete:");
            var todo = ValidateTodoIdInput();
            if (todo is not null)
            {
                DbContext.Todos?.Remove(todo);
                DbContext.SaveChanges();
            }
        }
    }
}
