using DinoRestaurant.Models.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DinoRestaurant.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        dinoMariaDBEntities db = new dinoMariaDBEntities();
        public ActionResult Index(string p)
        {
            var categories = from d in db.Categories select d;
            if (!string.IsNullOrEmpty(p))
            {
                categories = categories.Where(c => c.CategoryName.Contains(p));
            }
            return View(categories.ToList());

            //var categories = db.Categories.ToList();
            //return View(categories);
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

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult UpdateCategory(HttpPostedFileBase Image, Categories c)
        {
            var category = db.Categories.Find(c.CategoryId);
            if (category == null)
            {
                return HttpNotFound();
            }

            if (Image != null && Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Category"), fileName);
                Image.SaveAs(path);

                category.Image = fileName;
            }
           category.CategoryName = c.CategoryName;
           category.Description = c.Description;
           category.IsActive = c.IsActive;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }  
}