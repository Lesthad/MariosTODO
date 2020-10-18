using System.Collections.Generic;

namespace TODOCore.Repository
{
    public interface ITodoItemRepository
    {
         void Save(TodoItem list);
         TodoItem GetById(int id);
         IEnumerable<TodoItem> GetAll();

         void Delete(TodoItem modifiedItem);

          void Persist();
    }
}