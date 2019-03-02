using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project6.Models
{
    public class UserManager
    {
        private DatabaseContext _context;

        public List<string> GetUsers()
        {
            using (_context = new DatabaseContext())
            {
                return _context.Users.Select(x => x.Username).ToList();
            }
        }

        public User FindUser(int id)
        {
            using (_context = new DatabaseContext())
            {
                var user = _context.Users.Find(id);
                return user;
            }
        }

        public void AddUser(User user)
        {
            using (_context = new DatabaseContext())
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

        public User ValidateLogin(User user)
        {
            using (_context = new DatabaseContext())
            {
                var userToLogin = _context.Users.Where(x => x.Username == user.Username &&
                        x.Password == user.Password).SingleOrDefault();
                return userToLogin;
            }
        }

        public List<User> UnregisteredUsers()
        {
            using (_context = new DatabaseContext())
            {
                return _context.Users.Where(u => u.UserRegistration == false).ToList();
            }
        }

        public void AcceptUser(User user)
        {
            using (_context = new DatabaseContext())
            {
                var acceptUser = _context.Users.Where(x => x.Id == user.Id).First();
                acceptUser.UserRegistration = true;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            using (_context = new DatabaseContext())
            {
                var deleteUser = _context.Users.Where(x => x.Id == user.Id).First();
                _context.Users.Remove(deleteUser);
                _context.SaveChanges();
            }
        }
    }
}