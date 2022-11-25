using ITKT_PROJEKTAS.Entities;
using ITKT_PROJEKTAS.Helpers;
using ITKT_PROJEKTAS.Models;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;
namespace ITKT_PROJEKTAS.Services
{
    public interface IUserRepository
    {
        User Register(RegisterDto model);
        User Authenticate(LoginDto model);
        IEnumerable<User> GetUsers();
    }

    public class UserRepository : IUserRepository
    {

        private DataContext _db;

        public UserRepository(DataContext db)
        {
            _db = db;
        }
        public IEnumerable<User> GetUsers()
        {
            return _db.Users;
        }
        public User? Authenticate(LoginDto model)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == model.Username);

            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                return null;

            return user;
        }

        public User? Register(RegisterDto model)
        {

            if (_db.Users.Any(x => x.Username == model.Username))
                return null;

            User usr = new User();
            usr.PasswordHash = BCryptNet.HashPassword(model.Password);

            usr.Phone = model.Phone;
            usr.FirstName = model.FirstName;
            usr.LastName = model.LastName;
            usr.Username = model.Username;


            _db.Users.Add(usr);
            _db.SaveChanges();

            return usr;
        }
    }

}
