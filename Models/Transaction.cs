using System;
using System.ComponentModel.DataAnnotations;

namespace bank.Models
{
    public class Account 
    {
            
            [Key]
            public int TransID { get; set;}
            public int UserID { get; set; }
            public double Balance { get; set; }
            public double Transaction { get; set; }
            public string TransactionDate { get; set; }
    }
}