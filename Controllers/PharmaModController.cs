using MedManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MedManager.Controllers
{
    public class PharmaModController : Controller

    {
        MedManagerEntities db = new MedManagerEntities();
        

        public ActionResult PharmaModPanel(string search)
        {
            //ViewBag.Title = "PharmaModPanel";
            if (search == null || search.Length == 0)
            {
                return View(db.med_info.ToList());
            }
            else
            {

                List<med_info> med_Infos;
                med_Infos = db.med_info.Where(a => a.med_name.Contains(search)).ToList();
                return View(med_Infos);
                
            }
           // return View();
        }

        [HttpGet]
        public ActionResult addMed()
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMed([Bind(Include = "med_id,med_name,med_img,quantity,med_price")] med_info med_info)
        {
            if (ModelState.IsValid)
            {
                db.med_info.Add(med_info);
                db.SaveChanges();
                // return RedirectToAction("Index");
            }

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(med_info model, HttpPostedFileBase med_img_upload = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (med_img_upload != null && med_img_upload.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(med_img_upload.FileName);
        //            var path = Path.Combine(Server.MapPath("~/Images"), fileName);
        //            med_img_upload.SaveAs(path);
        //            model.med_img = "/Images/" + fileName;
        //        }

        //        db.med_info.Add(model);
        //        db.SaveChanges();
        //        // return RedirectToAction("Index");
        //    }

        //    return View();
        //}

    }
}