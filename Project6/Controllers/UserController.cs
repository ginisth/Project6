using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project6.Models;

namespace Project6.Controllers
{
    public class UserController : Controller
    {


        public ActionResult Register()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            UserManager manager = new UserManager();
            if (manager.GetUsers().Contains(user.Username))
            {
                return Content("User already exists");
            }
            User newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                UserRole = user.UserRole,
                UserRegistration = false
            };
            manager.AddUser(newUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]

        public ActionResult ValidateLogin([Bind(Include = "Username,Password")] User user)
        {
            UserManager manager = new UserManager();
            User userToLogin = manager.ValidateLogin(user);
            if (userToLogin != null)
                if (userToLogin.UserRegistration == true)
                {
                    Session["Id"] = userToLogin.Id;
                    Session["Username"] = userToLogin.Username;
                    Session["Role"] = userToLogin.UserRole;
                    return RedirectToAction("Index2", "MainMenu");
                }
                else
                {
                    ModelState.AddModelError("", "Your account is Unregistered");
                    return View("Login");
                }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View("Login");
            }
        }
    }
}