using System.Security.Authentication;

namespace TODOCore.Authentication
{
    public class Authenticator
    {
        public void Authenticate(string user, string password)
        {
            if (user != "user" || password != "password")
            {
                throw new InvalidCredentialException();
            }
        }
    }
}