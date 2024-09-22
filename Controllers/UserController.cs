using MedManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MedManager.Controllers
{

    public class UserController : Controller
    {
        // GET: User

        MedManagerEntities db = new MedManagerEntities();

        [HttpGet]
        public ActionResult UserReg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserReg(User u)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.Email == u.Email))
                {
                    ViewBag.Message = "Email is already taken";
                }
                else
                {
                    db.Users.Add(u);
                    db.SaveChanges();
                    return RedirectToAction("UserLogin");
                }
            }
            return View();
        }

        public ActionResult UserHome()
        {
            return View(db.med_info.ToList());
        }

        public ActionResult Cart()
        {

            return View();
        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(UserLoginClass ulc, AdminClass ad, PharmaModClass phm)
        {
            var query = db.Users.SingleOrDefault(m => m.Email == ulc.Email && m.PasswordHash == ulc.Password);
            if (query != null)
            {
                return RedirectToAction("UserHome");
            }
            else if (db.Admins.SingleOrDefault(m => m.Email == ad.Email && m.PasswordHash == ad.Password) != null)
            {
                return RedirectToAction("adminpanel", "Admin");
            }
            else if (db.pharmaMods.SingleOrDefault(m => m.Email == phm.Email && m.PasswordHash == phm.Password) != null)
            {
                return RedirectToAction("PharmaModPanel", "PharmaMod");
            }
            else
            {
                Response.Write("<script>alert('invalid credentials')</script>");
            }

            return View();

        }

        [HttpPost]
        public JsonResult SaveOrder(Order_info order)
        {
            try
            {
                // Serialize the cart items (medicine list) into a JSON string
                string medicineList = JsonConvert.SerializeObject(order.items);

                // Create a new order entry
                var newOrder = new Order_info
                {
                    name = order.name,
                    address = order.address,
                    phone = order.phone,
                    medicine_list = JsonConvert.SerializeObject(order.items),
                    total_price = order.total_price
                };

                // Save to database
                db.Order_info.Add(newOrder);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Handle error
                return Json(new { success = false, message = ex.Message });
            }

        }

    }
}