using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project6.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using static Project6.Models.User;

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

        // Taking place only after un-authorized actions
        public ActionResult LoginAfterForcedLogout()
        {
            ModelState.AddModelError("", "You don't have the privileges for this action.Login with a different account.");
            return View("Login");
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
        [ValidateAntiForgeryToken]
        public ActionResult ValidateLogin([Bind(Include = "Username,Password")] User user)
        {
            UserManager manager = new UserManager();
            User userToLogin = manager.ValidateLogin(user);
            if (userToLogin == null)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View("Login");
            }

            if(userToLogin.UserRegistration==false)
            {
                ModelState.AddModelError("", "Your account is Unregistered");
                return View("Login");
            }

            var claims = new List<Claim>(new[]
            {

                // adding following 2 claim just for supporting default antiforgery provider
                new Claim(ClaimTypes.NameIdentifier, userToLogin.Username),
                new Claim(
                    "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                new Claim(ClaimTypes.Name, userToLogin.Username)
            });

            claims.Add(new Claim(ClaimTypes.Role, userToLogin.UserRole.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(
                new AuthenticationProperties { IsPersistent = false }, identity);
            Session["Id"] = userToLogin.Id;
            Session["Username"] = userToLogin.Username;
            Session["Role"] = userToLogin.UserRole;
            return RedirectToAction("Index2", "MainMenu");
        }
    }
}