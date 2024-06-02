using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCueClub.Models;

namespace TheCueClub.Controllers
{
    public class UserController : Controller
    {
        Cue_Club_CoreEntities db = new Cue_Club_CoreEntities();
        // GET: User
        public ActionResult Index()
        {
            var data = db.Login_Details.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Login_Details ld)
        {
            ld.User_name = ld.User_name;
            ld.Email = ld.Email;
            ld.Password = ld.Password;
            string query = db.Login_Details.Max(a => a.for_user).ToString();
            int for_user = 0;
            if (query == "")
            {
                for_user = 1;
            }
            else
            {
                for_user = Convert.ToInt16(query) + 1;
            }
            ld.user_id = "USR-" + for_user;
            ld.for_user = for_user;
            ld.mobile_no = ld.mobile_no;
            if (ld.Master_user == "true")
            {
                ld.Master_user = "Valid";
            }
            else
            {
                ld.Master_user = "Invalid";
            }
            ld.User_role = ld.User_role;
            db.Login_Details.Add(ld);
            db.SaveChanges();
            return RedirectToAction("Index", "User");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = db.Login_Details.Where(x => x.id == id).FirstOrDefault();

            //foreach(var m in data)
            //{
            ViewBag.master_user = data.Master_user;
            //}
            return View("Edit",data);
        }
        [HttpPost]
        public ActionResult Edit(Login_Details ld)
        {
            var data = db.Login_Details.Where(x => x.id == ld.id).FirstOrDefault();
            if (data != null)
            {
                data.User_name = ld.User_name;
                data.mobile_no = ld.mobile_no;
                string master_user = "";
                if (ld.Master_user == "true")
                {
                    master_user = "Valid";
                }
                else
                {
                    master_user = "Invalid";
                }
                //ViewBag.master_user = data.Master_user;
                data.Master_user = master_user;
                data.User_role = ld.User_role;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.Login_Details.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                db.Login_Details.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "User");

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(Login_model lm)
        {
            if (ModelState.IsValid)
            {
                var data = db.Login_Details.Where(x => x.Email == lm.Email && x.Password == lm.password).FirstOrDefault();
                if (data != null)
                {
                  return  RedirectToAction("Index", "User");
                }
                else
                {
                    Response.Write("<script>alert('Invalid User..')</script>");
                }
            }
            return View();
        }
        
    }
}