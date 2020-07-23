using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Model;

namespace Auth.Repository.Interfaces
{
    public interface IUserRepository
    {
         Task<bool> Add(User user);

         Task<bool> Delete(User user);

         Task<List<User>> Get();

         Task<User> GetBy(Guid id);

         Task<User> GetBy(string email);

         Task<bool> SaveAll();
    }
}