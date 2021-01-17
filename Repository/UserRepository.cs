using Microsoft.Extensions.Options;
using ZurumPark.Data;
using ZurumPark.Entities;
using ZurumPark.Repository.IRepository;

namespace ZurumPark.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<AppSettings> _appsettings;

        public UserRepository(ApplicationDbContext context,
                                IOptions<AppSettings> appsettings)
        {
            _appsettings = appsettings;
            _context = context;

        }
        public User Authenticate(string username, string password)
        {
            throw new System.NotImplementedException();
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