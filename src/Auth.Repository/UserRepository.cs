using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Infra.Data;
using Auth.Model;
using Auth.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auth.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            this._context = context;
        }
        
        public async Task<bool> Add(User user)
        {
            this._context.Users.Add(user);
            
            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetBy(Guid id)
        {
            return await _context.Users.Where(u => u.Id == id).ToListAsync();
        }
    }
}
