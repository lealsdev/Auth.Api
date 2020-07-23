namespace Auth.Application.Interfaces
{
    public interface IBCryptApplication
    {
         string Encrypt(string password);

         bool ValidatePassword(string plain, string encrypted);
    }
}