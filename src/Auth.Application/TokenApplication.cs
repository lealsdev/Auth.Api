using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Application.Interfaces;
using Auth.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Application
{
    public class TokenApplication : ITokenApplication
    {
        private readonly IConfiguration _configuration;
        public TokenApplication(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string CreateFor(User user)
        {
            var claims = this.CreateClaimsWith(user);
            var key = this.CreateKey();
            var credentials = CreateCredentialsWith(key);
            var tokenDescriptor = this.CreateTokenDescriptorWith(claims, credentials);

            return CreateTokenWith(tokenDescriptor);
        }

        private Claim[] CreateClaimsWith(User user)
        {
            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Claims)
            };
        }

        private SymmetricSecurityKey CreateKey()
        {
            return new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        }

        private SigningCredentials CreateCredentialsWith(SymmetricSecurityKey key)
        {
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        }

        private SecurityTokenDescriptor CreateTokenDescriptorWith(Claim[] claims, SigningCredentials credentials)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(3600),
                SigningCredentials = credentials
            };
        }

        private string CreateTokenWith(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); 
        }
    }
}