using MariosTODOApp.Domain;
using System.Collections.Generic;

namespace MariosTODOApp.Repositories
{
    public interface ITodoItemRepository
    {
        void Save(TodoItem list);
        TodoItem GetById(int id);
        IEnumerable<TodoItem> GetAllByUser(string userId);
        void Delete(TodoItem modifiedItem);
        void Persist();
    }
}