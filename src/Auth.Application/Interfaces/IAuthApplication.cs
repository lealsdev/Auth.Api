using System.Threading.Tasks;
using Auth.Model;

namespace Auth.Application.Interfaces
{
    public interface IAuthApplication
    {
        Task<User> Login(string email, string password);
    }
}