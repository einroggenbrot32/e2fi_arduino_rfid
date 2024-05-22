using ZeiterfassungsAPI.Data;
using ZeiterfassungsAPI.Models;

namespace ZeiterfassungsAPI.Services
{
    public interface IUserService
    {
        public bool UserExists(int userId);
        public User GetUserByRFID(string rfid);
        public IEnumerable<User> GetAllUsers();
        public string GetUserName(int id);


    }

}
