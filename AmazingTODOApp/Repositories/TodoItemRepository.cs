namespace AmazingTODOApp.Repositories
{
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.IO;
    using Domain;

    public class TodoItemRepository : ITodoItemRepository
    {
        public List<TodoItem> list;
        private readonly string filePath;

        public TodoItemRepository(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            var json = File.ReadAllText(filePath);
            list = json.Length == 0 ? new List<TodoItem>() : JsonConvert.DeserializeObject<List<TodoItem>>(json);
            this.filePath = filePath;
        }

        public IEnumerable<TodoItem> GetAllByUser(string userId)
        {
            return list.Where(x => x.UserId == userId);
        }

        public TodoItem GetById(int id)
        {
            return list.FirstOrDefault(x => x.TodoItemId.Equals(id));
        }

        public void Delete(TodoItem modifiedItem)
        {
            list.Remove(modifiedItem);
        }

        public void Save(TodoItem modifiedItem)
        {
            var exisitingItem = GetById(modifiedItem.TodoItemId);

            if (exisitingItem is null)
            {
                modifiedItem.TodoItemId =
                    list
                    .Select(x => x.TodoItemId)
                    .OrderBy(x => x)
                    .DefaultIfEmpty(0)
                    .Last() + 1;

                list.Add(modifiedItem);
            }
            else
            {
                exisitingItem.IsCompleted = modifiedItem.IsCompleted;
                exisitingItem.Description = modifiedItem.Description;
            }
        }

        public void Persist()
        {
            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(filePath, json);
        }
    }
}