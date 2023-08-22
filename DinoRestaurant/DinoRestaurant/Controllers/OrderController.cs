using DinoRestaurant.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoRestaurant.Controllers
{
    public class OrderController : Controller
    {
        dinoMariaDBEntities db = new dinoMariaDBEntities();
        // GET: Order
        public ActionResult Index(string p)
        {
            var order = from d in db.Items select d;
            if (!string.IsNullOrEmpty(p))
            {
                order = order.Where(c => c.ItemName.Contains(p));
            }
            return View(order.ToList());
        }

        public ActionResult OrderReview()
        {
            var orderReviews = db.Items.ToList();
            return View(orderReviews);
          
        }
    }
}