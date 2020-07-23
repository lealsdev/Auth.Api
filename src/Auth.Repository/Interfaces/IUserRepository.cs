using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Model;

namespace Auth.Repository.Interfaces
{
    public interface IUserRepository
    {
         Task<bool> Add(User user);

         Task<List<User>> Get();

         Task<List<User>> GetBy(Guid id);
    }
}