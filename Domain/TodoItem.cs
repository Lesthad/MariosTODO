namespace TODOCore
{
    public class TodoItem
    {
        public TodoItem(int id, string description)
        {
            Description = description;
            IsCompleted = false;
            Id = id;
        }

        #region

        public int Id {get; set;}
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        #endregion
    }
}