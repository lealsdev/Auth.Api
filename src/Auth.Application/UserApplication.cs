using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Application.Interfaces;
using Auth.Model;
using Auth.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Application
{
    public class UserApplication : IUserApplication
    {
        public IUserRepository _userRepository { get; }
        private readonly IBCryptApplication _bCryptApplication;
        public UserApplication(IUserRepository userRepository, IBCryptApplication bCryptApplication)
        {
            this._bCryptApplication = bCryptApplication;
            this._userRepository = userRepository;
        }

        public async Task<User> Add(User user)
        {
            user.Claims = "user";
            user.Password = this._bCryptApplication.Encrypt(user.Password);

            await this._userRepository.Add(user);

            return user;
        }

        public async Task<bool> Delete(Guid id)
        {
            var userForDelete = await this.GetBy(id);

            return await this._userRepository.Delete(userForDelete);
        }

        public async Task<bool> SetToAdmin(Guid id)
        {
            var userForSetToAdmin = await this.GetBy(id);

            userForSetToAdmin.Claims = "admin";

            return await this._userRepository.SaveAll();
        }

        public async Task<IEnumerable<User>> Get()
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

        public async Task<bool> SaveAll()
        {
            return await this._userRepository.SaveAll();
        }

        public async Task<bool> checkUserExistsBy(string email)
        {
            return await GetBy(email) != null;
        }
    }
}
