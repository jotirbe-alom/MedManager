using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedManager.Models
{
    public class cartItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}