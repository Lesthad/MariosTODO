namespace TODOCore
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TodoItem(string description)
        {
            Description = description;
            IsCompleted = false;
        }
    }
}