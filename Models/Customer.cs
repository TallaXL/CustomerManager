using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerManager.Models
{
    public class Customer
    {
        [StringLength(12)]
        public string Inn { get; set; }
        public string FullName { get; set; }
    }
}