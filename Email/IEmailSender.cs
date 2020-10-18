namespace TODOCore.Email
{
    public interface IEmailSender
    {
         bool Send (string email, string message);
    }
}