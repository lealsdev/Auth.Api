using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Application.Interfaces;
using Auth.Model;
using Auth.Repository.Interfaces;

namespace Auth.Application
{
    public class UserApplication : IUserApplication
    {
        public IUserRepository _userRepository { get; }
        public UserApplication(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> Add(User user)
        {
            return await this._userRepository.Add(user);
        }

        public async Task<List<User>> Get()
        {
            return await this._userRepository.Get();
        }

        public async Task<User> GetBy(Guid id)
        {
            return await this._userRepository.GetBy(id);
        }

        public async Task<User> GetBy(string email)
        {
            return await this._userRepository.GetBy(email);
        }

        public async Task<bool> checkUserExistsBy(string email)
        {
            return await GetBy(email) != null;
        }
    }
}
