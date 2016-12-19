using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerManager.Models
{
    public class Order
    {
        public string Id { get; set; }
        public int Currency { get; set; }
        public float Sum { get; set; }
        [StringLength(12)]
        public string CustomerInn { get; set; }
    }
}