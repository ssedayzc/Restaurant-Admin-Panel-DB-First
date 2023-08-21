using DinoRestaurant.Models;
using DinoRestaurant.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoRestaurant.Controllers
{
    public class HomeController : Controller
    {
        dinoMariaDBEntities db = new dinoMariaDBEntities();
        public ActionResult Index()
        {
           
            return View();
        }

        public PartialViewResult HomeGraphics()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}