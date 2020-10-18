namespace AmazingTODOApp.Repositories
{
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.IO;
    using Domain;
    using System.Security.Authentication;

    public class AmazingTodoRepository : IAmazingTodoRepository
    {
        private readonly List<User> users;
        private readonly string filePath;

        public AmazingTodoRepository(string filePath)
        {
            var json = File.ReadAllText(filePath);
            users = json.Length is 0 ? new List<User>() : JsonConvert.DeserializeObject<List<User>>(json);
            this.filePath = filePath;
        }

        public User GetUser(string userName, string password)
        {
            if (users.FirstOrDefault(x =>
                 x.UserName == userName &&
                 x.Password == password
            ) is var user)
            {
                return user;
            }
            else
            {
                throw new InvalidCredentialException();
            }
        }

        public User GetUserById(string userId)
        {
            if (users.FirstOrDefault(x => x.UserId == userId) is var user)
            {
                return user;
            }
            else
            {
                throw new InvalidCredentialException();
            }
        }

        public IEnumerable<TodoItem> GetTodoItemsByUserId(string userId)
        {
            return GetUserById(userId).TodoItems;
        }

        public TodoItem GetTodoItemById(int todoItemId)
        {
            return users.SelectMany(x => x.TodoItems).FirstOrDefault(x => x.TodoItemId.Equals(todoItemId));
        }

        public void DeleteTodoItem(TodoItem modifiedItem)
        {
            if (GetUserById(modifiedItem.UserId) is var user)
            {
                user.TodoItems.Remove(modifiedItem);
            }
        }

        public void SaveTodoItem(TodoItem modifiedItem)
        {
            var exisitingItem = GetTodoItemById(modifiedItem.TodoItemId);

            if (exisitingItem is null)
            {
                modifiedItem.TodoItemId =
                    users.SelectMany(x => x.TodoItems)
                    .Select(x => x.TodoItemId)
                    .OrderBy(x => x)
                    .DefaultIfEmpty(0)
                    .Last() + 1;

                GetUserById(modifiedItem.UserId).TodoItems.Add(modifiedItem);
            }
            else
            {
                exisitingItem.IsCompleted = modifiedItem.IsCompleted;
                exisitingItem.Description = modifiedItem.Description;
            }
        }

        public void Persist()
        {
            var json = JsonConvert.SerializeObject(users);
            File.WriteAllText(filePath, json);
        }
    }
}