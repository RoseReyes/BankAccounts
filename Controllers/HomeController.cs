using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace bank.Controllers
{
    public class HomeController : Controller
    {
        private BankContext _context;
 
        public HomeController(BankContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register(RegisterViewModel registerVM)
        { 
            if(ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Email = registerVM.Email,
                    Password = registerVM.Password
                };

                //Hashed Password
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);

                //Save to DB
                 _context.Add(user);
                 _context.SaveChanges();

                 Account newaccount = new Account
                {
                    Balance = 0,
                    Transaction = 0,
                    TransactionDate = DateTime.Now.ToString(),
                    UserID = user.UserID
                };
                _context.Add(newaccount);
                _context.SaveChanges();

                //Get the userid and set to session
                int user_id = HttpContext.Session.GetInt32("active_user")?? user.UserID;
                HttpContext.Session.SetInt32("active_user", user_id);
                int? id = HttpContext.Session.GetInt32("active_user");

                string user_name = HttpContext.Session.GetString("active_name")?? user.FirstName;
                HttpContext.Session.SetString("active_name", user_name);
                string username = HttpContext.Session.GetString("active_name");

                return RedirectToAction("account", "MainPage", new { urlID = id});
            }
            return View(registerVM);
        }

        //url redirect
        [HttpGet]
        [Route("login")]
        public IActionResult login()
        {
            return View("Login");
        }

        //post data redirect
        [HttpPost]
        [Route("login")]
        public IActionResult login(LoginViewModel loginVM)
        {   
             if(ModelState.IsValid)
             {
                    List<User> ReturnedValues = _context.UserTb.Where(s => s.Email.Equals(loginVM.Email)).ToList();
                    
                    foreach (var users in ReturnedValues)
                    {                    
                        if(users.Email != null && loginVM.Email != null)
                        {
                            var Hasher = new PasswordHasher<User>();
                            // Pass the user object, the hashed password, and the PasswordToCheck
                            if(0 != Hasher.VerifyHashedPassword(users, users.Password, loginVM.Password))
                            {
                                //Handle success and Set ID and Name to session
                                int user_id = HttpContext.Session.GetInt32("active_user")?? users.UserID;
                                HttpContext.Session.SetInt32("active_user", user_id);
                                int? id = HttpContext.Session.GetInt32("active_user");
                                return RedirectToAction("account", "MainPage", new { urlID = id});
                            }
                        }
                        return View(loginVM);
                    }
            };
            return View(loginVM);
        }





        
    }  
}
