using System;

namespace TODOCore.Email
{
    public class EmailSender : IEmailSender
    {
        public bool Send(string email, string message)
        {
            Console.WriteLine("Email sent to {email}: {messaage}");
            return true;
        }
    }
}