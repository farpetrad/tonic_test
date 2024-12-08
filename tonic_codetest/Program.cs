using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace tonic_codetest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = CreateServices();

            var app = services.GetRequiredService<Application>();
            app.Run();
        }

        private static ServiceProvider CreateServices()
        {
            var serviceProvider = new ServiceCollection()
                                             .AddSingleton<Application>()
                                             .AddDbContext<TodoContext>()
                                             .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
