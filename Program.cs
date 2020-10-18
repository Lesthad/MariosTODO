namespace TODOCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TODOCore.Authentication;
    using TODOCore.Repository;
    using TODOCore.Util;

    class Program
    {
        private static readonly Authenticator authenticator;
        private static readonly ITodoItemRepository todoItemRepository;
        private static readonly ILogger logger;

        static Program()
        {
            authenticator = new Authenticator();
            todoItemRepository = new TodoItemRepository("repo.json");
            logger = new FileLogger(LoggerLevel.INFO);
        }

        private static void Main()
        {
            Console.WriteLine("Welcome to your personal TODO List!");
            Console.Write("User: ");
            string user = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            try
            {
                authenticator.Authenticate(user, password);
                Console.Clear();
                Console.WriteLine(string.Format("Welcome {0}!", user));
                logger.Log(string.Format("User '{0}' has logged in", user), LoggerLevel.INFO);
            }
            catch
            {
                Console.WriteLine("The user and password do not exists");
                logger.Log(string.Format("User '{0}' and password {1} do not exist", user, password), LoggerLevel.ERROR);
                return;
            }

            char selectedOption;

            do
            {
                IEnumerable<TodoItem> todoItems = todoItemRepository.GetAll();
                Console.WriteLine($"\rListing All {todoItems.Count()} Items:\r");

                foreach (TodoItem todoItem in todoItems)
                {
                    var checker = todoItem.IsCompleted ? "[x]" : "[ ]";
                    Console.WriteLine($"\t{todoItem.Id} {checker} {todoItem.Description}");
                }
                Console.WriteLine("\r\rDashboard");
                Console.WriteLine("q) Add new Item");
                Console.WriteLine("w) Complete Item");
                Console.WriteLine("e) Delete Item");
                Console.WriteLine("r) Delete Completed Items");
                Console.WriteLine("t) Logout and Close");
                Console.Write("\rType Option: ");

                selectedOption = Console.ReadKey().KeyChar;
                try
                {
                    switch (selectedOption)
                    {
                        case 'q': AddItem(); break;
                        case 'w': CompleteItem(); break;
                        case 'e': DeleteItem(); break;
                        case 'r': DeleteCompletedItems(); break;
                        default: Console.Clear(); break;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    logger.Log(e.Message, LoggerLevel.ERROR);
                }

            } while (selectedOption != 't');

            todoItemRepository.Persist();
            logger.Log("SaveChanges", LoggerLevel.INFO);
            logger.Log(string.Format("User '{0}' has logged off", user), LoggerLevel.INFO);
        }

        private static void DeleteCompletedItems()
        {
            logger.Log("DeleteCompletedItems", LoggerLevel.INFO);

            IEnumerable<TodoItem> todoItems =
                todoItemRepository
                    .GetAll()
                    .Where(x => x.IsCompleted)
                    .ToList();

            foreach (TodoItem todoItem in todoItems)
            {
                todoItemRepository.Delete(todoItem);
            }

            Console.Clear();
            Console.WriteLine("\r\rThe items were deleted");
        }

        private static void DeleteItem()
        {
            logger.Log("DeleteItem", LoggerLevel.INFO);
            Console.Write("\r\rWhat item do you want to delete?: ");

            int itemNumber = int.Parse(Console.ReadLine());
            logger.Log($"Selected Item {itemNumber}", LoggerLevel.INFO);

            var todoItem = todoItemRepository.GetById(itemNumber);

            if (todoItem is null)
            {
                throw new Exception("The item do not exists!");
            }

            todoItemRepository.Delete(todoItem);

            Console.Clear();
            Console.WriteLine($"The item was deleted");
        }

        private static void CompleteItem()
        {
            logger.Log($"CompleteItem", LoggerLevel.INFO);
            Console.Write("\r\rWhat item number?: ");
            int itemNumber = int.Parse(Console.ReadLine());
            logger.Log($"Selected Item {itemNumber}", LoggerLevel.INFO);

            var todoItem = todoItemRepository.GetById(itemNumber);

            if (todoItem is null)
            {
                throw new Exception("The item do not exists!");
            }

            todoItem.IsCompleted = true;

            todoItemRepository.Save(todoItem);

            Console.Clear();
            Console.WriteLine("The item was completed.");
        }

        private static void AddItem()
        {
            logger.Log($"Adding item", LoggerLevel.INFO);
            Console.Write("\r\rNew Todo item: ");

            string description = Console.ReadLine();

            TodoItem item = new TodoItem(description);
            todoItemRepository.Save(item);

            Console.Clear();
            Console.WriteLine("Item Added!");
            logger.Log($"Added item {item.Id}: {description}", LoggerLevel.INFO);
        }
    }
}
