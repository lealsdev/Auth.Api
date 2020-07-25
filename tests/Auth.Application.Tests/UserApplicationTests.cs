using System;
using Xunit;
using Auth.Application;
using Auth.Repository.Interfaces;
using Auth.Application.Tests.Fakes;
using Auth.Model;
using System.Collections.Generic;
using Auth.Application.Interfaces;
using System.Linq;

namespace Auth.Application.Tests
{
  public class UserApplicationTests {

    private IUserApplication _userApplication;

    public UserApplicationTests()
    {
      IBCryptApplication bCryptApplication = bCryptApplication = new BCryptApplicationFake();
      IUserRepository userRepository = userRepository = new UserRepositoryFake(10);
      
      this._userApplication = new UserApplication(userRepository, bCryptApplication);
    }

    [Fact]
    public async void GetUsers_ExpectingTrue() 
    {      
      IEnumerable<User> users = await this._userApplication.Get();

      Assert.True((users as List<User>).Count == 10);
    }

    [Fact]
    public async void GetUserById_ExpectingSame()
    {
      IEnumerable<User> users = await this._userApplication.Get();
      User userToSearch = (users as List<User>)[0];
      User userReturned = await this._userApplication.GetBy(userToSearch.Id);

      Assert.Same(userToSearch, userReturned);
    }

    [Fact]
    public async void GetUserById_ExpectingNull()
    {
      Assert.True(await this._userApplication.GetBy(Guid.NewGuid()) == null);
    }

    [Fact]
    public async void GetUserByEmail_ExpectingSame()
    {
      IEnumerable<User> users = await this._userApplication.Get();
      User userToSearch = (users as List<User>)[0];
      User userReturned = await this._userApplication.GetBy(userToSearch.Email);

      Assert.Same(userToSearch, userReturned);
    }

    [Fact]
    public async void GetUserByEmail_ExpectingFail()
    {
      Assert.True(await this._userApplication.GetBy(String.Empty) == null);
      Assert.True(await this._userApplication.GetBy("any@email.com") == null);
    }

    [Fact]
    public async void AddUser_ExpectingSuccess()
    {
      int numberOfUsersBeforeAdd = (await this._userApplication.Get()).ToList().Count;

      await this._userApplication.Add(new User(){
        Claims = "user",
        Email = "test@email.com",
        Name = "User Test",
        Password = "userpwd"
      });

      int numberOfUsersAfterAdd = (await this._userApplication.Get()).ToList().Count;

      Assert.True(numberOfUsersBeforeAdd + 1 == numberOfUsersAfterAdd);
    }

    [Fact]
    public async void AddUser_ExpectingException()
    {
      await Assert.ThrowsAsync<System.NullReferenceException>(async () => {
        await this._userApplication.Add(null);
      }); 
    }

    [Fact]
    public async void DeleteUser_ExpectingSuccess()
    {
      List<User> users = (await this._userApplication.Get()).ToList();

      int numberOfUsersBeforeDelete = users.Count;

      await this._userApplication.Delete(users[users.Count - 1].Id);

      int numberOfUsersAfterDelete = (await this._userApplication.Get()).ToList().Count;

      Assert.True(numberOfUsersBeforeDelete - 1 == numberOfUsersAfterDelete);
    }

    [Fact]
    public async void DeleteUser_ExpectingFail()
    {
      List<User> users = (await this._userApplication.Get()).ToList();

      int numberOfUsersBeforeDelete = users.Count;

      await this._userApplication.Delete(Guid.NewGuid());

      int numberOfUsersAfterDelete = (await this._userApplication.Get()).ToList().Count;

      Assert.True(numberOfUsersBeforeDelete == numberOfUsersAfterDelete);
    }

    [Fact]
    public async void CheckUserExists_ExpectingSuccess()
    {
      User user = (await this._userApplication.Get()).ToList()[0];

      Assert.True(await this._userApplication.checkUserExistsBy(user.Email));
    }

    [Fact]
    public async void CheckUserExists_ExpectingFail()
    {
      Assert.False(
        await this._userApplication.checkUserExistsBy("any@email.com"));
    }
  }
}
