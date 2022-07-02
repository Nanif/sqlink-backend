using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Sqlink_Server.GeneratedModels;
using Sqlink_Server.Models;

namespace Sqlink_Server.Handlers
{
    public interface IJWTAuthenticationManager
    {
        LoginResponseModel? Authenticate(string? email, string? password);
    }

    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string tokenKey;

        public JWTAuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }

        public LoginResponseModel? Authenticate(string? email, string? password)
        {
            var user = new User() { Name = "naomi" };//TODO get the user by email & password
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            Console.WriteLine("token key 1: " + tokenKey);
            var key = Encoding.ASCII.GetBytes(tokenKey);
            Console.WriteLine("token key 2: " + tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email ?? "")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponseModel(tokenHandler.WriteToken(token), user);
        }
    }
}
