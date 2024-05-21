using ZeiterfassungsAPI.Data;
using ZeiterfassungsAPI.Models;

namespace ZeiterfassungsAPI.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserService(
        AppDbContext context,
        IConfiguration config,
        IHttpContextAccessor contextAccessor,
        IServiceScopeFactory serviceScopeFactory)
        {
            _context = context;
            _config = config;
            _contextAccessor = contextAccessor;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public User GetUserByRFID(int rfid)
        { 
            return _context.Users.FirstOrDefault(u => u.Id == rfid); 
        }





    }

}
