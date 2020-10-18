using AmazingTODOApp.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;

namespace AmazingTODOApp.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly List<User> list;

        public AuthenticationRepository(string filePath)
        {
            var json = File.ReadAllText(filePath);
            list = json.Length is 0 ? new List<User>() : JsonConvert.DeserializeObject<List<User>>(json);
        }

        public User GetUser(string userName, string password)
        {
            var user = list.First(x =>
                x.UserName == userName &&
                x.Password == password
            );

            if (user is null)
            {
                throw new InvalidCredentialException();
            }
            else
            {
                return user;
            }
        }
    }
}