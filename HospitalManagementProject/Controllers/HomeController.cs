using HospitalManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementProject.Controllers
{
    public class HomeController : Controller
    {
        HospitalEntities db=new HospitalEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(registration r)
        {
            if (ModelState.IsValid == true)
            {
                db.registrations.Add(r);
               var a= db.SaveChanges();
                if(a>0)
                {
                    ModelState.Clear();
                    return Json("Submitted form");
                }
                else
                {
                    return Json("Not Submitted form");

                }
            }
            return View();
        }

       

    }
}