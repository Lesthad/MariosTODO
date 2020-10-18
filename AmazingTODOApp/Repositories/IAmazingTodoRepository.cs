using AmazingTODOApp.Domain;
using System.Collections.Generic;

namespace AmazingTODOApp.Repositories
{
    public interface IAmazingTodoRepository
    {
        public User GetUser(string userName, string password);
        public User GetUserById(string userId);
        void SaveTodoItem(TodoItem list);
        TodoItem GetTodoItemById(int id);
        IEnumerable<TodoItem> GetTodoItemsByUserId(string userId);
        void DeleteTodoItem(TodoItem modifiedItem);
        void Persist();
    }
}