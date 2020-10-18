namespace MariosTODOApp
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Repositories;
    using Loggers;

    class Program
    {
        private static readonly IAuthenticationRepository authenticator;
        private static readonly ITodoItemRepository todoItemRepository;
        private static readonly ILogger logger;
        private static User currentUser;

        static Program()
        {
            authenticator = new AuthenticationRepository("authentication_database.json");
            todoItemRepository = new TodoItemRepository("items_database.json");
            logger = new FileLogger(LoggerLevel.INFO);
        }

        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("Welcome to your personal TODO List!");

                while (currentUser is null)
                {
                    //----------------------------------------------------------- ASking for credentials
                    Console.Write("User (or type 'q' to quit): ");
                    var userName = Console.ReadLine();

                    if (userName == "q") //----------------------------------------------------------- Quit the app
                    {
                        return;
                    }

                    Console.Write("Password: ");
                    var password = Console.ReadLine();

                    //----------------------------------------------------------- Authenticating user
                    try
                    {
                        currentUser = authenticator.GetUser(userName, password);
                        Console.Clear();
                        logger.Log(string.Format("User '{0}' has logged in", userName), LoggerLevel.INFO);
                    }
                    catch
                    {
                        Console.WriteLine("The user and password do not exists");
                        logger.Log(string.Format("User '{0}' with that password do not exist", userName), LoggerLevel.ERROR);
                    }
                }

                char selectedOption;

                do //----------------------------------------------------------- TODO Main Menu
                {
                    Console.WriteLine(string.Format("Logged as {0}", currentUser.UserName));
                    var todoItems = todoItemRepository.GetAllByUser(currentUser.UserId);
                    Console.WriteLine($"\r\n---Listing All {todoItems.Count()} Items:\r\n");

                    foreach (TodoItem todoItem in todoItems)
                    {
                        var checker = todoItem.IsCompleted ? "[x]" : "[ ]";
                        Console.WriteLine($"\t{todoItem.TodoItemId} {checker} {todoItem.Description}");
                    }

                    //----------------------------------------------------------- TODO Rendering options
                    Console.WriteLine("\r\n---Dashboard");
                    Console.WriteLine("q) Add new Item");
                    Console.WriteLine("w) Complete Item");
                    Console.WriteLine("e) Delete Item");
                    Console.WriteLine("r) Delete Completed Items");
                    Console.WriteLine("t) Logout and Close");
                    Console.Write("\r\nType Option: ");

                    //----------------------------------------------------------- TODO handling option selected
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

                //----------------------------------------------------------- Logging out user
                todoItemRepository.Persist();
                logger.Log("SaveChanges", LoggerLevel.INFO);
                logger.Log(string.Format("User '{0}' has logged off", currentUser.UserName), LoggerLevel.INFO);
                
                currentUser = null;
            }
        }

        private static void DeleteCompletedItems()
        {
            logger.Log("DeleteCompletedItems", LoggerLevel.INFO);

            var todoItems =
                todoItemRepository
                    .GetAllByUser(currentUser.UserId)
                    .Where(x => x.IsCompleted && x.UserId == currentUser.UserId)
                    .ToList();

            foreach (TodoItem todoItem in todoItems)
            {
                todoItemRepository.Delete(todoItem);
            }

            Console.Clear();
            Console.WriteLine("\r\n\r\nThe items were deleted");
        }

        private static void DeleteItem()
        {
            logger.Log("DeleteItem", LoggerLevel.INFO);
            Console.Write("\r\n\r\nWhat item do you want to delete?: ");

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
            Console.Write("\r\n\r\nWhat item number?: ");
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
            Console.Write("\r\n\r\nNew Todo item: ");

            string description = Console.ReadLine();

            var todoItem = new TodoItem(currentUser.UserId, description);
            todoItemRepository.Save(todoItem);

            Console.Clear();
            Console.WriteLine("Item Added!");
            logger.Log($"Added item {todoItem.TodoItemId}: {description}", LoggerLevel.INFO);
        }
    }
}
