using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Model;
using Auth.Repository.Interfaces;

namespace Auth.Application.Tests.Fakes
{
    public class UserRepositoryFake : IUserRepository
    {
        private IEnumerable<User> _users;

        public UserRepositoryFake(int numberOfFakeUsersToCreate)
        {
            this._users = this.CreateFakeData(numberOfFakeUsersToCreate);
        }

        public Task<bool> Add(User user)
        {
            List<User> users = this._users.ToList();
            users.Add(user);

            this._users = users;

            return Task.FromResult(true);
        }

        public Task<bool> Delete(User user)
        {
            List<User> users = this._users.ToList();
            
            if(!users.Remove(user))
                return Task.FromResult(false);

            this._users = users;

            return Task.FromResult(true);
        }

        public Task<IEnumerable<User>> Get()
        {
            return Task.FromResult(this._users);
        }

        public Task<User> GetBy(Guid id)
        {
            return Task.FromResult(this._users.FirstOrDefault(u => u.Id == id));
        }

        public Task<User> GetBy(string email)
        {
            return Task.FromResult(this._users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower()));
        }

        public Task<bool> SaveAll()
        {
            return Task.FromResult(true);
        }

        private IEnumerable<User> CreateFakeData(int numberOfUsersToCreate)
        {
            List<User> users = new List<User>();

            for(int i = 0; i < numberOfUsersToCreate; ++i)
            {
                users.Add(new User(){ 
                    Claims = i < 2 ? "admin" : "user",
                    Email = $"email{i}@gmail.com",
                    Id = Guid.NewGuid(),
                    Name = $"Name{i}",
                    Password = "encrypted_password"
                 });
            }

            return users;
        }
    }
}