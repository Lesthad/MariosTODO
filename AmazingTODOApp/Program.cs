using AmazingTODOApp.Domain;
using System;
using System.Linq;
using AmazingTODOApp.Repositories;
using AmazingTODOApp.Loggers;

namespace AmazingTODOApp
{
    class Program
    {
        private static readonly AmazingTodoFileRepository amazingTodoRepository;
        private static readonly FileLogger logger;
        private static readonly AmazingTodoApp app;

        static Program()
        {
            amazingTodoRepository = new AmazingTodoFileRepository("database.json");
            logger = new FileLogger(LoggerLevel.INFO);
            app = new AmazingTodoApp(amazingTodoRepository, logger);
        }

        static void Main()
        {
            app.Execute();
        }
    }
}
