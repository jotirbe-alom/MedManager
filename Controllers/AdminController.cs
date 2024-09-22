using MedManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedManager.Controllers
{
    public class AdminController : Controller
    {
        MedManagerEntities db = new MedManagerEntities();
       
      
        
        public ActionResult AdminPanel()
        {
          
            return View();
        }

        public ActionResult UserInfo()
        {

            return View(db.Users.ToList());
        }
        public ActionResult PharmaModInfo()
        {

            return View(db.pharmaMods.ToList());
        }

        public ActionResult UserPaymentInfo()
        {

            return View(db.Order_info.ToList());
        }


        [HttpGet]
       
        public ActionResult AddPharmaMod()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPharmaMod(pharmaMod phm)
        {
            if (ModelState.IsValid)
            {
                if (db.pharmaMods.Any(x => x.Email == phm.Email))
                {
                    ViewBag.Message = "Email is already taken";
                }
                else
                {
                    db.pharmaMods.Add(phm);
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel");
                }
            }
            return View();
        }

    }
}