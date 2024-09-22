using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedManager.Models
{
    public class orderclass
    {
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public cartItem[] items { get; set; }
        public decimal totalPrice { get; set; }
    }
}