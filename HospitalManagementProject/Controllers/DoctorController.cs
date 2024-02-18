using HospitalManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementProject.Controllers
{
    public class DoctorController : Controller
    {
        HospitalEntities db=new HospitalEntities();
        public ActionResult DoctorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoctorLogin(doctorLogin d)
        {
            if(ModelState.IsValid==true) 
            {
              var data= db.doctorDetails.Where(model=>model.name==d.name && model.password==d.password).FirstOrDefault();
               if(data==null) 
                {
                    ViewBag.failed = "Login Failed";
                    return View("DoctorLogin");
                }
                else
                {
                    /*Session["login"] = d.name;*/
                    return RedirectToAction("Dashboard", "Doctor");
                }
            }
            return View();
     
        }

        public ActionResult Dashboard()
        {
            var data=  db.registrations.ToList();
            return View(data);
        }

        public ActionResult Edit(int id)
        {
           var data= db.registrations.Where(model=>model.r_id==id).FirstOrDefault();

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(registration r)
        {
            db.Entry(r).State = System.Data.Entity.EntityState.Modified;
            var a = db.SaveChanges();
            if (a > 0)
            {
                TempData["edit"] = "Successfully Edited";
                return RedirectToAction("Dashboard", "Doctor");
               
            }
            else
            {
                ViewBag.notEdit = "Not editted";
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if(id>0)
            {
              var row=  db.registrations.Where(model => model.r_id == id).FirstOrDefault();
                if(row != null)
                {
                    db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                   var a= db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["deleted"] = "Deleted!!!";
                        return RedirectToAction("Dashboard", "Doctor");
                    }
                    else
                    {
                        TempData["notdeleted"] = "Not Deleted!!!";

                    }
                }
            }
            return View();
        }
        public ActionResult user()
        {
            return View();
        }
    }
}