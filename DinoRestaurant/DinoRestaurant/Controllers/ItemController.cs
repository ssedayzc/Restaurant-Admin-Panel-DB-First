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
        public ActionResult Index(string p)
        {

            var items = from d in db.Items select d;
            if (!string.IsNullOrEmpty(p))
            {
                items = items.Where(c => c.ItemName.Contains(p));
            }
            return View(items.ToList());
            //var items = db.Items.ToList();
            //return View(items);
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

        [HttpGet]
        public ActionResult UpdateItem(int id)
        {
            var item = db.Items.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            var categories = db.Categories.ToList(); // Kategorileri veritabanından al
            ViewBag.Categories = categories; // ViewBag içine ata

            return View(item);
        }


        [HttpPost]
        public ActionResult UpdateItem(HttpPostedFileBase Image, Items p)
        {

            var item = db.Items.Find(p.ItemId);
            if (item == null)
            {
                return HttpNotFound();
            }

            if (Image != null && Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Item"), fileName);
                Image.SaveAs(path);

                item.Image = fileName;
            }
            item.ItemName = p.ItemName;
            item.Description = p.Description;
            item.CategoryId = p.CategoryId;
            item.Image = p.Image;
            item.IsActive = p.IsActive;
            item.CategoryId = p.CategoryId;
            item.Price = p.Price;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ListByCategory(int id)
        {
            var menus = db.Items.Where(item => item.CategoryId == id).ToList();
            return View(menus);
        }
    }
}