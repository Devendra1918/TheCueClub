using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCueClub.Models;

namespace TheCueClub.Controllers
{
    public class CustomerController : Controller
    {
        Cue_Club_CoreEntities db = new Cue_Club_CoreEntities();
        // GET: Customer
        [Route]
       
        public ActionResult Index()
        {
            var obj = db.customer_details.ToList();
            return View(obj);
        }

        [HttpGet]
         public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(customer_details cs)
        {
            if (ModelState.IsValid)
            { 

                cs.Customer_name = cs.Customer_name;
                cs.mobile_no = cs.mobile_no;
                cs.opening_balance =cs.opening_balance;
                string  query = db.customer_details.Max(a => a.for_cust_id).ToString();
                int for_cust = 0;
                if (query == "")
                {
                    for_cust = 1;
                }
                else
                {
                    for_cust = Convert.ToInt16(query) + 1;

                }
                cs.for_cust_id = for_cust;
                cs.Customer_id = "Cust_" + for_cust;
                db.customer_details.Add(cs);
                db.SaveChanges();
                     
                            
            }

          return  RedirectToAction("Index", "Customer");
            //return View();
        }

        [HttpGet]
        public ActionResult Edit(string Customer_id)
        {
            var data=db.customer_details.Where(x=>x.Customer_id== Customer_id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(customer_details cs)
        {
            var data = db.customer_details.Where(x => x.Customer_id == cs.Customer_id).FirstOrDefault();
            if (data != null)
            {
                data.Customer_name = cs.Customer_name;
                data.mobile_no = cs.mobile_no;
                data.opening_balance = cs.opening_balance;
                db.SaveChanges();
                                    
            }
            return RedirectToAction("Index", "Customer");
            //return View();
        }
        [HttpGet]
        public ActionResult Delete(string Customer_id)
        {
            var obj = db.customer_details.Where(x => x.Customer_id == Customer_id).FirstOrDefault();
            if (obj != null)
            {
                db.customer_details.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
       
    }
}