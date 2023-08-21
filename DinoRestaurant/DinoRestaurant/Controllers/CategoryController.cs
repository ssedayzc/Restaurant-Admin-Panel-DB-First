using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoRestaurant.Models.Entity;


namespace DinoRestaurant.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        dinoMariaDBEntities db = new dinoMariaDBEntities();
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult AddCategory(HttpPostedFileBase Image, Categories p)
        {
            if (Image != null && Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Category"), fileName);
                Image.SaveAs(path);

                p.Image = fileName;
            }

            db.Categories.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}