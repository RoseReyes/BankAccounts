using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using bank.Models;


namespace bank.Controllers
{
    public class MainPageController : Controller
    {
        [HttpPost]
        [Route("process")]
        public IActionResult process(string DepWithdraw)
        {
            System.Console.WriteLine("I am here in process");
            int? id = HttpContext.Session.GetInt32("active_user");
            System.Console.WriteLine(DepWithdraw);
            return View("MainPage");
        }
        
      [HttpGet]
      [Route("account/{urlID}")]
      public IActionResult account()
      { 
          string username = HttpContext.Session.GetString("active_name");
          TempData["name"] = username;
          return View("MainPage");
      }


        [HttpPost]
        [Route("withdrawdeposit")]
        public IActionResult WithdrawDeposit(int withdrawdeposit)
        {
            HttpContext.Session.SetInt32("withdrawdepositAmount", withdrawdeposit);
            List<Account> curr_account = HttpContext.Session.GetObjectFromJson<List<Account>("curr_account");
            User current_user = HttpContext.Session.GetObjectFromJson<User>("curr_user");
            double balance = curr_account[0].Balance;
            if(HttpContext.Session.GetInt32("withdrawdepositAmount") != 0 && HttpContext.Session.GetInt32("withdrawdepositAmount") >= -balance)
            {
                Account newaccount = new Account
                    {
                        Balance = curr_account[curr_account.Count - 1].Balance + (double)HttpContext.Session.GetInt32("withdrawdepositAmount"),
                        Transaction = (double)HttpContext.Session.GetInt32("withdrawdepositAmount"),
                        TransactionDate = DateTime.Now.ToString(),
                        UserId = current_user.UserId
                    };
                    _context.Add(newaccount);
                    _context.SaveChanges();
            }
            return Redirect("dashboard");
        }

        [HttpGetAttribute]
        [RouteAttribute("dashboard")]
        public IActionResult Dashboard()
        {
            // ViewBag.Number = HttpContext.Session.GetInt32("withdrawdepositAmount");
            ViewBag.Curr_user = HttpContext.Session.GetObjectFromJson<User>("curr_user");
            User current_user = HttpContext.Session.GetObjectFromJson<User>("curr_user");
            List<Account> ReturnedAccount = _context.account.Where(account => account.UserId == current_user.UserId).ToList();
            HttpContext.Session.SetObjectAsJson("curr_account", ReturnedAccount);
            ViewBag.AccountInfo = ReturnedAccount;
            return View();
        }


      [HttpPost]
      [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

    }
}




