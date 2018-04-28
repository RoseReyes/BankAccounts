using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bank.Models
{
 public abstract class BaseEntity {}
 public class User 
    {
            public int UserID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public List<Account> Accounts { get; set; }
            public User()
                {
                    Accounts = new List<Account>();
                }
    }
}