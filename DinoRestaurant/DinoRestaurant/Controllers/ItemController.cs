using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoRestaurant.Models.Entity;

namespace DinoRestaurant.Controllers
{
    public class ItemController : Controller
    {
        dinoMariaDBEntities db = new dinoMariaDBEntities();
        // GET: Item
        public ActionResult Index()
        {
            var items = db.Items.ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult AddItem()
        {
            var categories = db.Categories.ToList(); // Kategorileri veritabanından al
            ViewBag.Categories = categories; // ViewBag içine ata
            return View();
        }


        [HttpPost]
        public ActionResult AddItem(HttpPostedFileBase Image, Items p)
        {
            if (Image != null && Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Item"), fileName);
                Image.SaveAs(path);

                p.Image = fileName;
            }

            db.Items.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteItem(int id)
        {
            var items = db.Items.Find(id);
            db.Items.Remove(items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}