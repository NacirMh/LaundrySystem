using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace LaundrySystem.WebApi.Business.Services
{
    public class JWTTokenManager : IJWTTokenManager
    {
        private readonly IOwnerDao _ownerDao;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IConfiguration _configuration;
        private readonly byte[] _key;
        public JWTTokenManager(IOwnerDao ownerDao,IConfiguration configuration)
        {
            _ownerDao = ownerDao;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _configuration = configuration;
            _key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

        }
        public bool Authenticate(string id, string password) { 
            var owner = _ownerDao.Login(id, password);
            if (owner == null) {
                return false;
            }
            return true;
        }

        public bool verifyToken(string Token)
        {
            try
            {
                _jwtSecurityTokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public string NewToken(string id)
        {
       
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, id) }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = _configuration["JWT:ValidIssuer"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key)
                    , SecurityAlgorithms.HmacSha256Signature)
            };
            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return _jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
