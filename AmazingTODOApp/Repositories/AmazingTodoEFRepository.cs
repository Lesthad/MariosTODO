using AmazingTODOApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazingTODOApp.Repositories
{
    public class AmazingTodoEFRepository : IAmazingTodoRepository
    {
        public AmazingTodoEFContext Context { get; }

        public AmazingTodoEFRepository(AmazingTodoEFContext context)
        {
            Context = context;
        }

        public void DeleteTodoItem(TodoItem modifiedItem)
        {
            Context.TodoItems.Remove(modifiedItem);
            Context.Entry(modifiedItem).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public TodoItem GetTodoItemById(int id)
        {
            return Context.TodoItems.Find(id);
        }

        public IEnumerable<TodoItem> GetTodoItemsByUserId(string userId)
        {
            return Context.Users
                .Include(x => x.TodoItems)
            .First(user => user.UserId == userId).TodoItems;
        }

        public User GetUser(string userName, string password)
        {
            return Context.Users.FirstOrDefault(x =>
            x.UserName == userName &&
            x.Password == password);
        }

        public User GetUserById(string userId)
        {
            return Context.Users.Find(userId);
        }

        public void Persist()
        {
            Context.SaveChanges();
        }

        public void SaveTodoItem(TodoItem todoItem)
        {
            if (Context.TodoItems.Contains(todoItem))
            {
                Context.TodoItems.Add(todoItem);
                Context.Entry(todoItem).State = EntityState.Modified;
            }
            else
            {
                Context.TodoItems.Add(todoItem);
                Context.SaveChanges();
            }
        }
    }
}
