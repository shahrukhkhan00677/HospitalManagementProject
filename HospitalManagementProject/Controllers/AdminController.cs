using HospitalManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementProject.Controllers
{
    public class AdminController : Controller
    {
        HospitalEntities db = new HospitalEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin a)
        {
            if(ModelState.IsValid==true)
            {
               var data= db.Admins.Where(model => model.a_name == a.a_name && model.a_password == a.a_password).FirstOrDefault();
                if(data!=null)
                {
                    /*Session["user"] = a.a_name;*/
                    return RedirectToAction("AdminPage", "Admin");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");

                }
            }
            return View();
        }

        public ActionResult AdminPage()
        {
           var dtotaldoctor= db.doctorDetails.Count();
            TempData["doctor"]= dtotaldoctor;
            var dTotalmale = db.doctorDetails.Count(model => model.gender == "Male");
            TempData["dmale"] = dTotalmale;
            var dTotalfemale = db.doctorDetails.Count(model => model.gender == "Female");
            TempData["dfemale"] = dTotalfemale;

            var ptotalpatient= db.registrations.Count();
            TempData["patient"]= ptotalpatient;
           var pTotalmale= db.registrations.Count(model => model.r_Gender == "Male");
            TempData["male"] = pTotalmale;
            var pTotalfemale= db.registrations.Count(model => model.r_Gender == "Female");
            TempData["female"] = pTotalfemale;

            var atotaladmin = db.Admins.Count();
            TempData["admin"] = atotaladmin;

            var QueryTotal = db.feedbacks.Count();
            TempData["querytotal"] = QueryTotal;
            var QTotalmale = db.feedbacks.Count(model => model.gender == "Male");
            TempData["Qmale"] = QTotalmale;
            var QTotalfemale = db.feedbacks.Count(model => model.gender == "Female");
            TempData["Qfemale"] = QTotalfemale;

            return View();
        }
        public ActionResult DoctorAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoctorAdd(doctorDetail d)
        {
            db.doctorDetails.Add(d);
          var a=  db.SaveChanges();
            if(a>0)
            {
                TempData["adddoctor"]= "SuccessFully Add Doctor";
                return RedirectToAction("DoctorList", "Admin");
            }
            return View();
        }

        public ActionResult DoctorList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
           var data= db.doctorDetails.ToList();
            return View(data);
        }
        public ActionResult Edit(int id)
        {
          var data=  db.doctorDetails.Where(model => model.id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(doctorDetail d)
        {
           db.Entry(d).State=System.Data.Entity.EntityState.Modified;
            var a=db.SaveChanges();
            if(a>0)
            {
                TempData["editted"] = "Editted SuccessFully";
                return RedirectToAction("DoctorList");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
               var row= db.doctorDetails.Where(model => model.id == id).FirstOrDefault();
                if (row != null)
                {
                    db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                   var a= db.SaveChanges();
                    if (a>0)
                    {
                        TempData["deleted"] = "Deleted Successfully";
                        return RedirectToAction("DoctorList");
                    }
                    else
                    {
                        return RedirectToAction(" not DoctorList");

                    }
                }
            }
            
            return View();
        }
    }

}