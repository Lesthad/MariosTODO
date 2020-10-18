using MariosTODOApp.Domain;

namespace MariosTODOApp.Repositories
{
    public interface IAuthenticationRepository
    {
        User GetUser(string userName, string password);
    }
}