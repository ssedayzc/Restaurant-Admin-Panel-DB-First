using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoRestaurant.Models.Entity;

namespace DinoRestaurant.Controllers
{
    public class ContactController : Controller
    {
        dinoMariaDBEntities db = new dinoMariaDBEntities();
        // GET: Contact
        public ActionResult Index()
        {
            var contacts = db.Contacts.ToList();
            return View(contacts);
        }

        public PartialViewResult MessageListMenu()
        {
            return PartialView();

        }

        public ActionResult Inbox()
        {
            var contactValues = db.Contacts.ToList();
            return View(contactValues);
        }

        public ActionResult Sendbox()
        {
            var contactValues = db.Contacts.ToList();
            return View(contactValues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = db.Contacts.Where(x=>x.ContactId==id);
            return View(contactValues);
        }


        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Contacts p)
        { 
                db.Contacts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Sendbox");
                             
        }
        public ActionResult DeleteMsg(int id)
        {
            var msg = db.Contacts.Find(id);
            db.Contacts.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Trash"); // Çöp kutusu sayfasına yönlendir
        }

        public ActionResult ConfirmDeleteMsg(int id)
        {
            var msg = db.Contacts.Find(id);
            return View(msg);
        }

        [HttpPost]
        public ActionResult ConfirmDeleteMsgConfirmed(int id)
        {
            var msg = db.Contacts.Find(id);
            db.Contacts.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Trash"); // Çöp kutusu sayfasına yönlendir
        }

    }
}