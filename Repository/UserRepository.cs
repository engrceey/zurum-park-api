using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZurumPark.Data;
using ZurumPark.Entities;
using ZurumPark.Repository.IRepository;

namespace ZurumPark.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appsettings;

        public UserRepository(ApplicationDbContext context,
                                IOptions<AppSettings> appsettings)
        {
            _appsettings = appsettings.Value;
            _context = context;

        }
        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username== username && x.Password == password);
            
            if(user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_appsettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key),
                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;

        }

        public bool IsUniqueUser(string username)
        {
            throw new System.NotImplementedException();
        }

        public User Register(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}