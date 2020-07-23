using Auth.Application.Interfaces;
using BCrypt;

namespace Auth.Application
{
    public class BCryptApplication : IBCryptApplication
    {
        public string Encrypt(string password)
        {
            string salt = BCryptHelper.GenerateSalt(BCrypt.SaltRevision.Revision2);

            return BCryptHelper.HashPassword(password, salt);
        }

        public bool ValidatePassword(string plain, string encrypted)
        {
            return BCryptHelper.CheckPassword(plain, encrypted);
        }
    }
}