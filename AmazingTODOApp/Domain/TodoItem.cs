namespace AmazingTODOApp.Domain
{
    public class TodoItem
    {
        public int TodoItemId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TodoItem()
        {
            UserId = string.Empty;
            Description = string.Empty;
            IsCompleted = false;
        }

        public TodoItem(string userId, string description)
        {
            UserId = userId;
            Description = description;
            IsCompleted = false;
        }


    }
}