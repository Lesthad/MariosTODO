using AmazingTODOApp.Repositories;
using AmazingTODOApp.Loggers;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace AmazingTODOApp
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = GetServiceProvider();
            var app = serviceProvider.GetService<AmazingTodoApp>();
            app.Execute();
        }

        static IServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddDbContext<AmazingTodoEFContext>(options =>
                {
                    options.UseSqlServer("Data Source=TDG-MBL-MTY0096;Initial Catalog=AwesomeTODO;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                }
            );

            

            serviceCollection.AddScoped<IAmazingTodoRepository, AmazingTodoEFRepository>();
            serviceCollection.AddScoped<ILogger, FileLogger>(x => new FileLogger(LoggerLevel.INFO));
            serviceCollection.AddScoped<AmazingTodoApp>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
