using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Model;

namespace Auth.Application.Interfaces
{
    public interface IUserApplication
    {
         Task<bool> Add(User user);

         Task<List<User>> Get();

         Task<User> GetBy(Guid id);

         Task<User> GetBy(string email);

         Task<bool> checkUserExistsBy(string email);
    }
}