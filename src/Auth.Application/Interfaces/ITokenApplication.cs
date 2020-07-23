using Auth.Model;

namespace Auth.Application.Interfaces
{
    public interface ITokenApplication
    {
         string CreateFor(User user);
    }
}