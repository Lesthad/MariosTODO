namespace AmazingTODOApp.Domain
{
    public class User
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public User()
        {
        }

        public User(string userId, string userName, string password)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
        }
    }
}
