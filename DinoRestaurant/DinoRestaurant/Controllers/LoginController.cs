using DinoRestaurant.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoRestaurant.Controllers
{
    public class LoginController : Controller
    {
        dinoMariaDBEntities db = new dinoMariaDBEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admins p)
        {
            var adminuserinfo = db.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName &&
                                                         x.AdminPassword == p.AdminPassword);
            if(adminuserinfo != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
      
    }
}