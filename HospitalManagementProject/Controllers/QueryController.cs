using HospitalManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementProject.Controllers
{
    public class QueryController : Controller
    {
        HospitalEntities db=new HospitalEntities();
        public ActionResult Query()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Query(receaptioninquery r)
        {
            if (ModelState.IsValid == true)
            {
                db.receaptioninqueries.Add(r);
                var a = db.SaveChanges();
                if (a > 0)
                {
                    ViewBag.query = "Submitted Successfully";
                    ModelState.Clear();
                }
            }
            return View();
        }

        public ActionResult QueryList()
        {
          var data=  db.receaptioninqueries.ToList();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            if(id>0)
            {
               var row= db.receaptioninqueries.Where(model => model.r_id == id).FirstOrDefault();

                if (row!=null)
                {
                    db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                   var a= db.SaveChanges();
                    if(a > 0)
                    {
                        ViewBag.delete = "Deleted Successfully";
                    }
                }
            }
            
            return View();
        }
    }
}