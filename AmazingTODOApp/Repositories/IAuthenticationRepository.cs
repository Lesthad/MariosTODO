using AmazingTODOApp.Domain;

namespace AmazingTODOApp.Repositories
{
    public interface IAuthenticationRepository
    {
        User GetUser(string userName, string password);
    }
}